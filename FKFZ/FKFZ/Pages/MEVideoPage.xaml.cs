using FKFZ.Controls;
using FKFZ.Log;
using FKFZ.Utils;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace FKFZ.Pages
{
    /// <summary>
    /// VideoPage.xaml 的交互逻辑
    /// </summary>
    public partial class MEVideoPage : Page
    {
        String mVideoPath;
        DispatcherTimer timer = null;
        double mTotalSecond;

        public MEVideoPage()
        {
            InitializeComponent();
            Player.MouseLeftButtonUp += Player_MouseLeftButtonUp;
        }
        
        void Player_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Status.Visibility == Visibility.Visible)
            {
                Status.Visibility = Visibility.Hidden;
            }
            else
            {
                Status.Visibility = Visibility.Visible;
            }
        }        

        // Play the media.
        void OnMouseDownPlayMedia(object sender, MouseButtonEventArgs args)
        {
            //var mp4_path = AppDomain.CurrentDomain.BaseDirectory + mVideoPath;// "video.mp4";
            //Player.Source = new Uri(mp4_path, UriKind.RelativeOrAbsolute);
            //Player.Play();
            //InitializePropertyValues();
            
        }
        
        void OnMouseDownPauseMedia(object sender, MouseButtonEventArgs args)
        {
            Player.Pause();

        }

        void OnMouseDownStopMedia(object sender, MouseButtonEventArgs args)
        {
            Player.Stop();

        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            Player.Volume = (double)volumeSlider.Value;
        }

        private void ChangeMediaSpeedRatio(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            Player.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void Element_MediaOpened(object sender, EventArgs e)
        {
            mTotalSecond = Player.NaturalDuration.TimeSpan.TotalSeconds;

            //timelineSlider.Maximum = Player.NaturalDuration.TimeSpan.TotalMilliseconds;
            //Slider.Maximum = Player.NaturalDuration.TimeSpan.TotalMilliseconds;
            TBDur.Text = string.Format("{0}{1:00}:{2:00}:{3:00}", "时长：", Player.NaturalDuration.TimeSpan.Hours, Player.NaturalDuration.TimeSpan.Minutes, Player.NaturalDuration.TimeSpan.Seconds);
            //媒体文件打开成功
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
        }
        int i = 0;
        private void timer_tick(object sender, EventArgs e)
        {
            i++;
            if (i == 6)
            {
                Status.Visibility = Visibility.Collapsed;
            }
            TBProgress.Text = string.Format("{0}{1:00}:{2:00}:{3:00}", "进度：", Player.Position.Hours, Player.Position.Minutes, Player.Position.Seconds);
            if (mTotalSecond != 0)
            {
                Progress.Value = Player.Position.TotalSeconds / mTotalSecond;
                spb.Value = Player.Position.TotalSeconds / mTotalSecond;
            }
        }

        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            Player.Stop();
            Player.Source = null;
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

        // Jump to different parts of the media (seek to). 
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            //int SliderValue = (int)Slider.Value;
            //Debug.WriteLine(String.Format("SliderValue = {0}", SliderValue));
            //TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            //Player.Position = ts;
        }

        void InitializePropertyValues()
        {
            Player.Volume = (double)volumeSlider.Value;
            Player.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            PlayVideo();
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            mVideoPath = Application.Current.Properties[Constant.PATH_VIDEO] as String;
            PlayVideo();
        }

        void PlayVideo()
        {
            //byte[] inArray = System.Text.Encoding.UTF8.GetBytes(mVideoPath);
            //string fileName = Convert.ToBase64String(inArray);
            //var uriStr = Uri.EscapeUriString("http://127.0.0.1:3579/file/get?path=" + fileName);
            //Player.Source = new Uri(uriStr, true);
            Player.Source = new Uri(mVideoPath, UriKind.RelativeOrAbsolute);
            Player.Play();

            InitializePropertyValues();
            btnPlay.Visibility = Visibility.Hidden;
        }

        long last;
        private void Slider_PercentChange(object sender, PercentRoutedEventArgs e)
        {
            e.Handled = true;
            switch (e.MouseEvent)
            { 
                case EventType.Down:
                    if (null != timer)
                    {
                        timer.Stop();
                    }
                break;
                case EventType.Up:
                    if (null != timer)
                    {
                        timer.Start();
                    }
                    long sp = (long)(mTotalSecond * e.Percent);
                    if (e.Percent == 1.0 || e.Percent == 0.0)
                    {
                        return;
                    }
                    Debug.WriteLine(String.Format("SliderValue = {0},e.Percent = {1}", sp,e.Percent));
                    Player.Position = new TimeSpan(0, 0, 0, (int)sp);
                    spb.Value = e.Percent;
                    last = sp;
                    i = 0;
                break;
                case EventType.Move:
                    
                break;
            }
            
        }

        private void Element_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            RecordLog.RecordException(e.ErrorException);
        }
    }
}
