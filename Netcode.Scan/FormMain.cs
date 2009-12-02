using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using Netcode.Controls;
using Netcode.Common;
using Netcode.Common.Search;
using Netcode.Common.Settings;
using Local_DCrypt.Controls;
using System.Text.RegularExpressions;
using Netcode.Backup;
using Netcode.Crypt;
using Necode.Common.Action;
using Netcode.Common.Controls;
using System.Diagnostics;

namespace Netcode.Scan
{
    public partial class FormMain : Form
    {
        enum ScanningType
        {
            AddNewSigns = 1,
            CheckSigns  = 2
        }

        Messages ms = new Messages();
        Search srch = new Search();

        long count_files = 0;//файлов проверено
        long count_copy_files = 0;//файлов скопировано
        string log_file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log-"+DateTime.Now.ToFileTime().ToString()+".log");

        //структурой пользуемся только в трех нижних функциях!!!!!!!!!!!!!!!!!
        Options o = new Options();
        
        private void LoadToControls()
        {
            textBoxSelectFolderSaveDir.InputText = o.save_dir;

            string[] dirs = Regex.Split(o.bup_dirs, "\\|", RegexOptions.IgnoreCase);
            for (int i = 0; i < dirs.Length; i++)
            {
                if (!string.IsNullOrEmpty(dirs[i]))
                {
                    if (Directory.Exists(dirs[i]))
                    {
                        TreeNode tn = new TreeNode();
                        tn.Tag = dirs[i];
                        tn.Checked = true;
                        folderTreeExploer.Managing_list_folders(tn, true);

                        //для выделения дисков
                        if (dirs[i].Length < 4)
                        {
                            for (int j = 0; j < folderTreeExploer.tV.Nodes.Count; j++)
                            {
                                if (folderTreeExploer.tV.Nodes[j].Tag.ToString().ToUpper() == dirs[i].ToUpper())
                                {
                                    folderTreeExploer.tV.Nodes[j].Checked = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        my_MakeError("Директория " + dirs[i] + " не найдена, поэтому не будет зайдействована в создании резервной копии");
                    }
                }
            }

            numericUpDownMin.Value = o.min_filesize;
            numericUpDownMax.Value = o.max_filesize;

            dateTimePickerTimePrev.Value = o.prev_datetime;
            dateTimePickerTimeFwd.Value = o.fwd_datetime;

            dateTimePicker_datePrev.Value = o.prev_datetime;
            dateTimePickerDateFwd.Value = o.fwd_datetime;

            checkedListBox_prev.SetItemChecked(0, o.is_prew_copy);
            checkedListBox_fwd.SetItemChecked(0, o.is_fwd_copy);

            comboBox_action_backup.SelectedIndex = o.if_fexists;
            checkBox_isEncrypt.Checked = o.is_encrypt;

            //что делать при запуске программы
            comboBox_start_action.SelectedIndex = o.start_action;
            switch (o.start_action)
            {
                case 0:
                    {
                    }
                    break;
                case 1:
                    {
                        //запустить бекап системы
                        toolStripButtonStart_Click(this, null);
                    }
                    break;
                default:
                    break;
            }
            //что делать после бекапа
            comboBox_set_dt.SelectedIndex = o.endbup_action;

            //по завершению программы
            comboBox_exit_app.SelectedIndex = o.exit_action;

            //во время проверки
            comboBox_in_bup.SelectedIndex = o.log_action;

            //шифрование
            if (string.IsNullOrEmpty(o.prefix))
            {
                textBox_prefix.Text = FileCrypt.default_prefix;
            }
            else
            {
                textBox_prefix.Text = o.prefix;
            }
            if (string.IsNullOrEmpty(o.pwd_file_enc))
            {
                textBox_pwd_file_enc.Text = FileCrypt.default_key;
            }
            else
            {
                textBox_pwd_file_enc.Text = o.pwd_file_enc;
            }
            if (string.IsNullOrEmpty(o.key_size.ToString()))
            {
                comboBox_key_size.SelectedIndex = 0;
            }
            else
            {
                comboBox_key_size.SelectedItem = o.key_size.ToString();
            }
            if (string.IsNullOrEmpty(o.pwd_namefile_enc))
            {
                textBox_pwd_namefile_enc.Text = FileCrypt.default_key;
            }
            else
            {
                textBox_pwd_namefile_enc.Text = o.pwd_namefile_enc;
            }
        }

        private void SaveFromControls()
        {
            o.save_dir = TextBoxSelectFolder.set_std_path(textBoxSelectFolderSaveDir.InputText);

            string dirs = string.Empty;
            foreach (object dir in folderTreeExploer.SelectedFolders.Keys)
            {
                dirs += dir.ToString() + "|";
            }
            o.bup_dirs = dirs;

            o.min_filesize = Convert.ToUInt64(numericUpDownMin.Value);
            o.max_filesize = Convert.ToUInt64(numericUpDownMax.Value);

            o.prev_datetime = dateTimePickerTimePrev.Value;
            o.fwd_datetime = dateTimePickerTimeFwd.Value;

            o.prev_datetime = dateTimePicker_datePrev.Value;
            o.fwd_datetime = dateTimePickerDateFwd.Value;

            o.is_prew_copy = checkedListBox_prev.GetItemChecked(0);
            o.is_fwd_copy = checkedListBox_fwd.GetItemChecked(0);

            o.if_fexists = (UInt16)comboBox_action_backup.SelectedIndex;

            o.is_encrypt = checkBox_isEncrypt.Checked;
            o.start_action = (UInt16)comboBox_start_action.SelectedIndex;

            o.endbup_action = (UInt16)comboBox_set_dt.SelectedIndex;
            o.exit_action = (UInt16)comboBox_exit_app.SelectedIndex;
            o.log_action = (UInt16)comboBox_in_bup.SelectedIndex;

            //шифрование
            o.prefix = textBox_prefix.Text;
            o.pwd_file_enc = textBox_pwd_file_enc.Text;
            o.key_size = Convert.ToUInt16( comboBox_key_size.SelectedItem.ToString());
            o.pwd_namefile_enc = textBox_pwd_namefile_enc.Text; 
        }

        private void SetDefaultVal()
        {
            o.save_dir = TextBoxSelectFolder.basepath;
            o.bup_dirs = string.Empty;
            o.min_filesize = 0;
            o.max_filesize = 100000000;
            o.if_fexists = 0;
            o.is_encrypt = false;
            o.prev_datetime = DateTime.Now.AddDays(1);
            o.fwd_datetime = DateTime.Now.AddDays(-1);
            o.is_prew_copy = false;
            o.is_fwd_copy = true;
            o.start_action = 0;
            o.endbup_action = 1;
            o.exit_action = 0;
            o.log_action = 0;

            //шифрование
            o.prefix = Netcode.Crypt.FileCrypt.default_prefix;
            o.pwd_file_enc = Netcode.Crypt.FileCrypt.default_key;
            o.key_size = (UInt16)Netcode.Crypt.FileCrypt.key_size.K256;
            o.pwd_namefile_enc = Netcode.Crypt.FileCrypt.default_key; ; 
        }

        public FormMain()
        {
            InitializeComponent();
            this.FormClosed+=new FormClosedEventHandler(FormMain_FormClosed);
            this.Shown += new EventHandler(FormMain_Shown);
            this.FormClosing += new FormClosingEventHandler(FormMain_FormClosing);
            folderTreeExploer.AddFolder += new FolderTreeExplorer.OnAddFolder(folderTreeExploer_AddFolder);
            folderTreeExploer.RemoveFolder += new FolderTreeExplorer.OnRemoveFolder(folderTreeExploer_RemoveFolder);
            toolStripButtonStart.Click += new EventHandler(toolStripButtonStart_Click);
            toolStripButtonPause.Click += new EventHandler(toolStripButtonPause_Click);
            toolStripButtonStop.Click += new EventHandler(toolStripButtonStop_Click);

            srch.FindFile += new Search.OnFindFile(srch_FindFile);
            srch.MakeError += new Search.OnMakeError(srch_MakeError);

            numericUpDownMin.Maximum = 100000000;
            numericUpDownMax.Maximum = 100000000;

            Application.DoEvents();

            информацияОФайлеToolStripMenuItem.Click += new EventHandler(информацияОФайлеToolStripMenuItem_Click);
            удалитьФайлToolStripMenuItem.Click += new EventHandler(удалитьФайлToolStripMenuItem_Click);
            очиститьToolStripMenuItem.Click += new EventHandler(очиститьToolStripMenuItem_Click);
            ОчиститьtoolStripMenuItem_trace.Click += new EventHandler(ОчиститьtoolStripMenuItem_trace_Click);
            listView_journal.DoubleClick += new EventHandler(информацияОФайлеToolStripMenuItem_Click);

            contextMenuStrip_journal.Opened += new EventHandler(contextMenuStrip_journal_Opened);
            contextMenuStrip_trace.Opened += new EventHandler(contextMenuStrip_trace_Opened);

            сохранитьНастройкиToolStripMenuItem.Click += new EventHandler(сохранитьНастройкиToolStripMenuItem_Click);
            сохранитьНастройкиКакToolStripMenuItem.Click += new EventHandler(сохранитьНастройкиКакToolStripMenuItem_Click);
            посмотретьВерсииФайловToolStripMenuItem.Click += new EventHandler(посмотретьВерсииФайловToolStripMenuItem_Click);
            button_run_viewer.Click += new EventHandler(button_run_viewer_Click);


        }

        void button_run_viewer_Click(object sender, EventArgs e)
        {
            RunViewer();
        }

        void посмотретьВерсииФайловToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunViewer();
        }

        private void RunViewer()
        {
            string viewer_file=Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ManageSetting.viewer_file);
            if (File.Exists(viewer_file))
            {
                try
                {
                    SaveSettToFile();
                    Process p = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo(viewer_file, "\"" + ManageSetting.path_to_set_file + "\"");
                    p.StartInfo = psi;
                    p.Start();
                }
                catch (Exception ex)
                {
                    ms.PrintError(ex, listView_trace);
                }
            }
            else
            {
                ms.PrintError("Не найден файл \"" + ManageSetting.viewer_file + "\" (" + viewer_file + ")", listView_trace);
            }
        }


        void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //по завершению программы
            switch (comboBox_exit_app.SelectedIndex)
            {
                case 0:
                    {
                        //ничего не делать
                    }
                    break;
                case 1:
                    {
                        //сохранить настройки
                        SaveSettToFile();
                    }
                    break;
                default:
                    break;
            }
        }

