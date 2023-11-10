using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DashboardCommon.ViewerData;
using TutorialsCustomItems.CustomItems;
using DevExpress.Spreadsheet;

namespace TutorialsCustomItems {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            dashboardDesigner1.CreateRibbon();
            dashboardDesigner1.LoadDashboard(@"..\..\Data\TutorialCustomItems.xml");
            dashboardDesigner1.CreateCustomItemBars();
            dashboardDesigner1.CustomizeExportDocument += dashboardDesigner1_CustomizeExportDocument;
        }

        private void DashboardDesigner1_DataLoading(object sender, DevExpress.DashboardCommon.DataLoadingEventArgs e){
            if (e.DataSourceComponentName == "dsExport")
                e.Data = CustomItemDataGenerator.GetExportData();
            else if (e.DataSourceComponentName == "dsContinent")
                e.Data = CustomItemDataGenerator.GetContinentData();
        }

        private void DashboardDesigner1_CustomDashboardItemControlCreating(object sender, DevExpress.DashboardWin.CustomDashboardItemControlCreatingEventArgs e) {
            if (e.MetadataType == typeof(HelloWorldItemMetadata))
                e.CustomControlProvider = new HelloWorldItemProvider();
            if (e.MetadataType == typeof(SimpleTableMetadata))
                e.CustomControlProvider = new SimpleTableProvider();
            if (e.MetadataType == typeof(FunnelItemMetadata))
                e.CustomControlProvider = new FunnelItemControlProvider(dashboardDesigner1.Dashboard.Items[e.DashboardItemName] as CustomDashboardItem<FunnelItemMetadata>);
        }

        private void dashboardDesigner1_CustomizeExportDocument(object sender, CustomizeExportDocumentEventArgs e) {
            CustomDashboardItem item = dashboardDesigner1.Dashboard.Items.FirstOrDefault(i => i.ComponentName == e.ItemComponentName) as CustomDashboardItem;

            if (item != null) {
                DevExpress.Spreadsheet.Workbook workbook = new DevExpress.Spreadsheet.Workbook();
                Worksheet worksheet = workbook.Worksheets[0];

                MultiDimensionalData itemData = e.GetItemData(e.ItemComponentName);
                CustomItemData customItemData = new CustomItemData(item, itemData);

                DashboardFlatDataSource flatData = customItemData.GetFlatData();
                IList<DashboardFlatDataColumn> columns = flatData.GetColumns();
                for (int colIndex = 0; colIndex < columns.Count; colIndex++) {
                    worksheet.Cells[0, colIndex].Value = columns[colIndex].DisplayName;
                    worksheet.Cells[0, colIndex].FillColor = Color.LightGreen;
                    worksheet.Cells[0, colIndex].Font.FontStyle = SpreadsheetFontStyle.Bold;
                    int headerOffset = 1;
                    for (int rowIndex = 0; rowIndex < flatData.Count; rowIndex++)
                        worksheet.Cells[rowIndex + headerOffset, colIndex].Value = flatData.GetDisplayText(columns[colIndex].Name, rowIndex);
                }
                e.Stream.SetLength(0);
                workbook.SaveDocument(e.Stream, DocumentFormat.Xlsx);
            }
        }
    }
}
