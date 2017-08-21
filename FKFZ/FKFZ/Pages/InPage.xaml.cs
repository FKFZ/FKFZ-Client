using FKFZ.Log;
using FKFZ.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace FKFZ.Pages
{
    /// <summary>
    /// InPage.xaml 的交互逻辑
    /// </summary>
    public partial class InPage : Page
    {
        DispatcherTimer timer = null;
        public InPage()
        {
            InitializeComponent();
            InitTitle();
            //媒体文件打开成功
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_tick);
        }
        void InitTitle()
        {
            try
            {
                String str = IniUtil.ReadIniData("InPage", "leftabspic", "", AppDomain.CurrentDomain.BaseDirectory + "config.ini");
                if (null != str && str.Trim().Length > 0)
                {
                    Image img = InitImage(str);
                    if (null != img)
                    {
                        LeftImg.Source = img.Source;
                    }
                }
                str = IniUtil.ReadIniData("InPage", "rightabspic", "", AppDomain.CurrentDomain.BaseDirectory + "config.ini");
                if (null != str && str.Trim().Length > 0)
                {
                    Image img = InitImage(str);
                    if (null != img)
                    {
                        RightImg.Source = img.Source;
                    }
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


        private void RenBtn_Click_1(object sender, RoutedEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            if (null != timer)
            {
                timer.Stop();
            }
            e.Handled = true;
            PagePathUtils.GetInstance().Push("人防知识");
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Source = new Uri("Pages/HomeFrame.xaml", UriKind.Relative);
        }

        private void MinBtn_Click_1(object sender, RoutedEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            if (null != timer)
            {
                timer.Stop();
            }
            e.Handled = true;
            PagePathUtils.GetInstance().Push("民防知识");
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Source = new Uri("Pages/HomeFrame.xaml", UriKind.Relative);
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            int a =(int) Application.Current.Properties["InParam"];
            AppDB.GetInstance().Reset();
            NavigationService ns = NavigationService.GetNavigationService(this);
            while (ns.CanGoBack)
            {
                ns.RemoveBackEntry();
            }
            if (null != timer)
            {
                AppDB.GetInstance().IdleSeconds = 0;
                timer.Start();
            }

        }
        private void timer_tick(object sender, EventArgs e)
        {
            Debug.WriteLine("timer_tick,InFrame");
            AppDB.GetInstance().IdleSeconds++;
            int t = AppDB.GetIdle();
            Debug.WriteLine(String.Format("time {0},{1}", AppDB.GetInstance().IdleSeconds, AppDB.GetIdle()));
            if (AppDB.GetInstance().IdleSeconds >= AppDB.GetIdle())
            {
                Debug.WriteLine(String.Format("goback {0}", AppDB.GetInstance().IdleSeconds));
                GoBackToInPage("Pages/LoginPage.xaml");
                AppDB.GetInstance().IdleSeconds = 0;
                timer.Stop();
                timer = null;
            }
        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            timer.Stop();
            timer = null;
            GoBackToInPage("Pages/LoginPage.xaml");
        }

        void GoBackToInPage(String path)
        {
            Application.Current.Properties.Remove(Constant.PATH_SUBJECT);
            Application.Current.Properties.Remove(Constant.PATH_MULTI);
            Application.Current.Properties.Remove(Constant.KEY_PATH);
            Application.Current.Properties.Remove(Constant.MEDIA_PATH);
            Application.Current.Properties.Remove(Constant.PATH_QA);
            Application.Current.Properties.Remove(Constant.PATH_VIDEO);

            PagePathUtils.GetInstance().ClearHistory();

            NavigationService ns = NavigationService.GetNavigationService(this);
            if (ns != null)
            {
                ns.Source = new Uri(path, UriKind.Relative);
            }
        }
    }
}
