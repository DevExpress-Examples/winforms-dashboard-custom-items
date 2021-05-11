
# WinForms Dashboard - Custom Items

The following example shows how to implement a custom dashboard item in a WinForms application. Custom items allow you to embed any WinForms UI control in a dashboard. You can interact with custom items in the Dashboard Designer just like with any built-in item.


The example's solution contains two projects. 
## CustomItemsSample

*Files to look at*:

* [Form1.cs](./CS/CustomItemsSample/Form1.cs) (VB: [Form1.vb](./VB/CustomItemsSample/Form1.vb))
* [Modules](./CS/CustomItemsSample/CustomItems/) (VB: [Modules](./VB/CustomItemsSample/CustomItems/))

This project contains the following custom dashboard items that allow you to add additional functionality to the WinForms Dashboard:

* [Sankey diagram](./CS/CustomItemsSample/CustomItems/SankeyChart/readme.md)

    A Sankey diagram visualizes data as weighted flows or relationships between nodes. 
* [Sunburst](./CS/CustomItemsSample/CustomItems/SunburstChart/readme.md)

    A Sunburst combines a TreeMap and a Pie chart to visualize hierarchical data in a circular layout. 
* [Waypoint map](./CS/CustomItemsSample/CustomItems/WaypointMap/readme.md) 

    A Waypoint map visualized data as linked points. 

## TutorialsCustomItems

*Files to look at*:

* [Form1.cs](./CS/TutorialsCustomItems/Form1.cs) (VB: [Form1.vb](./VB/TutorialsCustomItems/Form1.vb))
* [Modules](./CS/TutorialsCustomItems/CustomItems/) (VB: [Modules](./VB/TutorialsCustomItems/CustomItems/))


This project contains the following custom dashboard items that allow you to add additional functionality to the WinForms Dashboard:

* Hello World ([С#](./CS/TutorialsCustomItems/CustomItems/)/ [VB](./VB/TutorialsCustomItems/CustomItems/))

    A custom item that displays the 'Hello World!' text. 

* Simple Table ([С#](./CS/TutorialsCustomItems/CustomItems/)/ [VB](./VB/TutorialsCustomItems/CustomItems/))

    A custom item based on the [Grid](http://docs.devexpress.devx/WindowsForms/DevExpress.XtraGrid.GridControl?v=21.1) control.

* Funnel Chart ([С#](./CS/TutorialsCustomItems/CustomItems/)/ [VB](./VB/TutorialsCustomItems/CustomItems/))

    A Funnel chart displays a wide area at the top, indicating the total points' value, while other areas are proportionally smaller.
