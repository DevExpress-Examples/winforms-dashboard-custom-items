using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;

namespace CustomItemsSample {
    public class SunburstItemExtensionModule : IExtensionModule {
        IDashboardControl dashboardControl;
        public void AttachViewer(DashboardViewer viewer) {
            AttachDashboardControl(viewer);
        }
        public void DetachViewer() {
            Detach();
        }
        public void AttachDesigner(DashboardDesigner designer) {
            AttachDashboardControl(designer);
            designer.CreateCustomItemsBars(typeof(SunburstItemMetadata));
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
                dashboardControl.CalculateHiddenTotals = true;
                dashboardControl.CustomDashboardItemControlCreating += OnCustomDashboardItemControlCreating;
            }
        }
        void OnCustomDashboardItemControlCreating(object sender, CustomDashboardItemControlCreatingEventArgs e) {
            IDashboardControl dashboardControl = (IDashboardControl)sender;
            if(e.MetadataType == typeof(SunburstItemMetadata))
                e.CustomControlProvider = new SunburstItemControlProvider(
                    dashboardControl.Dashboard.Items[e.DashboardItemName] as CustomDashboardItem<SunburstItemMetadata>);
        }
    }
}
