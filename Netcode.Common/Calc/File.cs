using System;
using System.Collections.Generic;
using System.Text;

namespace Netcode.Common.Calc
{
    class FileSize
    {
        /// <summary>
        /// Конвертирование размера файла в строку
        /// </summary>
        /// <param name="size">Размер в байтах</param>
        /// <returns>Строка с размером и разрядностью</returns>
        public static string PrintFileSize(long size)
        {
            string ret = string.Empty;
            if (size < 10240)
            {
                return size.ToString() + " Байт";
            }
            if (size < 1048576)
            {
                return Math.Floor((double)size / 1024) + " кБайт";
            }

            if (size < 737418240)
            {
                return string.Format("{0:0.00}", (double)size / 1048576) + " МБайт";
            }
            else
            {
                return string.Format("{0:0.00}", ((double)size / 1073741824)) + " ГБайт";
            }
        }
    }
}
