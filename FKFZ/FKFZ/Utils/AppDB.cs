using FKFZ.DataStore;
using FKFZ.Log;
using FKFZ.Models;
using FKFZ.XmlModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace FKFZ.Utils
{
    public class SubjectItem
    {
        public SubjectItem()
        { 
        
        }

        public String AbsPath
        { get; set; }

        public ObservableCollection<Subject> Subjects
        { get; set; }
    }

    public class Home
    {
        public Home()
        {

        }
        /// <summary>
        /// 视频按钮数据源
        /// </summary>
        public List<VideoModel> VideoScroll
        { get; set; }
        /// <summary>
        /// 视频列表数据源
        /// </summary>
        public List<VideoModel> Video
        { get; set; }
        /// <summary>
        /// 轮播数据源
        /// </summary>
        public List<MediaModel> MultiMediaScroll
        { get; set; }

        /// <summary>
        /// 媒体按钮
        /// </summary>
        public ObservableCollection<Medias> Medias
        { get; set; }
    }

    public class AppDB
    {
        private static int idle = -2;
        /// <summary>
        /// 设置空闲时间，单位秒
        /// </summary>
        public static int GetIdle() 
        {
            if (-2 == idle)
            {
                if (InitIdleTime() && idle > 0)
                {
                    return idle;
                }
            }
            else
            {
                return idle > 0 ? idle : 40;
            }

            return 40;
        }
        Dictionary<String, SubjectItem> subjectDic = new Dictionary<String, SubjectItem>();
        Dictionary<String, Home> homeDic = new Dictionary<String, Home>();

        private static AppDB Instance;
        private AppDB()
        {
            Init();
        }

        public static AppDB GetInstance()
        {
            if (null == Instance)
            {
                Instance = new AppDB();
            }
            return Instance;
        }

        static bool InitIdleTime()
        {
            try
            {
                String str = IniUtil.ReadIniData("App", "idletime", "", AppDomain.CurrentDomain.BaseDirectory + "config.ini");
                if (null != str && str.Trim().Length > 0)
                {
                    idle = Int32.Parse(str);
                }
            }
            catch (Exception ex)
            {
                idle = -1;
                RecordLog.RecordException(ex);
                return false;
            }
            return true;
        }

        void Init()
        {
            GetSubjects();
        }

        void GetSubjects()
        {
            String basePath = AppDomain.CurrentDomain.BaseDirectory + @"db\";
            try
            {
                DirectoryInfo[] dirs = DirUtils.GetDirectories(basePath);
                if (null != dirs && dirs.Length > 0)
                {
                    foreach (DirectoryInfo info in dirs)
                    {
                        SubjectItem item = new SubjectItem();
                        item.AbsPath = info.FullName + @"\";

                        ObservableCollection<Subject> subjects = LocalLoader.LoadSubjects(item.AbsPath);
                        item.Subjects = subjects;

                        subjectDic.Add(item.AbsPath ,item);
                        foreach (Subject sub in subjects)
                        {
                            GetHomeDatas(item.AbsPath + sub.ID + "@" + sub.Name);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }

        void GetHomeDatas(String path)
        {
            Home home = new Home();
            try
            {
                home.Video = LocalLoader.LoadVideos(path + @"\视频");
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
            try{
                //绑定多媒体
                ObservableCollection<Medias> medias = LocalLoader.LoadMedias(path + @"\多媒体\");
                home.Medias = medias;
                //滚动图
                if (null != medias && medias.Count > 0)
                {
                    home.MultiMediaScroll = new List<MediaModel>();
                    int i = 0;
                    foreach (Medias m in medias)
                    {
                        String folder = Helper.GetFolderName(medias[i++]);
                        try
                        {
                            MediaModel mm = LocalLoader.GetMutiFirstPic(path + @"\多媒体\" + folder);
                            //用这个媒体的id和名称代替
                            mm.Name = m.Name;
                            mm.Id = m.ID;
                            home.MultiMediaScroll.Add(mm);
                        }
                        catch (Exception ex)
                        {
                            RecordLog.RecordException(ex);
                        }
                    }
                }

                homeDic.Add(path, home);
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }

        public Dictionary<String, SubjectItem> SubjectDic
        {
            get { return subjectDic; }
        }

        public Dictionary<String, Home> HomeDic
        {
            get { return homeDic; }
        }

        public void Reset()
        {
            foreach (KeyValuePair<String, SubjectItem> kv in subjectDic)
            {
                SubjectItem item = kv.Value;
                foreach (Subject sbj in item.Subjects)
                {
                    sbj.IsChecked = false;
                }
            }
        }
        /// <summary>
        /// 用于设置无操作返回
        /// </summary>
        public int IdleSeconds
        { get; set; }
    }
}
