using FKFZ.Utils;
using FKFZ.XmlModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FKFZ.Controls
{
    

    /// <summary>
    /// QuestionCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class QuestionCtrl : UserControl
    {
        public QuestionCtrl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty QuestionValueProperty = DependencyProperty.Register("QuestionValue", typeof(QAModel),
        typeof(QuestionCtrl), new PropertyMetadata(null, new PropertyChangedCallback(OnQuestionValueChanged)));

        private static void OnQuestionValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            QuestionCtrl dtb = (QuestionCtrl)d;
            dtb.QuestionValue = (QAModel)e.NewValue;

            dtb.InitData();
        }

        [Category("Common Properties")]
        public QAModel QuestionValue
        {
            get { return (QAModel)GetValue(QuestionValueProperty); }
            set { SetValue(QuestionValueProperty, value); }
        }

        void InitData()
        {
            SelectItems.ItemsSource = QuestionValue.Options;
        }

        private void RdoBtn_Checked(object sender, RoutedEventArgs e)
        {
            AppDB.GetInstance().IdleSeconds = 0;
            Radio3Btn btn = (Radio3Btn)sender;
            if (btn.IsChecked == true)
            {
                ObservableCollection<OptionModel> oms = QuestionValue.Options;
                for (int i = 0; i < oms.Count; i++)
                {
                    oms[i].Result = 0;
                }

                OptionModel om = (OptionModel)btn.Tag;
                if (QuestionValue.AnswerId == btn.Id)
                {
                    btn.SelectState = ResultState.RIGHT;
                    QuestionValue.SelResult = 1;
                    om.Result = 1;
                }
                else
                {
                    btn.SelectState = ResultState.ERROR;
                    QuestionValue.SelResult = 2;
                    om.Result = 2;
                }

                RaiseHitEvent(btn.Id);//调用RaiseHitEvent()方法引发路由事件
            }           
            
        }
        //自定义路由事件
        public static readonly RoutedEvent OptionSelectedEvent = EventManager.RegisterRoutedEvent("OptionSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(QuestionCtrl));
        public event RoutedEventHandler OptionSelected
        {
            add { AddHandler(OptionSelectedEvent, value); }
            remove { RemoveHandler(OptionSelectedEvent, value); }
        }
        public void RaiseHitEvent(String optionid)
        {
            OptionSelectedEventArgs routedEventArgs = new OptionSelectedEventArgs(QuestionCtrl.OptionSelectedEvent,this);
            routedEventArgs.AnswerId = QuestionValue.AnswerId;
            routedEventArgs.OptionId = optionid;
            routedEventArgs.QAId = QuestionValue.Id;
            this.RaiseEvent(routedEventArgs);//触发路由事件方法
        }
    }

    public class OptionSelectedEventArgs : RoutedEventArgs
    {
        public OptionSelectedEventArgs(RoutedEvent routedEvent, object source)  
            :base(routedEvent,source){}
        public String AnswerId { get; set; }
        public String OptionId { get; set; }  
        public int QAId { get; set; }  
    }
}
