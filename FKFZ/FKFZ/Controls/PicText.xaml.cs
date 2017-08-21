using FKFZ.Log;
using FKFZ.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace FKFZ.Controls
{
    public class PicScrllModel
    {

        public PicScrllModel()
        { 
        }
        public String Name { get; set; }
        public String AbsImgPath { get; set; }
        public String AbsExtPath { get; set; }

        public int ID { get; set; }


    }
    /// <summary>
    /// PicText.xaml 的交互逻辑
    /// </summary>
    public partial class PicText : UserControl
    {
        public PicText()
        {
            InitializeComponent();
        }

        // 枚举动画类型  
        public enum CtrlType
        {
            /// <summary>  
            /// 视频
            /// </summary>  
            VideoPlayer = 1,
            /// <summary>  
            /// 轮播图  
            /// </summary>  
            MediaPlayer,
            /// <summary>  
            /// 图片  
            /// </summary>  
            Picture,
            /// <summary>
            /// 视频轮播
            /// </summary>
            NONE
        }  
        #region 自定义依赖属性
        /// <summary>  
        /// 动画类型  
        /// </summary>  
        public CtrlType UIType
        {
            get { return (CtrlType)GetValue(UITypeProperty); }
            set { SetValue(UITypeProperty, value); }
        }

        public static readonly DependencyProperty UITypeProperty = DependencyProperty.Register("UIType", typeof(CtrlType),
            typeof(PicText), new PropertyMetadata(CtrlType.NONE,new PropertyChangedCallback(OnUITypeChanged)) );

        private static void OnUITypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PicText dtb = (PicText)d;
        }

        public static readonly DependencyProperty ArrowVisibleProperty = DependencyProperty.Register("ArrowVisible", typeof(Visibility),
        typeof(PicText), new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback(OnArrowVisibleChanged)));

        private static void OnArrowVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PicText dtb = (PicText)d;
            dtb.BtnLeft.Visibility = (Visibility)e.NewValue;
            dtb.BtnRight.Visibility = (Visibility)e.NewValue;
        }

        [Description("获取或设置Play可见性")]
        [Category("Common Properties")]
        public Visibility ArrowVisible
        {
            get { return (Visibility)GetValue(ArrowVisibleProperty); }
            set { SetValue(ArrowVisibleProperty, value); }
        }

        public static readonly DependencyProperty PlayVisibleProperty = DependencyProperty.Register("PlayVisible", typeof(Visibility),
        typeof(PicText), new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback(OnPlayVisibleChanged)));

        private static void OnPlayVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PicText dtb = (PicText)d;
            dtb.Play.Visibility = (Visibility)e.NewValue;
        }
        [Description("获取或设置Play可见性")]
        [Category("Common Properties")]
        public Visibility PlayVisible
        {
            get { return (Visibility)GetValue(PlayVisibleProperty); }
            set { SetValue(PlayVisibleProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("TitleName", typeof(string),
        typeof(PicText), new PropertyMetadata("TextBlock", new PropertyChangedCallback(OnTextChanged)));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PicText dtb = (PicText)d;
            dtb.TitleTb.Text = (string)e.NewValue;
        }

        [Description("获取或设置当前标题")]
        [Category("Common Properties")]
        public string TitleName
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImgSource", typeof(ImageSource),
       typeof(PicText), new PropertyMetadata(null, new PropertyChangedCallback(OnImageSourceChanged)));

        private static void OnImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PicText dtb = (PicText)d;
            dtb.TopImg.Source = (ImageSource)e.NewValue;
        }
        [Description("获取或设置当前图片")]
        [Category("Common Properties")]
        public ImageSource ImgSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }


        public static readonly DependencyProperty TitleProperty = 
            DependencyProperty.Register("Title", typeof(String), typeof(PicText),
            new FrameworkPropertyMetadata("title", new PropertyChangedCallback(TitlePropertyChangedCallback)));

        private static void TitlePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            if (sender != null && sender is PicText)
            {
                PicText clock = sender as PicText;

            }
        }
        private static void OnTitleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            PicText pic = (PicText)obj;
            RoutedPropertyChangedEventArgs<String> e = new RoutedPropertyChangedEventArgs<String>
            ((String)args.OldValue, (String)args.NewValue, ValueChangedEvent);
        }


        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
            "ValueChanged", RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<String>), typeof(PicText));


        [Description("获取或设置当前标题")]
        [Category("Common Properties")]
        public String Title
        {
            get
            {
                return (String)this.GetValue(TitleProperty);
            }
            set
            {
                this.SetValue(TitleProperty, value);
            }
        }

        //public static DependencyProperty MediaPathProperty = DependencyProperty.Register("_mediaPath", typeof(ObservableCollection<String>), typeof(PicText)
        //     , new PropertyMetadata(new PropertyChangedCallback(ModifyMediaPath)));

        //public ObservableCollection<String> MediaPath
        //{
        //    get { return (ObservableCollection<String>)GetValue(MediaPathProperty); }
        //    set { SetValue(MediaPathProperty, value); }
        //}
        //private static void ModifyMediaPath(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{
        //    PicText ctrl = (PicText)sender;
        //    if (e.Property == MediaPathProperty)
        //    {
        //        ctrl.MediaPath = (ObservableCollection<String>)e.NewValue;
        //    }
        //}

        private static ObservableCollection<PicScrllModel> _imagelist;
        public static DependencyProperty ImageListProperty = DependencyProperty.Register("_imagelist", typeof(ObservableCollection<PicScrllModel>), typeof(PicText)
             , new PropertyMetadata(new PropertyChangedCallback(loadAdvertPic)));

        public ObservableCollection<PicScrllModel> ImageList
        {
            get { return (ObservableCollection<PicScrllModel>)GetValue(ImageListProperty); }
            set { SetValue(ImageListProperty, value); }
        }

        private static void loadAdvertPic(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PicText pt = (PicText)sender;
            if (e.Property == ImageListProperty)
            {
                pt.ImageList = (ObservableCollection<PicScrllModel>)e.NewValue;
                _imagelist = pt.ImageList;
                if (null != _imagelist)
                {
                    pt.ImageListStr = pt.GetMultiPicPaths(_imagelist);
                    pt.BeginRoll();
                }
                else
                {
                    pt.StopRoll();
                }
            }
        }
        #endregion

        public event RoutedPropertyChangedEventHandler<String> ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        protected virtual void OnValueChanged(RoutedPropertyChangedEventArgs<String> args)
        {
            RaiseEvent(args);
        }

        //声明和注册路由事件\
        public static readonly RoutedEvent PlayEvent =
            EventManager.RegisterRoutedEvent("PlayClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PicText));
        //CLR事件包装
        public event RoutedEventHandler PlayClick
        {
            add { this.AddHandler(PlayEvent, value); }
            remove { this.RemoveHandler(PlayEvent, value); }
        }
        List<String> ImageListStr = new List<string>();
        
        private void Play_Click_1(object sender, RoutedEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            e.Handled = true;
            int index = rollImg.GetCurrentPlayIndex();
            if (null != ImageList && ImageList.Count > index && index > -1)
            {
                PicScrllModel sbj = ImageList[index];
                if (null != sbj)
                {
                    //传值
                    Application.Current.Properties[Constant.PATH_VIDEO] = sbj.AbsExtPath;
                    NavigationService ns = NavigationService.GetNavigationService(this);
                    ns.Source = new Uri("Pages/MEVideoPage.xaml", UriKind.Relative);
                }
            }
        }

        private void BtnLeft_Click_1(object sender, RoutedEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            e.Handled = true;
            Button btn = sender as Button;
            if (null != btn)
            {
                if (btn.Name == "BtnLeft")
                {
                    rollImg.StartStory(FKFZ.Controls.StoryTypes.RightToLeft);
                }
                else if (btn.Name == "BtnRight")
                {
                    rollImg.StartStory(FKFZ.Controls.StoryTypes.LeftToRight);
                }
            }
        }

        private void scrollImg_Click_1(object sender, ClickEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            e.Handled = true;     
            if (null != e)
            {
                if (null != ImageList && ImageList.Count > e.index)
                {
                    //传值
                    if (UIType == CtrlType.MediaPlayer)
                    {
                        Application.Current.Properties[Constant.MEDIA_PATH] = ImageList[e.index].ID + "@" + ImageList[e.index].Name;
                        NavigationService ns = NavigationService.GetNavigationService(this);
                        if (null != ns)
                        {
                            ns.Source = new Uri("Pages/MutiPage2.xaml", UriKind.Relative);
                        }
                    }
                    else if (UIType == CtrlType.VideoPlayer)
                    {
                        Application.Current.Properties[Constant.PATH_VIDEO] = ImageList[e.index].AbsExtPath;
                        NavigationService ns = NavigationService.GetNavigationService(this);
                        if (null != ns)
                        {
                            ns.Source = new Uri("Pages/MEVideoPage.xaml", UriKind.Relative);
                        }
                    }
                }
            }       
        }

        private void TopImg_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            e.Handled = true;
            if (UIType == CtrlType.Picture)
            {
                NavigationService ns = NavigationService.GetNavigationService(this);
                ns.Source = new Uri("Pages/QAPage3.xaml", UriKind.Relative);
            }
        }


        private void rollImg_WordChanged_1(object sender, WordChangedEventArgs e)
        {
            e.Handled = true;
            if (null != e)
            {
                if (ImageList.Count > e.index)
                {
                    TitleTb.Text = ImageList[e.index].Name;
                }
            }       
        }

        List<String> GetMultiPicPaths(ObservableCollection<PicScrllModel> mediaData)
        {
            List<String> path = new List<string>();
            try
            {
                if (null != mediaData && mediaData.Count > 0)
                {
                    foreach (PicScrllModel mm in mediaData)
                    {
                        path.Add(mm.AbsImgPath);
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
            return path;
        }

        public void BeginRoll()
        {
            try
            {
                rollImg.StopStory();
                if (null != ImageListStr && ImageListStr.Count > 0 && (UIType == CtrlType.MediaPlayer || UIType == CtrlType.VideoPlayer))
                {
                    List<Image> listImage = new List<Image>();
                    int count = ImageListStr.Count;
                    for (int i = 0; i < count; i++)
                    {
                        listImage.Add(InitImage(ImageListStr[i]));
                    }
                    rollImg.Images = listImage;

                    rollImg.Begin();
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }

        public void StopRoll()
        {
            try
            {
                if (null == ImageListStr)
                {
                    rollImg.Images = null;

                    rollImg.StopStory();
                    return;
                }
                if (null != ImageListStr && ImageListStr.Count > 0 && (UIType == CtrlType.MediaPlayer || UIType == CtrlType.VideoPlayer))
                {
                    List<Image> listImage = new List<Image>();
                    int count = ImageListStr.Count;
                    for (int i = 0; i < count; i++)
                    {
                        listImage.Add(InitImage(ImageListStr[i]));
                    }
                    rollImg.Images = listImage;

                    rollImg.StopStory();
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }

        private Image InitImage(String filePath)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    FileInfo fi = new FileInfo(filePath);
                    byte[] bytes = reader.ReadBytes((int)fi.Length);
                    reader.Close();

                    Image image = new Image();
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(bytes);
                    bitmapImage.EndInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    image.Source = bitmapImage;
                    return image;
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
            return null;
        }

     }
}
