//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using System.Windows.Threading;

//namespace FKFZ.Controls
//{
//    /// <summary>
//    /// AdvertPicCtrl.xaml 的交互逻辑
//    /// </summary>
//    public partial class AdvertPicCtrl : UserControl
//    {
//        public AdvertPicCtrl()
//        {
//            InitializeComponent();
//        }
//        #region 加载List数据
//        /// <summary>
//        /// 当前图片地址播放列表
//        /// </summary>
//        private static List<string> currentList;
//        public static DependencyProperty advertPicList = DependencyProperty.Register("advertPicList", typeof(List<string>), typeof(AdvertPicCtrl)
//             , new PropertyMetadata(new PropertyChangedCallback(loadAdvertPic)));

//        public List<string> AdvertPicList
//        {
//            get { return (List<string>)GetValue(advertPicList); }
//            set { SetValue(advertPicList, value); }
//        }

//        private static void loadAdvertPic(DependencyObject sender, DependencyPropertyChangedEventArgs e)
//        {
//            AdvertPicCtrl advertPicControl = (AdvertPicCtrl)sender;
//            if (e.Property == advertPicList)
//            {
//                advertPicControl.AdvertPicList = (List<string>)e.NewValue;
//                currentList = advertPicControl.AdvertPicList;
//            }
//        }

//        #endregion

//        #region 加载图片停留时间
//        /// <summary>
//        /// 当前图片地址播放列表
//        /// </summary>
//        private static List<int> currentTimeList;
//        public static DependencyProperty advertPicStayTime = DependencyProperty.Register("advertPicStayTime", typeof(List<string>), typeof(AdvertPicCtrl)
//             , new PropertyMetadata(new PropertyChangedCallback(loadAdvertPic)));

//        public List<int> AdvertPicStayTime
//        {
//            get { return (List<int>)GetValue(advertPicStayTime); }
//            set { SetValue(advertPicStayTime, value); }
//        }

//        private static void loadAdvertStayTime(DependencyObject sender, DependencyPropertyChangedEventArgs e)
//        {
//            AdvertPicCtrl advertPicControl = (AdvertPicCtrl)sender;
//            if (e.Property == advertPicStayTime)
//            {
//                advertPicControl.AdvertPicList = (List<string>)e.NewValue;
//                currentList = advertPicControl.AdvertPicList;
//            }
//        }
//        #endregion

//        #region 注册自定义事件和参数
//        public static readonly RoutedEvent AdvertPicPlayStateChangedEvent;
//        public class AdvertPicPlayEventArgs : RoutedEventArgs
//        {
//            public int playState { get; set; }
//            public int playLength { get; set; }
//            public int playIndex { get; set; }
//        }

//        static AdvertPicCtrl()
//        {
//            AdvertPicPlayStateChangedEvent = EventManager.RegisterRoutedEvent("AdvertPicPlayStateChanged",
//                RoutingStrategy.Bubble, typeof(AdvertPicPlayStateChangedHandler), typeof(AdvertPicCtrl));
//        }
//        public delegate void AdvertPicPlayStateChangedHandler(object sender, AdvertPicPlayEventArgs e);
//        public event AdvertPicPlayStateChangedHandler AdvertPicPlayStateChanged
//        {
//            add { AddHandler(AdvertPicCtrl.AdvertPicPlayStateChangedEvent, value); }
//            remove { RemoveHandler(AdvertPicCtrl.AdvertPicPlayStateChangedEvent, value); }
//        }
//        #endregion

//        int i = 0;
//        DispatcherTimer switchPicTimer = new DispatcherTimer();
//        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
//        {
//            switchPicTimer.Tick += SwitchPicEvent;
//            for (int j = 0; j < currentList.Count; j++)
//            { 
//                Button btn=new Button();
//                btn.Width = 20;
//                btn.Height = 20;
//                btn.Content = j+1;
//                btn.Tag = j;
//                btn.Click+=new RoutedEventHandler(btn_Click);
//                PicCountNum.Children.Add(btn);
//            }
//        }

//        void btn_Click(object sender, RoutedEventArgs e)
//        { 
//            Button btn = (Button) sender;
//            BitmapImage bitmap = new BitmapImage(new Uri(currentList[Convert.ToInt32( btn.Tag)], UriKind.Absolute));
//            imgAdvertPic.Stretch = Stretch.Fill;
//            imgAdvertPic.Source = bitmap;
        
//        }

//        /// <summary>
//        /// 开始播放
//        /// </summary>
//        /// <param name="interval">图片切换时间</param>
//        public void Play(int interval)
//        {
//            int defaultinterval = 0;
//            if (interval != 0)
//                defaultinterval = interval;

//            switchPicTimer.IsEnabled = true;
//            switchPicTimer.Interval = new TimeSpan(0, 0, defaultinterval);
//            switchPicTimer.Start();
//            i = 0;
//        }

//        /// <summary>
//        /// 停止播放
//        /// </summary>
//        public void Stop()
//        {
//            switchPicTimer.IsEnabled = false;
//            switchPicTimer.Stop();
//        }

//        /// <summary>
//        /// 切换图片事件
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void SwitchPicEvent(object sender, EventArgs e)
//        {
//            if (null != currentList)
//            {
//                // Console.WriteLine("开始切换~~~");
//                if (i <= currentList.Count - 1)//修改实现循环播放。
//                {
//                    DoHandlerStop(Image.OpacityProperty, 20, 0, 4, imgAdvertPic, SwitchPic);
//                }
//                else
//                {
//                    //AdvertPicPlayEventArgs args = new AdvertPicPlayEventArgs();
//                    //args.RoutedEvent = AdvertPicPlayStateChangedEvent;
//                    //args.playState = 1;
//                    //RaiseEvent(args);
//                    // switchPicTimer.Stop();
//                    // switchPicTimer.IsEnabled = false;
//                    i = 0;
//                    DoHandlerStop(Image.OpacityProperty, 20, 0, 4, imgAdvertPic, SwitchPic);

//                }
//                if (null != currentTimeList)
//                {
//                    Thread.Sleep(currentTimeList[i]); //图片停留时间
//                }
//            }
//        }

//        /// <summary>
//        /// 动画播放完毕切换图片
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void SwitchPic(object sender, EventArgs e)
//        {
//            BitmapImage bitmap = new BitmapImage(new Uri(currentList[i], UriKind.Absolute));
//            imgAdvertPic.Stretch = Stretch.Fill;
//            imgAdvertPic.Source = bitmap;
//            if (i < currentList.Count)
//            {
//                i++;
//            }

//        }

//        public void ClickToPic(int id)
//        {

//        }


//        /// <summary>
//        /// 动画
//        /// </summary>
//        /// <param name="dp"></param>
//        /// <param name="from"></param>
//        /// <param name="to"></param>
//        /// <param name="duration"></param>
//        /// <param name="element"></param>
//        /// <param name="complateHander"></param>
//        public void DoHandlerStop(DependencyProperty dp, double from, double to, double duration, UIElement element, EventHandler complateHander)
//        {
//            DoubleAnimation doubleAnimation = new DoubleAnimation();//创建双精度动画对象
//            doubleAnimation.From = from;
//            doubleAnimation.To = to;//设置动画的结束值
//            doubleAnimation.Duration = TimeSpan.FromSeconds(duration);//设置动画时间线长度
//            doubleAnimation.FillBehavior = FillBehavior.Stop;//设置动画完成后执行的操作 
//            doubleAnimation.Completed += complateHander;
//            element.BeginAnimation(dp, doubleAnimation);//设置动画应用的属性并启动动画
//        }
//    }
//}
