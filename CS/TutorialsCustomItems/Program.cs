using DevExpress.DashboardCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TutorialsCustomItems.CustomItems;

namespace TutorialsCustomItems
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Dashboard.CustomItemMetadataTypes.Register<HelloWorldItemMetadata>();
            Dashboard.CustomItemMetadataTypes.Register<SimpleTableMetadata>();
            Dashboard.CustomItemMetadataTypes.Register<FunnelItemMetadata>();
            Application.Run(new Form1());
        }
    }
}
