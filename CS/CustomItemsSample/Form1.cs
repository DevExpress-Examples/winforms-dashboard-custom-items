using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;

namespace CustomItemsSample {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            SunburstItemExtensionModule SunburstItemModule = new SunburstItemExtensionModule();
            SankeyItemExtensionModule SankeyItemModule = new SankeyItemExtensionModule();
            WaypointMapItemExtensionModule WaypointMapItemModule = new WaypointMapItemExtensionModule();

            SunburstItemModule.AttachDesigner(dashboardDesigner1);
            SankeyItemModule.AttachDesigner(dashboardDesigner1);
            WaypointMapItemModule.AttachDesigner(dashboardDesigner1);

            dashboardDesigner1.LoadDashboard(@"..\..\CustomItems.xml");
        }

        private void dashboardDesigner1_DataLoading(object sender, DataLoadingEventArgs e) {
            if(e.DataSourceComponentName == "dsExport")
                e.Data = CustomItemDataGenerator.GetExportData();
            else if(e.DataSourceComponentName == "dsContinent")
                e.Data = CustomItemDataGenerator.GetContinentData();
        }
    }
}
