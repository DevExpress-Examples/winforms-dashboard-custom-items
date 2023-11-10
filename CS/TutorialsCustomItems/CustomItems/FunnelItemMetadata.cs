using System;
using System.ComponentModel;
using DevExpress.DashboardCommon;
using DevExpress.XtraCharts;

namespace TutorialsCustomItems{
    [DisplayName("Funnel"),
    Description("Funnel description"),
    CustomItemImage("TutorialsCustomItems.images.Funnel.svg")]
    public class FunnelItemMetadata : CustomItemMetadata {
        [DisplayName("Value"),
        EmptyDataItemPlaceholder("Value"),
        SupportColoring(DefaultColoringMode.None)]
        public Measure Value {
            get { return GetPropertyValue<Measure>(); }
            set { SetPropertyValue(value); }
        }
        [DisplayName("Arguments"),
        EmptyDataItemPlaceholder("My Argument"),
        SupportColoring(DefaultColoringMode.Hue),
        SupportInteractivity]
        public DimensionCollection Arguments { get; } = new DimensionCollection();

    }
}
