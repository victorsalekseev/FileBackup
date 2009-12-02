using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace Netcode.Common.Settings
{
    //структура для хранения сохраняемой в файл инфы
    public struct Options
    {
        /// <summary>
        /// директория сохранения бекапа
        /// </summary>
        public string save_dir;
        /// <summary>
        /// директории для бекапа
        /// </summary>
        public string bup_dirs;
        /// <summary>
        /// мин. размер файла для бекапа
        /// </summary>
        public UInt64 min_filesize;
        /// <summary>
        /// макс. размер файла для бекапа
        /// </summary>
        public UInt64 max_filesize;
        /// <summary>
        /// если целевой файл существует, то 0-сгерер нов имя, 1 - заменить, 2 - ничего
        /// </summary>
        public UInt16 if_fexists;
        /// <summary>
        /// шифрование файлов
        /// </summary>
        public bool is_encrypt;
        /// <summary>
        /// время ДО
        /// </summary>
        public DateTime prev_datetime;
        /// <summary>
        /// время ПОСЛЕ
        /// </summary>
        public DateTime fwd_datetime;
        /// <summary>
        /// если найден ДО, то копировать 0-нет, 1-да
        /// </summary>
        public bool is_prew_copy;
        /// <summary>
        /// если найден ПОСЛЕ, то копировать 0-нет, 1-да
        /// </summary>
        public bool is_fwd_copy;
        /// <summary>
        /// при запуске начать автоматически резервное копирование 0-нет, 1-да
        /// </summary>
        public UInt16 start_action;
        /// <summary>
        /// по завершению проверки автоматически установить время 0-нет, 1-да
        /// </summary>
        public UInt16 endbup_action;
        /// <summary>
        /// при выходе из программы сохранить файл настроек 0-нет, 1-да
        /// </summary>
        public UInt16 exit_action;
        /// <summary>
        /// при проверке вести лог 0-нет, 1-да
        /// </summary>
        public UInt16 log_action;

        //Ниже относится к шифрованию файлов
        /// <summary>
        /// Префикс "0." шифруемых файлов
        /// </summary>
        public string prefix;
        /// <summary>
        /// Ключ-пароль для шифрования файлов
        /// </summary>
        public string pwd_file_enc; 
        /// <summary>
        /// Длина ключа шифрования файлов (256 192 128)
        /// </summary>
        public UInt16 key_size;
        /// <summary>
        /// Ключ для шифрования имен файлов
        /// </summary>
        public string pwd_namefile_enc; 
    }

    public class ManageSetting
    {
        //Лишаем возможности создавать объекты этого класса
        private ManageSetting()
        {

        }
        public static string set_file = "Default.cfg";
        public static string path_to_set_file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, set_file);
        public static string viewer_file = "Netcode.Viewer.exe";

        public static bool SaveSettings(object o)
        {
            bool res = false;
            try
            {

                XmlSerializer myXmlSer = new XmlSerializer(o.GetType());
                using (StreamWriter sWriter = new StreamWriter(path_to_set_file))
                {
                    myXmlSer.Serialize(sWriter, o);
                    sWriter.Close();
                }
                res = true;
            }
            catch (Exception ex)
            {
                new CriticalErrors().PrintError("S1", ex.Message + " | " + ex.TargetSite);
                res = false;
            }

            return res;
        }

        /// <summary>
        /// Преобразовываем путь диска, если он X: в X:\
        /// </summary>
        /// <param name="path">Диск</param>
        /// <returns></returns>
        public static string FixDrivePath(string path)
        {
            if (path.Length == 2 && path[1]==Convert.ToChar(":"))
            {
                path += "\\";
            }
            return path;
        }

        public static bool LoadSettings(ref Options o)
        {
            bool res = false;
            try
            {
                XmlSerializer myXmlSer = new XmlSerializer(typeof(Options));
                using (FileStream ms = new FileStream(path_to_set_file, FileMode.Open))
                {
                    o = (Options)myXmlSer.Deserialize(ms);
                    ms.Close();
                }

                res = true;
            }
            catch (Exception ex)
            {
                new CriticalErrors().PrintError("L1", ex.Message + " | " + ex.TargetSite);
                res = false;
            }
            return res;
        }
    }
}
