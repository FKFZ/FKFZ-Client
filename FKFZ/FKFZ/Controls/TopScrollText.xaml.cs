using FKFZ.Log;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FKFZ.Controls
{
    /// <summary>
    /// ScrollText.xaml 的交互逻辑
    /// </summary>
    public partial class TopScrollText : UserControl
    {
        public TopScrollText()
        {
            InitializeComponent();
           
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(String),
        typeof(TopScrollText), new PropertyMetadata("", new PropertyChangedCallback(OnTextValueChanged)));

        private static void OnTextValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TopScrollText dtb = (TopScrollText)d;
            dtb.Text = (String)e.NewValue;
        }

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set {
                SetValue(TextProperty, value);
                textBlock1.Text = value;
                CeaterAnimation(textBlock1);
            }
        }
        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double),
        typeof(TopScrollText), new PropertyMetadata(0.0, new PropertyChangedCallback(OnFontSizeChanged)));

        private static void OnFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TopScrollText dtb = (TopScrollText)d;
            dtb.FontSize = (double)e.NewValue;
        }
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set {
                SetValue(FontSizeProperty, value);
                textBlock1.FontSize = value;
            }
        }

        public static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(FontFamily),
        typeof(TopScrollText), new PropertyMetadata(null, new PropertyChangedCallback(OnFontFamilyChanged)));

        private static void OnFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TopScrollText dtb = (TopScrollText)d;
            dtb.FontFamily = (FontFamily)e.NewValue;
        }

        public FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set
            {
                SetValue(FontFamilyProperty, value);
                textBlock1.FontFamily = value;
            }
        }

        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush),
        typeof(TopScrollText), new PropertyMetadata(null, new PropertyChangedCallback(OnForegroundChanged)));

        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TopScrollText dtb = (TopScrollText)d;
            dtb.Foreground = (Brush)e.NewValue;
        }
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set {
                SetValue(ForegroundProperty, value);
                textBlock1.Foreground = value;
            }
        }

        //获取文字长度
        private double MeasureTextWidth(string text, double fontSize, string fontFamily)
        {
            FormattedText formattedText = new FormattedText(
            text,
            System.Globalization.CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface(fontFamily.ToString()),
            fontSize,
            Brushes.Black
            );
            return formattedText.WidthIncludingTrailingWhitespace;
        }
        Storyboard mStoryboard;
        private void CeaterAnimation(TextBlock text)
        {
            //创建动画资源
            mStoryboard = new Storyboard();

            double lenth = MeasureTextWidth(text.Text, text.FontSize, text.FontFamily.Source);
            if (lenth < canva1.Width)//90
            {
                textBlock1.SetValue(Canvas.LeftProperty, (canva1.Width - lenth) / 2);
                return;
            }
            //移动动画
            {
                DoubleAnimationUsingKeyFrames WidthMove = new DoubleAnimationUsingKeyFrames();
                Storyboard.SetTarget(WidthMove, text);
                DependencyProperty[] propertyChain = new DependencyProperty[]
                {
                    TextBlock.RenderTransformProperty,
                    TransformGroup.ChildrenProperty,
                    TranslateTransform.XProperty,
                };
                Storyboard.SetTargetProperty(WidthMove, new PropertyPath("(0).(1)[3].(2)", propertyChain));
                WidthMove.KeyFrames.Add(new EasingDoubleKeyFrame(20, KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0))));
                WidthMove.KeyFrames.Add(new EasingDoubleKeyFrame(canva1.Width - lenth-15, KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 4))));
                mStoryboard.Children.Add(WidthMove);
            }

            mStoryboard.RepeatBehavior = RepeatBehavior.Forever;
            mStoryboard.Begin();
        }

        private void UserControl_Unloaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (null != mStoryboard)
                {
                    mStoryboard.Stop();
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }
    }   
}
