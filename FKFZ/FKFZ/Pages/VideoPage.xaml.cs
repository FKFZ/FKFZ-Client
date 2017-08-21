using FKFZ.Controls;
using FKFZ.Log;
using FKFZ.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using Vlc.DotNet.Core.Interops;

namespace FKFZ.Pages
{
    /// <summary>
    /// VideoPage.xaml 的交互逻辑
    /// </summary>
    public partial class VideoPage : Page
    {
        String mVideoPath;
        double mTotalSecond;

        public VideoPage()
        {
            InitializeComponent();
            myControl.MediaPlayer.PositionChanged += MediaPlayer_PositionChanged;
            myControl.MediaPlayer.EncounteredError += MediaPlayer_EncounteredError;
            myControl.MediaPlayer.Playing += MediaPlayer_Playing;
            myControl.MediaPlayer.EndReached += MediaPlayer_EndReached;
            myControl.MediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
            //初始化配置，指定引用库  
            myControl.MediaPlayer.VlcLibDirectoryNeeded += OnVlcControlNeedsLibDirectory;
            try
            {
                myControl.MediaPlayer.EndInit();
            }
            catch (Exception e)
            {
                RecordLog.RecordException(e);
            }
        }
        VlcManager manager;
        private void OnVlcControlNeedsLibDirectory(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)  
         { 
            var currentAssembly = Assembly.GetEntryAssembly(); 
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
            {
                return;
            }
            if (AssemblyName.GetAssemblyName(currentAssembly.Location).ProcessorArchitecture == ProcessorArchitecture.Amd64)
            {
                e.VlcLibDirectory = new DirectoryInfo(currentDirectory + @"\lib\x64\");
            }
            else
            {
                e.VlcLibDirectory = new DirectoryInfo(currentDirectory + @"\lib\x86\");
            }

            manager = VlcManager.GetInstance(e.VlcLibDirectory);
        }  
        string GetAssemblyDir()
        {
            Assembly currentAssembly = Assembly.GetEntryAssembly(); 
            string currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
            {
                return null;
            }
            return currentDirectory;
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

        void MediaPlayer_Playing(object sender, Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs e)
        {
            myControl.MediaPlayer.Video.AspectRatio = String.Format("{0}:{1}", myControl.ActualWidth, myControl.ActualHeight);
            mTotalSecond = myControl.MediaPlayer.Length;
            Dispatcher.Invoke(new Action(() =>
            {
                TimeSpan ts = new TimeSpan(0, 0, (int)myControl.MediaPlayer.Length / 1000);
                TBDur.Text = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            }));
        }

        long lastSecond = 0;
        void MediaPlayer_TimeChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerTimeChangedEventArgs e)
        {
            long current = e.NewTime / 1000;
            if (current - lastSecond > 0)
            {
                lastSecond = current;
                Dispatcher.Invoke(new Action(() =>
               {
                   TimeSpan ts = new TimeSpan(0, 0, (int)current);
                   //if (ts.Seconds == 6)
                   //{
                   //    Status.Visibility = Visibility.Collapsed;
                   //}
                   TBProgress.Text = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
               }));
            }
        }

        double lastPos = 0.0;
        void MediaPlayer_PositionChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerPositionChangedEventArgs e)
        {
            if (e.NewPosition - lastPos > 0.01)
            {
                lastPos = e.NewPosition;
                Dispatcher.Invoke(new Action(() =>
                {
                    spb.Value = e.NewPosition + 0.01;
                }));
            }
        }

        void MediaPlayer_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (NavigationService.CanGoBack)
                {
                    //回退一级
                    PagePathUtils.GetInstance().Pop();
                    NavigationService.GoBack();
                }
            }));
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
            string[] options = new string[] {
                             //":no-video",
                             //":no-audio",
                             //":directx-use-sysmem",
                             //":autoscale",
                             //":direct3d-desktop",
                             //":dummy", 
                             ":ignore-config",
                             //"--no-video-title"
                             //":video-title-position=4",
                             //":sout=#duplicate{dst=display,dst=\'transcode{venc=x264{profile=baseline},vcodec=h264,vb=10,width=320,height=240,fps=10,scale=1}:rtp{dst=127.0.0.1,port=1234,mux=ts}\'}",
                             //":sout-ts-dts-delay=30"
                             //":sout-transcode-deinterlace",
                             //":yuv-yuv4mpeg2",
                             //":sout-transcode-audio-sync",
                             //":sout-dirac-quality=10.0",
                             //":sout-dirac-lossless",
                             //":sout-dirac-quality=50000",
                             //":ffmpeg-workaround-bugs=1",
                             //":sout-standard-mux=ts",
                             //":sout-transcode-vb=189000",
                             //":sout-transcode-deinterlace"
                             //":sout-x264-profile=baseline",//baseline,main,high
                             //":sout=#duplicate{dst=display}",
                             //":dshow-caching=5",
                             //":imem-caching=2"
                             //":sout-ffmpeg-noise-reduction=5",
                             //":sout-x264-preset=ultrafast",

                             //":sout-mux-caching=3",
                             //":sout-transcode-deinterlace-mode=ffmpeg-deinterlace",
                             //":no-sout-transcode-hurry-up",
                             //":sout-transcode-vcodec=h264",
                             //":sout-x264-scenecut=100",
                             //":ffmpeg-lowres=2",
                             //":ffmpeg-fast",
                             //":dshow-fps=50",
                             //":sout-transcode-fps=50",
                             //":sout-transcode-scale=0.25",
                             //":ffmpeg-skiploopfilter=4",
                             //":file-caching=1500",
                             //":no-kate-formatted",
                             //":sout-mux-caching=1500",
                             //":ffmpeg-skip-idct=3",
                             //":ffmpeg-skip-frame=3",
                             //":ffmpeg-hw",
                             //":sout-dirac-prefilter=rectlp",
                             //":sout-dirac-prefilter-strength=10",
                             //":sout-transcode-high-priority",
                             //":sout-transcode-threads=3",
                             ":overlay",
                             ":directx-hw-yuv",
                             ":directx-3buffering",
                             ":sout-qsv-software",
                             ":direct3d-hw-blending",
                             //":avcodec-skip-frame=0",
                             //":avcodec-skip-idct=0",
                             ":avcodec-skiploopfilter=3",
                             //":no-avcodec-hurry-up",
                             ":avcodec-threads=0",
                             ":avcodec-error-resilience=0",
                             };
            FileInfo file = new FileInfo(mVideoPath);
            if (!file.Exists)
            {
                RecordLog.RecordException(new Exception("file not exists:path =" + mVideoPath));
                return;
            }
            myControl.MediaPlayer.Play(file,options);
            btnPlay.Visibility = Visibility.Hidden;
            Status.Visibility = Visibility.Visible;
        }

        long last;
        private void Slider_PercentChange(object sender, PercentRoutedEventArgs e)
        {
            e.Handled = true;
            switch (e.MouseEvent)
            { 
                case EventType.Down:
                break;
                case EventType.Up:
                    long sp = (long)(mTotalSecond * e.Percent);
                    if (e.Percent == 1.0 || e.Percent == 0.0)
                    {
                        return;
                    }
                    Debug.WriteLine(String.Format("SliderValue = {0},e.Percent = {1}", sp,e.Percent));
                    myControl.MediaPlayer.Time = sp;
                    spb.Value = e.Percent;
                    last = sp;
                break;
                case EventType.Move:
                    
                break;
            }
            
        }
        
        void MediaPlayer_EncounteredError(object sender, Vlc.DotNet.Core.VlcMediaPlayerEncounteredErrorEventArgs e)
        {
            String errMsg = manager.GetLastErrorMessage();
            RecordLog.RecordException(new Exception("MediaPlayer_EncounteredError:"+errMsg));
        }

        private void Page_Unloaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (myControl.MediaPlayer.IsPlaying)
                {
                    myControl.MediaPlayer.Stop();
                }
                myControl.MediaPlayer.Dispose();
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }
    }
}
