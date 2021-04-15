using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;

namespace CustomItemsSample {
    public class WaypointMapItemExtensionModule : IExtensionModule {
        IDashboardControl dashboardControl;
        public void AttachViewer(DashboardViewer viewer) {
            AttachDashboardControl(viewer);
        }
        public void DetachViewer() {
            Detach();
        }
        public void AttachDesigner(DashboardDesigner designer) {
            AttachDashboardControl(designer);
            designer.CreateCustomItemsBars(typeof(WaypointMapItemMetadata));
        }
        public void DetachDesigner() {
            Detach();
        }
        void Detach() {
            if(dashboardControl != null)
                dashboardControl.CustomDashboardItemControlCreating -= OnCustomDashboardItemControlCreating;
        }
        void AttachDashboardControl(IDashboardControl dashboardControl) {
            if(dashboardControl != null) {
                this.dashboardControl = dashboardControl;
                dashboardControl.CustomDashboardItemControlCreating += OnCustomDashboardItemControlCreating;
            }
        }
        void OnCustomDashboardItemControlCreating(object sender, CustomDashboardItemControlCreatingEventArgs e) {
            IDashboardControl dashboardControl = (IDashboardControl)sender;
            if(e.MetadataType == typeof(WaypointMapItemMetadata))
                e.CustomControlProvider = new WaypointMapItemControlProvider(dashboardControl.Dashboard.Items[e.DashboardItemName] as CustomDashboardItem<WaypointMapItemMetadata>,
                    "YOUR BING KEY");
        }
    }
}
