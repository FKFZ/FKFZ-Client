using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FKFZ.Utils
{
    /// <summary>
    /// 路径记录帮助类
    /// </summary>
    public class PagePathUtils
    {
        private static PagePathUtils instance;
        private static object _lock = new object();
        /// <summary>
        /// 基础路径
        /// </summary>
        private String BasePath = AppDomain.CurrentDomain.BaseDirectory+@"db\";

        Stack<String> paths = new Stack<string>();

        private PagePathUtils() { }

        public static PagePathUtils GetInstance()
        {
            if (null == instance)
            {
                lock (_lock)
                {
                    if (null == instance)
                    {
                        instance = new PagePathUtils();
                    }
                }
            }

            return instance;
        }

        public String Pop()
        {
            if (paths.Count > 0)
            {
                return paths.Pop();
            }
            return null;
        }
        /// <summary>
        /// 只返回最上层路径
        /// </summary>
        /// <returns></returns>
        public String GetCurrentSinglePath()
        {
            return paths.Peek();
        }

        public void Push(String path)
        {
            paths.Push(path);
        }
        /// <summary>
        /// 获取路径字符串，不包含末尾字符
        /// </summary>
        /// <returns></returns>
        public String GetPathString()
        {
            List<String> listPaths = paths.Reverse<String>().ToList<String>();
            StringBuilder sb = new StringBuilder();
            sb.Append(BasePath);
            foreach (String item in listPaths)
            {
                sb.Append(item);
                sb.Append("\\");
            }
            String path = sb.ToString();
            if(path.EndsWith("\\"))
            {
                path = path.Substring(0,path.Length-1);
            }
            return path;
            
        }


        public void ClearHistory()
        {
            paths.Clear();
        }

        public String GetBasePath()
        {
            return BasePath;
        }
    }
}
