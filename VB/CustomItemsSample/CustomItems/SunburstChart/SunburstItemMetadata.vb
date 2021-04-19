Imports DevExpress.DashboardCommon
Imports System.ComponentModel

Namespace CustomItemsSample
	<DisplayName("Sunburst"), CustomItemDescription("Sunburst description"), CustomItemImage("CustomItemsSample.Images.SunburstCustomItem.svg")>
	Public Class SunburstItemMetadata
		Inherits CustomItemMetadata

'INSTANT VB NOTE: The field arguments was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private ReadOnly arguments_Conflict As New DimensionCollection()
		<DisplayName("Value"), EmptyDataItemPlaceholder("Value"), >
		Public Property Value() As Measure
			Get
				Return GetPropertyValue(Of Measure)()
			End Get
			Set(ByVal value As Measure)
				SetPropertyValue(value)
			End Set
		End Property
		<DisplayName("Arguments"), EmptyDataItemPlaceholder("Argument"), SupportColoring(DefaultColoringMode.None), SupportInteractivity>
		Public ReadOnly Property Arguments() As DimensionCollection
			Get
				Return arguments_Conflict
			End Get
		End Property
	End Class
End Namespace
