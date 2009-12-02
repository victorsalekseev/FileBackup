using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Netcode.Common;
using Netcode.Common.Search;
using System.Collections;
using System.IO;
using Netcode.Crypt;
using Necode.Common.Action;
using Netcode.Common.Settings;

namespace Netcode.Viewer
{
    public partial class FormVersionExplorer : Form
    {
        Messages ms = new Messages();
        Search srch_in_fldr = new Search();//������������ ��� ������ ������ �����������
        //���������� ���������� ������ � ���� ������ ��������!!!!!!!!!!!!!!!!!
        Options o = new Options();

        string _pwd_namefile_enc=FileCrypt.default_key;
        string _prefix = FileCrypt.default_prefix;
        string _pwd_file_enc=FileCrypt.default_key;
        UInt16 _key_size = (UInt16)FileCrypt.key_size.K256;
        string _recover_dir = string.Empty;


        public FormVersionExplorer()
        {
            InitializeComponent();
            Init();
            try
            {
                ms.write_lview_message("����������������...", "��������� ������ � ������", Color.GhostWhite, 5, listView_trace);
                //�������� ������������
                if (File.Exists(ManageSetting.path_to_set_file))
                {
                    if (!ManageSetting.LoadSettings(ref o))
                    {
                        ms.PrintError("��������� �������� �����������, �������� ������ ����������� ������", listView_trace);
                    }
                }
                else
                {
                    SetDefaultVal();
                }
                LoadToControls();//�������� ���� ��������� �� ������������� ������������ �������

            }
            catch (Exception ex)
            {
                new CriticalErrors().PrintError("L2", ex.Message + " | " + ex.TargetSite);
                ms.PrintError("��������� �������� �����������, �������� ������ ����������� ������", listView_trace);
            }
        }

        private void Init()
        {
            Shown += new EventHandler(FormVersionExplorer_Shown);
            FormClosed += new FormClosedEventHandler(FormVersionExplorer_FormClosed);
            folderTreeExplorer.AfterSelect += new Netcode.Controls.FolderTreeExplorer.OnAfterSelect(folderTreeExplorer_AfterSelect);
            folderTreeExplorer.AddFolder += new Netcode.Controls.FolderTreeExplorer.OnAddFolder(folderTreeExplorer_AddFolder);
            folderTreeExplorer.RemoveFolder += new Netcode.Controls.FolderTreeExplorer.OnRemoveFolder(folderTreeExplorer_RemoveFolder);
            srch_in_fldr.FileSizeMax = long.MaxValue;
            srch_in_fldr.FileSizeMin = 0;
            srch_in_fldr.FindFile += new Search.OnFindFile(srch_in_fldr_FindFile);
            srch_in_fldr.MakeError += new Search.OnMakeError(srch_in_fldr_MakeError);

            fileTreeExplorer.AfterSelect += new Netcode.Common.Controls.FileTreeExplorer.OnAfterSelect(fileTreeExplorer_AfterSelect);
            fileTreeExplorer.Error += new Netcode.Common.Controls.FileTreeExplorer.OnError(fileTreeExplorer_Error);
            fileTreeExplorer.AddFile += new Netcode.Common.Controls.FileTreeExplorer.OnAddFile(fileTreeExplorer_AddFile);
            fileTreeExplorer.RemoveFile += new Netcode.Common.Controls.FileTreeExplorer.OnRemoveFile(fileTreeExplorer_RemoveFile);
            toolStripSplitButtonRecover.DropDownOpening += new EventHandler(toolStripSplitButtonRecover_DropDownOpening);
            toolStripSplitButtonRecover.Click += new EventHandler(toolStripSplitButtonRecover_Click);
            ���ToolStripMenuItem.Click += new EventHandler(���ToolStripMenuItem_Click);
            �����ToolStripMenuItem.Click += new EventHandler(�����ToolStripMenuItem_Click);
            �����ToolStripMenuItem.Click += new EventHandler(�����ToolStripMenuItem_Click);
            ���ToolStripMenuItemCh.Click += new EventHandler(���ToolStripMenuItemCh_Click);
            �����ToolStripMenuItemCh.Click += new EventHandler(�����ToolStripMenuItemCh_Click);
            �����ToolStripMenuItemCh.Click += new EventHandler(�����ToolStripMenuItemCh_Click);
            toolStripButton_checked.Click += new EventHandler(toolStripButton_checked_Click);
            toolStripComboBox_MethodRec.SelectedIndex = 0;
            toolStripSplitButtonRecover.Enabled = false;
        }

        void FormVersionExplorer_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (Directory.Exists(o.save_dir))
            {
                try
                {
                    folderTreeExplorer.ExpandDir(o.save_dir);
                    fileTreeExplorer.PrintFiles(o.save_dir, _prefix, _pwd_namefile_enc);
                    SetCurrentPath(o.save_dir);
                }
                catch(Exception ex)
                {
                    ms.PrintError(ex, listView_trace);
                }
            }
        }

