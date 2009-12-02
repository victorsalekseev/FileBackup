using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace Netcode.Scan
{
    public partial class FormDetails : Form
    {
        public FormDetails()
        {
            InitializeComponent();
            
        }

        public FormDetails(string[] tag)
        {
            
            InitializeComponent();

            if (!File.Exists(tag[1]))
            {
                propertyGridFS.Enabled = false;
                propertyGridExe.Enabled = false;
                this.Text += " (файл не найден)";
            }
            else
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(tag[1]);
                FileInfo fi = new FileInfo(tag[1]);
                propertyGridFS.SelectedObject = fi;
                propertyGridExe.SelectedObject = fvi;
            }
        }
    }


}