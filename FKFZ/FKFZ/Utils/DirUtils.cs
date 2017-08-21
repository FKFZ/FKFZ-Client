using System;
using System.IO;

namespace FKFZ.Utils
{
    public class DirUtils
    {
        public static DirectoryInfo[] GetDirectories(String path)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            if (!folder.Exists)
            {
                return null;
            }
            return folder.GetDirectories();
        }

        public static FileInfo[] GetFiles(String path)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            if (!folder.Exists)
            {
                return null;
            }
            return folder.GetFiles();
        }
    }
}
