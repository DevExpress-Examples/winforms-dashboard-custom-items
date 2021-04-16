Imports Microsoft.VisualBasic
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin

Namespace CustomItemsSample
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()

			Dim SunburstItemModule As New SunburstItemExtensionModule()
			Dim SankeyItemModule As New SankeyItemExtensionModule()
			Dim WaypointMapItemModule As New WaypointMapItemExtensionModule()

			SunburstItemModule.AttachDesigner(dashboardDesigner1)
			SankeyItemModule.AttachDesigner(dashboardDesigner1)
			WaypointMapItemModule.AttachDesigner(dashboardDesigner1)
            dashboardDesigner1.CreateRibbon()
            dashboardDesigner1.CreateCustomItemsBars()
            dashboardDesigner1.LoadDashboard("..\..\CustomItems.xml")
		End Sub

		Private Sub dashboardDesigner1_DataLoading(ByVal sender As Object, ByVal e As DataLoadingEventArgs) Handles dashboardDesigner1.DataLoading
			If e.DataSourceComponentName = "dsExport" Then
				e.Data = CustomItemDataGenerator.GetExportData()
			ElseIf e.DataSourceComponentName = "dsContinent" Then
				e.Data = CustomItemDataGenerator.GetContinentData()
			End If
		End Sub
	End Class
End Namespace
