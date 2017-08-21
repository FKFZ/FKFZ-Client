using FKFZ.Log;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace FKFZ.XmlModel
{
    public class LinqUtils
    {
        /// <summary>
        /// 读取指定路径下视频配置信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<VideoModel> ReadAllVideoInfo(String path)
        {
            try
            {
                path += @"\config.xml";
                XElement xe = XElement.Load(path);
                IEnumerable<XElement> elements = from ele in xe.Elements("video") select ele;
                return ShowVideoInfoByElements(elements);
            }
            catch (Exception e)
            {
                RecordLog.RecordException(e);
            }
            return null;
        }

        public List<VideoModel> ShowVideoInfoByElements(IEnumerable<XElement> elements)
        {
            List<VideoModel> modelList = new List<VideoModel>();
            try
            {
                foreach (var ele in elements)
                {
                    VideoModel model = new VideoModel();
                    model.ID = ele.Element("id").Value;
                    model.Title = ele.Element("title").Value;
                    model.RelativePath = ele.Element("path").Value;
                    model.Cover = ele.Element("cover").Value;
                    model.VideoType = ele.Attribute("Type").Value;
                    modelList.Add(model);
                }
            }
            catch (Exception e)
            {
                RecordLog.RecordException(e);
            }

            return modelList;
        }

        /// <summary>
        /// 读取知识问答xml 内容
        /// </summary>
        /// <param name="path"> 不包含文件名的绝对路径</param>
        /// <returns></returns>
        public List<QAModel> ReadAllQAInfo(String path)
        {
            try
            {
                path += @"\config.xml";
                XElement xe = XElement.Load(path);
                IEnumerable<XElement> elements = from ele in xe.Elements("question") select ele;
                return ShowQAInfoByElements(elements);
            }
            catch (Exception e)
            {
                RecordLog.RecordException(e);
            }
            return null;
        }

        public List<QAModel> ShowQAInfoByElements(IEnumerable<XElement> elements)
        {
            List<QAModel> modelList = new List<QAModel>();
            try
            {
                int group = 0;
                foreach (var ele in elements)
                {
                    QAModel model = new QAModel();
                    model.Id = Convert.ToInt32(ele.Element("id").Value);
                    model.Title = ele.Element("title").Value;
                    model.Score = Convert.ToInt32(ele.Element("score").Value);
                    model.AnswerId = ele.Element("answerid").Value;
                    model.Options = new ObservableCollection<OptionModel>();
                    IEnumerable<XElement> eleOpts = from opt in ele.Elements("option") select opt;
                    foreach (var opt in eleOpts)
                    {
                        OptionModel om = new OptionModel();
                        om.OptionId = opt.Attribute("id").Value;
                        om.Value = opt.Value;
                        om.Group = group + "";
                        model.Options.Add(om);
                    }
                    group++;
                    modelList.Add(model);
                }
            }
            catch (Exception e)
            {
                RecordLog.RecordException(e);
            }

            return modelList;
        }


        /// <summary>
        /// 读取知识问答xml 内容
        /// </summary>
        /// <param name="path"> 不包含文件名的绝对路径</param>
        /// <returns></returns>
        public List<MediaModel> ReadAllMediaInfo(String path)
        {
            try
            {
                XElement xe = XElement.Load(path + @"\config.xml");
                IEnumerable<XElement> elements = from ele in xe.Elements("page") select ele;
                return ShowMediaPageInfoByElements(elements, path);
            } 
            catch (Exception e)
            {
                RecordLog.RecordException(e);
            }
            return null;
        }

        public List<MediaModel> ShowMediaPageInfoByElements(IEnumerable<XElement> elements,String path)
        {
            List<MediaModel> modelList = new List<MediaModel>();
            try
            {
                int group = 0;
                foreach (var ele in elements)
                {
                    MediaModel model = new MediaModel();
                    model.Id = Convert.ToInt32(ele.Element("id").Value);
                    model.Name = ele.Element("Name").Value;
                    String str = ele.Element("StartTime").Value;
                    model.StartTime = GetStartTime(str);
                    model.TextPath = ele.Element("TextPath").Value;
                    model.PicturePath = new List<PicModel>();
                    model.AbsPath = path;
                    IEnumerable<XElement> eleOpts = from opt in ele.Elements("PicPath") select opt;
                    foreach (var opt in eleOpts)
                    {
                        PicModel om = new PicModel();
                        om.Id = Convert.ToInt32(opt.Attribute("index").Value);
                        om.Path = opt.Value;
                        model.PicturePath.Add(om);
                    }
                    group++;
                    modelList.Add(model);
                }
            }
            catch (Exception e)
            {
                RecordLog.RecordException(e);
            }

            return modelList;
        }

        private TimeSpan GetStartTime(String content)
        {
            Regex rx = new Regex(@"([01]?\d|2[0-3]):[0-5]?\d:[0-5]?\d", RegexOptions.RightToLeft); 
                //new Regex(@"/^([0-2][0-9]):([0-5][0-9]):([0-5][0-9])$/", RegexOptions.Singleline);
            //匹配表达式  
            foreach (Match x in rx.Matches(content))
            {
                try
                {
                    String[] v = x.Value.Split(':');
                    return new TimeSpan(int.Parse(v[0]), int.Parse(v[1]), int.Parse(v[2]));
                    
                }
                catch (Exception e)
                {
                    RecordLog.RecordException(e);
                }
            }

            return new TimeSpan();
        }
    }
}
