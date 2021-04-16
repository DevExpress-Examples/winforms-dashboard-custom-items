using System.ComponentModel;
using DevExpress.DashboardCommon;

namespace CustomItemsSample {
    [
    DisplayName("Waypoint Map"),
    CustomItemDescription("Waypoint Map Description"),
    CustomItemImage("CustomItemsSample.Images.WaypointCustomItem.svg")
    ]
    public class WaypointMapItemMetadata : CustomItemMetadata {
        [
        DisplayName("Source Latitude"),
        EmptyDataItemPlaceholder("Latitude"),
        SupportInteractivity,
        SupportedDataTypes(DataSourceFieldType.Numeric)
        ]
        public Dimension SourceLatitude {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Source Longitude"),
        EmptyDataItemPlaceholder("Longitude"),
        SupportInteractivity,
        SupportedDataTypes(DataSourceFieldType.Numeric)
        ]
        public Dimension SourceLongitude {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Target Latitude"),
        EmptyDataItemPlaceholder("Latitude"),
        SupportInteractivity,
        SupportedDataTypes(DataSourceFieldType.Numeric)
        ]
        public Dimension TargetLatitude {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Target Longitude"),
        EmptyDataItemPlaceholder("Longitude"),
        SupportInteractivity,
        SupportedDataTypes(DataSourceFieldType.Numeric)
        ]
        public Dimension TargetLongitude {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
    }
}
