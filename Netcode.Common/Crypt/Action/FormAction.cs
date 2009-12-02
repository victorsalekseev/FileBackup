using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using Netcode.Common.Settings;
using Netcode.Crypt;
using Netcode.Common.Controls;
using Netcode.Common;

namespace Necode.Common.Action
{
    public partial class FormAction : Form
    {
        public FormAction()
        {
            InitializeComponent();
        }

        string _src_file_path = string.Empty;
        string _dst_path = string.Empty;
        string _dst_file_name = string.Empty;
        string _prefix = FileCrypt.default_prefix;
        string _pwd_file_enc = FileCrypt.default_key;
        UInt16 _key_size = (UInt16)FileCrypt.key_size.K256;
        string _pwd_namefile_enc = FileCrypt.default_key;
        string _time_stamp = string.Empty;

        bool _IsEncrypt = true;

        /// <summary>
        /// Конструктор формы де/шифрования файла. Предпреждений перезаписи нет!
        /// </summary>
        /// <param name="src_file_path">Исходный файл</param>
        /// <param name="dst_path">Папку, в которую надо сохранять</param>
        /// <param name="dst_file_name">Имя файла, с которым его следует сохранить (работает только при расшифровании)</param>
        /// <param name="time_stamp">Штамп времени в тиках, не шифруется, добавляется в начало имени файла</param>
        /// <param name="IsEncrypt">Шифрация ли</param>
        /// <param name="prefix">Префикс "0." шифруемых файлов</param>
        /// <param name="pwd_file_enc">Ключ-пароль для шифрования файлов</param>
        /// <param name="key_size">Длина ключа шифрования файлов (256 192 128)</param>
        /// <param name="pwd_namefile_enc">Ключ для шифрования имен файлов</param>
        public FormAction(string src_file_path, string dst_path, string dst_file_name, string time_stamp, bool IsEncrypt, string prefix, string pwd_file_enc, UInt16 key_size, string pwd_namefile_enc)
        {
            FileCrypt cr = new FileCrypt();
            InitializeComponent();

            dst_path = ManageSetting.FixDrivePath(dst_path);

            _src_file_path = src_file_path;
            _dst_path = dst_path;
            _dst_file_name = dst_file_name;
            _IsEncrypt = IsEncrypt;

            _prefix = prefix;
            _pwd_file_enc = pwd_file_enc;
            _key_size = key_size;
            _pwd_namefile_enc = pwd_namefile_enc;
            if (!string.IsNullOrEmpty(time_stamp))
            {
                _time_stamp = time_stamp + FileTreeExplorer.delimiter;
            }
            else
            {
                _time_stamp = string.Empty;
            }
        }

        private void FormAction_Shown(object sender, EventArgs e)
        {
            //Заокмментировано, потому что проверка пароля для входа не используется пока
            //if (PrefSettings.pwd == null)
            //{
            //    FormPwd fp = new FormPwd();
            //    if (fp.ShowDialog() == DialogResult.OK)
            //    {
            //        FileTransfer(_src_file_path, _dst_path, _IsEncrypt);
            //    }
            //}
            //else
            //{
            FileTransfer(_src_file_path, _dst_path, _IsEncrypt);
            //}
        }

        private void FileTransfer(string src_file_path, string dst_path, bool isEncrypt)
        {
            Application.DoEvents();
            FileCrypt cr = new FileCrypt();
            string dst_file_path;
            string _enc_file_name = string.Empty;
            string _dec_file_name = string.Empty;
            textBox_src_file.Text = src_file_path;

            try
            {
                if (isEncrypt)
                {
                    this.Text = "Шифрование";
                    listBox_log.Items.Add("Шифрование начато...");
                    //EncryptFName - вызов из флат-эксплорера по хорошему
                    //Здесь результативное имя файла всегда постоянное
                    _enc_file_name = cr.EncryptFName(Path.GetFileName(src_file_path), _pwd_namefile_enc, _prefix);
                    dst_file_path = Path.Combine(dst_path, _time_stamp+_enc_file_name);
                    textBox_dst_file.Text = dst_file_path;

                    if (dst_path.Length < 3)
                    {
                        new CriticalErrors().PrintError("S2", "В эту папку сохранять нельзя"); 
                    }
                    else
                    {
                        cr.crypt_file(true, _pwd_file_enc, _key_size, src_file_path, dst_file_path, progressBar_crypt);
                    }
                }
                else
                {
                    this.Text = "Дешифрование";
                    listBox_log.Items.Add("Дешифрование начато...");
                    _dec_file_name = _dst_file_name;
                    if (string.IsNullOrEmpty(_dst_file_name))
                    {
                        _dec_file_name = cr.DecryptFName(Path.GetFileName(src_file_path), _pwd_namefile_enc, _prefix);
                    }
                    dst_file_path = Path.Combine(dst_path, _dec_file_name);
                    textBox_dst_file.Text = dst_file_path;

                    if (dst_path.Length < 3)
                    {
                        new CriticalErrors().PrintError("S2", "В эту папку сохранять нельзя"); 
                    }
                    else
                    {
                         cr.crypt_file(false, _pwd_file_enc, _key_size, src_file_path, dst_file_path, progressBar_crypt);
                    }                    
                }
                listBox_log.Items.Add("OK");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                new CriticalErrors().PrintError("E1", ex.Message + " | " + ex.TargetSite); 
                listBox_log.Items.Add(ex.Message);
                listBox_log.Items.Add("ERROR...");
            }
            finally
            {
                button_ok.Enabled = true;
            }
        }



        private void button_ok_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void FormAction_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!button_ok.Enabled)
            //{

            //}
        }

    }
}