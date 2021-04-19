Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardCommon.ViewerData
Imports DevExpress.DashboardWin
Imports DevExpress.Utils
Imports DevExpress.Utils.Extensions
Imports DevExpress.XtraCharts.Sankey
Imports DevExpress.XtraReports.UI

Namespace CustomItemsSample
	Public Class SankeyItemControlProvider
		Inherits CustomControlProviderBase

		Private flatData As DashboardFlatDataSource
		Private multiDimensionalData As MultiDimensionalData
		Private sankey As SankeyDiagramControl
		Private dashboardItem As CustomDashboardItem(Of SankeyItemMetadata)
		Private toolTipController As ToolTipController

		Public Overrides ReadOnly Property Control() As Control
			Get
				Return sankey
			End Get
		End Property

		Public Sub New(ByVal dashboardItem As CustomDashboardItem(Of SankeyItemMetadata))
			Me.dashboardItem = dashboardItem
			sankey = New SankeyDiagramControl()
			sankey.EmptySankeyText.Text = String.Empty
			AddHandler sankey.SelectedItemsChanged, AddressOf Sankey_SelectedItemsChanged
			toolTipController = New ToolTipController()
			AddHandler toolTipController.BeforeShow, AddressOf ToolTipController_BeforeShow
			sankey.ToolTipController = toolTipController
			AddHandler sankey.SelectedItemsChanging, AddressOf Sankey_SelectedItemsChanging
			AddHandler sankey.HighlightedItemsChanged, AddressOf Sankey_HighlightedItemsChanged
		End Sub
		Public Overrides Sub UpdateControl(ByVal customItemData As CustomItemData)
			multiDimensionalData = customItemData.GetMultiDimensionalData()
			sankey.DataSource = Nothing
			flatData = customItemData.GetFlatData(New DashboardFlatDataSourceOptions() With {.AddColoringColumns = True})
			If ValidateBindings() Then
				SetDataBindings(flatData)
				SetSelectionMode()
			End If
		End Sub
		Public Overrides Sub SetSelection(ByVal selection As CustomItemSelection)
			Dim selectedRows As IList(Of DashboardFlatDataSourceRow) = selection.AsDashboardFlatDataSourceRows(flatData)
			sankey.SelectedItems.Clear()
			selectedRows.ForEach(Sub(r) sankey.SelectedItems.Add(r))
		End Sub
		Public Overrides Function GetPrintableControl(ByVal customItemData As CustomItemData, ByVal exportInfo As CustomItemExportInfo) As XRControl
			Dim container As New PrintableComponentContainer()
			container.PrintableComponent = sankey
			Return container
		End Function
		Private Sub Sankey_HighlightedItemsChanged(ByVal sender As Object, ByVal e As SankeyHighlightedItemsChangedEventArgs)
			If sankey.SelectionMode = SankeySelectionMode.Single AndAlso e.HighlightedNodes.Count > 0 Then
				sankey.HighlightedItems.Clear()
			End If
		End Sub
		Private Sub Sankey_SelectedItemsChanging(ByVal sender As Object, ByVal e As SankeySelectedItemsChangingEventArgs)
			If sankey.SelectionMode = SankeySelectionMode.Single AndAlso e.NewNodes.Count > 0 Then
				e.Cancel = True
			End If
		End Sub
		Private Sub ToolTipController_BeforeShow(ByVal sender As Object, ByVal e As DevExpress.Utils.ToolTipControllerShowEventArgs)
			If dashboardItem.Metadata.Weight Is Nothing Then
				e.ToolTip = String.Empty
			ElseIf TypeOf e.SelectedObject Is SankeyLink Then
				Dim link As SankeyLink = TryCast(e.SelectedObject, SankeyLink)
				e.ToolTip = multiDimensionalData.GetMeasures()(0).Format(link.TotalWeight)
			ElseIf TypeOf e.SelectedObject Is SankeyNode Then
				Dim node As SankeyNode = TryCast(e.SelectedObject, SankeyNode)
				e.ToolTip = multiDimensionalData.GetMeasures()(0).Format(node.TotalWeight)
			End If
		End Sub
		Private Sub Sankey_SelectedItemsChanged(ByVal sender As Object, ByVal e As SankeySelectedItemsChangedEventArgs)
			If sankey.SelectedItems.Count = 0 AndAlso Interactivity.CanClearMasterFilter Then
				Interactivity.ClearMasterFilter()
			ElseIf Interactivity.CanSetMasterFilter Then
				Interactivity.SetMasterFilter(sankey.SelectedItems.OfType(Of DashboardFlatDataSourceRow)())
			End If
		End Sub
		Private Function ValidateBindings() As Boolean
			If Interactivity.IsDrillDownEnabled Then
				sankey.EmptySankeyText.Text = "Sankey Item does not support Drill-Down"
				Return False
			End If
			If dashboardItem.Metadata.Source Is Nothing OrElse dashboardItem.Metadata.Target Is Nothing Then
				sankey.EmptySankeyText.Text = "Add the Source and Target dimensions"
				Return False
			End If
			If dashboardItem.Metadata.Source.DataMember = dashboardItem.Metadata.Target.DataMember Then
				sankey.EmptySankeyText.Text = "Add different fields to the Source and Target dimensions"
				Return False
			End If
			Return True
		End Function
		Private Sub SetDataBindings(ByVal flatData As DashboardFlatDataSource)
			sankey.Colorizer = New SankeyItemColorizer(flatData)
			sankey.SourceDataMember = dashboardItem.Metadata.Source.UniqueId
			sankey.TargetDataMember = dashboardItem.Metadata.Target.UniqueId
			If dashboardItem.Metadata.Weight IsNot Nothing Then
				sankey.WeightDataMember = dashboardItem.Metadata.Weight.UniqueId
			End If
			Try
				sankey.DataSource = flatData
			Catch
				sankey.DataSource = Nothing
			End Try
		End Sub
		Private Sub SetSelectionMode()
			Select Case Interactivity.MasterFilterMode
				Case DashboardItemMasterFilterMode.None
					sankey.SelectionMode = SankeySelectionMode.None
					Return
				Case DashboardItemMasterFilterMode.Multiple
					sankey.SelectionMode = SankeySelectionMode.Extended
				Case DashboardItemMasterFilterMode.Single
					sankey.SelectionMode = SankeySelectionMode.Single
			End Select
		End Sub
	End Class
	Friend Class SankeyItemColorizer
		Implements ISankeyColorizer

		Private ReadOnly nodeDefaultColor As Color = Color.FromArgb(255, 95, 139, 149)
		Private ReadOnly flatData As DashboardFlatDataSource

		Public Sub New(ByVal flatData As DashboardFlatDataSource)
			Me.flatData = flatData
		End Sub
		Public Function GetLinkSourceColor(ByVal link As SankeyLink) As Color
			Return GetLinkColorBase(link)
		End Function
		Public Function GetLinkTargetColor(ByVal link As SankeyLink) As Color
			Return GetLinkColorBase(link)
		End Function
		Public Function GetLinkColorBase(ByVal link As SankeyLink) As Color
			Dim row As DashboardFlatDataSourceRow = TryCast(link.Tags(0), DashboardFlatDataSourceRow)
			Dim colorData As Integer = CInt(Math.Truncate(flatData.GetValue(flatData.GetColoringColumn().Name, row)))
			Return Color.FromArgb(colorData)
		End Function
		Public Function GetNodeColor(ByVal info As SankeyNode) As Color
			Return nodeDefaultColor
		End Function
	End Class
End Namespace
