using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game.Controller
{
    public partial class PrintPreviewDialogEx : PrintPreviewDialog
    {
        public PrintPreviewDialogEx()
        {
            foreach (Control ctrl in base.Controls)
            {
                System.Windows.Forms.Application.DoEvents();
                if (ctrl.GetType() == typeof(ToolStrip))
                {
                    ToolStrip tools = ctrl as ToolStrip;
                    tools.Items.Insert(0, CreatePrintsetButton());
                }
            }
        }

        ToolStripButton CreatePrintsetButton()
        {
            ToolStripButton Stripbutton = new ToolStripButton();
            Stripbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //Stripbutton.Image = global::UserCtrl.CtrlResource.property;
            Stripbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            Stripbutton.Name = "printsetStripButton";
            Stripbutton.Size = new System.Drawing.Size(23, 22);
            Stripbutton.Text = "列印設定";
            Stripbutton.Click += new System.EventHandler(this.Stripbutton_Click);
            return Stripbutton;
        }

        private void Stripbutton_Click(object sender, EventArgs e)
        {
            using (PrintDialog diag = new PrintDialog())
            {
                diag.Document = base.Document;
                diag.ShowDialog();
            }
        }
    }
}