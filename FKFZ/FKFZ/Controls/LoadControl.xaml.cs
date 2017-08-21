using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace FKFZ.Controls
{
    /// <summary>
    /// LoadControl.xaml 的交互逻辑
    /// </summary>
    public partial class LoadControl : UserControl
    {
        public LoadControl()
        {
            InitializeComponent();
            Timeline.DesiredFrameRateProperty.OverrideMetadata(
                       typeof(Timeline),new FrameworkPropertyMetadata { DefaultValue = 20 });
        }
    }
}
