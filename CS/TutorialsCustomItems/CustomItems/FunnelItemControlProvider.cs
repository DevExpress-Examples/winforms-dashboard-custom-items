﻿using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;

namespace TutorialsCustomItems{
    public class FunnelItemControlProvider : CustomControlProviderBase {
        CustomDashboardItem<FunnelItemMetadata> dashboardItem;
        ChartControl chart;
        DashboardFlatDataSource flatData;
        protected override Control Control { get { return chart; } }

        public FunnelItemControlProvider(CustomDashboardItem<FunnelItemMetadata> dashboardItem) {
            this.dashboardItem = dashboardItem;
            chart = new ChartControl();
            chart.RuntimeHitTesting = true;
            chart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.SeriesSelectionMode = SeriesSelectionMode.Point;
            chart.MouseDoubleClick += MouseDoubleClick;
            chart.SelectedItemsChanged += ChartSelectedItemsChanged;
            chart.SelectedItemsChanging += ChartSelectedItemsChanging;
            chart.CustomDrawSeriesPoint += CustomDrawSeriesPoint;
        }

        protected override void UpdateControl(CustomItemData customItemData)
        {
            UpdateSelectionMode();
            flatData = customItemData.GetFlatData(new DashboardFlatDataSourceOptions() { AddColoringColumns = true });
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chart.Series.Clear();
            Series series = ConfigureSeries(flatData);
            chart.Series.Add(series);
        }
        protected override XRControl GetPrintableControl(CustomItemData customItemData, CustomItemExportInfo info)
        {
            PrintableComponentContainer container = new PrintableComponentContainer();
            container.PrintableComponent = chart;
            return container;
        }
        Series ConfigureSeries(DashboardFlatDataSource flatData)
        {
            Series series = new Series("A Funnel Series", ViewType.Funnel);
            if (dashboardItem.Metadata.Value != null && dashboardItem.Metadata.Arguments.Count > 0)
            {
                series.DataSource = flatData;
                series.ValueDataMembers.AddRange(dashboardItem.Metadata.Value.UniqueId);
                if (Interactivity.IsDrillDownEnabled)
                {
                    int drillDownLevel = Interactivity.GetCurrentDrillDownValues().Count;
                    series.ArgumentDataMember = dashboardItem.Metadata.Arguments[drillDownLevel].UniqueId;
                }
                else
                    series.ArgumentDataMember = dashboardItem.Metadata.Arguments.Last().UniqueId;
                series.ColorDataMember = flatData.GetColoringColumn(dashboardItem.Metadata.Value.UniqueId).Name;
            }
            ((FunnelSeriesLabel)series.Label).Position = FunnelSeriesLabelPosition.Center;
            return series;
        }
        void CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            DashboardFlatDataSourceRow row = e.SeriesPoint.Tag as DashboardFlatDataSourceRow;
            string formattedValue = flatData.GetDisplayText(dashboardItem.Metadata.Value.UniqueId, row);
            e.LabelText = e.SeriesPoint.Argument + " - " + formattedValue;
            e.LegendText = e.SeriesPoint.Argument;
        }
        # region MasterFiltering
        protected override void SetSelection(CustomItemSelection selection)
        {
            chart.ClearSelection();
            foreach (DashboardFlatDataSourceRow item in selection.GetDashboardFlatDataSourceRows(flatData))
                chart.SelectedItems.Add(item);
        }
        void ChartSelectedItemsChanging(object sender, SelectedItemsChangingEventArgs e)
        {
            if (Interactivity.MasterFilterMode == DashboardItemMasterFilterMode.Single && e.NewItems.OfType<DashboardFlatDataSourceRow>().Count() == 0)
                e.Cancel = true;
        }
        void ChartSelectedItemsChanged(object sender, SelectedItemsChangedEventArgs e)
        {
            if (chart.SelectedItems.Count == 0 && Interactivity.CanClearMasterFilter)
                Interactivity.ClearMasterFilter();
            else if (Interactivity.CanSetMasterFilter)
                Interactivity.SetMasterFilter(chart.SelectedItems.OfType<DashboardFlatDataSourceRow>());
        }
        void UpdateSelectionMode()
        {
            switch (Interactivity.MasterFilterMode)
            {
                case DashboardItemMasterFilterMode.None:
                    chart.SelectionMode = ElementSelectionMode.None;
                    break;
                case DashboardItemMasterFilterMode.Single:
                    chart.SelectionMode = ElementSelectionMode.Single;
                    break;
                case DashboardItemMasterFilterMode.Multiple:
                    chart.SelectionMode = ElementSelectionMode.Extended;
                    break;
                default:
                    chart.SelectionMode = ElementSelectionMode.None;
                    break;
            }
        }
            #endregion
            void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChartHitInfo hitInfo = chart.CalcHitInfo(e.Location);
            if (hitInfo.InSeriesPoint)
            {
                if (Interactivity.CanPerformDrillDown)
                    Interactivity.PerformDrillDown(hitInfo.SeriesPoint.Tag as DashboardFlatDataSourceRow);
            }
        }
    }
}