using FKFZ.DataStore;
using FKFZ.Log;
using FKFZ.Utils;
using FKFZ.XmlModel;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace FKFZ.Pages
{
    /// <summary>
    /// MutiPage.xaml 的交互逻辑
    /// </summary>
    public partial class MutiPage2 : Page
    {
        String mPath;
        DispatcherTimer timer = null;
        double mTotalSecond;
        ObservableCollection<MediaModel> medias;
        public MutiPage2()
        {
            InitializeComponent();
           
            //绑定音频结束时的事件  
            AudioPlayer.MediaEnded += new RoutedEventHandler(Element_MediaEnded);
        }
       
        //读取文本文件  
        private void SwitchText(string FileText)
        {
            string lrc = File.ReadAllText(FileText, System.Text.Encoding.GetEncoding("GB2312"));

            FlowDocument doc = new FlowDocument();
            doc.IsOptimalParagraphEnabled = true;
            doc.IsHyphenationEnabled = true;
            //doc.TextAlignment = TextAlignment.Justify;
            Paragraph p = new Paragraph();
            //if (string.IsNullOrEmpty("abc") == false)
            //    doc.RegisterName("abc", p);
            Run r = new Run(lrc);// Run 是一个 Inline 的标签  
            p.TextAlignment = TextAlignment.Justify;
            p.Inlines.Add(r);
            doc.Blocks.Add(p);
            doc.LineHeight = 60;// FontSize;
          
            RTB.Document = doc;              
        }

        private void SwitchImageSource(Image imgCtrl, String path)
        {
            try
            {
                Image image = InitImage(path);
                if (null != image)
                {
                    imgCtrl.Source = image.Source;                    
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }

        private Image InitImage(String filePath)
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
        int index = 0;
        long second = 0;
        int lastPage;
        private void timer_tick(object sender, EventArgs e)
        {
            try
            {

                Debug.WriteLine("timer_tick,MultiFrame");
                index = -1;
                //second++;
                foreach(MediaModel model in medias)
                {
                    index++;
                    long nValue = Convert.ToInt64(model.StartTime.TotalSeconds);
                    long tValue = Convert.ToInt64(AudioPlayer.Position.TotalSeconds);
                    if (nValue - tValue == 0 && lastPage != index)
                    {
                        ShowFirstContent(model);
                        lastPage = index;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }

        }

        private void ShowFirstContent(MediaModel model)
        {
            try
            {
                SwitchText(mPath+ @"\" + model.TextPath);
                SwitchImageSource(ImgTop, mPath + @"\" + model.PicturePath[0].Path);
                SwitchImageSource(ImgBottom, mPath + @"\" + model.PicturePath[1].Path);
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }

        #region 音频操作

        // Pause the media.
        void OnMouseDownPauseMedia(object sender, MouseButtonEventArgs args)
        {
            AudioPlayer.Pause();
        }

        // Stop the media.
        void OnMouseDownStopMedia(object sender, MouseButtonEventArgs args)
        {
            AudioPlayer.Stop();
        }

        private void Element_MediaOpened(object sender, EventArgs e)
        {
            mTotalSecond = AudioPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            //媒体文件打开成功
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.4);//0.94);
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
        }
        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            AudioPlayer.Stop();
            AudioPlayer.Source = null;
            if (null != timer)
            {
                timer.Stop();
                timer = null;
            }
            if (NavigationService.CanGoBack)
            {
                //回退一级
                PagePathUtils.GetInstance().Pop();
                NavigationService.GoBack();
            }
        }
        #endregion

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //存储的是多媒体id
                String mediaPathName = (String)Application.Current.Properties[Constant.MEDIA_PATH];
                if (null != mediaPathName && mediaPathName.Trim().Length>0)
                {
                    //@"D:\work\FKFZ\FKFZ\bin\Debug\db\民房工程\1@泥石流\多媒体\"
                    mPath = PagePathUtils.GetInstance().GetPathString() + @"\多媒体\"+mediaPathName;
                    medias = LocalLoader.LoadMutiMedia(mPath);
                    if (medias.Count > 0)
                    {
                        ShowFirstContent(medias[0]);
                        lastPage = 0;
                    }
                }

                //BgMusic bm = new BgMusic();
                //bm.BgMusicPath = new Uri(mPath + @"\" + "bgmusic.mp3", UriKind.RelativeOrAbsolute);
                //AudioPlayer.DataContext = bm;
                AudioPlayer.Source = new Uri(mPath + @"\" + "bgmusic.mp3", UriKind.RelativeOrAbsolute);
                AudioPlayer.Play();
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }

        private void Element_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            RecordLog.RecordException(e.ErrorException);
        }

        public bool ReachHeader()
        {
            return null == medias ? false : lastPage == 0;
        }

        public bool ReachEnd()
        {
            return null == medias ? false : lastPage == medias.Count - 1;
        }

        public bool GoPrevious()
        {
            if (ReachHeader())
            {
                return false;
            }
            if (lastPage > 0)
            {

                if (null != timer)
                {
                    timer.Stop();
                }
                lastPage--;
                if (lastPage < 0)
                {
                    lastPage = 0;
                }
                MediaModel mm = medias[lastPage];
                AudioPlayer.Position = new TimeSpan(0, 0, 0, (int)mm.StartTime.TotalSeconds);
                second = Convert.ToInt64(mm.StartTime.TotalSeconds);
                ShowFirstContent(mm);

                if (null != timer)
                {
                    timer.Start();
                }
            }

            return true;
        }

        public bool GoNext()
        {
            if (ReachEnd())
            {
                return false;
            }
            if (lastPage < medias.Count - 1)
            {

                if (null != timer)
                {
                    timer.Stop();
                }
                lastPage++;
                if (lastPage > medias.Count - 1)
                {
                    lastPage = medias.Count - 1;
                }
                MediaModel mm = medias[lastPage];
                AudioPlayer.Position = new TimeSpan(0, 0, 0, (int)mm.StartTime.TotalSeconds);
                second = Convert.ToInt64(mm.StartTime.TotalSeconds);
                ShowFirstContent(mm);
                if (null != timer)
                {
                    timer.Start();
                }
            }
            return true;            
        }

        private void Page_Unloaded_1(object sender, RoutedEventArgs e)
        {
            AudioPlayer.Stop();
            if (null != timer)
            {
                timer.Stop();
            }
        }

    }

    public class BgMusic
    {
        public Uri BgMusicPath { get; set; }
    }
}
