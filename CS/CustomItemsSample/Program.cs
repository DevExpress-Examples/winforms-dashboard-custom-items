using DevExpress.DashboardCommon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomItemsSample {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            WindowsFormsSettings.SetDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Dashboard.CustomItemMetadataTypes.Register<SunburstItemMetadata>();
            Dashboard.CustomItemMetadataTypes.Register<SankeyItemMetadata>();
            Dashboard.CustomItemMetadataTypes.Register<WaypointMapItemMetadata>();
            Application.Run(new Form1());
        }
    }
}
