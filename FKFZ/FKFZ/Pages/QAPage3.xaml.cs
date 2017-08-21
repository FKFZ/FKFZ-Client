using FKFZ.Controls;
using FKFZ.Log;
using FKFZ.Utils;
using FKFZ.ViewModel;
using FKFZ.XmlModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace FKFZ.Pages
{
    /// <summary>
    /// QAPage.xaml 的交互逻辑
    /// </summary>
    public partial class QAPage3 : Page
    {
        QAViewModel mViewModel;
        String mPath;
        ObservableCollection<QAModel> _questions = null;
        public QAPage3()
        {
            InitializeComponent();
            
            AddHandler(QuestionCtrl.OptionSelectedEvent, new RoutedEventHandler(Question_OptionSelected));
        }

        private void Question_OptionSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                OptionSelectedEventArgs args = e as OptionSelectedEventArgs;
                
                if (args.AnswerId == args.OptionId)
                {
                    //TODO 播放正确音效
                    PlayMusic(Music.RIGHT);
                }
                else
                {
                    //TODO 播放错误音效
                    PlayMusic(Music.ERROR);
                }

                Indicator.OnPageChange(args.QAId, _questions.Count);
                //TODO 完成做题音效
                if (HasFinish())
                {
                    TBScroe.Text = GetTotalScore() + "";
                    GridScore.Visibility = Visibility.Visible;
                    Storyboard sbd = (Storyboard)this.FindResource("abc");
                    sbd.Begin(this);

                    PlayMusic(Music.FINISH);
                }
                //自动跳转到下一题
                mViewModel.Query(1, dataPager.GetCurrentPageIndex());

            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }

        }

        void PlayMusic(Music music)
        {
            string mp4_path;
            switch(music)
            {
                case Music.RIGHT:
                    mp4_path = AppDomain.CurrentDomain.BaseDirectory + @"music\right.wma";
                    break;
                case Music.ERROR:
                    mp4_path = AppDomain.CurrentDomain.BaseDirectory + @"music\error.wma";
                    break;
                case Music.FINISH:
                    mp4_path = AppDomain.CurrentDomain.BaseDirectory + @"music\finish.mp3";
                    break;
                default:
                    mp4_path = AppDomain.CurrentDomain.BaseDirectory + "SayGoodbye.mp3";
                    break;
            }
            AudioPlayer.Source = new Uri(mp4_path, UriKind.RelativeOrAbsolute);
            AudioPlayer.Play();
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                mPath = PagePathUtils.GetInstance().GetPathString() + @"\知识问答";
                //_questions = LocalLoader.LoadQA(mPath);
                //ListCtrl.ItemsSource = _questions;
                mViewModel = new QAViewModel(mPath);
                _questions = mViewModel._questions;
                //加载数据异常
                if (null == _questions || _questions.Count < 1)
                {
                    Indicator.Visibility = Visibility.Collapsed;
                    dataPager.Visibility = Visibility.Collapsed;
                    return;
                }
                Indicator.ItemsSource = _questions;
                DataContext = mViewModel;
                dataPager.setPageInfo(1, mViewModel._questions.Count);
                dataPager.PageSize = 1;
                dataPager.PageCount = mViewModel._questions.Count;
            }
            catch (Exception ex)
            {
                //加载数据异常
                if (null == _questions || _questions.Count < 1)
                {
                    Indicator.Visibility = Visibility.Collapsed;
                    dataPager.Visibility = Visibility.Collapsed;
                    return;
                }

                RecordLog.RecordException(ex);
            }
        }
        
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            AudioPlayer.Stop();
            AudioPlayer.Source = null;
        }

        private bool HasFinish()
        {
            foreach (QAModel item in _questions)
            {
                if (null != item && item.SelResult == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private int GetTotalScore()
        {
            int scroe = 0;
            foreach (QAModel item in _questions)
            {
                if (null != item && item.SelResult == 1)
                {
                    scroe += item.Score;
                }
            }
            return scroe;
        }

        private void dataPager_PageChanged_1(object sender, PageChangedEventArgs args)
        {
            mViewModel.Query(args.PageSize, args.PageIndex);
            //Indicator.OnPageChange(args.PageIndex, args.PageSize);
        }

        private void Page_Unloaded_1(object sender, RoutedEventArgs e)
        {
            AudioPlayer.Stop();
        }

    }

}
