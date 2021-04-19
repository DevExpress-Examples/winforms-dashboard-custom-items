Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin

Namespace CustomItemsSample
	Public Class WaypointMapItemExtensionModule
		Implements IExtensionModule

		Private dashboardControl As IDashboardControl
		Public Sub AttachViewer(ByVal viewer As DashboardViewer) Implements IExtensionModule.AttachViewer
			AttachDashboardControl(viewer)
		End Sub
		Public Sub DetachViewer() Implements IExtensionModule.DetachViewer
			Detach()
		End Sub
		Public Sub AttachDesigner(ByVal designer As DashboardDesigner) Implements IExtensionModule.AttachDesigner
			AttachDashboardControl(designer)
			designer.CreateCustomItemsBars(GetType(WaypointMapItemMetadata))
		End Sub
		Public Sub DetachDesigner() Implements IExtensionModule.DetachDesigner
			Detach()
		End Sub
		Private Sub Detach()
			If dashboardControl IsNot Nothing Then
				RemoveHandler dashboardControl.CustomDashboardItemControlCreating, AddressOf OnCustomDashboardItemControlCreating
			End If
		End Sub
		Private Sub AttachDashboardControl(ByVal dashboardControl As IDashboardControl)
			If dashboardControl IsNot Nothing Then
				Me.dashboardControl = dashboardControl
				AddHandler dashboardControl.CustomDashboardItemControlCreating, AddressOf OnCustomDashboardItemControlCreating
			End If
		End Sub
		Private Sub OnCustomDashboardItemControlCreating(ByVal sender As Object, ByVal e As CustomDashboardItemControlCreatingEventArgs)
			Dim dashboardControl As IDashboardControl = DirectCast(sender, IDashboardControl)
			If e.MetadataType Is GetType(WaypointMapItemMetadata) Then
				e.CustomControlProvider = New WaypointMapItemControlProvider(TryCast(dashboardControl.Dashboard.Items(e.DashboardItemName), CustomDashboardItem(Of WaypointMapItemMetadata)), "YOUR BING KEY")
			End If
		End Sub
	End Class
End Namespace
