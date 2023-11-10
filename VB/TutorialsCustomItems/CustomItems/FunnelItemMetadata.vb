Imports System
Imports System.ComponentModel
Imports DevExpress.DashboardCommon
Imports DevExpress.XtraCharts

Namespace TutorialsCustomItems
    <DisplayName("Funnel"), Description("Funnel description"), CustomItemImage("Funnel.svg")>
    Public Class FunnelItemMetadata
		Inherits CustomItemMetadata

		<DisplayName("Value"), EmptyDataItemPlaceholder("Value"), SupportColoring(DefaultColoringMode.None)>
		Public Property Value() As Measure
			Get
				Return GetPropertyValue(Of Measure)()
			End Get
			Set(ByVal value As Measure)
				SetPropertyValue(value)
			End Set
		End Property
		<DisplayName("Arguments"), EmptyDataItemPlaceholder("My Argument"), SupportColoring(DefaultColoringMode.Hue), SupportInteractivity>
		Public ReadOnly Property Arguments() As New DimensionCollection()

	End Class
End Namespace
