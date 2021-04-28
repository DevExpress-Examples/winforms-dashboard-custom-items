Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace TutorialsCustomItems.CustomItems
    <DisplayName("Simple Table"), CustomItemDescription("Simple Table description"), CustomItemImage("CustomGrid.svg")>
    Public Class SimpleTableMetadata
		Inherits CustomItemMetadata

		<DisplayName("Columns"), EmptyDataItemPlaceholder("Dimension Column")>
		Public ReadOnly Property DimensionColumns() As New DimensionCollection()
		<DisplayName("Columns"), EmptyDataItemPlaceholder("Measure Column")>
		Public Property MeasureColumn() As Measure
			Get
				Return GetPropertyValue(Of Measure)()
			End Get
			Set(ByVal value As Measure)
				SetPropertyValue(value)
			End Set
		End Property
	End Class

	Public Class SimpleTableProvider
		Inherits CustomControlProviderBase

		Private view As GridView
		Private grid As GridControl
		Public Overrides ReadOnly Property Control() As Control
			Get
				Return grid
			End Get
		End Property
		Public Sub New()
			grid = New GridControl()
			view = New GridView()
			view.OptionsBehavior.Editable = False
			view.OptionsView.ShowGroupPanel = False
			AddHandler view.CustomColumnDisplayText, AddressOf View_CustomColumnDisplayText
			grid.MainView = view
		End Sub

		Private DataSource As DashboardFlatDataSource
		Public Overrides Sub UpdateControl(ByVal customItemData As CustomItemData)
			Dim flatData As DashboardFlatDataSource = customItemData.GetFlatData()
			DataSource = flatData
			grid.DataSource = flatData
			view.PopulateColumns()
			view.BestFitColumns()


		End Sub
		Private Sub View_CustomColumnDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs)
			Dim data As DashboardFlatDataSource = DirectCast(grid.DataSource, DashboardFlatDataSource)
			e.DisplayText = data.GetDisplayText(e.Column.FieldName, e.ListSourceRowIndex)
		End Sub

	End Class
End Namespace
