using FKFZ.Log;
using FKFZ.Models;
using FKFZ.Utils;
using FKFZ.XmlModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace FKFZ.DataStore
{
    public class LocalLoader
    {
        /// <summary>
        /// 加载主题信息
        /// </summary>
        /// <param name="path">工程目录</param>
        public static ObservableCollection<Subject> LoadSubjects(String path)
        {
            List<Subject> sbjList = new List<Subject>();
            DirectoryInfo[] dirinfo = DirUtils.GetDirectories(path);
            if (null == dirinfo)
            {
                return new ObservableCollection<Subject>();
            }
            foreach (DirectoryInfo info in dirinfo)
            {
                if(info.Name.Contains("@"))
                {
                    try
                    {
                        String[] val = info.Name.Split('@');
                        sbjList.Add(new Subject(Convert.ToInt32(val[0]),val[1]));
                    }
                    catch(Exception e)
                    {
                        RecordLog.RecordException(e);
                    }
                }
            }

            if (sbjList.Count > 1)
            {
                Subject sub = new Subject();
                Reverser<Subject> reverser = new Reverser<Subject>(sub.GetType(), "ID", ReverserInfo.Direction.ASC);
                sbjList.Sort(reverser);
            }
            return new ObservableCollection<Subject>(sbjList);
        }

        public static List<VideoModel> LoadVideos(String path)
        {
            LinqUtils linq = new LinqUtils();
            List<VideoModel> videoList = linq.ReadAllVideoInfo(path);

            return null == videoList ? new List<VideoModel>():videoList;
        }

        public static ObservableCollection<Medias> LoadMedias(String path)
        {
            List<Medias> sbjList = new List<Medias>();
            DirectoryInfo[] dirinfo = DirUtils.GetDirectories(path);
            if (null == dirinfo)
            {
                return new ObservableCollection<Medias>();
            }
            foreach (DirectoryInfo info in dirinfo)
            {
                if (info.Name.Contains("@"))
                {
                    try
                    {
                        String[] val = info.Name.Split('@');
                        FileInfo fi = new FileInfo(info.FullName+"\\config.xml");
                        if (fi.Exists)
                        {
                            sbjList.Add(new Medias(Convert.ToInt32(val[0]), val[1]));
                        }
                    }
                    catch (Exception e)
                    {
                        RecordLog.RecordException(e);
                    }
                }
            }

            if (sbjList.Count > 1)
            {
                Medias sub = new Medias();
                Reverser<Medias> reverser = new Reverser<Medias>(sub.GetType(), "ID", ReverserInfo.Direction.ASC);
                sbjList.Sort(reverser);
            }
            return new ObservableCollection<Medias>(sbjList);
        }

        public static ObservableCollection<MediaModel> LoadMutiMedia(String path)
        {
            LinqUtils linq = new LinqUtils();
            List<MediaModel> videoList = linq.ReadAllMediaInfo(path);
            if (null != videoList && videoList.Count > 1)
            {
                MediaModel sub = new MediaModel();
                Reverser<MediaModel> reverser = new Reverser<MediaModel>(sub.GetType(), "Id", ReverserInfo.Direction.ASC);
                videoList.Sort(reverser);
            }

            return null == videoList ? new ObservableCollection<MediaModel>() : new ObservableCollection<MediaModel>(videoList);
        }

        public static MediaModel GetMutiFirstPic(String path)
        {
            LinqUtils linq = new LinqUtils();
            List<MediaModel> list = linq.ReadAllMediaInfo(path);
            if (list.Count > 1)
            {
                MediaModel sub = new MediaModel();
                Reverser<MediaModel> reverser = new Reverser<MediaModel>(sub.GetType(), "Id", ReverserInfo.Direction.ASC);
                list.Sort(reverser);
            }
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public static List<MediaModel> GetMutiMedia(String path)
        {
            LinqUtils linq = new LinqUtils();
            List<MediaModel> videoList = linq.ReadAllMediaInfo(path);
            if (null != videoList && videoList.Count > 1)
            {
                MediaModel sub = new MediaModel();
                Reverser<MediaModel> reverser = new Reverser<MediaModel>(sub.GetType(), "Id", ReverserInfo.Direction.ASC);
                videoList.Sort(reverser);
            }

            return null == videoList ? new List<MediaModel>() : videoList;
        }

        public static ObservableCollection<QAModel> LoadQA(String path)
        {
            LinqUtils linq = new LinqUtils();
            List<QAModel> videoList = linq.ReadAllQAInfo(path);
            if (null != videoList && videoList.Count > 1)
            {
                QAModel sub = new QAModel();
                Reverser<QAModel> reverser = new Reverser<QAModel>(sub.GetType(), "Id", ReverserInfo.Direction.ASC);
                videoList.Sort(reverser);
            }

            return null == videoList ? new ObservableCollection<QAModel>(): new ObservableCollection<QAModel>(videoList);
        }


    }
}
