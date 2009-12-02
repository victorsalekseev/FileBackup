using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;

namespace Netcode.Common.Search
{
    /// <summary>
    /// Класс поиска файлов
    /// </summary>
    public class Search
    {
        public Search()
        {
        }

        public delegate void OnFindFile(FileInfo nm);
        /// <summary>
        /// Вызывается при нахождении файла
        /// </summary>
        public event OnFindFile FindFile;

        public delegate void OnMakeError(string Error);
        /// <summary>
        /// Вызывается при ошибке
        /// </summary>
        public event OnMakeError MakeError;

        private Int64 _file_size_min = 0;
        /// <summary>
        /// Минимальный размер файла для поиска
        /// </summary>
        public System.Int64 FileSizeMin
        {
            get { return _file_size_min; }
            set { _file_size_min = value; }
        }

        private Int64 _file_size_max = Int64.MaxValue;
        /// <summary>
        /// Максимальный размер файла для поиска
        /// </summary>
        public System.Int64 FileSizeMax
        {
            get { return _file_size_max; }
            set { _file_size_max=value; }
        }

        private bool _is_scanning = false;
        /// <summary>
        /// Идет ли сканирование
        /// </summary>
        public bool IsScanning
        {
            get { return _is_scanning; }
        }

        private void ExtractDirInfo(DirectoryInfo root_dir)
        {
            DirectoryInfo[] dirs = root_dir.GetDirectories("*", SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo dirx in dirs)
            {
                try
                {
                    FileInfo[] fi = dirx.GetFiles("*", SearchOption.TopDirectoryOnly);
                    ExtractFileInfo(fi);
                    ExtractDirInfo(dirx);
                }
                catch (Exception ex)
                {
                    if (MakeError != null)
                    {
                        MakeError.Invoke(ex.Message + " | " + ex.TargetSite);
                    }
                }
            }
        }

        private void ExtractFileInfo(FileInfo[] fi)
        {
            if (!_is_scanning)
            {
                return;
            }
            foreach (FileInfo nm in fi)
            {
                if (!_is_scanning)
                {
                    return;
                }
                try
                {
                    if (nm.Length >= _file_size_min && nm.Length <= _file_size_max)
                    {
                        if (FindFile != null)
                        {
                            FindFile.Invoke(nm);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (MakeError != null)
                    {
                        MakeError.Invoke(ex.Message + " | " + ex.TargetSite);
                    }
                }
            }
        }

        /// <summary>
        /// Чтение файла в массив байт
        /// </summary>
        /// <param name="FileName">Имя файла</param>
        /// <returns>Массив байт</returns>
        public byte[] ReadFile(string FileName)
        {
            byte[] bt;
            using (FileStream sr = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                bt = new byte[sr.Length];
                int offset = 0;
                int block_size = 512000;
                double div = 1.00000000000000 * (int)sr.Length / block_size;
                int count_iteration = Convert.ToInt32(Math.Truncate(div));

                for (int i = 0; i < count_iteration; i++)
                {
                    offset = sr.Read(bt, offset * i, block_size);
                    //Application.DoEvents();
                }

                if (count_iteration < div)
                {
                    sr.Read(bt, offset * count_iteration, (int)sr.Length - ((offset * (count_iteration - 1)) + offset));
                }//sr.Read(bt, 0, (int)sr.Length );
            }
            return bt;
        }

        /// <summary>
        /// Старт поиска файлов. В Keys Hashtable - полные пути диреторий для поиска
        /// </summary>
        /// <param name="ht"></param>
        public void ScanFiles(Hashtable ht)
        {
            try
            {
                _is_scanning = true;
                foreach (object folder_to_scan in ht.Keys)
                {
                    try
                    {
                        DirectoryInfo dir = new DirectoryInfo(folder_to_scan.ToString());
                        ExtractFileInfo(dir.GetFiles("*", SearchOption.TopDirectoryOnly));
                        ExtractDirInfo(dir);
                    }
                    catch (Exception ex)
                    {
                        if (MakeError != null)
                        {
                            MakeError.Invoke(ex.Message + " | " + ex.TargetSite);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _is_scanning = false;
                if (MakeError != null)
                {
                    MakeError.Invoke(ex.Message + " | " + ex.TargetSite);
                }
            }
        }

        /// <summary>
        /// Остановка поиска файлов
        /// </summary>
        public void ScanStop()
        {
            _is_scanning = false;
        }
    }
}
