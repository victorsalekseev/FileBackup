using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using Netcode.Crypt;
using Netcode.Common.Calc;
using System.Text.RegularExpressions;

namespace Netcode.Common.Controls
{
    public partial class FileTreeExplorer : UserControl
    {
        Hashtable list_files = new Hashtable(20);
        public delegate void OnAddFile(object FileName);
        public event OnAddFile AddFile;

        public delegate void OnRemoveFile(object FileName);
        public event OnRemoveFile RemoveFile;

        public delegate void OnAfterSelect(object FileName);
        public event OnAfterSelect AfterSelect;

        public delegate void OnError(object Error);
        public event OnError Error;

        /// <summary>
        /// Длина TimeStamp
        /// </summary>
        int _len_ticks = DateTime.Now.Ticks.ToString().Length;
        /// <summary>
        /// разделитель TimeStamp и имени файла.
        /// </summary>
        public const string delimiter = "_";



        public FileTreeExplorer()
        {
            InitializeComponent();
            tV.CheckBoxes = true;

            tV.AfterCheck += new TreeViewEventHandler(tV_AfterCheck);
            tV.AfterSelect += new TreeViewEventHandler(tV_AfterSelect);
            tV.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(tV_NodeMouseDoubleClick);

            tV.ShowNodeToolTips = true;
            tV.HideSelection = false;
        }

        public void SetChecked(bool isChecked)
        {
            foreach (TreeNode tn in tV.Nodes)
            {
                tn.Checked = isChecked;
                foreach (TreeNode tn_c in tn.Nodes)
                {
                    tn_c.Checked = isChecked;
                }
            }
        }

        public Hashtable SelectedFiles
        {
            get { return list_files; }
        }

        /// <summary>
        /// Есть ли в имени файла TimeStamp
        /// </summary>
        /// <param name="_FileName">имя файла "как есть"</param>
        /// <returns></returns>
        public bool IsTimeStampFile(string _FileName)
        {
            bool ret = false;
            if (_FileName.Length > _len_ticks && Regex.IsMatch(_FileName, @"^\d{" + _len_ticks + "}_") && _FileName.Substring(_len_ticks, delimiter.Length) == delimiter)//Если есть TimeStamp
            {
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// Убирает из имени файла TimeStamp, не расшифровывая его
        /// </summary>
        /// <param name="FileName">имя файла "как есть"</param>
        /// <returns></returns>
        public string GetTrueFileName(string _FileName)
        {
            if (IsTimeStampFile(_FileName))
            {
                return _FileName.Substring(_len_ticks + delimiter.Length);//исходное имя файла
            }
            else
            {
                return _FileName;
            }
        }

        /// <summary>
        /// Узнает, зашифровано ли имя файла
        /// </summary>
        /// <param name="_FileName">имя файла "как есть"</param>
        /// <param name="_prefix">префикс шифрованного имени</param>
        /// <returns></returns>
        public bool IsCryptFile(string _FileName, string _prefix)
        {
            bool ret = false;
            string is_crypt = _prefix + ".";
            if ((_FileName.Length > _len_ticks + is_crypt.Length + delimiter.Length) && Regex.IsMatch(_FileName, @"^\d{" + _len_ticks + "}_") && (_FileName.Substring(_len_ticks, is_crypt.Length + delimiter.Length) == delimiter + is_crypt))
            {
                ret = true;//Если есть TimeStamp и признаки зашированного файла
            }

            if (_FileName.Substring(0, is_crypt.Length) == is_crypt)
            {
                ret = true;//Если нет TimeStamp и есть признаки зашированного файла
            }

            return ret;
        }

        public void PrintFiles(string folder, string _prefix, string _pwd_namefile_enc)
        {
            try
            {
                tV.Nodes.Clear();
                FileInfo[] afi = new DirectoryInfo(folder).GetFiles("*", SearchOption.TopDirectoryOnly);

                foreach (FileInfo fi in afi)
                {
                    DateTime dt;
                    string date_stamp = string.Empty;
                    string TrueFileName = fi.Name;
                    string FileName = fi.Name;

                    if (IsTimeStampFile(FileName))//Если есть TimeStamp
                    {
                        TrueFileName = GetTrueFileName(FileName);//исходное имя файла
                        dt = new DateTime(Convert.ToInt64(FileName.Remove(_len_ticks)));
                    }
                    else
                    {
                        dt = fi.LastWriteTime;
                    }
                    date_stamp = " :: " + dt.ToShortDateString() + " " + dt.ToShortTimeString();

                    //признак зашифрованнго файла

                    if (IsCryptFile(FileName, _prefix))
                    {
                        TrueFileName = new FileCrypt().DecryptFName(TrueFileName, _pwd_namefile_enc, _prefix);//без разбора пытаемся его расшифровать
                        TrueFileName += FileCrypt.crypted_mess;
                    }

                    string node_key = TrueFileName.Replace(FileCrypt.crypted_mess, string.Empty);

                    TreeNode tn = new TreeNode();
                    tn.Name = node_key;
                    tn.Text = TrueFileName + date_stamp;
                    tn.Tag = fi.FullName;
                    tn.ToolTipText = FileSize.PrintFileSize(fi.Length);

                    if (tV.Nodes.ContainsKey(node_key))//или в подноду, если имя совпало
                    {
                        TreeNode tn_child = new TreeNode();
                        tn_child.Name = node_key;
                        tn_child.Text = TrueFileName + date_stamp;
                        tn_child.Tag = fi.FullName;
                        tn_child.ToolTipText = FileSize.PrintFileSize(fi.Length);

                        if (list_files.ContainsKey(fi.FullName))//выставляем чекбоксы, если есть в хеш-таблице
                        {
                            tn_child.Checked = true;
                        }
                        tV.Nodes[node_key].Nodes.Add(tn_child);
                        Application.DoEvents();
                    }
                    else
                    {
                        if (list_files.ContainsKey(fi.FullName))//выставляем чекбоксы, если есть в хеш-таблице
                        {
                            tn.Checked = true;
                        }
                        tV.Nodes.Add(tn);//или в основную ноду
                        Application.DoEvents();
                    }
                }
            }
            catch (Exception ex)
            {
                if (Error != null)
                {
                    Error.Invoke(ex.Message + " | " + ex.TargetSite);
                }
            }
        }

        void tV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (AfterSelect != null)
            {
                AfterSelect.Invoke(e.Node.Tag);
            }
        }

        void tV_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes != null && e.Node.Nodes.Count==0)
            {
                if (e.Node.Checked)
                {
                    e.Node.Checked = false;
                }
                else
                {
                    e.Node.Checked = true;
                }
            }
        }

