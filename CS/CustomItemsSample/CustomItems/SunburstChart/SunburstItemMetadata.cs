using DevExpress.DashboardCommon;
using System.ComponentModel;

namespace CustomItemsSample {
    [
    DisplayName("Sunburst"),
    CustomItemDescription("Sunburst description"),
    CustomItemImage("DashboardMainDemo.Images.SunburstCustomItem.svg")
    ]
    public class SunburstItemMetadata : CustomItemMetadata {
        readonly DimensionCollection arguments = new DimensionCollection();
        [
        DisplayName("Value"),
        EmptyDataItemPlaceholder("Value"),
        ]
        public Measure Value {
            get { return GetPropertyValue<Measure>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Arguments"),
        EmptyDataItemPlaceholder("Argument"),
        SupportColoring(DefaultColoringMode.None),
        SupportInteractivity
        ]
        public DimensionCollection Arguments { get { return arguments; } }
    }
}