        void FormMain_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            try
            {
                //Загрузка конфигурации
                if (File.Exists(ManageSetting.path_to_set_file))
                {
                    ManageSetting.LoadSettings(ref o);
                }
                else
                {
                    SetDefaultVal();
                }
                LoadToControls();

                ms.write_lview_message("Инициализировано...", "Программа готова к работе", Color.GhostWhite, 5, listView_trace);
            }
            catch (Exception ex)
            {
                new CriticalErrors().PrintError("L2", ex.Message + " | " + ex.TargetSite);
                ms.PrintError("Программа работает некорректно, смотрите список критических ошибок", listView_trace);
            }
        }

        void сохранитьНастройкиКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveSettDialog.ShowDialog() == DialogResult.OK)
            {
                ManageSetting.path_to_set_file = saveSettDialog.FileName;//глоб.перем
                SaveSettToFile();
            }            
        }

        void сохранитьНастройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSettToFile();
        }

        private void SaveSettToFile()
        {
            SaveFromControls();
            if (ManageSetting.SaveSettings(o))
            {
                ms.write_lview_message("Файл настроек сохранен...", ManageSetting.path_to_set_file, Color.GhostWhite, 5, listView_trace);
            }
        }


        void srch_MakeError(string Error)
        {
            ms.PrintError(Error, listView_trace);
        }

        void my_MakeError(string Error)
        {
            ms.PrintError(Error, listView_trace);
        }

        /// <summary>
        /// Только эта функция копирует (бекапит) файлы
        /// </summary>
        /// <param name="fi">FileInfo</param>
        /// <param name="save_dir">Каталог для сохранения</param>
        /// <param name="indx">Индекс действия</param>
        /// <param name="isEncrypt">Шифровать ли файл</param>
        void CopyEx(FileInfo fi, string save_dir, int indx, bool isEncrypt, string prefix, string pwd_file_enc, UInt16 key_size, string pwd_namefile_enc)
        {
            string friendly_path = fi.DirectoryName.Replace(":", string.Empty);
            string dst_name_std = Path.Combine(friendly_path, fi.LastWriteTime.Ticks.ToString() + FileTreeExplorer.delimiter + fi.Name);//относительный путь до непошифрованного бекапленного файла
            string dst_full_path_std = Path.Combine(save_dir, dst_name_std);//абс. путь до непошифрованного бекапленного файла

            string dst_full_path_enc_std = Path.Combine(save_dir, Path.Combine(friendly_path, fi.LastWriteTime.Ticks.ToString() + FileTreeExplorer.delimiter + new FileCrypt().EncryptFName(fi.Name, pwd_namefile_enc, prefix)));//абс. путь до пошифрованного бекапленного файла, каким он должен быть

            Directory.CreateDirectory(Path.Combine(save_dir,friendly_path));
            switch (indx)
            {
                case 0://Сгенерировать новое имя
                    {
                        if (isEncrypt)
                        {
                            if (File.Exists(dst_full_path_enc_std))
                            {
                                using (FormAction frm_a = new FormAction(fi.FullName, Path.Combine(save_dir, friendly_path), string.Empty, DateTime.Now.Ticks.ToString(), true, prefix, pwd_file_enc, key_size,pwd_namefile_enc))
                                {
                                    frm_a.ShowDialog();
                                }
                            }
                            else
                            {
                                using (FormAction frm_a = new FormAction(fi.FullName, Path.Combine(save_dir, friendly_path), string.Empty, fi.LastWriteTime.Ticks.ToString(), true, prefix, pwd_file_enc, key_size, pwd_namefile_enc))
                                {
                                    frm_a.ShowDialog();
                                }
                            }
                        }
                        else
                        {
                            if (File.Exists(dst_full_path_std))
                            {
                                File.Copy(fi.FullName, Path.Combine(save_dir, Path.Combine(friendly_path, DateTime.Now.Ticks.ToString() + FileTreeExplorer.delimiter + fi.Name)), true);
                            }
                            else
                            {
                                File.Copy(fi.FullName, dst_full_path_std, true);
                            }
                        }
                    }
                    break;

                case 1://Заменить файл
                    {
                        if (isEncrypt)
                        {
                            using (FormAction frm_a = new FormAction(fi.FullName, Path.Combine(save_dir, friendly_path), string.Empty, fi.LastWriteTime.Ticks.ToString(), true, prefix, pwd_file_enc, key_size, pwd_namefile_enc))
                            {
                                frm_a.ShowDialog();
                            }
                        }
                        else
                        {
                            File.Copy(fi.FullName, dst_full_path_std, true);
                        }
                    }
                    break;

                default://ничего не делать
                    break;
            }
        }

        void WriteToLog(string text, string log_file)
        {
            int indx = (int)ms.get_ComboBox_property(comboBox_in_bup, "SelectedIndex");
            switch (indx)
            {
                case 0:
                    {
                    }
                    break;
                case 1:
                    {
                        using (StreamWriter sw = new StreamWriter(log_file, true, Encoding.UTF8))
                        {
                            sw.WriteLine(text);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        void srch_FindFile(FileInfo nm)
        {
            ms.set_tlabel_property(toolStripStatusLabelScanFile, new Messages().EyeFriendlyPath(nm.FullName), "Text");
            //string md5sum = md.Analysing(FileBytes);
            string save_dir = string.Empty;
            save_dir=(string)ms.get_TextBoxSelectFolder_property(textBoxSelectFolderSaveDir , "InputText");
            
            
            DateTime prev_dt = new DateTime(dateTimePicker_datePrev.Value.Year, dateTimePicker_datePrev.Value.Month, dateTimePicker_datePrev.Value.Day,
                dateTimePickerTimePrev.Value.Hour, dateTimePickerTimePrev.Value.Minute, dateTimePickerTimePrev.Value.Second);

            DateTime fwd_dt = new DateTime(dateTimePickerDateFwd.Value.Year, dateTimePickerDateFwd.Value.Month, dateTimePickerDateFwd.Value.Day,
    dateTimePickerTimeFwd.Value.Hour, dateTimePickerTimeFwd.Value.Minute, dateTimePickerTimeFwd.Value.Second);

            string date_last_write = nm.LastWriteTime.ToShortDateString() + " " + nm.LastWriteTime.ToShortTimeString();

            int selected = (int)ms.get_ComboBox_property(comboBox_action_backup, "SelectedIndex");//что делать, если файл существует
            bool isEncrypt = (bool)ms.get_CheckBox_property(checkBox_isEncrypt, "Checked");//шифровать?

            string prefix = (string)ms.get_TextBox_property(textBox_prefix, "Text");
            string pwd_file_enc = (string)ms.get_TextBox_property(textBox_pwd_file_enc, "Text");
            string key_size = (string)ms.get_ComboBox_property(comboBox_key_size, "Text");
            string pwd_namefile_enc = (string)ms.get_TextBox_property(textBox_pwd_namefile_enc, "Text");

            if (nm.LastWriteTime < prev_dt)
            {
                if ((bool)ms.get_ch_box_property(checkedListBox_prev, 0, "GetItemChecked"))
                {
                    CopyEx(nm, save_dir, selected, isEncrypt, prefix, pwd_file_enc, Convert.ToUInt16(key_size), pwd_namefile_enc);
                    ms.write_lview_ex_message(nm.Name, nm.FullName, "Скопирован (найден измененный раньше "+prev_dt.ToString()+")", date_last_write, listView_journal, Color.White, 5);

                    DateTime dt = DateTime.Now;
                    WriteToLog(dt.ToLongDateString() + "/" + dt.ToLongTimeString() + "\t" + nm.Name + "\t" + date_last_write + "\t" + nm.FullName + "\t" + "Скопирован (найден измененный раньше)", log_file);
                    count_copy_files++;
                }
            }

            if (nm.LastWriteTime > fwd_dt)
            {

                if ((bool)ms.get_ch_box_property(checkedListBox_fwd, 0, "GetItemChecked"))
                {
                    CopyEx(nm, save_dir, selected, isEncrypt, prefix, pwd_file_enc, Convert.ToUInt16(key_size), pwd_namefile_enc);
                    ms.write_lview_ex_message(nm.Name, nm.FullName, "Скопирован (найден измененный позже "+fwd_dt.ToString()+")", date_last_write, listView_journal, Color.White, 5);
                    
                    DateTime dt = DateTime.Now;
                    WriteToLog(dt.ToLongDateString() + "/" + dt.ToLongTimeString() + "\t" + nm.Name + "\t" + date_last_write + "\t" + nm.FullName + "\t" + "Скопирован (найден измененный позже)", log_file);
                    count_copy_files++;
                }
            }

            ++count_files;
            ms.set_tlabel_property(toolStripStatusLabelScanCnt, count_files.ToString(), "Text");

        }

        //Начать сканирование. Только здесь, в этой функции, условия учитываются и выполняются
        private void ScanStart(ScanningType st)
        {
            count_files = 0;
            count_copy_files = 0;
            ms.set_tbtn_property(toolStripButtonStart, false, "Enabled");
            ms.set_tbtn_property(toolStripButtonStart, "Идет сканирование", "ToolTipText");

            ms.set_tbtn_property(toolStripButtonPause, false, "Checked");
            ms.set_tbtn_property(toolStripButtonPause, "Приостановить сканирование", "ToolTipText");

            ms.set_tbtn_property(toolStripButtonStop, false, "Checked");


            ms.set_fte_property(folderTreeExploer, false, "Enabled");

            ms.set_num_ud_property(numericUpDownMax, false, "Enabled");
            ms.set_num_ud_property(numericUpDownMin, false, "Enabled");

            ms.set_tlabel_property(toolStripStatusLabelScanCnt, count_files.ToString(), "Text");

            switch (st)
            {
                case ScanningType.AddNewSigns:
                    {
                        //ms.write_lview_message("Сообщение", "Добавление новых сигнатур запущено", Color.GhostWhite, 1, listView_trace);

                        //ms.set_tbtn_property(toolStripButtonStart, false, "Checked");
                        //ms.set_tbtn_property(toolStripButtonPause, false, "Enabled");
                        //ms.set_tbtn_property(toolStripButtonStop, false, "Enabled");

                    }
                    break;
                case ScanningType.CheckSigns:
                    {
                        srch.FileSizeMax = (long)Math.Truncate((decimal)ms.get_num_up_down_property(numericUpDownMax, "Value"));
                        srch.FileSizeMin = (long)Math.Truncate((decimal)ms.get_num_up_down_property(numericUpDownMin, "Value"));

                        ms.write_lview_message("Сообщение", "Сканирование запущено", Color.GhostWhite, 1, listView_trace);

                        ms.set_tbtn_property(toolStripButtonStart, true, "Checked");
                        ms.set_tbtn_property(toolStripButtonPause, true, "Enabled");
                        ms.set_tbtn_property(toolStripButtonStop, true, "Enabled");

                        srch.ScanFiles(folderTreeExploer.SelectedFolders);
                        //tabControl.SelectedTab = tabPageFirst;
                    }
                    break;
                default:
                    break;
            }

            ms.set_tlabel_property(toolStripStatusLabelScanCnt, count_files.ToString(), "Text");
            ScanStop(st, true);
            ms.write_lview_message("Сканировано", "Файлов: " + count_files.ToString(), Color.DodgerBlue, 5, listView_trace);
            ms.write_lview_message("Скопировано", "Файлов: " + count_copy_files.ToString(), Color.DodgerBlue, 5, listView_trace);
        }

        private void ScanPause()
        {
            //Не используется.
            ms.write_lview_message("Сообщение", "Сканирование приостановлено", Color.GhostWhite, 2, listView_trace);
            //toolStripButtonStart.Checked = false;
            //toolStripButtonStart.Enabled = true;
            //toolStripButtonStart.ToolTipText = "Начать сканирование";

            //toolStripButtonPause.Checked = true;
            //toolStripButtonPause.Enabled = false;
            //toolStripButtonPause.ToolTipText = "Сканирование приостановлено";

            //toolStripButtonStop.Checked = false;
        }

        private void ScanStop(ScanningType st, bool self)
        {
            string end = string.Empty;
            if (self)
            {
                end = "завершено";
            }
            else
            {
                end = "остановлено пользователем";
            }

            switch (st)
            {
                case ScanningType.AddNewSigns:
                    {

                    }

                    break;
                case ScanningType.CheckSigns:
                    {
                        srch.ScanStop();
                        while (srch.IsScanning) { }
                        ms.write_lview_message("Сообщение", "Сканирование " + end, Color.GhostWhite, 3, listView_trace);
                    }
                    break;
                default:
                    break;
            }

            ms.set_tlabel_property(toolStripStatusLabelScanCnt, count_files.ToString(), "Text");
            ms.set_tbtn_property(toolStripButtonStart, false, "Checked");
            ms.set_tbtn_property(toolStripButtonStart, true, "Enabled");
            ms.set_tbtn_property(toolStripButtonStart, "Начать сканирование", "ToolTipText");

            ms.set_tbtn_property(toolStripButtonPause, false, "Checked");
            ms.set_tbtn_property(toolStripButtonPause, true, "Enabled");
            ms.set_tbtn_property(toolStripButtonPause, "Приостановить сканирование", "ToolTipText");

            ms.set_tbtn_property(toolStripButtonStop, false, "Checked");

            ms.set_fte_property(folderTreeExploer, true, "Enabled");

            ms.set_num_ud_property(numericUpDownMax, true, "Enabled");
            ms.set_num_ud_property(numericUpDownMin, true, "Enabled");

            ms.set_tlabel_property(toolStripStatusLabelScanFile, "(idle)", "Text");

            //что делать после бекапа
            switch ((int)ms.get_ComboBox_property(comboBox_set_dt, "SelectedIndex"))
            {
                case 0:
                    {
                        //ничего не делать
                    }
                    break;
                case 1:
                    {
                        //установить время
                        ms.set_dt_p_property(dateTimePicker_datePrev, DateTime.Now, "Value");//1
                        ms.set_dt_p_property(dateTimePickerTimePrev, DateTime.Now, "Value");//1

                        ms.set_dt_p_property(dateTimePickerDateFwd, DateTime.Now, "Value");//-1
                        ms.set_dt_p_property(dateTimePickerTimeFwd, DateTime.Now, "Value");//-1
                    }
                    break;
                //case 2:
                //    {
                //        //установить время и сохранить файл настроек
                //        ms.set_dt_p_property(dateTimePicker_datePrev, DateTime.Now.AddDays(1), "Value");
                //        ms.set_dt_p_property(dateTimePickerTimePrev, DateTime.Now.AddDays(1), "Value");

                //        ms.set_dt_p_property(dateTimePickerDateFwd, DateTime.Now.AddDays(-1), "Value");
                //        ms.set_dt_p_property(dateTimePickerTimeFwd, DateTime.Now.AddDays(-1), "Value");

                //        SaveSettToFile();
                //    }
                //    break;
                default:
                    break;
            }
        }

        delegate void FindFileDelegate(ScanningType nm);
        FindFileDelegate calcPi;

        /// <summary>
        /// Начтать поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            calcPi = new FindFileDelegate(ScanStart);
            calcPi.BeginInvoke(ScanningType.CheckSigns, null, null);
        }

        ///////////////////////////////////////////////////////////////////////

        void folderTreeExploer_RemoveFolder(object FolderName)
        {
            listView_trace.Items.RemoveByKey(FolderName.ToString());
        }

        void folderTreeExploer_AddFolder(object FolderName)
        {
            ms.write_lview_message("Проверить", FolderName.ToString(), Color.LightGreen, 0, listView_trace);
        }


        void ОчиститьtoolStripMenuItem_trace_Click(object sender, EventArgs e)
        {
            listView_trace.Items.Clear();
        }

        void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView_journal.Items.Clear();
        }

        void contextMenuStrip_journal_Opened(object sender, EventArgs e)
        {
            if (listView_journal.SelectedItems == null || listView_journal.SelectedItems.Count < 1)
            {
                информацияОФайлеToolStripMenuItem.Enabled = false;
                удалитьФайлToolStripMenuItem.Enabled = false;
                if (listView_journal.Items == null || listView_journal.Items.Count < 1)
                {
                    очиститьToolStripMenuItem.Enabled = false;
                }
                else
                {
                    очиститьToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                информацияОФайлеToolStripMenuItem.Enabled = true;
                очиститьToolStripMenuItem.Enabled = true;
                удалитьФайлToolStripMenuItem.Enabled = true;
            }
        }

        void contextMenuStrip_trace_Opened(object sender, EventArgs e)
        {
            if (listView_trace.SelectedItems == null || listView_trace.SelectedItems.Count < 1)
            {
                if (listView_trace.Items == null || listView_trace.Items.Count < 1)
                {
                    ОчиститьtoolStripMenuItem_trace.Enabled = false;
                }
                else
                {
                    ОчиститьtoolStripMenuItem_trace.Enabled = true;
                }
            }
            else
            {
                ОчиститьtoolStripMenuItem_trace.Enabled = true;
            }
        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            ScanPause();
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            ScanStop(ScanningType.CheckSigns, false);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        void удалитьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] prop = new string[2];
            if (listView_journal.SelectedItems.Count > 0)
            {
                prop = (string[])listView_journal.SelectedItems[0].Tag;
                if (MessageBox.Show("Вы действительно хотите удалить файл" + Environment.NewLine + Environment.NewLine + prop[1], "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(prop[1]);
                        listView_journal.SelectedItems[0].Remove();
                        ms.write_lview_message("Файл удален", prop[1], Color.White, 5, listView_trace);
                    }
                    catch (Exception ex)
                    {
                        ms.PrintError(ex, listView_trace);
                    }
                }
            }
        }

        void сгенерироватьСигнатуруToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        void принудительноДобавитьВБДToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        void информацияОФайлеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] prop = new string[2];
            if (listView_journal.SelectedItems.Count > 0)
            {
                prop = (string[])listView_journal.SelectedItems[0].Tag;
                try
                {
                    using (FormDetails frm_d = new FormDetails(prop))
                    {
                        frm_d.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    ms.PrintError(ex, listView_trace);
                }
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string AppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file.txt");

        //    Hashtable ht = new Hashtable(100000);

        //    for (int i = 0; i < 100000; i++)
        //    {
        //        string hash = string.Empty;
        //        for (int j = 0; j < 5; j++)
        //        {                    
        //            hash += new Random(i).Next(10000, 99999).ToString();
        //        }
        //        ht.Add(i+1, hash);
        //    }

            


        //    //XmlSerializer myXmlSer = new XmlSerializer(ht.GetType());
        //    using (FileStream fStream = File.Open(AppPath, FileMode.Create, FileAccess.Write))
        //    {
        //        //Rijndael rijndaelAlg = Rijndael.Create();
        //        //PasswordDeriveBytes pdb = new PasswordDeriveBytes(pwd_cr_file_sett, null); //класс, позволяющий генерировать ключи на базе паролей
        //        //pdb.HashName = "SHA512"; //будем использовать SHA512
        //        //byte[] iv = new Byte[rijndaelAlg.BlockSize >> 3];
        //        //byte[] key = pdb.GetBytes(rijndaelAlg.KeySize >> 3);

        //        //using (CryptoStream cStream = new CryptoStream(fStream,
        //        //  rijndaelAlg.CreateEncryptor(key, iv),
        //        //  CryptoStreamMode.Write))
        //        //using (StreamWriter sWriter = new StreamWriter(fStream))
        //        //{
        //        //    myXmlSer.Serialize(sWriter, ht);
        //        //    sWriter.Close();
        //        //}
        //        BinaryFormatter bf = new BinaryFormatter();
        //        bf.Serialize(fStream, ht);

        //    }

        //    //XmlSerializer myXmlSer = new XmlSerializer(o.GetType());
        //    //StreamWriter myWriter = new StreamWriter(path_to_set_file);
        //    //myXmlSer.Serialize(myWriter, o);
        //    //myWriter.Close();
        //}

//        private void button2_Click(object sender, EventArgs e)
//        {
//            string AppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file.txt");

//            using (FileStream fStream = File.Open(AppPath, FileMode.Open, FileAccess.Read))
//            {
//                BinaryFormatter outFormatter = new BinaryFormatter();
//                Hashtable ht_out = (Hashtable)outFormatter.Deserialize(fStream);
//                MessageBox.Show("OK");
//                if (ht_out.ContainsValue("4997749977499774997749977"))
//                {
//                    MessageBox.Show("1");
//                }
//            }
//            /*
//                  Dim E As IDictionaryEnumerator  
//                  E = T.GetEnumerator  
//                  Console.WriteLine(ControlChars.NewLine & Header)  
//                  While E.MoveNext()  
//                   Console.WriteLine(E.Key & "=" & E.Value)  
//                  End While  
                 
                 
//Hashtable ht = new Hashtable();
//ht.Add("key1", "value1");
//ht.Add("key2", 2);
////получаем результат
//String s1 = ht["key1"].ToString();
//int i1 = (int)ht["key2"];
//                */

//        }
    }
}