using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Netcode.Common
{

    public class CriticalErrors : Form
    {
        Messages ms = new Messages();
        ListView lv = new ListView();
        public CriticalErrors()
        {
            this.Size = new Size(600, 197);
            this.Text = "Критическая ошибка";
            this.Name = "CriticalErrors";
            TopMost = true;
            Opacity = 100;
            ControlBox = true;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;

            lv.Name = "lb";
            lv.Dock = DockStyle.Fill;
            this.Controls.Add(lv);

            lv.BackColor = Color.Red;
            lv.ForeColor = Color.White;
            lv.View = View.Details;
            lv.GridLines = true;
            lv.MultiSelect = false;
            lv.Columns.Add("Время", "Время", 134);
            lv.Columns.Add("Тип", "Тип", 40);
            lv.Columns.Add("Комментарий", "Комментарий", 394);
        }

        public void PrintError(string class_error, string error)
        {
            if (Application.OpenForms["CriticalErrors"] != null)
            {
                ListView lbc = (ListView)Application.OpenForms["CriticalErrors"].Controls["lb"];
                ms.write_lview_message(class_error, error, Color.Red, 0, lbc);
            }
            else
            {
                this.Show();
                ms.write_lview_message(class_error, error, Color.Red, 0, lv);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CriticalErrors
            // 
            this.ClientSize = new System.Drawing.Size(292, 84);
            this.Name = "CriticalErrors";
            this.ResumeLayout(false);

        }
    }
}
