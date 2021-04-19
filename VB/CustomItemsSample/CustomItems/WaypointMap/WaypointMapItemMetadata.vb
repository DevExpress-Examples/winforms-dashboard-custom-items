Imports System.ComponentModel
Imports DevExpress.DashboardCommon

Namespace CustomItemsSample
	<DisplayName("Waypoint Map"), CustomItemDescription("Waypoint Map Description"), CustomItemImage("CustomItemsSample.Images.WaypointCustomItem.svg")>
	Public Class WaypointMapItemMetadata
		Inherits CustomItemMetadata

		<DisplayName("Source Latitude"), EmptyDataItemPlaceholder("Latitude"), SupportInteractivity, SupportedDataTypes(DataSourceFieldType.Numeric)>
		Public Property SourceLatitude() As Dimension
			Get
				Return GetPropertyValue(Of Dimension)()
			End Get
			Set(ByVal value As Dimension)
				SetPropertyValue(value)
			End Set
		End Property
		<DisplayName("Source Longitude"), EmptyDataItemPlaceholder("Longitude"), SupportInteractivity, SupportedDataTypes(DataSourceFieldType.Numeric)>
		Public Property SourceLongitude() As Dimension
			Get
				Return GetPropertyValue(Of Dimension)()
			End Get
			Set(ByVal value As Dimension)
				SetPropertyValue(value)
			End Set
		End Property
		<DisplayName("Target Latitude"), EmptyDataItemPlaceholder("Latitude"), SupportInteractivity, SupportedDataTypes(DataSourceFieldType.Numeric)>
		Public Property TargetLatitude() As Dimension
			Get
				Return GetPropertyValue(Of Dimension)()
			End Get
			Set(ByVal value As Dimension)
				SetPropertyValue(value)
			End Set
		End Property
		<DisplayName("Target Longitude"), EmptyDataItemPlaceholder("Longitude"), SupportInteractivity, SupportedDataTypes(DataSourceFieldType.Numeric)>
		Public Property TargetLongitude() As Dimension
			Get
				Return GetPropertyValue(Of Dimension)()
			End Get
			Set(ByVal value As Dimension)
				SetPropertyValue(value)
			End Set
		End Property
	End Class
End Namespace
