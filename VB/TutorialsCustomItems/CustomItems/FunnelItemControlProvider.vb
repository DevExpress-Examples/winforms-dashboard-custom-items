Imports System
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.XtraCharts
Imports DevExpress.XtraReports.UI

Namespace TutorialsCustomItems
	Public Class FunnelItemControlProvider
		Inherits CustomControlProviderBase

		Private dashboardItem As CustomDashboardItem(Of FunnelItemMetadata)
		Private chart As ChartControl
		Private flatData As DashboardFlatDataSource
		Protected Overrides ReadOnly Property Control() As Control
			Get
				Return chart
			End Get
		End Property

		Public Sub New(ByVal dashboardItem As CustomDashboardItem(Of FunnelItemMetadata))
			Me.dashboardItem = dashboardItem
			chart = New ChartControl()
			chart.RuntimeHitTesting = True
			chart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False
			chart.SeriesSelectionMode = SeriesSelectionMode.Point
			AddHandler chart.MouseDoubleClick, AddressOf MouseDoubleClick
			AddHandler chart.SelectedItemsChanged, AddressOf ChartSelectedItemsChanged
			AddHandler chart.SelectedItemsChanging, AddressOf ChartSelectedItemsChanging
			AddHandler chart.CustomDrawSeriesPoint, AddressOf CustomDrawSeriesPoint
		End Sub

		Protected Overrides Sub UpdateControl(ByVal customItemData As CustomItemData)
			UpdateSelectionMode()
			flatData = customItemData.GetFlatData(New DashboardFlatDataSourceOptions() With {.AddColoringColumns = True})
			chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
			chart.Series.Clear()
			Dim series As Series = ConfigureSeries(flatData)
			chart.Series.Add(series)
		End Sub
		Protected Overrides Function GetPrintableControl(ByVal customItemData As CustomItemData, ByVal info As CustomItemExportInfo) As XRControl
			Dim container As New PrintableComponentContainer()
			container.PrintableComponent = chart
			Return container
		End Function
		Private Function ConfigureSeries(ByVal flatData As DashboardFlatDataSource) As Series
            Dim series As New Series("A Funnel Series", ViewType.Funnel)
            If dashboardItem.Metadata.Value IsNot Nothing AndAlso dashboardItem.Metadata.Arguments.Count > 0 Then
                series.DataSource = flatData
                series.ValueDataMembers.AddRange(dashboardItem.Metadata.Value.UniqueId)
                If Interactivity.IsDrillDownEnabled Then
                    Dim drillDownLevel As Integer = Interactivity.GetCurrentDrillDownValues().Count
                    series.ArgumentDataMember = dashboardItem.Metadata.Arguments(drillDownLevel).UniqueId
                Else
                    series.ArgumentDataMember = dashboardItem.Metadata.Arguments.Last().UniqueId
                End If
                series.ColorDataMember = flatData.GetColoringColumn(dashboardItem.Metadata.Value.UniqueId).Name
            End If
            CType(series.Label, FunnelSeriesLabel).Position = FunnelSeriesLabelPosition.Center
            Return series
        End Function
        Private Sub CustomDrawSeriesPoint(ByVal sender As Object, ByVal e As CustomDrawSeriesPointEventArgs)
			Dim row As DashboardFlatDataSourceRow = TryCast(e.SeriesPoint.Tag, DashboardFlatDataSourceRow)
			Dim formattedValue As String = flatData.GetDisplayText(dashboardItem.Metadata.Value.UniqueId, row)
			e.LabelText = e.SeriesPoint.Argument & " - " & formattedValue
			e.LegendText = e.SeriesPoint.Argument
		End Sub
#Region "MasterFiltering"
		Protected Overrides Sub SetSelection(ByVal selection As CustomItemSelection)
			chart.ClearSelection()
			For Each item As DashboardFlatDataSourceRow In selection.GetDashboardFlatDataSourceRows(flatData)
				chart.SelectedItems.Add(item)
			Next item
		End Sub
		Private Sub ChartSelectedItemsChanging(ByVal sender As Object, ByVal e As SelectedItemsChangingEventArgs)
			If Interactivity.MasterFilterMode = DashboardItemMasterFilterMode.Single AndAlso e.NewItems.OfType(Of DashboardFlatDataSourceRow)().Count() = 0 Then
				e.Cancel = True
			End If
		End Sub
		Private Sub ChartSelectedItemsChanged(ByVal sender As Object, ByVal e As SelectedItemsChangedEventArgs)
			If chart.SelectedItems.Count = 0 AndAlso Interactivity.CanClearMasterFilter Then
				Interactivity.ClearMasterFilter()
			ElseIf Interactivity.CanSetMasterFilter Then
				Interactivity.SetMasterFilter(chart.SelectedItems.OfType(Of DashboardFlatDataSourceRow)())
			End If
		End Sub
		Private Sub UpdateSelectionMode()
			Select Case Interactivity.MasterFilterMode
				Case DashboardItemMasterFilterMode.None
					chart.SelectionMode = ElementSelectionMode.None
				Case DashboardItemMasterFilterMode.Single
					chart.SelectionMode = ElementSelectionMode.Single
				Case DashboardItemMasterFilterMode.Multiple
					chart.SelectionMode = ElementSelectionMode.Extended
				Case Else
					chart.SelectionMode = ElementSelectionMode.None
			End Select
		End Sub
			#End Region
			Private Sub MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs)
			Dim hitInfo As ChartHitInfo = chart.CalcHitInfo(e.Location)
			If hitInfo.InSeriesPoint Then
				If Interactivity.CanPerformDrillDown Then
					Interactivity.PerformDrillDown(TryCast(hitInfo.SeriesPoint.Tag, DashboardFlatDataSourceRow))
				End If
			End If
			End Sub
	End Class
End Namespace
