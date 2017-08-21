using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;

namespace FKFZ
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        private int i = 0, n = 0;
        private List<Itemdata> idata = new List<Itemdata>();
        private ObservableCollection<Itemdata> otherdata = new ObservableCollection<Itemdata>();
        private Storyboard storyBoard;
        System.Threading.Timer _timer;
        delegate void UpdateTimer();


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            idata.Add(new Itemdata() { ent = "ceshi1", chs = "测试1", timespan = 5 });
            idata.Add(new Itemdata() { ent = "ceshi2", chs = "测试2", timespan = 10 });
            idata.Add(new Itemdata() { ent = "ceshi3", chs = "测试3", timespan = 16 });
            idata.Add(new Itemdata() { ent = "ceshi4", chs = "测试4", timespan = 19 });
            idata.Add(new Itemdata() { ent = "ceshi5", chs = "测试5", timespan = 27 });
            idata.Add(new Itemdata() { ent = "ceshi6", chs = "测试6", timespan = 36 });
            idata.Add(new Itemdata() { ent = "ceshi7", chs = "测试7", timespan = 50 });

            startStoryboard();
        }

        void startStoryboard()
        {
            GridPrev.DataContext = null;
            GridNow.DataContext = idata[i];
            if (idata.Count > 1)
            {
                GridNext.DataContext = idata[i + 1];
            }
            else
            {
                GridNext.DataContext = null;
            }
            for (int j = 2; j < idata.Count; j++)
            {
                otherdata.Add(idata[j]);
            }
            ItemsControl1.ItemsSource = otherdata;

            storyBoard = this.FindResource("Storyboard1") as Storyboard;

            _timer = new System.Threading.Timer(new TimerCallback(UpdatetimerDelegate));
            _timer.Change(0, 500);   
        }

        void UpdatetimerDelegate(object state)
        {
            if (idata.Count - i > 0)
            {
                if (n>=idata[i].timespan)
                {
                    i++;
                    this.Dispatcher.BeginInvoke(new UpdateTimer(StoryBoardFunc));
                }
                n++;
            }
        }

        void StoryBoardFunc()
        {
            storyBoard.Begin();
        }



        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonRepeat.IsEnabled = false;
            i = 0;
            n = 0;

            startStoryboard();
        }


        private void Storyboard1_OnCompleted(object sender, EventArgs e)
        {
            switch (idata.Count - i)
            {
                case 0:
                    GridPrev.DataContext = idata[i - 1];
                    GridNow.DataContext = null;
                    GridNext.DataContext = null;

                    _timer.Dispose();
                    ButtonRepeat.IsEnabled = true;
                    break;
                case 1:
                    GridPrev.DataContext = idata[i - 1];
                    GridNow.DataContext = idata[i];
                    GridNext.DataContext = null;
                    break;
                default:
                    GridPrev.DataContext = idata[i - 1];
                    GridNow.DataContext = idata[i];
                    GridNext.DataContext = idata[i + 1];
                    break;
            }
            if (otherdata.Count > 0)
            {
                otherdata.Remove(otherdata[0]);
            }
        }
    }

    public class Itemdata
    {
        public string ent { get; set; }
        public string chs { get; set; }
        public int timespan { get; set; }
    }
}
