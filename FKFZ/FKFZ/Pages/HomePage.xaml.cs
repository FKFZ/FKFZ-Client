using FKFZ.Controls;
using FKFZ.Log;
using FKFZ.Models;
using FKFZ.Utils;
using FKFZ.XmlModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace FKFZ.Pages
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        ObservableCollection<VideoModel> mVideoData = new ObservableCollection<VideoModel>();
        
        ObservableCollection<Medias> medias = new ObservableCollection<Medias>();
        List<VideoModel> videos;
        public HomePage()
        {
            InitializeComponent();
            InitTitle();
        }

        void InitTitle()
        {
            try
            {
                String str = IniUtil.ReadIniData("ListPage", "videotitle", "", AppDomain.CurrentDomain.BaseDirectory + "config.ini");
                if (null != str && str.Trim().Length > 0)
                {
                    ptVideo.TitleName = str;
                }
                str = IniUtil.ReadIniData("ListPage", "multititle", "", AppDomain.CurrentDomain.BaseDirectory + "config.ini");
                if (null != str && str.Trim().Length > 0)
                {
                    ptMulti.TitleName = str;
                }
                str = IniUtil.ReadIniData("ListPage", "qatitle", "", AppDomain.CurrentDomain.BaseDirectory + "config.ini");
                if (null != str && str.Trim().Length > 0)
                {
                    ptQa.TitleName = str;
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }
        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ptMulti.ImageList = null;
                ptVideo.ImageList = null;
                mVideoData.Clear();
                VideoBtns.ItemsSource = null;
                medias.Clear();
                MultiBtns.ItemsSource = null;

                String subject = Application.Current.Properties[Constant.PATH_SUBJECT] as String;
                if (null != subject)
                {
                    PagePathUtils.GetInstance().Push(subject);
                    String path = PagePathUtils.GetInstance().GetPathString();
                    //绑定视频
                    //@"D:\work\FKFZ\FKFZ\bin\Debug\db\民房工程\1@泥石流\视频\");
                    //videos = LocalLoader.LoadVideos(path + @"\视频");
                    Home home = new Home();
                    AppDB.GetInstance().HomeDic.TryGetValue(path, out home);
                    if (null != home)
                    {
                        mVideoData.Clear();
                        VideoBtns.ItemsSource = null;
                        videos = home.Video;
                        if (null != videos)
                        {
                            foreach (VideoModel vm in videos)
                            {
                                mVideoData.Add(vm);
                            }
                            //滚动图
                            if (null != mVideoData && mVideoData.Count > 0)
                            {
                                ObservableCollection<PicScrllModel> imglist = new ObservableCollection<PicScrllModel>();
                                for (int i = 0; i < mVideoData.Count; i++)
                                {
                                    PicScrllModel psm = new PicScrllModel();
                                    //此处id 无用
                                    psm.Name = mVideoData[i].Title;
                                    psm.AbsImgPath = path + @"\视频\" + mVideoData[i].Cover;
                                    psm.AbsExtPath = path + @"\视频\" + mVideoData[i].RelativePath;
                                    imglist.Add(psm);
                                }
                                ptVideo.ImageList = imglist;
                                //移除第一项
                                //mVideoData.RemoveAt(0);
                            }
                            VideoBtns.ItemsSource = mVideoData;
                        }

                        //绑定多媒体
                        //medias = LocalLoader.LoadMedias(PagePathUtils.GetInstance().GetPathString() + @"\多媒体\");
                        medias.Clear();
                        MultiBtns.ItemsSource = null;
                        if (null != home.Medias)
                        {
                            foreach (Medias m in home.Medias)
                            {
                                medias.Add(m);
                            }
                            //滚动图
                            if (null != medias && medias.Count > 0)
                            {
                                String folder = Helper.GetFolderName(medias[0]);

                                ObservableCollection<PicScrllModel> imglist = new ObservableCollection<PicScrllModel>();
                                for (int i = 0; i < home.MultiMediaScroll.Count; i++)
                                {
                                    PicScrllModel psm = new PicScrllModel();
                                    psm.ID = home.MultiMediaScroll[i].Id;
                                    psm.Name = home.MultiMediaScroll[i].Name;
                                    List<PicModel> pm = home.MultiMediaScroll[i].PicturePath;
                                    if (null != pm && pm.Count > 0)
                                    {
                                        psm.AbsImgPath = home.MultiMediaScroll[i].AbsPath + @"\" + pm[0].Path;
                                    }

                                    imglist.Add(psm);
                                }
                                ptMulti.ImageList = imglist;
                                //移除第一项
                                //medias.RemoveAt(0);
                            }
                        }
                        MultiBtns.ItemsSource = medias;
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }
       
        private void vbtn_Click_1(object sender, RoutedEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            Button btn = sender as Button;
            if (null != btn)
            {
                VideoModel sbj = btn.Tag as VideoModel;
                if (null != sbj)
                {
                    //传值
                    Application.Current.Properties[Constant.PATH_VIDEO]= 
                        PagePathUtils.GetInstance().GetPathString()+@"\视频\"+sbj.RelativePath;
                    NavigationService ns = NavigationService.GetNavigationService(this);
                    ns.Source = new Uri("Pages/MEVideoPage.xaml", UriKind.Relative);
                }
            }
            e.Handled = true;
        }

        private void MutiBtn_Click_1(object sender, RoutedEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            Button btn = sender as Button;
            if (null != btn)
            {
                Medias sbj = btn.Tag as Medias;
                if (null != sbj)
                {
                    //传值
                    Application.Current.Properties[Constant.MEDIA_PATH] = GetFolderName(sbj);// sbj.Id;
                    NavigationService ns = NavigationService.GetNavigationService(this);
                    ns.Source = new Uri("Pages/MutiPage2.xaml", UriKind.Relative);
                }
            }
            e.Handled = true;
        }

        private void QABtn_Click_1(object sender, RoutedEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Source = new Uri("Pages/QAPage3.xaml", UriKind.Relative);

            e.Handled = true;
        }

        private String GetFolderName(Medias media)
        {
            if (null == media)
            {
                return null;
            }
            return media.ID + "@" + media.Name;
        }

        private void Page_Unloaded_1(object sender, RoutedEventArgs e)
        {
            ptVideo.StopRoll();
            ptMulti.StopRoll();
        }
    }
}
