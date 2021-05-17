using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraMap;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CustomItemsSample {
    public class WaypointMapItemControlProvider : CustomControlProviderBase {
        readonly Color defaultLineColor = Color.FromArgb(125, 255, 212, 106);
        readonly Color highlightLineColor = Color.FromArgb(200, 255, 212, 106);
        readonly Color SelectionLineColor = Color.FromArgb(255, 255, 212, 106);

        CustomDashboardItem<WaypointMapItemMetadata> dashboardItem;
        MapControl map;
        protected ImageLayer imageLayer;
        VectorItemsLayer vectorLayer;
        MapItemStorage mapItemStorage;
        MapOverlayTextItem validationInfoItem;
        DashboardFlatDataSource flatData;
        bool skipSelectionEvent = false;

        protected override Control Control { get { return map; } }
        public WaypointMapItemControlProvider(CustomDashboardItem<WaypointMapItemMetadata> dashboardItem, string bingKey) {
            this.dashboardItem = dashboardItem;
            map = new MapControl();
            map.NavigationPanelOptions.Visible = false;
            imageLayer = new ImageLayer();
            mapItemStorage = new MapItemStorage();
            vectorLayer = new VectorItemsLayer();
            vectorLayer.Data = mapItemStorage;
            map.Layers.Add(imageLayer);
            map.Layers.Add(vectorLayer);
            imageLayer.DataProvider = new BingMapDataProvider() { BingKey = bingKey };
            MapOverlay overlay = new MapOverlay() { Alignment = ContentAlignment.MiddleCenter };
            validationInfoItem = new MapOverlayTextItem() { Visible = false };
            overlay.Items.Add(validationInfoItem);
            map.Overlays.Add(overlay);
            map.SelectionChanged += Map_SelectionChanged;
        }
        protected override void UpdateControl(CustomItemData customItemData) {
            flatData = customItemData.GetFlatData();
            mapItemStorage.Items.BeginUpdate();
            mapItemStorage.Items.Clear();
            if(ValidateBindings())
                PopulateMapItems(flatData);
            mapItemStorage.Items.EndUpdate();
            map.ZoomToFitLayerItems();
            SetSelectionMode();
        }
        protected override void SetSelection(CustomItemSelection selection) {
            skipSelectionEvent = true;
            vectorLayer.SelectedItems.Clear();
            IList<DashboardFlatDataSourceRow> selectedRows = selection.GetDashboardFlatDataSourceRows(flatData);
            var selectedLines = mapItemStorage.Items.Where(item => selectedRows.Contains(item.Tag));
            vectorLayer.SelectedItems.AddRange(selectedLines.ToList());
            skipSelectionEvent = false;
        }
        protected override XRControl GetPrintableControl(CustomItemData customItemData, CustomItemExportInfo exportInfo) {
            PrintableComponentContainer container = new PrintableComponentContainer();
            container.PrintableComponent = map;
            return container;
        }
        void Map_SelectionChanged(object sender, MapSelectionChangedEventArgs e) {
            if(skipSelectionEvent) return;
            var selectedRows = e.Selection.OfType<MapPolyline>().Select(polyline => polyline.Tag).OfType<DashboardFlatDataSourceRow>();

            if(selectedRows.Count() > 0 && Interactivity.CanSetMasterFilter)
                Interactivity.SetMasterFilter(selectedRows);
            else if(Interactivity.CanClearMasterFilter)
                Interactivity.ClearMasterFilter();
        }
        bool ValidateBindings() {
            if(Interactivity.IsDrillDownEnabled) {
                validationInfoItem.Text = "Waypoint Map Item does not suppot Drill-Down";
                validationInfoItem.Visible = true;
                return false;
            }
            else
                validationInfoItem.Visible = false;
            return dashboardItem.Metadata.SourceLatitude != null && dashboardItem.Metadata.SourceLongitude != null &&
                dashboardItem.Metadata.TargetLatitude != null && dashboardItem.Metadata.TargetLongitude != null;
        }
        void PopulateMapItems(DashboardFlatDataSource flatData) {
            foreach(DashboardFlatDataSourceRow dataRow in flatData) {
                CartesianPoint startPoint = new CartesianPoint(
                    Convert.ToDouble(flatData.GetValue(dashboardItem.Metadata.SourceLongitude.UniqueId, dataRow)),
                    Convert.ToDouble(flatData.GetValue(dashboardItem.Metadata.SourceLatitude.UniqueId, dataRow)));
                CartesianPoint endPoint = new CartesianPoint(
                    Convert.ToDouble(flatData.GetValue(dashboardItem.Metadata.TargetLongitude.UniqueId, dataRow)),
                    Convert.ToDouble(flatData.GetValue(dashboardItem.Metadata.TargetLatitude.UniqueId, dataRow)));
                var polyline = new MapPolyline() { Tag = dataRow };
                polyline.Points.AddRange(new CartesianPoint[] {
                    startPoint,
                    endPoint
                });
                SetPolylineDrawOptions(polyline);
                mapItemStorage.Items.Add(polyline);
            }
        }
        void SetSelectionMode() {
            switch(Interactivity.MasterFilterMode) {
                case DashboardItemMasterFilterMode.None:
                    map.SelectionMode = ElementSelectionMode.None;
                    vectorLayer.EnableHighlighting = false;
                    return;
                case DashboardItemMasterFilterMode.Multiple:
                    map.SelectionMode = ElementSelectionMode.Extended;
                    vectorLayer.EnableHighlighting = true;
                    break;
                case DashboardItemMasterFilterMode.Single:
                    map.SelectionMode = ElementSelectionMode.Single;
                    vectorLayer.EnableHighlighting = true;
                    break;
            }
        }
        void SetPolylineDrawOptions(MapPolyline shape) {
            shape.IsGeodesic = true;
            shape.Stroke = defaultLineColor;
            shape.StrokeWidth = 3;
            shape.SelectedStroke = SelectionLineColor;
            shape.SelectedStrokeWidth = 4;
            shape.HighlightedStroke = highlightLineColor;
            shape.HighlightedStrokeWidth = 4;
        }
    }
}
