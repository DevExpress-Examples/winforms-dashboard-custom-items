﻿Imports Microsoft.VisualBasic
Imports DevExpress.DashboardWin
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace CustomItemsSample
	Public Interface IExtensionModule
		Sub AttachViewer(ByVal viewer As DashboardViewer)
		Sub DetachViewer()
		Sub AttachDesigner(ByVal designer As DashboardDesigner)
		Sub DetachDesigner()
	End Interface
End Namespace