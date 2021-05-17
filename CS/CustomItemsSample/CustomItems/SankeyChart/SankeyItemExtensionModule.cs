using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;

namespace CustomItemsSample {
    public class SankeyItemExtensionModule : IExtensionModule {
        IDashboardControl dashboardControl;
        public void AttachViewer(DashboardViewer viewer) {
            AttachDashboardControl(viewer);
        }
        public void DetachViewer() {
            Detach();
        }
        public void AttachDesigner(DashboardDesigner designer) {
            AttachDashboardControl(designer);
            designer.CreateCustomItemBars(typeof(SankeyItemMetadata));
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
            if(e.MetadataType == typeof(SankeyItemMetadata))
                e.CustomControlProvider = new SankeyItemControlProvider(dashboardControl.Dashboard.Items[e.DashboardItemName] as CustomDashboardItem<SankeyItemMetadata>);
        }
    }
}
