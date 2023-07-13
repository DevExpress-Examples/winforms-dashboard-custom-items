Imports DevExpress.DashboardCommon
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports TutorialsCustomItems.CustomItems

Namespace TutorialsCustomItems
	Friend Module Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread>
		Sub Main()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Dashboard.CustomItemMetadataTypes.Register(Of HelloWorldItemMetadata)()
			Dashboard.CustomItemMetadataTypes.Register(Of SimpleTableMetadata)()
			Dashboard.CustomItemMetadataTypes.Register(Of FunnelItemMetadata)()
			Application.Run(New Form1())
		End Sub
	End Module
End Namespace
