Imports Microsoft.VisualBasic
Imports System.ComponentModel
Imports DevExpress.DashboardCommon

Namespace CustomItemsSample
    <DisplayName("Sankey"), CustomItemDescription("Sankey description"), CustomItemImage("SankeyCustomItem.svg")>
    Public Class SankeyItemMetadata
		Inherits CustomItemMetadata
		<DisplayName("Weight"), EmptyDataItemPlaceholder("Weight")> _
		Public Property Weight() As Measure
			Get
				Return GetPropertyValue(Of Measure)()
			End Get
			Set(ByVal value As Measure)
				SetPropertyValue(value)
			End Set
		End Property
		<DisplayName("Target"), EmptyDataItemPlaceholder("Target"), SupportColoring(DefaultColoringMode.None), SupportInteractivity> _
		Public Property Target() As Dimension
			Get
				Return GetPropertyValue(Of Dimension)()
			End Get
			Set(ByVal value As Dimension)
				SetPropertyValue(value)
			End Set
		End Property
		<DisplayName("Source"), EmptyDataItemPlaceholder("Source"), SupportColoring(DefaultColoringMode.Hue), SupportInteractivity> _
		Public Property Source() As Dimension
			Get
				Return GetPropertyValue(Of Dimension)()
			End Get
			Set(ByVal value As Dimension)
				SetPropertyValue(value)
			End Set
		End Property
	End Class
End Namespace
