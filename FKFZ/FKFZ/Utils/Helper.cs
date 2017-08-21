using FKFZ.Models;
using System;

namespace FKFZ.Utils
{
    public class Helper
    {
        public static String GetFolderName(Subject subject)
        {
            if (null == subject)
            {
                return null;
            }
            return subject.ID + "@" + subject.Name;
        }

        public static String GetFolderName(Medias meadia)
        {
            if (null == meadia)
            {
                return null;
            }
            return meadia.ID + "@" + meadia.Name;
        }
    }
}
