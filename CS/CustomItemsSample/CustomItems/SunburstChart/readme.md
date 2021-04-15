A custom Sunburst item supports the following capabilities:

* [Export](http://docs.devexpress.devx/Dashboard/15187/winforms-dashboard/winforms-designer/create-dashboards-in-the-winforms-designer/printing-and-exporting?v=21.1&p=netframework)
* [Master-Filtering](http://docs.devexpress.devx/Dashboard/15702/winforms-dashboard/winforms-designer/create-dashboards-in-the-winforms-designer/interactivity/master-filtering?v=21.1)
* [Coloring](http://docs.devexpress.devx/Dashboard/17868/winforms-dashboard/winforms-designer/create-dashboards-in-the-winforms-designer/appearance-customization/coloring?v=21.1) 

## Example Structure

The SunburstChart module consists of the following classes:

* [SunburstItemMetadata](SunburstItemMetadata.cs)

:    Configures the data item structure in a custom Sunburst dashboard item and creates a [binding panel](http://docs.devexpress.devx/Dashboard/15622/winforms-dashboard/winforms-designer/ui-elements/data-items-pane?v=21.1). 

* [SunburstItemControlProvider](SunburstItemControlProvider.cs)

:    Configures a custom control that displays the Sunburst custom item. The [CustomControlProviderBase.UpdateControl(CustomItemData)](http://docs.devexpress.devx/Dashboard/DevExpress.DashboardWin.CustomControlProviderBase.UpdateControl(DevExpress.DashboardCommon.CustomItemData)?v=21.1&p=netframework) method is called each time when custom item's data or settings change. The method supplies calculated data for a custom item based on measures and dimensions that are specified in `SunburstItemMetadata`. The [GetPrintableControl](http://docs.devexpress.devx/Dashboard/DevExpress.DashboardWin.CustomControlProviderBase.GetPrintableControl(DevExpress.DashboardCommon.CustomItemData-DevExpress.DashboardCommon.CustomItemExportInfo)?v=21.1&p=netframework) method exports the custom item. 
The `SetSelection()` method applies the element selection according to the master filter state. The SelectedItemsChanged() method processes the element selection to apply the master filter to the dashboard.


* [SunburstItemExtensionModule](SunburstItemExtensionModule.cs)

:    Contains the `Attach()` and `Detach()` methods that create custom item bars in the Ribbon, attach the DashboardDesigner to the module and subscribe and unsubscribe the [DashboardDesigner.CustomDashboardItemControlCreating](xref:DevExpress.DashboardWin.DashboardDesigner.CustomDashboardItemControlCreating) event used to visualize the custom item in a dashboard.

## How to Integrate SunburstChart Module 

* Add the `SunburstChart` module to your solution.
* Add a reference to this project to References in your project with a dashboard control.
* Call the following code to create to register the `SunburstItemMetadata` type in your application:

**C# code**:
```csharp
namespace CustomItemsSample {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            //...
            Dashboard.CustomItemMetadataTypes.Register<SunburstItemMetadata>();
            Application.Run(new Form1());
        }
    }
}
```

**VB code**: 
```vb
Namespace CustomItemsSample
    Friend NotInheritable Class Program
        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        Private Sub New()
        End Sub
        <STAThread> _
        Shared Sub Main()
          '...
          Dashboard.CustomItemMetadataTypes.Register(Of SunburstItemMetadata)()
          Application.Run(New Form1())
        End Sub
    End Class
End Namespace
```
* Call the following code to attach the extension to the DashboardDesigner control:

**C# code**:
```csharp
public Form1() {
        InitializeComponent();
        SunburstItemModule.AttachDesigner(dashboardDesigner1);
}
``` 

**VB code**: 
```vb
Public Sub New()
		InitializeComponent()
		SunburstItemModule.AttachDesigner(dashboardDesigner1)
End Sub
```