        private void LoadToControls()
        {
            //folderTreeExplorer.ExpandDir(o.save_dir);

            //����������
            if (string.IsNullOrEmpty(o.prefix))
            {
                _prefix = FileCrypt.default_prefix;
            }
            else
            {
                _prefix = o.prefix;
            }
            if (string.IsNullOrEmpty(o.pwd_file_enc))
            {
                _pwd_file_enc = FileCrypt.default_key;
            }
            else
            {
                _pwd_file_enc = o.pwd_file_enc;
            }
            if (string.IsNullOrEmpty(o.key_size.ToString()))
            {
                _key_size=(UInt16)FileCrypt.key_size.K256;
            }
            else
            {
                _key_size = (UInt16)o.key_size;
            }
            if (string.IsNullOrEmpty(o.pwd_namefile_enc))
            {
                _pwd_namefile_enc = FileCrypt.default_key;
            }
            else
            {
                _pwd_namefile_enc = o.pwd_namefile_enc;
            }
        }

        private void SetDefaultVal()
        {
            o.save_dir = AppDomain.CurrentDomain.BaseDirectory;

            //����������
            o.prefix = Netcode.Crypt.FileCrypt.default_prefix;
            o.pwd_file_enc = Netcode.Crypt.FileCrypt.default_key;
            o.key_size = (UInt16)Netcode.Crypt.FileCrypt.key_size.K256;
            o.pwd_namefile_enc = Netcode.Crypt.FileCrypt.default_key; ;
        }

        //����� 3 ������� ��������� � ���������
        void �����ToolStripMenuItemCh_Click(object sender, EventArgs e)
        {
        }

        void �����ToolStripMenuItemCh_Click(object sender, EventArgs e)
        {
            fileTreeExplorer.SetChecked(false);
        }

        void ���ToolStripMenuItemCh_Click(object sender, EventArgs e)
        {
             fileTreeExplorer.SetChecked(false);
        }

