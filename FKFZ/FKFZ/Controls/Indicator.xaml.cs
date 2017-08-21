using FKFZ.XmlModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FKFZ.Controls
{
    /// <summary>
    /// LoadControl.xaml 的交互逻辑
    /// </summary>
    public partial class Indicator : UserControl, INotifyPropertyChanged
    {
        List<Ellipse> mElls = new List<Ellipse>();
        Color gray = Color.FromRgb(166,166,166);
        Color red = Color.FromRgb(233,34,34);
        Color green = Color.FromRgb(104,200,0);
        public Indicator()
        {
            InitializeComponent();
        }
        #region 自定义依赖属性
        public int Total
        {
            get { return (int)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        public static readonly DependencyProperty TotalProperty =
            DependencyProperty.Register("Total", typeof(int), typeof(Indicator), new UIPropertyMetadata(0));



        public int Index
        {
            get { return (int)GetValue(PageIndexProperty); }
            set
            {
                SetValue(PageIndexProperty, value);
            }
        }

        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("Index", typeof(int), typeof(Indicator), new UIPropertyMetadata(1));

        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// ItemsSource数据源
        /// </summary>
        public static DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<object>), typeof(Indicator), new UIPropertyMetadata(null, new PropertyChangedCallback(OnItemsSourceChanged)));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Indicator indicator = (Indicator)d;
            indicator.InitUI();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        public void InitUI()
        {
            if (null != ItemsSource)
            {
                ClearCanvasChild();
                double cw = (int)canvas.ActualWidth;
                double ch = (int)canvas.ActualHeight;
                double radius = 20;
                int count = ItemsSource.Count();
                double unitW = cw / count;
                double offset = (unitW - radius) / 2;
                AddRectangle("rc", cw / 2, ch / 2, cw - offset*2, 15);
                for (int i = 0; i < count; i++)
                {
                    AddEllipse("e" + i, radius, i * unitW + offset, (ch - radius * 2) / 2);
                }
            }
        }

        void ClearCanvasChild()
        {
            if (null != canvas)
            {
                int count = canvas.Children.Count;
                while (canvas.Children.Count>0)
                {
                    canvas.Children.RemoveAt(0);
                }
            }
        }

        void AddEllipse(String name, double radias, double left, double top)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Name = name;
            ellipse.Width = ellipse.Height = radias * 2;
            ellipse.Fill = new SolidColorBrush(gray);
            ellipse.SetValue(Canvas.LeftProperty,left);
            ellipse.SetValue(Canvas.TopProperty,top);
            canvas.Children.Add(ellipse);
            mElls.Add(ellipse);
        }
        void AddRectangle(String name, double cx, double cy, double width, double height)
        {
            Rectangle rect = new Rectangle();
            rect.Name = name;
            rect.Fill = new SolidColorBrush(gray);
            rect.Width = width;
            rect.Height = height;
            rect.SetValue(Canvas.LeftProperty, cx - width / 2+5);
            rect.SetValue(Canvas.TopProperty, cy - height / 2);
            canvas.Children.Add(rect);
        }


        public void OnPageChange(int pageIndex, int pageCount)
        {
            if (null != ItemsSource)
            { 
                int count = ItemsSource.Count();
                for (int i = 0; i < count; i++)
                {
                    QAModel model = (QAModel)ItemsSource.ElementAt(i);
                    if(null == model)
                    {
                        break;
                    }
                    if (model.SelResult == 0)
                    {
                        mElls[i].Fill = new SolidColorBrush(gray);
                    }
                    else if (model.SelResult == 1)
                    {
                        mElls[i].Fill = new SolidColorBrush(green);
                    }
                    else if (model.SelResult == 2)
                    {
                        mElls[i].Fill = new SolidColorBrush(red);
                    }
                }
            }
        }
    }


}
