Imports DevExpress.DashboardCommon
Imports DevExpress.XtraEditors
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace CustomItemsSample
	Friend Module Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread>
		Sub Main()
			WindowsFormsSettings.SetDPIAware()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Dashboard.CustomItemMetadataTypes.Register(Of SunburstItemMetadata)()
			Dashboard.CustomItemMetadataTypes.Register(Of SankeyItemMetadata)()
			Dashboard.CustomItemMetadataTypes.Register(Of WaypointMapItemMetadata)()
			Application.Run(New Form1())
		End Sub
	End Module
End Namespace
