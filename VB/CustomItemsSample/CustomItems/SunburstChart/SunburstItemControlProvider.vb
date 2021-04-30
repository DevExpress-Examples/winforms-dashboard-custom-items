Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardCommon.ViewerData
Imports DevExpress.DashboardWin
Imports DevExpress.TreeMap
Imports DevExpress.Utils
Imports DevExpress.Utils.Extensions
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraTreeMap

Namespace CustomItemsSample
	Public Class SunburstItemControlProvider
		Inherits CustomControlProviderBase
		Private skipSelectionEvent As Boolean = False
		Private sunburst As SunburstControl
		Private dataAdapter As SunburstFlatDataAdapter
		Private dashboardItem As CustomDashboardItem(Of SunburstItemMetadata)
		Private toolTipController As ToolTipController
		Private multiDimensionalData As MultiDimensionalData
		Private flatData As DashboardFlatDataSource
		Private emptyTitle As Title

		Public Overrides ReadOnly Property Control() As Control
			Get
				Return sunburst
			End Get
		End Property
		Public Sub New(ByVal dashboardItem As CustomDashboardItem(Of SunburstItemMetadata))
			Me.dashboardItem = dashboardItem
			sunburst = New SunburstControl()
			Me.dataAdapter = New SunburstFlatDataAdapter()
			sunburst.DataAdapter = dataAdapter
			toolTipController = New ToolTipController()
			AddHandler toolTipController.BeforeShow, AddressOf ToolTipController_BeforeShow
			sunburst.ToolTipController = toolTipController
			AddHandler sunburst.SelectionChanged, AddressOf Sunburst_SelectionChanged
			emptyTitle = New Title() With {.Visible = False}
			sunburst.Titles.Add(emptyTitle)
			AddHandler sunburst.MouseClick, AddressOf Sunburst_MouseClick
		End Sub
		Public Overrides Sub UpdateControl(ByVal customItemData As CustomItemData)
			ClearDataBindings()
			If ValidateBindings() Then
				flatData = customItemData.GetFlatData(New DashboardFlatDataSourceOptions() With {.AddColoringColumns = True})
				multiDimensionalData = customItemData.GetMultiDimensionalData()
				SetDataBindings(flatData)
				SetColorizer(flatData)
				SetSelectionMode()
			End If
		End Sub
		Public Overrides Sub SetSelection(ByVal selection As CustomItemSelection)
			skipSelectionEvent = True
            Dim selectedRows As IList(Of DashboardFlatDataSourceRow) = selection.AsDashboardFlatDataSourceRows(flatData)
            sunburst.SelectedItems.Clear()
			selectedRows.ForEach(Function(r) sunburst.SelectedItems.Add(r))
			skipSelectionEvent = False
		End Sub
		Public Overrides Function GetPrintableControl(ByVal customItemData As CustomItemData, ByVal exportInfo As CustomItemExportInfo) As XRControl
			Dim container As New PrintableComponentContainer()
			container.PrintableComponent = sunburst
			Return container
		End Function
		Private Sub ClearDataBindings()
			dataAdapter.LabelDataMember = Nothing
			dataAdapter.ValueDataMember = dataAdapter.LabelDataMember
			dataAdapter.DataSource = dataAdapter.ValueDataMember
			dataAdapter.GroupDataMembers.Clear()
		End Sub
		Private Function ValidateBindings() As Boolean
			If Interactivity.IsDrillDownEnabled Then
				emptyTitle.Text = "Sunburst Item does not support Drill-Down"
				emptyTitle.Visible = True
				Return False
			Else
				emptyTitle.Visible = False
			End If
			Return dashboardItem.Metadata.Value IsNot Nothing AndAlso dashboardItem.Metadata.Arguments.Count() > 0
		End Function
		Private Sub SetDataBindings(ByVal flatDataSource As DashboardFlatDataSource)
			dataAdapter.ValueDataMember = dashboardItem.Metadata.Value.UniqueId
			dataAdapter.LabelDataMember = dashboardItem.Metadata.Arguments.Last().UniqueId
            dataAdapter.GroupDataMembers.AddRange(dashboardItem.Metadata.Arguments.Where(Function(d) Not d.Equals(dashboardItem.Metadata.Arguments.Last())).Select(Function(d) d.UniqueId).ToList())
            Try
				dataAdapter.DataSource = flatDataSource
			Catch
				dataAdapter.DataSource = Nothing
			End Try
		End Sub
		Private Sub SetColorizer(ByVal flatDataSource As DashboardFlatDataSource)
			Dim coloringIndices = dashboardItem.Metadata.Arguments.Where(Function(d) d.ColoringMode = ColoringMode.Hue).Select(Function(d) dashboardItem.Metadata.Arguments.IndexOf(d))
			Dim maxcoloringIndex As Integer = 0
			If coloringIndices.Any() Then
				maxcoloringIndex = coloringIndices.Max()
			End If
			sunburst.Colorizer = New SunburstItemColorizer(flatDataSource, maxcoloringIndex)
		End Sub
		Private Sub SetSelectionMode()
			Select Case Interactivity.MasterFilterMode
				Case DashboardItemMasterFilterMode.None
					sunburst.SelectionMode = ElementSelectionMode.None
					Return
				Case DashboardItemMasterFilterMode.Multiple
					sunburst.SelectionMode = ElementSelectionMode.Multiple
				Case DashboardItemMasterFilterMode.Single
					sunburst.SelectionMode = ElementSelectionMode.None
			End Select
		End Sub
		Private Sub Sunburst_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs)
			If Interactivity.MasterFilterMode = DashboardItemMasterFilterMode.Single Then
				Dim hi As SunburstHitInfo = sunburst.CalcHitInfo(e.Location)
				If hi.InSunburstItem AndAlso (Not hi.SunburstItem.IsGroup) Then
					If Interactivity.CanSetMasterFilter Then
						Interactivity.SetMasterFilter(TryCast(hi.SunburstItem.Tag, DashboardFlatDataSourceRow))
					End If
				End If
			End If
		End Sub
		Private Sub Sunburst_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
			If skipSelectionEvent Then
				Return
			End If
			Dim selectedRows As IEnumerable(Of DashboardFlatDataSourceRow) = e.SelectedItems.OfType(Of DashboardFlatDataSourceRow)()
			If selectedRows.Count() = 0 AndAlso Interactivity.CanClearMasterFilter Then
				Interactivity.ClearMasterFilter()
			ElseIf Interactivity.CanSetMasterFilter Then
				Interactivity.SetMasterFilter(selectedRows)
			End If
		End Sub
		Private Sub ToolTipController_BeforeShow(ByVal sender As Object, ByVal e As ToolTipControllerShowEventArgs)
			Dim item As IHierarchicalItem = TryCast(e.SelectedObject, IHierarchicalItem)
			If item IsNot Nothing Then
				e.ToolTip = String.Format("{0}: {1}", item.Label, multiDimensionalData.GetMeasures()(0).Format(item.Value))
			End If
		End Sub
	End Class
    Friend Class SunburstItemColorizer
        Implements ISunburstColorizer
        Public Event ColorizerChanged As ColorizerChangedEventHandler
        Private Event ISunburstColorizer_ColorizerChanged As ColorizerChangedEventHandler Implements ISunburstColorizer.ColorizerChanged
        Private defaultColor As Color = Color.Gray
        Private ReadOnly flatData As DashboardFlatDataSource
        Private maxcoloringIndex As Integer
        Public Sub New(ByVal flatData As DashboardFlatDataSource, ByVal maxcoloringIndex As Integer)
            Me.flatData = flatData
            Me.maxcoloringIndex = maxcoloringIndex
        End Sub
        Public Function GetItemColor(ByVal item As ISunburstItem, ByVal group As SunburstItemGroupInfo) As Color Implements ISunburstColorizer.GetItemColor
            If group.GroupLevel < maxcoloringIndex Then
                Dim alpha As Integer = CType(255 * (group.MaxGroupLevel - group.GroupLevel + 1) / (group.MaxGroupLevel + 1), Integer)
                Return Color.FromArgb(alpha, defaultColor)
            End If
            If TypeOf item.Tag Is DashboardFlatDataSourceRow Then
                Dim row As DashboardFlatDataSourceRow = TryCast(item.Tag, DashboardFlatDataSourceRow)
                Dim colorData As Object = flatData.GetValue(flatData.GetColoringColumn().Name, row)
                If colorData IsNot Nothing Then
                    Return Color.FromArgb(CInt(Fix(colorData)))
                End If
            End If
            If TypeOf item.Tag Is List(Of Object) Then
                Dim colors As IEnumerable(Of Integer) = (TryCast(item.Tag, List(Of Object))).OfType(Of DashboardFlatDataSourceRow)().Select(Function(row) flatData.GetValue(flatData.GetColoringColumn().Name, row)).OfType(Of Integer)().Distinct()
                If colors.Count() = 1 Then
                    Return Color.FromArgb(colors.First())
                End If
            End If
            Return defaultColor
        End Function
    End Class
End Namespace
