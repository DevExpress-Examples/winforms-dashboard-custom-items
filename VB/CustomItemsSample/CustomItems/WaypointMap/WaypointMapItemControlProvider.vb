Imports Microsoft.VisualBasic
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.XtraMap
Imports DevExpress.XtraReports.UI
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms

Namespace CustomItemsSample
	Public Class WaypointMapItemControlProvider
		Inherits CustomControlProviderBase
		Private ReadOnly defaultLineColor As Color = Color.FromArgb(125, 255, 212, 106)
		Private ReadOnly highlightLineColor As Color = Color.FromArgb(200, 255, 212, 106)
		Private ReadOnly SelectionLineColor As Color = Color.FromArgb(255, 255, 212, 106)

		Private dashboardItem As CustomDashboardItem(Of WaypointMapItemMetadata)
		Private map As MapControl
		Protected imageLayer As ImageLayer
		Private vectorLayer As VectorItemsLayer
		Private mapItemStorage As MapItemStorage
		Private validationInfoItem As MapOverlayTextItem
		Private flatData As DashboardFlatDataSource
		Private skipSelectionEvent As Boolean = False

		Public Overrides ReadOnly Property Control() As Control
			Get
				Return map
			End Get
		End Property
		Public Sub New(ByVal dashboardItem As CustomDashboardItem(Of WaypointMapItemMetadata), ByVal bingKey As String)
			Me.dashboardItem = dashboardItem
			map = New MapControl()
			map.NavigationPanelOptions.Visible = False
			imageLayer = New ImageLayer()
			mapItemStorage = New MapItemStorage()
			vectorLayer = New VectorItemsLayer()
			vectorLayer.Data = mapItemStorage
			map.Layers.Add(imageLayer)
			map.Layers.Add(vectorLayer)
			imageLayer.DataProvider = New BingMapDataProvider() With {.BingKey = bingKey}
			Dim overlay As New MapOverlay() With {.Alignment = ContentAlignment.MiddleCenter}
			validationInfoItem = New MapOverlayTextItem() With {.Visible = False}
			overlay.Items.Add(validationInfoItem)
			map.Overlays.Add(overlay)
			AddHandler map.SelectionChanged, AddressOf Map_SelectionChanged
		End Sub
		Public Overrides Sub UpdateControl(ByVal customItemData As CustomItemData)
			flatData = customItemData.GetFlatData()
			mapItemStorage.Items.BeginUpdate()
			mapItemStorage.Items.Clear()
			If ValidateBindings() Then
				PopulateMapItems(flatData)
			End If
			mapItemStorage.Items.EndUpdate()
			map.ZoomToFitLayerItems()
			SetSelectionMode()
		End Sub
		Public Overrides Sub SetSelection(ByVal selection As CustomItemSelection)
			skipSelectionEvent = True
			vectorLayer.SelectedItems.Clear()
            Dim selectedRows As IList(Of DashboardFlatDataSourceRow) = selection.GetDashboardFlatDataSourceRows(flatData)
            Dim selectedLines = mapItemStorage.Items.Where(Function(item) selectedRows.Contains(CType(item.Tag, DashboardFlatDataSourceRow)))
            vectorLayer.SelectedItems.AddRange(selectedLines.ToList())
			skipSelectionEvent = False
		End Sub
		Public Overrides Function GetPrintableControl(ByVal customItemData As CustomItemData, ByVal exportInfo As CustomItemExportInfo) As XRControl
			Dim container As New PrintableComponentContainer()
			container.PrintableComponent = map
			Return container
		End Function
		Private Sub Map_SelectionChanged(ByVal sender As Object, ByVal e As MapSelectionChangedEventArgs)
			If skipSelectionEvent Then
				Return
			End If
			Dim selectedRows = e.Selection.OfType(Of MapPolyline)().Select(Function(polyline) polyline.Tag).OfType(Of DashboardFlatDataSourceRow)()

			If selectedRows.Count() > 0 AndAlso Interactivity.CanSetMasterFilter Then
				Interactivity.SetMasterFilter(selectedRows)
			ElseIf Interactivity.CanClearMasterFilter Then
				Interactivity.ClearMasterFilter()
			End If
		End Sub
		Private Function ValidateBindings() As Boolean
			If Interactivity.IsDrillDownEnabled Then
				validationInfoItem.Text = "Waypoint Map Item does not suppot Drill-Down"
				validationInfoItem.Visible = True
				Return False
			Else
				validationInfoItem.Visible = False
			End If
			Return dashboardItem.Metadata.SourceLatitude IsNot Nothing AndAlso dashboardItem.Metadata.SourceLongitude IsNot Nothing AndAlso dashboardItem.Metadata.TargetLatitude IsNot Nothing AndAlso dashboardItem.Metadata.TargetLongitude IsNot Nothing
		End Function
		Private Sub PopulateMapItems(ByVal flatData As DashboardFlatDataSource)
			For Each dataRow As DashboardFlatDataSourceRow In flatData
				Dim startPoint As New CartesianPoint(Convert.ToDouble(flatData.GetValue(dashboardItem.Metadata.SourceLongitude.UniqueId, dataRow)), Convert.ToDouble(flatData.GetValue(dashboardItem.Metadata.SourceLatitude.UniqueId, dataRow)))
				Dim endPoint As New CartesianPoint(Convert.ToDouble(flatData.GetValue(dashboardItem.Metadata.TargetLongitude.UniqueId, dataRow)), Convert.ToDouble(flatData.GetValue(dashboardItem.Metadata.TargetLatitude.UniqueId, dataRow)))
				Dim polyline = New MapPolyline() With {.Tag = dataRow}
				polyline.Points.AddRange(New CartesianPoint() { startPoint, endPoint })
				SetPolylineDrawOptions(polyline)
				mapItemStorage.Items.Add(polyline)
			Next dataRow
		End Sub
		Private Sub SetSelectionMode()
			Select Case Interactivity.MasterFilterMode
				Case DashboardItemMasterFilterMode.None
					map.SelectionMode = ElementSelectionMode.None
					vectorLayer.EnableHighlighting = False
					Return
				Case DashboardItemMasterFilterMode.Multiple
					map.SelectionMode = ElementSelectionMode.Extended
					vectorLayer.EnableHighlighting = True
				Case DashboardItemMasterFilterMode.Single
					map.SelectionMode = ElementSelectionMode.Single
					vectorLayer.EnableHighlighting = True
			End Select
		End Sub
		Private Sub SetPolylineDrawOptions(ByVal shape As MapPolyline)
			shape.IsGeodesic = True
			shape.Stroke = defaultLineColor
			shape.StrokeWidth = 3
			shape.SelectedStroke = SelectionLineColor
			shape.SelectedStrokeWidth = 4
			shape.HighlightedStroke = highlightLineColor
			shape.HighlightedStrokeWidth = 4
		End Sub
	End Class
End Namespace
