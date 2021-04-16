Imports Microsoft.VisualBasic
Imports DevExpress.DashboardCommon
Imports DevExpress.XtraEditors
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace CustomItemsSample
	Friend NotInheritable Class Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		Private Sub New()
		End Sub
		<STAThread> _
		Shared Sub Main()
			WindowsFormsSettings.SetDPIAware()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Dashboard.CustomItemMetadataTypes.Register(Of SunburstItemMetadata)()
			Dashboard.CustomItemMetadataTypes.Register(Of SankeyItemMetadata)()
			Dashboard.CustomItemMetadataTypes.Register(Of WaypointMapItemMetadata)()
			Application.Run(New Form1())
		End Sub
	End Class
End Namespace
