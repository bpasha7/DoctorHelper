using Repository;
using Security;
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
    public partial class Menu : MetroFramework.Forms.MetroForm
    {
        private Form1 _f;

        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void Menu_Shown(object sender, EventArgs e)
        {
            //var uc = new UsbChecker();
            //if (!uc.Check())
            //{
            //    this.Close();
            //    return;
            //}
            prgSpinner.Value = 10;
            _f = new Form1();
            prgSpinner.Value = 20;
            System.Threading.Thread.Sleep(500);
            NewDayDirectiry();
            _f?.LoadTab(0);
            prgSpinner.Value = 40;
            prgSpinner.Style = MetroFramework.MetroColorStyle.Orange;
            System.Threading.Thread.Sleep(500);
            _f?.LoadTab(1);
            prgSpinner.Value = 75;
            prgSpinner.Style = MetroFramework.MetroColorStyle.Yellow;
            System.Threading.Thread.Sleep(500);
            _f?.LoadTab(2);
            prgSpinner.Style = MetroFramework.MetroColorStyle.Green;
            prgSpinner.Value = 100;
            this.Style = MetroFramework.MetroColorStyle.Green;
            System.Threading.Thread.Sleep(500);
            this.Hide();
            _f.ShowDialog();
            this.Close();
        }

        private bool CheckKey()
        {


            return false;
        }

        private void NewDayDirectiry()
        {
            string path = $@"{Directory.GetCurrentDirectory()}\Data\{DateTime.Now.ToShortDateString()}"; ;

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                //MessageBox
            }
        }
    }
}