        void tV_AfterCheck(object sender, TreeViewEventArgs e)
        {
            SelectFile(e.Node);
        }

        /// <summary>
        /// Добавление/удаление директории
        /// </summary>
        /// <param name="node">Нода дерева. В Tag - полный путь до директории. В Checked - выбрана до нажатия/нет</param>
        void SelectFile(TreeNode node)
        {
            Managing_list_Files(node, false);
        }


        public void Managing_list_Files(TreeNode node, bool ClsChildNodes)
        {
            //должны быть ключи - именами, а значения - путями :)
            if (node.Checked)
            {
                if (!list_files.ContainsKey(node.Tag))
                {
                    list_files.Add(node.Tag, node.Tag);

                    if (AddFile != null)
                    {
                        AddFile.Invoke(node.Tag);
                    }
                }

                Hashtable to_remove = new Hashtable(20);

                foreach (object path in list_files.Keys)
                {
                    string input = string.Empty;
                    string pattern = string.Empty;

                    if (path.ToString().Length > node.Tag.ToString().Length)
                    {
                        input = path.ToString();
                        pattern = node.Tag.ToString();
                    }
                    else
                    {
                        input = node.Tag.ToString();
                        pattern = path.ToString();
                    }

                    bool cmp = input.Contains(pattern);//если выбранная папка ближе к корню, чем ранее выбранная
                    if ((cmp) &&//так вот если она есть
                        (path.ToString() != node.Tag.ToString()) &&//и не равна самой себе
                        (path.ToString().Split(Convert.ToChar("\\")).Length != node.Tag.ToString().Split(Convert.ToChar("\\")).Length) //и их уровни вложенности разные

                        )
                    {
                        if (!to_remove.ContainsKey((object)input))
                        {
                            to_remove.Add((object)input, (object)input);
                        }
                        if (RemoveFile != null)
                        {
                            RemoveFile.Invoke(path);
                        }
                    }
                }

                foreach (object path in to_remove.Keys)
                {
                    list_files.Remove((object)path);
                }

                if (ClsChildNodes)
                {
                    node.Nodes.Clear();
                }
                //MessageBox.Show(list_files.Count.ToString() + "/" + to_remove.Count.ToString());
            }
            else
            {
                list_files.Remove(node.Tag);
                if (RemoveFile != null)
                {
                    RemoveFile.Invoke(node.Tag);
                }
            }
        }


    }
}
