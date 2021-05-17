using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TutorialsCustomItems.CustomItems
{
    [DisplayName("Hello World Item"),
     CustomItemDescription("Hello World Description"),
     CustomItemImage("TutorialsCustomItems.images.HelloWorldIcon.svg")]
    class HelloWorldItemMetadata : CustomItemMetadata
    {

    }

    public class HelloWorldItemProvider : CustomControlProviderBase
    {
        MemoEdit textbox;
        protected override Control Control { get { return textbox; } }
        public HelloWorldItemProvider()
        {
            textbox = new MemoEdit();
            textbox.Text = "Hello World!";
            textbox.ReadOnly = true;
            textbox.ContextMenu = new ContextMenu();

        }
    }
}
