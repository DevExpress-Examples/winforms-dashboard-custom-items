using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using TutorialsCustomItems.CustomItems;

namespace TutorialsCustomItems
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dashboardDesigner1.CreateRibbon();
            dashboardDesigner1.LoadDashboard(@"..\..\Data\TutorialCustomItems.xml");
            dashboardDesigner1.CreateCustomItemBars();
        }

        private void DashboardDesigner1_DataLoading(object sender, DevExpress.DashboardCommon.DataLoadingEventArgs e){
            if (e.DataSourceComponentName == "dsExport")
                e.Data = CustomItemDataGenerator.GetExportData();
            else if (e.DataSourceComponentName == "dsContinent")
                e.Data = CustomItemDataGenerator.GetContinentData();
        }

        private void DashboardDesigner1_CustomDashboardItemControlCreating(object sender, DevExpress.DashboardWin.CustomDashboardItemControlCreatingEventArgs e)
        {
            if (e.MetadataType == typeof(HelloWorldItemMetadata))
                e.CustomControlProvider = new HelloWorldItemProvider();
            if (e.MetadataType == typeof(SimpleTableMetadata))
                e.CustomControlProvider = new SimpleTableProvider();
            if (e.MetadataType == typeof(FunnelItemMetadata))
                e.CustomControlProvider = new FunnelItemControlProvider(dashboardDesigner1.Dashboard.Items[e.DashboardItemName] as CustomDashboardItem<FunnelItemMetadata>);
        }
    }
}