        //����� 3 ������� ��������� � ��������������
        void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckRecoverDir();
            DisableControls();
            srch_in_fldr.ScanFiles(folderTreeExplorer.SelectedFolders);
            EnableControls();
            EndOfRecovery();
        }

        void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckRecoverDir();
            DisableControls();
            RecoverOnlyFiles(fileTreeExplorer.SelectedFiles, toolStripComboBox_MethodRec.SelectedIndex);
            EnableControls();
            EndOfRecovery();
        }
        
        void ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckRecoverDir();
            DisableControls();
            RecoverOnlyFiles(fileTreeExplorer.SelectedFiles, toolStripComboBox_MethodRec.SelectedIndex);
            srch_in_fldr.ScanFiles(folderTreeExplorer.SelectedFolders);
            EnableControls();
            EndOfRecovery();
        }

        private void EndOfRecovery()
        {
            ms.write_lview_message("����������� ���������", "����� �������������", Color.DodgerBlue, 3, listView_trace);
        }

        void toolStripButton_checked_Click(object sender, EventArgs e)
        {
            toolStripButton_checked.DropDown.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void EnableControls()
        {
            textBoxSelectFolderRecTo.Enabled = true;
            toolStripSplitButtonRecover.Enabled = true;
            toolStripButton_checked.Enabled = true;
            folderTreeExplorer.Enabled = true;
            fileTreeExplorer.Enabled = true;
        }

        private void DisableControls()
        {
            textBoxSelectFolderRecTo.Enabled = false;
            toolStripSplitButtonRecover.Enabled = false;
            toolStripButton_checked.Enabled = false;
            folderTreeExplorer.Enabled = false;
            fileTreeExplorer.Enabled = false;
        }

        /// <summary>
        /// �������� ������� ����� ��� ����������
        /// </summary>
        private void CheckRecoverDir()
        {
            if (!Directory.Exists(textBoxSelectFolderRecTo.InputText))
            {
                if (folderBrowserDialog_recover_dir.ShowDialog() == DialogResult.OK)
                {
                    _recover_dir = folderBrowserDialog_recover_dir.SelectedPath;
                    textBoxSelectFolderRecTo.InputText = _recover_dir;
                }
            }
            else
            {
                _recover_dir = textBoxSelectFolderRecTo.InputText;
            }
        }

        /// <summary>
        /// �������������� ���������� ������
        /// </summary>
        /// <param name="files">������ ������</param>
        /// <param name="indx_action">0: ���� � �����...</param>
        void RecoverOnlyFiles(Hashtable files, int indx_action)
        {
            if (Directory.Exists(_recover_dir))
            {
                switch (toolStripComboBox_MethodRec.SelectedIndex)
                {
                    case 0://���� � �����...
                        {
                            foreach (string file in files.Keys)
                            {
                                RecoverFile(file, _recover_dir);
                            }
                        }
                        break;

                    case 1://� ����������� �������������� ���� �����
                        {

                        }
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// �������������� �����
        /// </summary>
        /// <param name="file">��� src-�����, _recover_dir ������ ���� �����������</param>
        private void RecoverFile(string file, string _rec_dir)
        {
            try
            {
                string decrypt_name = fileTreeExplorer.GetTrueFileName(Path.GetFileName(file));//���� ������� ��� �����
                if (fileTreeExplorer.IsCryptFile(decrypt_name, _prefix))
                {
                    decrypt_name = new FileCrypt().DecryptFName(decrypt_name, _pwd_namefile_enc, _prefix);
                    //�������������� ���� � �������� � _recover_dir
                    using (FormAction frm_a = new FormAction(file, _rec_dir, decrypt_name, string.Empty, false, _prefix, _pwd_file_enc, _key_size, _pwd_namefile_enc))
                    {
                        frm_a.ShowDialog();
                    }
                }
                else
                {
                    //���������� ����� �������� � _recover_dir
                    File.Copy(file, Path.Combine(_rec_dir, decrypt_name), true);
                }
                ms.write_lview_message("���� ��������", Path.Combine(_rec_dir, decrypt_name), Color.GhostWhite, 0, listView_trace);
            }
            catch (Exception ex)
            {
                ms.PrintError(ex, listView_trace);
            }
        }
        
        void srch_in_fldr_FindFile(System.IO.FileInfo nm)
        {
            string full_dir = Path.GetFullPath(nm.FullName);
            Hashtable _tmp = folderTreeExplorer.SelectedFolders;
            foreach (string fldr in _tmp.Keys)
            {
                if (full_dir.Substring(0, fldr.Length) == fldr)
                {
                    string _reltmp = ManageSetting.FixDrivePath(fldr.Remove(fldr.LastIndexOf("\\")));
                    string rel_dir = full_dir.Remove(0, _reltmp.Length).Substring(1);//�� ��������� ��������� �����
                    string abs_full_name = Path.Combine(_recover_dir, rel_dir);
                    string abs_dir = Path.GetDirectoryName(abs_full_name);

                    if (!Directory.Exists(abs_dir))
                    {
                        Directory.CreateDirectory(abs_dir);
                    }
                    RecoverFile(nm.FullName, abs_dir);
                }
            }
        }

        void toolStripSplitButtonRecover_Click(object sender, EventArgs e)
        {
            toolStripSplitButtonRecover.DropDown.Show(Cursor.Position.X, Cursor.Position.Y);
            toolStripSplitButtonRecover_DropDownOpening(this, null);
        }

        void toolStripSplitButtonRecover_DropDownOpening(object sender, EventArgs e)
        {
            if ((folderTreeExplorer.SelectedFolders.Count < 1) && (fileTreeExplorer.SelectedFiles.Count < 1))
            {
                �����ToolStripMenuItem.Enabled = false;
                �����ToolStripMenuItem.Enabled = false;
                ���ToolStripMenuItem.Enabled = false;
            }
            else
            {
                ���ToolStripMenuItem.Enabled = true;
                if (folderTreeExplorer.SelectedFolders.Count < 1)
                {
                    �����ToolStripMenuItem.Enabled = false;
                }
                else
                {
                    �����ToolStripMenuItem.Enabled = true;
                }

                if (fileTreeExplorer.SelectedFiles.Count < 1)
                {
                    �����ToolStripMenuItem.Enabled = false;
                }
                else
                {
                    �����ToolStripMenuItem.Enabled = true;
                }
            }
        }

        void CheckSelectedItems()
        {
            if ((folderTreeExplorer.SelectedFolders.Count < 1) && (fileTreeExplorer.SelectedFiles.Count < 1))
            {
                toolStripSplitButtonRecover.Enabled = false;
            }
            else
            {
                toolStripSplitButtonRecover.Enabled = true;
            }
        }

        void folderTreeExplorer_RemoveFolder(object FolderName)
        {
            listView_trace.Items.RemoveByKey(FolderName.ToString());
            CheckSelectedItems();
        }

        void folderTreeExplorer_AddFolder(object FolderName)
        {
            ms.write_lview_message("�������", FolderName.ToString(), Color.LightGreen, 2, listView_trace);
            CheckSelectedItems();
        }

        void fileTreeExplorer_RemoveFile(object FileName)
        {
            listView_trace.Items.RemoveByKey(FileName.ToString());
            CheckSelectedItems();
        }

        void fileTreeExplorer_AddFile(object FileName)
        {
            ms.write_lview_message("�������", FileName.ToString(), Color.LightGreen, 2, listView_trace);
            CheckSelectedItems();
        }

        void folderTreeExplorer_AfterSelect(object FolderName)
        {
            fileTreeExplorer.PrintFiles(FolderName.ToString(), _prefix, _pwd_namefile_enc);
            SetCurrentPath(FolderName.ToString());
        }

        private void SetCurrentPath(string folderName)
        {
            textBox_path.Text = folderName.Replace(Path.GetPathRoot(folderName), Path.GetPathRoot(folderName).ToUpper());
        }

        void fileTreeExplorer_AfterSelect(object FileName)
        {
            //ms.write_lview_message("����", FileName.ToString(), Color.GhostWhite, 1, listView_trace);
        }

        void fileTreeExplorer_Error(object Error)
        {
            ms.PrintError(Error.ToString(), listView_trace);
        }

        void srch_in_fldr_MakeError(string Error)
        {
            ms.PrintError(Error, listView_trace);
        }

        void FormVersionExplorer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}