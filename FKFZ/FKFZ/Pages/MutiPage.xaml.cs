using FKFZ.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FKFZ.Pages
{
    /// <summary>
    /// MutiPage.xaml 的交互逻辑
    /// </summary>
    public partial class MutiPage : Page
    {
        DispatcherTimer timer = null;
        double mTotalSecond;
        public MutiPage()
        {
            InitializeComponent();
            //绑定音频结束时的事件  
            AudioPlayer.MediaEnded += new RoutedEventHandler(Element_MediaEnded);  
        }

        #region


        ObservableCollection<LrcInfo> lrcList = new ObservableCollection<LrcInfo>();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var mp4_path = AppDomain.CurrentDomain.BaseDirectory + "SayGoodbye.mp3";
            AudioPlayer.Source = new Uri(mp4_path, UriKind.RelativeOrAbsolute);
            AudioPlayer.Play();
            var lrc_path = AppDomain.CurrentDomain.BaseDirectory + "SayGoodbye.lrc";
            ReadLyric(lrc_path);
        }
        //读取lrc文件  
        private void ReadLyric(string filelyric)
        {
            string lrc = File.ReadAllText(filelyric, System.Text.Encoding.GetEncoding("GB2312"));
            Regex rx = new Regex(@"(?<=^\[)(\d+:\d+\.\d+).(.+)(?=$)", RegexOptions.Multiline);
            int i = 0;
            //匹配表达式  
            foreach (Match x in rx.Matches(lrc))
            {
                try
                {
                    //读取时间  
                    TimeSpan ti = new TimeSpan(0, int.Parse(x.Value.Substring(0, 2)),int.Parse(x.Value.Substring(3, 2)));
                    //读取歌词  
                    string content = x.Value.Substring(9);
                    lrcList.Add(new LrcInfo(i,ti, content));
                    appendLine(i,null, content);
                    i++;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }
            }
          
            RTB.Document = doc;              
        }

        // Play the media.
        void OnMouseDownPlayMedia(object sender, MouseButtonEventArgs args)
        {
            var mp4_path = AppDomain.CurrentDomain.BaseDirectory + "video.mp4";
            AudioPlayer.Source = new Uri(mp4_path, UriKind.RelativeOrAbsolute);
            AudioPlayer.Play();

        }

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
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            //获取指定行的内容
            BlockCollection bc = RTB.Document.Blocks;
            int index = FindPlayLrcIndex();
            if (index == pIndex||index == -1)
            {
                return;
            }
            int i = 0;
            TextElement prev = null;
            foreach (TextElement item in bc)
            {
                //修改当前行的样式
                if (i == index-1)
                {
                    AlterStyle(item, prev);
                }
                if (i == 0)
                {
                    Paragraph cP = item as Paragraph;
                    cP.Foreground = Brushes.White;
                }
                prev = item;
                i++;
            }
            pIndex = index;

        }

        private int FindPlayLrcIndex()
        {
            int count = lrcList.Count;
            int i= pIndex == -1 ? 0:pIndex;
            for(;i<count;i++)
            {
                if (AudioPlayer.Position <= lrcList[i].Time)
                {
                    return lrcList[i].Line;
                }
            }
            return -1;
        }
        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            AudioPlayer.Stop();
        }

        // Jump to different parts of the media (seek to). 
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            //int SliderValue = (int)timelineSlider.Value;

            // Overloaded constructor takes the arguments days, hours, minutes, seconds, miniseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
            //TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            //AudioPlayer.Position = ts;
        }

        #endregion

        #region

        FlowDocument doc = new FlowDocument();
        private void appendLine(int line,string name, string content)
        {
            Paragraph p = new Paragraph();
            if (string.IsNullOrEmpty(name) == false)
                doc.RegisterName(name, p);
            Run r = new Run(content);// Run 是一个 Inline 的标签  
            p.TextAlignment = TextAlignment.Center;
            p.Inlines.Add(r);
            if (line == 0)
            {
                p.Foreground = Brushes.Red;//设置字体颜色  
            }
            doc.Blocks.Add(p);
            doc.LineHeight = 4;// FontSize;
        }

        int pIndex = -1;
        double curTop = 0;
        private void AlterStyle(TextElement item, TextElement prev)
        {
            //当前行
            Paragraph cP = item as Paragraph;
            cP.Foreground = Brushes.Red;
            TextRange range = new TextRange(cP.ContentStart, cP.ContentEnd);
            //滚动位置控制
            if (pIndex > 0 && range.Text.Length > 0)
            {
                //上一行，样式回调
                if (prev != null)
                {
                    prev.Foreground = Brushes.White;
                }

                curTop += range.Text.Length > 7 ? 50 : 20;
                curTop += 40;
                RTB.ScrollToVerticalOffset(curTop);
            }
        }
        #endregion
    }
}
