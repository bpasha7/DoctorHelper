using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocCreater
{
    public partial class PreviewForm : Form
    {
        public PreviewForm()
        {
            InitializeComponent();
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate($@"{Directory.GetCurrentDirectory()}\Temp\temp.pdf");
            webBrowser1.ShowPrintPreviewDialog();
        }
    }
}
