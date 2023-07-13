Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.XtraEditors
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace TutorialsCustomItems.CustomItems
    <DisplayName("Hello World Item"), CustomItemDescription("Hello World Description"), CustomItemImage("HelloWorldIcon.svg")>
    Friend Class HelloWorldItemMetadata
		Inherits CustomItemMetadata

	End Class

	Public Class HelloWorldItemProvider
		Inherits CustomControlProviderBase

		Private textbox As MemoEdit
		Protected Overrides ReadOnly Property Control() As Control
			Get
				Return textbox
			End Get
		End Property
		Public Sub New()
			textbox = New MemoEdit()
			textbox.Text = "Hello World!"
			textbox.ReadOnly = True
			textbox.ContextMenu = New ContextMenu()

		End Sub
	End Class
End Namespace
