using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TutorialsCustomItems.CustomItems
{
    [DisplayName("Simple Table"),
    CustomItemDescription("Simple Table description"),
    CustomItemImage("TutorialsCustomItems.images.CustomGrid.svg")]
    public class SimpleTableMetadata : CustomItemMetadata
    {
        [DisplayName("Columns"),
        EmptyDataItemPlaceholder("Dimension Column")]
        public DimensionCollection DimensionColumns { get; } = new DimensionCollection();
        [DisplayName("Columns"),
        EmptyDataItemPlaceholder("Measure Column")]
        public Measure MeasureColumn
        {
            get { return GetPropertyValue<Measure>(); }
            set { SetPropertyValue(value); }
        }
    }

    public class SimpleTableProvider : CustomControlProviderBase
    {
        GridView view;
        GridControl grid;
        protected override Control Control { get { return grid; } }
        public SimpleTableProvider()
        {
            grid = new GridControl();
            view = new GridView();
            view.OptionsBehavior.Editable = false;
            view.OptionsView.ShowGroupPanel = false;
            view.CustomColumnDisplayText += View_CustomColumnDisplayText;
            grid.MainView = view;
        }
 
        DashboardFlatDataSource DataSource;
        protected override void UpdateControl(CustomItemData customItemData)
        {
            DashboardFlatDataSource flatData = customItemData.GetFlatData();
            DataSource = flatData;
            grid.DataSource = flatData;
            view.PopulateColumns();
            view.BestFitColumns();


        }
        void View_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            DashboardFlatDataSource data = (DashboardFlatDataSource)grid.DataSource;
            e.DisplayText = data.GetDisplayText(e.Column.FieldName, e.ListSourceRowIndex);
        }

    }
}
