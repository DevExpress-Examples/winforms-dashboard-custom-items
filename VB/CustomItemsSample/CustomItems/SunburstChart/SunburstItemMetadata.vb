Imports Microsoft.VisualBasic
Imports DevExpress.DashboardCommon
Imports System.ComponentModel

Namespace CustomItemsSample
    <DisplayName("Sunburst"), CustomItemDescription("Sunburst description"), CustomItemImage("SunburstCustomItem.svg")>
    Public Class SunburstItemMetadata
		Inherits CustomItemMetadata
		Private ReadOnly arguments_Renamed As New DimensionCollection()
		<DisplayName("Value"), EmptyDataItemPlaceholder("Value")> _
		Public Property Value() As Measure
			Get
				Return GetPropertyValue(Of Measure)()
			End Get
			Set(ByVal value As Measure)
				SetPropertyValue(value)
			End Set
		End Property
		<DisplayName("Arguments"), EmptyDataItemPlaceholder("Argument"), SupportColoring(DefaultColoringMode.None), SupportInteractivity> _
		Public ReadOnly Property Arguments() As DimensionCollection
			Get
				Return arguments_Renamed
			End Get
		End Property
	End Class
End Namespace
