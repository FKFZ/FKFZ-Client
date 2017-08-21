using FKFZ.Log;
using FKFZ.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace FKFZ.Controls
{
    /// <summary>
    /// RollingImageCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class RollingImageCtrl : UserControl
    {
        double timeChange, timeHold;
        public RollingImageCtrl()
        {
            InitializeComponent();
            InitTime();
        }

        void InitTime()
        {
            try
            {
                String str = IniUtil.ReadIniData("ListPage", "timehold", "0.0", AppDomain.CurrentDomain.BaseDirectory + "config.ini");
                if (null != str && str.Trim().Length > 0)
                {
                    try
                    {
                        timeHold = Double.Parse(str)/1000;
                    }
                    catch (Exception e)
                    {
                        RecordLog.RecordException(e);
                    }
                }
                str = IniUtil.ReadIniData("ListPage", "timechange", "0.0", AppDomain.CurrentDomain.BaseDirectory + "config.ini");
                if (null != str && str.Trim().Length > 0)
                {
                    try
                    {
                        timeChange = Double.Parse(str)/1000;
                    }
                    catch (Exception e)
                    {
                        RecordLog.RecordException(e);
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }
         /// <summary>  
        /// 动画静止时间  
        /// </summary>  
        public double TimeHold  
        {  
            get {
                if (timeHold > 0)
                {
                    return timeHold;
                }
                else
                {
                    return (double)GetValue(TimeHoldProperty); 
                }
            }  
            set { SetValue(TimeHoldProperty, value); }  
        }  
        public static readonly DependencyProperty TimeHoldProperty =  
            DependencyProperty.Register("TimeHold", typeof(double), // 注册属性名  
            typeof(RollingImageCtrl),    // 所属控件  
            new PropertyMetadata(5.0) // 默认值  
            );  
  
        /// <summary>  
        /// 动画播放时间  
        /// </summary>  
        public double TimeChange  
        {
            get
            {
                if (timeChange > 0)
                {
                    return timeChange;
                }
                else
                {
                    return (double)GetValue(TimeChangeProperty);
                }
            }
            set { SetValue(TimeChangeProperty, value); }  
        }  
        public static readonly DependencyProperty TimeChangeProperty =  
            DependencyProperty.Register("TimeChange", typeof(double), // 注册属性名  
            typeof(RollingImageCtrl),    // 所属控件  
            new PropertyMetadata(3.0));  // 默认值  
       
        /// <summary>  
        /// 动画类型  
        /// </summary>  
        public StoryTypes StoryType  
        {  
            get { return (StoryTypes)GetValue(StoryTypeProperty); }  
            set { SetValue(StoryTypeProperty, value); }  
        }  
        /// <summary>  
        /// 在xaml中操作属性，需要声明为DependencyProperty  
        /// </summary>  
        public static readonly DependencyProperty StoryTypeProperty =  
            DependencyProperty.Register("StoryType", typeof(StoryTypes),  
            typeof(RollingImageCtrl),  
            new PropertyMetadata(StoryTypes.RightToLeft)  
            );  
  
        /// <summary>  
        /// 是否开始滚动  
        /// </summary>  
        public bool isBegin = false;  

        private List<Image> _images;  
        /// <summary>  
        /// 滚动图片组  
        /// </summary>  
        public List<Image> Images  
        {  
            set  
            {  
                _images = value;                     
            }  
            get { return _images; }  
        }  
  
        /// <summary>  
        /// 滚动宽度  
        /// </summary>  
        public double width  
        {  
            get { return this.canvas_board.Width; }  
        }  
  
        /// <summary>  
        /// 滚动高度  
        /// </summary>  
        public double height  
        {  
            get { return this.canvas_board.Height; }  
        }  
  
        private int cIndex = 0;    // 滚动索引  

        private Storyboard _StoryboardRTL;  
        /// <summary>  
        /// 滚动动画板，从右到左  
        /// </summary>  
        public Storyboard StoryboardRTL  
        {  
            get  
            {
                if (_StoryboardRTL == null)
                {
                    rtl_img1 = getDAUKF(0.0, -this.width, image1, new PropertyPath("(Canvas.Left)"));
                    rtl_img2 = getDAUKF(this.width, 0.0, image2, new PropertyPath("(Canvas.Left)"));
                    _StoryboardRTL = new Storyboard();
                    _StoryboardRTL.Children.Add(rtl_img1);
                    _StoryboardRTL.Children.Add(rtl_img2);
                    _StoryboardRTL.FillBehavior = FillBehavior.Stop;
                    _StoryboardRTL.Completed += new EventHandler(storyboard_Completed);  
                }
                return _StoryboardRTL;  
            }  
        }
        private DoubleAnimationUsingKeyFrames ltr_img1,ltr_img2,rtl_img1,rtl_img2;
        private Storyboard _StoryboardLTR;  
        /// <summary>  
        /// 滚动动画板，从左到右  
        /// </summary>  
        public Storyboard StoryboardLTR  
        {  
            get  
            {
                if (_StoryboardLTR == null)  
                {
                    ltr_img1 = getDAUKF(0.0, this.width, image1, new PropertyPath("(Canvas.Left)"));
                    ltr_img2 = getDAUKF(-this.width, 0.0, image2, new PropertyPath("(Canvas.Left)"));
                    _StoryboardLTR = new Storyboard();
                    _StoryboardLTR.Children.Add(ltr_img1);
                    _StoryboardLTR.Children.Add(ltr_img2);
                    _StoryboardLTR.FillBehavior = FillBehavior.Stop;
                    _StoryboardLTR.Completed += new EventHandler(storyboard_Completed);  
                }
                return _StoryboardLTR;  
            }  
        }

        void BeginAnimation()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.From = this.Width;
            widthAnimation.To = 0;
            widthAnimation.Duration = TimeSpan.FromSeconds(5);
            canvas_board.BeginAnimation(Canvas.LeftProperty, widthAnimation);

        }
        /// <summary>  
        /// 获取动画中Double类型属性的关键帧组  
        /// </summary>  
        /// <param name="from">初始值</param>  
        /// <param name="to">目标值</param>  
        /// <param name="obj">动画控件</param>  
        /// <param name="path">动画属性</param>  
        /// <returns>关键帧组</returns>  
        DoubleAnimationUsingKeyFrames getDAUKF(double from, double to, DependencyObject obj, PropertyPath path)  
        {  
            DoubleAnimationUsingKeyFrames daukf = new DoubleAnimationUsingKeyFrames();  
            LinearDoubleKeyFrame k1 = new LinearDoubleKeyFrame(from, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(this.TimeHold)));  
            LinearDoubleKeyFrame k2 = new LinearDoubleKeyFrame(to, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(this.TimeHold + this.TimeChange)));  
            daukf.KeyFrames.Add(k1);  
            daukf.KeyFrames.Add(k2);
            Storyboard.SetTarget(daukf, obj);  
            Storyboard.SetTargetProperty(daukf, path);  
            return daukf;  
        }  
  
        // 动画结束时  
        void storyboard_Completed(object sender, EventArgs e)  
        {
            if (isBegin)
            {
                // 显示图片  
                ResetStory();
                // 继续下轮动画  
                BeginStory();
            }
        }
  
        /// <summary>  
        /// 开始滚动动画  
        /// </summary>  
        public void Begin()  
        {
            cIndex = 0;
            if (!isBegin)  
            {  
                isBegin = true;
                // 显示图片  
                ResetStory();  
  
                // 开始动画  
                BeginStory();  
            }
        }  
  
        /// <summary>  
        /// 开始播放动画  
        /// </summary>  
        void BeginStory()
        {
            switch (this.StoryType)  
            {  
                case StoryTypes.LeftToRight:
                    StoryboardLTR.Begin(this,true);
                    //StoryboardLTR.Begin(image1, true);  
                    //StoryboardLTR.Begin(image2, true); 
                    break;
                case StoryTypes.RightToLeft:
                    StoryboardRTL.Begin(this, true);
                    //StoryboardRTL.Begin(image1, true);
                    //StoryboardRTL.Begin(image2, true);
                    break;  
            }  
        }

         public void StopStory()
        {
            isBegin = false;
            if (StoryType == StoryTypes.LeftToRight)
            {
                StoryboardLTR.Stop(this);
                _StoryboardLTR = null;
            }
            if (StoryType == StoryTypes.RightToLeft)
            {
                StoryboardRTL.Stop(this);
                _StoryboardRTL = null;
            }

        }

        /// <summary>  
        /// 初始化动画版，显示动画中的图片  
        /// </summary>  
        void ResetStory()  
        {
            try
            {
                // 图片复位  
                switch (this.StoryType)
                {
                    case StoryTypes.LeftToRight:
                        this.image1.SetValue(Canvas.LeftProperty, 0.0);
                        this.image2.SetValue(Canvas.LeftProperty, -this.width);
                        break;
                    case StoryTypes.RightToLeft:
                        this.image1.SetValue(Canvas.LeftProperty, 0.0);
                        this.image2.SetValue(Canvas.LeftProperty, this.width);
                        break;
                }

                // 显示图片  
                if (null != Images && this.Images.Count > 0)
                {
                    try
                    {
                        try
                        {
                            if (cIndex >= Images.Count)
                            {
                                cIndex %= Images.Count;
                            }
                            WordChangedEventArgs ca = new WordChangedEventArgs(TitleChangeEvent, this);
                            ca.index = cIndex;
                            RaiseEvent(ca);
                        }
                        catch (Exception ex)
                        {
                            RecordLog.RecordException(ex);
                        }
                        int i = cIndex++ % this.Images.Count;
                        Debug.WriteLine(String.Format("image1,{0}", i));
                        if (null != Images[i])
                        {
                            this.image1.Source = Images[i].Source;
                        }
                        else
                        {
                            Images.RemoveAt(i);
                        }
                        if (cIndex >= Images.Count)
                        {
                            cIndex %= Images.Count;
                        }
                        i = cIndex % this.Images.Count;
                        Debug.WriteLine(String.Format("image2,{0}", i));
                        if (null != Images[i])
                        {
                            this.image2.Source = Images[i].Source;
                        }
                        else
                        {
                            Images.RemoveAt(i);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.image1.Source = new BitmapImage();
                        this.image2.Source = new BitmapImage();
                    }
                }
                else
                {
                    this.image1.Source = new BitmapImage();
                    this.image2.Source = new BitmapImage();
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }

        public void StartStory(StoryTypes type)
        {
            StoryType = type;
        }

        public void StopStory(StoryTypes type)
        {
            StoryType = type;
            StopStory();
        }

        public int GetCurrentPlayIndex()
        {
            if (null != Images && cIndex >= Images.Count)
            {
                cIndex %= Images.Count;
            }
            return cIndex;
        }

        private void image1_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            if(null != img)
            {
                try
                {
                    if (cIndex >= Images.Count)
                    {
                        cIndex %= Images.Count;
                    }
                    ClickEventArgs ca = new ClickEventArgs(ClickEvent, this);
                    ca.index = cIndex;
                    RaiseEvent(ca);
                }
                catch (Exception ex)
                {
                    RecordLog.RecordException(ex);
                }
            }
        }

        //声明和注册路由事件
        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(EventHandler<ClickEventArgs>), typeof(RollingImageCtrl));
        //CLR事件包装
        public event RoutedEventHandler Click
        {
            add { this.AddHandler(ClickEvent, value); }
            remove { this.RemoveHandler(ClickEvent, value); }
        }

        //声明和注册路由事件
        public static readonly RoutedEvent TitleChangeEvent =
            EventManager.RegisterRoutedEvent("TitleChange", RoutingStrategy.Bubble, typeof(EventHandler<WordChangedEventArgs>), typeof(RollingImageCtrl));
        //CLR事件包装
        public event RoutedEventHandler TitleChange
        {
            add { this.AddHandler(TitleChangeEvent, value); }
            remove { this.RemoveHandler(TitleChangeEvent, value); }
        }

    }

    public class ClickEventArgs : RoutedEventArgs
    {
        public ClickEventArgs(RoutedEvent routedEvent, object source):base(routedEvent, source)
        { 
        
        }
        public int index { get; set; }
    }
    /// <summary>
    /// 切换文本标题
    /// </summary>
    public class WordChangedEventArgs : RoutedEventArgs
    {
        public WordChangedEventArgs(RoutedEvent routedEvent, object source)
            : base(routedEvent, source)
        {

        }
        public int index { get; set; }
    }
    // 枚举动画类型  
    public enum StoryTypes
    {
        /// <summary>  
        /// 从右向左  
        /// </summary>  
        RightToLeft = 1,
        /// <summary>  
        /// 从左向右  
        /// </summary>  
        LeftToRight
    }  
}
