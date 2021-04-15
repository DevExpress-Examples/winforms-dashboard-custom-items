using System.ComponentModel;
using DevExpress.DashboardCommon;

namespace CustomItemsSample {
    [
    DisplayName("Sankey"),
    CustomItemDescription("Sankey description"),
    CustomItemImage("DashboardMainDemo.Images.SankeyCustomItem.svg")
    ]
    public class SankeyItemMetadata : CustomItemMetadata {
        [
        DisplayName("Weight"),
        EmptyDataItemPlaceholder("Weight")
        ]
        public Measure Weight {
            get { return GetPropertyValue<Measure>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Target"),
        EmptyDataItemPlaceholder("Target"),
        SupportColoring(DefaultColoringMode.None),
        SupportInteractivity
        ]
        public Dimension Target {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Source"),
        EmptyDataItemPlaceholder("Source"),
        SupportColoring(DefaultColoringMode.Hue),
        SupportInteractivity
        ]
        public Dimension Source {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
    }
}
