Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports TutorialsCustomItems.CustomItems

Namespace TutorialsCustomItems
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
			dashboardDesigner1.CreateRibbon()
			dashboardDesigner1.LoadDashboard("..\..\Data\TutorialCustomItems.xml")
			dashboardDesigner1.CreateCustomItemsBars()
		End Sub

		Private Sub DashboardDesigner1_DataLoading(ByVal sender As Object, ByVal e As DevExpress.DashboardCommon.DataLoadingEventArgs) Handles dashboardDesigner1.DataLoading
			If e.DataSourceComponentName = "dsExport" Then
				e.Data = CustomItemDataGenerator.GetExportData()
			ElseIf e.DataSourceComponentName = "dsContinent" Then
				e.Data = CustomItemDataGenerator.GetContinentData()
			End If
		End Sub

		Private Sub DashboardDesigner1_CustomDashboardItemControlCreating(ByVal sender As Object, ByVal e As DevExpress.DashboardWin.CustomDashboardItemControlCreatingEventArgs) Handles dashboardDesigner1.CustomDashboardItemControlCreating
			If e.MetadataType Is GetType(HelloWorldItemMetadata) Then
				e.CustomControlProvider = New HelloWorldItemProvider()
			End If
			If e.MetadataType Is GetType(SimpleTableMetadata) Then
				e.CustomControlProvider = New SimpleTableProvider()
			End If
			If e.MetadataType Is GetType(FunnelItemMetadata) Then
				e.CustomControlProvider = New FunnelItemControlProvider(TryCast(dashboardDesigner1.Dashboard.Items(e.DashboardItemName), CustomDashboardItem(Of FunnelItemMetadata)))
			End If
		End Sub

		Private Sub DashboardDesigner1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles dashboardDesigner1.Load

		End Sub
	End Class
End Namespace
