using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FKFZ.Controls
{
    /// <summary>
    /// SliderProgressBar.xaml 的交互逻辑
    /// </summary>
    public partial class SliderProgressBar : UserControl
    {
        public SliderProgressBar()
        {
            InitializeComponent();
            mSlider.AddHandler(Slider.MouseLeftButtonUpEvent, new MouseButtonEventHandler(Slider_MouseLeftButtonUp), true);
            mSlider.AddHandler(Slider.MouseLeftButtonDownEvent, new MouseButtonEventHandler(Slider_MouseLeftButtonDown), true);
            mSlider.IsSnapToTickEnabled = false;
            //mSlider.Visibility = Visibility.Collapsed;
            mSlider.Value = 0;
            mSlider.Maximum = 1;
            mSlider.Minimum = 0;
            mSlider.TickFrequency = 0.01;
        }
        //声明和注册路由事件\
        public static readonly RoutedEvent ValueChangeEvent =
            EventManager.RegisterRoutedEvent("ValueChange", RoutingStrategy.Bubble, typeof(EventHandler<PercentRoutedEventArgs>), typeof(SliderProgressBar));
        //CLR事件包装
        public event RoutedEventHandler ValueChange
        {
            add { this.AddHandler(ValueChangeEvent, value); }
            remove { this.RemoveHandler(ValueChangeEvent, value); }
        }

        public double Value
        {
            get
            {
                return Progress.Value;
            }

            set
            {
                Progress.Value = value;
                mSlider.Value = value;
            }
        }
        private void Slider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Slider slider = sender as Slider;
            MouseEventArgs mea = e as MouseEventArgs;
            double x = mea.GetPosition(slider).X;// 获得Mouse对于Slider的位置            
            double value = slider.Minimum + (double)(x) / slider.ActualWidth * (slider.Maximum - slider.Minimum);// 计算当前Mouse相对于Slider的比例并计算出值。

            Progress.Value = value;
            PercentRoutedEventArgs args = new PercentRoutedEventArgs(ValueChangeEvent, this);
            args.Percent = value;
            args.MouseEvent = EventType.Up;
            RaiseEvent(args);
        }

        private void Slider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Progress.Value = mSlider.Value;
            PercentRoutedEventArgs args = new PercentRoutedEventArgs(ValueChangeEvent, this);
            args.Percent = mSlider.Value;
            args.MouseEvent = EventType.Down;
            RaiseEvent(args);
        }

        private void UserControl_MouseLeave_1(object sender, MouseEventArgs e)
        {
            //mSlider.Visibility = Visibility.Collapsed;
        }

        private void UserControl_MouseEnter_1(object sender, MouseEventArgs e)
        {
            //mSlider.Visibility = Visibility.Visible;
        }

    }

    public class PercentRoutedEventArgs : RoutedEventArgs
    {
        public PercentRoutedEventArgs(RoutedEvent routedEvent, object source)  
            :base(routedEvent,source){}
        public double Percent { get; set; }
        public EventType MouseEvent { get; set; }
    }
    public enum EventType
    {
        Down,
        Up,
        Move
    }
}
