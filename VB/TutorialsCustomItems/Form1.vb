Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardCommon.ViewerData
Imports DevExpress.Spreadsheet
Imports TutorialsCustomItems.CustomItems

Namespace TutorialsCustomItems
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
			dashboardDesigner1.CreateRibbon()
			dashboardDesigner1.LoadDashboard("..\..\Data\TutorialCustomItems.xml")
			dashboardDesigner1.CreateCustomItemBars()
			AddHandler dashboardDesigner1.CustomizeExportDocument, AddressOf dashboardDesigner1_CustomizeExportDocument
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

		Private Sub dashboardDesigner1_CustomizeExportDocument(ByVal sender As Object, ByVal e As CustomizeExportDocumentEventArgs)
			Dim item As CustomDashboardItem = TryCast(dashboardDesigner1.Dashboard.Items.FirstOrDefault(Function(i) i.ComponentName = e.ItemComponentName), CustomDashboardItem)

			If item IsNot Nothing Then
				Dim workbook As New DevExpress.Spreadsheet.Workbook()
				Dim worksheet As Worksheet = workbook.Worksheets(0)

				Dim itemData As MultiDimensionalData = e.GetItemData(e.ItemComponentName)
				Dim customItemData As New CustomItemData(item, itemData)

				Dim flatData As DashboardFlatDataSource = customItemData.GetFlatData()
				Dim columns As IList(Of DashboardFlatDataColumn) = flatData.GetColumns()
				For colIndex As Integer = 0 To columns.Count - 1
					worksheet.Cells(0, colIndex).Value = columns(colIndex).DisplayName
					worksheet.Cells(0, colIndex).FillColor = Color.LightGreen
					worksheet.Cells(0, colIndex).Font.FontStyle = SpreadsheetFontStyle.Bold
					Dim headerOffset As Integer = 1
					For rowIndex As Integer = 0 To flatData.Count - 1
						worksheet.Cells(rowIndex + headerOffset, colIndex).Value = flatData.GetDisplayText(columns(colIndex).Name, rowIndex)
					Next rowIndex
				Next colIndex
				e.Stream.SetLength(0)
				workbook.SaveDocument(e.Stream, DocumentFormat.Xlsx)
			End If
		End Sub
	End Class
End Namespace
