using FKFZ.Log;
using FKFZ.Models;
using FKFZ.Utils;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace FKFZ.Pages
{
    public class Task
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    /// <summary>
    /// HomeFrame.xaml 的交互逻辑
    /// </summary>
    public partial class HomeFrame : Page
    {
        DispatcherTimer timer = null;
        String mCurrentFolderName;
        //ObservableCollection<Subject> subject = null;
        public HomeFrame()
        {
            InitializeComponent();
            InitTitle();
            CenterPage.LoadCompleted += CenterPage_LoadCompleted;
            CenterPage.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;

            //媒体文件打开成功
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();

            try
            {
                //subject = LocalLoader.LoadSubjects(PagePathUtils.GetInstance().GetPathString() + @"\");
                //@"D:\work\FKFZ\FKFZ\bin\Debug\db\民房工程");
                String path = PagePathUtils.GetInstance().GetPathString() + @"\";
                SubjectItem si = new SubjectItem();
                if(AppDB.GetInstance().SubjectDic.TryGetValue(path,out si))
                {
                    TopBtns.ItemsSource = si.Subjects;

                    if (si.Subjects.Count > 0)
                    {
                        si.Subjects[0].IsChecked = true;
                        mCurrentFolderName = GetFolderName(si.Subjects[0]);
                        Application.Current.Properties[Constant.PATH_SUBJECT] = mCurrentFolderName;
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
            HomePage page = new HomePage();
            CenterPage.Content = page;           
        }

        void InitTitle()
        {
            try
            {
                String str = IniUtil.ReadIniData("App", "title1", "", AppDomain.CurrentDomain.BaseDirectory + "config.ini");
                if (null != str && str.Trim().Length > 0)
                {
                    AppMainTitle.Text = str;
                }
                str = IniUtil.ReadIniData("App", "title2", "", AppDomain.CurrentDomain.BaseDirectory + "config.ini");
                if (null != str && str.Trim().Length > 0)
                {
                    AppMainTitle2.Text = str;
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }

       
        void CenterPage_LoadCompleted(object sender, NavigationEventArgs e)
        {
            MutiPage2 mp = CenterPage.NavigationService.Content as MutiPage2;
            if (null != mp)
            {
                btn_next.Visibility = Visibility.Visible;
                btn_pre.Visibility = Visibility.Visible;
                btn_next.IsEnabled = true;
                btn_pre.IsEnabled = true;
            }
            else
            {
                btn_next.Visibility = Visibility.Hidden;
                btn_pre.Visibility = Visibility.Hidden;
            }
            MEVideoPage mevp = CenterPage.NavigationService.Content as MEVideoPage;
            VideoPage vp = CenterPage.NavigationService.Content as VideoPage;
            if (vp != null || mp != null || mevp != null)
            {
                if (null != timer)
                {
                    timer.Stop();
                }
                AppDB.GetInstance().IdleSeconds = AppDB.GetIdle()+ 20;
            }
            else
            {
                if (null != timer)
                {
                    timer.Start();
                }
                AppDB.GetInstance().IdleSeconds = 0;
            }
        }


        private void timer_tick(object sender, EventArgs e)
        {
            Debug.WriteLine("timer_tick,HomeFrame");
            AppDB.GetInstance().IdleSeconds++;
            if (AppDB.GetInstance().IdleSeconds == AppDB.GetIdle())
            {
                GoBackToInPage("Pages/LoginPage.xaml");
            }
        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            e.Handled = true;
            if (CenterPage.NavigationService.CanGoBack)
            {
                //回退一级
                PagePathUtils.GetInstance().Pop();
                CenterPage.NavigationService.GoBack();
            }
            else
            {
                GoBackToInPage("Pages/InPage.xaml");
            }
        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            e.Handled = true;
            GoBackToInPage("Pages/LoginPage.xaml");
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            //清理回退历史
            while (CenterPage.NavigationService.CanGoBack)
            {
                CenterPage.NavigationService.RemoveBackEntry();
            }
        }

        void GoBackToInPage(String path)
        {
            TopBtns.ItemsSource = null;
            Application.Current.Properties.Remove(Constant.PATH_SUBJECT);
            Application.Current.Properties.Remove(Constant.PATH_MULTI);
            Application.Current.Properties.Remove(Constant.KEY_PATH);
            Application.Current.Properties.Remove(Constant.MEDIA_PATH);
            Application.Current.Properties.Remove(Constant.PATH_QA);
            Application.Current.Properties.Remove(Constant.PATH_VIDEO);

            PagePathUtils.GetInstance().ClearHistory();

            NavigationService ns = NavigationService.GetNavigationService(this);
            if (null != ns)
            {
                ns.Source = new Uri(path, UriKind.Relative);
            }
        }
        //private void TopBtnItem_Click_1(object sender, RoutedEventArgs e)
        //{

        //    Uri uri = CenterPage.NavigationService.CurrentSource;
        //    if (null != uri)
        //    {
        //        String path = uri.OriginalString;
        //        if (uri.OriginalString != "Pages/HomePage.xaml")
        //        {
        //            PagePathUtils.GetInstance().Pop();
        //            if (CenterPage.NavigationService.CanGoBack)
        //            {
        //                CenterPage.NavigationService.GoBack();
        //            }
        //        }
        //    }
        //    Button btn = sender as Button;
        //    if (null != btn)
        //    {
        //        Subject sbj = btn.Tag as Subject;
        //        if (null != sbj)
        //        {
        //            mCurrentFolderName = GetFolderName(sbj);
        //            //传值
        //            Application.Current.Properties[Constant.PATH_SUBJECT] = mCurrentFolderName;
        //        }
        //    }
            
        //}

        private String GetFolderName(Subject subject)
        {
            if (null == subject)
            {
                return null;
            }
            return subject.ID + "@" + subject.Name;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            e.Handled = true;
            try
            {
                Uri uri = CenterPage.NavigationService.CurrentSource;
                if (null != uri)
                {
                    String path = uri.OriginalString;
                    if (uri.OriginalString != "Pages/HomePage.xaml")
                    {
                        if (CenterPage.NavigationService.CanGoBack)
                        {
                            RadioButton btn = sender as RadioButton;
                            if (null != btn && btn.IsChecked == true)
                            {
                                Subject sbj = btn.Tag as Subject;
                                if (null != sbj)
                                {
                                    mCurrentFolderName = GetFolderName(sbj);
                                    //传值
                                    Application.Current.Properties[Constant.PATH_SUBJECT] = mCurrentFolderName;
                                }
                            }
                            PagePathUtils.GetInstance().Pop();
                            CenterPage.NavigationService.GoBack();
                        }
                    }
                }
                else
                {
                    RadioButton btn = sender as RadioButton;
                    if (null != btn && btn.IsChecked == true)
                    {
                        Subject sbj = btn.Tag as Subject;
                        if (null != sbj)
                        {
                            mCurrentFolderName = GetFolderName(sbj);
                            //传值
                            Application.Current.Properties[Constant.PATH_SUBJECT] = mCurrentFolderName;
                            PagePathUtils.GetInstance().Pop();
                            CenterPage.NavigationService.Refresh();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }

        private void btn_Pre_Click(object sender, RoutedEventArgs e)
        {
            MutiPage2 mp = CenterPage.NavigationService.Content as MutiPage2;
            if (null != mp)
            {
                mp.GoPrevious();
                if (mp.ReachHeader())
                {
                    btn_pre.IsEnabled = false;
                }
            }
            btn_next.IsEnabled = true;
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            MutiPage2 mp = CenterPage.NavigationService.Content as MutiPage2;
            if (null != mp)
            {
                mp.GoNext();
                if (mp.ReachEnd())
                {
                    btn_next.IsEnabled = false;
                }
            }
            btn_pre.IsEnabled = true;
        }

        private void Page_Unloaded_1(object sender, RoutedEventArgs e)
        {
            if (null != timer)
            {
                timer.Stop();
            }
        }
    }
}
