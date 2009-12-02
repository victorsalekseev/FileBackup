using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Netcode.Common;
using System.IO;


namespace Netcode.Scan
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0)
            {
                string input_file = args[0];
                if (File.Exists(input_file))
                {
                    switch (Path.GetExtension(input_file).ToUpper())
                    {
                        case ".CFG":
                            {
                                Netcode.Common.Settings.ManageSetting.path_to_set_file = input_file;
                            }
                            break;
                        case ".LST":
                            {
                            }
                            break;
                        default:
                            {
                                new CriticalErrors().PrintError("L4", "Переданное расширение файла конфигурации не соответствует стандарту.");
                            }
                            break;
                    }
                }
                else
                {
                    new CriticalErrors().PrintError("L3", "Запрошенного файла конфигурации не существует. Применены настройки по умолчанию.");
                }
            }

            Application.Run(new splash());
        }
    }

    public class splash : Form
    {
        public splash()
        {
            this.Shown += new EventHandler(splash_Shown);
            this.Size = new Size(218, 57);
            TopMost = true;
            Opacity = 100;
            StartPosition = FormStartPosition.CenterScreen;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;

            PictureBox pb = new PictureBox();
            pb.Dock = DockStyle.Fill;
            pb.Image = global::Netcode.Backup.Properties.Resources.splash;//В каждом приложении для быстроты!!!
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            this.Controls.Add(pb);
        }

        void splash_Shown(object sender, EventArgs e)
        {
            FormMain fm = new FormMain();
            fm.Show();
            this.Hide();
        }

    }
}