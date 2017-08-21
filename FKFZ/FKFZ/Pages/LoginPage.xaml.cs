using FKFZ.Log;
using FKFZ.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace FKFZ.Pages
{
    /// <summary>
    /// LoadPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        #region Anim
        void AnimHand()
        {
            handImg.Visibility = Visibility.Visible;
            Storyboard myStoryboard = new Storyboard();
            DoubleAnimation OpacityDoubleAnimation = new DoubleAnimation();
            OpacityDoubleAnimation.From = 0;
            OpacityDoubleAnimation.To = 1;
            OpacityDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.4));
            Storyboard.SetTargetName(OpacityDoubleAnimation, handImg.Name);
            Storyboard.SetTargetProperty(OpacityDoubleAnimation, new PropertyPath(Image.OpacityProperty));

            handImg.RenderTransform = new TranslateTransform();
            DependencyProperty[] propertyChain = new DependencyProperty[]
            {
                Image.RenderTransformProperty,
                TranslateTransform.XProperty,
                TranslateTransform.YProperty
            };
            DoubleAnimation InDoubleAnimation = new DoubleAnimation();
            InDoubleAnimation.From = -100;
            InDoubleAnimation.To = 0;
            InDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.4));
            Storyboard.SetTargetName(InDoubleAnimation, handImg.Name);
            Storyboard.SetTargetProperty(InDoubleAnimation, new PropertyPath("(0).(1)", propertyChain));

            DoubleAnimation YAnimation = new DoubleAnimation();
            YAnimation.From = -100;
            YAnimation.To = 0;
            YAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.4));
            Storyboard.SetTargetName(YAnimation, handImg.Name);
            Storyboard.SetTargetProperty(YAnimation, new PropertyPath("(0).(2)", propertyChain));

            myStoryboard.Children.Add(OpacityDoubleAnimation);
            myStoryboard.Children.Add(InDoubleAnimation);
            myStoryboard.Children.Add(YAnimation);
            myStoryboard.Begin(this);
        }
        #endregion

        private void EnterBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["InParam"] = 20;
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Source = new Uri("Pages/InPage.xaml", UriKind.Relative);
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            ReadConfig();
            AnimHand();
        }

        void ReadConfig()
        {
            try {
                String organize =IniUtil.ReadIniData("LoadPage", "organize", "", AppDomain.CurrentDomain.BaseDirectory+"config.ini");
                if (null != organize && organize.Trim().Length > 0)
                {
                    tb1.Text = organize;
                }
                organize = IniUtil.ReadIniData("LoadPage", "company", "", AppDomain.CurrentDomain.BaseDirectory + "config.ini");
                if (null != organize && organize.Trim().Length > 0)
                {
                    organize = organize.Replace("&copy;", "©");
                    tb2.Text = organize;
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
        }
    }
}
