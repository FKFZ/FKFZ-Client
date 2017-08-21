using FKFZ.Controls;
using FKFZ.DataStore;
using FKFZ.Log;
using FKFZ.Utils;
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
    public partial class QAPage : Page
    {
        String mPath;
        ObservableCollection<QAModel> _questions = null;
        public QAPage()
        {
            InitializeComponent();
#region 自定义command
            //var ResultCmdBinding = new CommandBinding(ApplicationCommands.Find, ResultCmdExecuted, ResultCmdCanExecute);
            //CommandBindings.Add(ResultCmdBinding);
            //void ResultCmdExecuted(object target, ExecutedRoutedEventArgs e)
            //{
            //    //计算结果
            //    int a = 0;
            //    int b = a;
            //}

            //void ResultCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
            //{
            //    e.CanExecute = true;
            //}

#endregion
            #region
            //_questions = new ObservableCollection<QAModel>()
            //{
            //    new QAModel(0,"1、你最喜欢看什么类型的书籍（）",new ObservableCollection<QuestionItem>
            //        {
            //            new QuestionItem()
            //            {
            //                Content="A、武侠类",
            //                Group = "1"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="B、言情类",
            //                Group = "1"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="C、技术类",
            //                Group = "1"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="D、科普类",
            //                Group = "1"

            //            }
            //        }), 
            //         new QAModel(0,"2、你最喜欢看什么类型的书籍（）",new ObservableCollection<QuestionItem>
            //        {
            //            new QuestionItem()
            //            {
            //                Content="A、武侠类",
            //                Group = "2"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="B、言情类",
            //                Group = "2"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="C、技术类",
            //                Group = "2"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="D、科普类",
            //                Group = "2"

            //            }
            //        }), 
            //         new QAModel(0,"3、你最喜欢看什么类型的书籍（）",new ObservableCollection<QuestionItem>
            //        {
            //            new QuestionItem()
            //            {
            //                Content="A、武侠类",
            //                Group = "3"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="B、言情类",
            //                Group = "3"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="C、技术类",
            //                Group = "3"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="D、科普类",
            //                Group = "3"

            //            }
            //        }), 
            //         new QAModel(0,"4、你最喜欢看什么类型的书籍（）",new ObservableCollection<QuestionItem>
            //        {
            //            new QuestionItem()
            //            {
            //                Content="A、武侠类",
            //                Group = "4"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="B、言情类",
            //                Group = "4"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="C、技术类",
            //                Group = "4"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="D、科普类",
            //                Group = "4"

            //            }
            //        }), 

            //         new QAModel(0,"5、你最喜欢看什么类型的书籍（）",new ObservableCollection<QuestionItem>
            //        {
            //            new QuestionItem()
            //            {
            //                Content="A、武侠类",
            //                Group = "5"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="B、言情类",
            //                Group = "5"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="C、技术类",
            //                Group = "5"
            //            },
            //             new QuestionItem()
            //            {
            //                Content="D、科普类",
            //                Group = "5"
            //            }
            //        })
            //};

            #endregion
            
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
                //TODO 完成做题音效
                if (HasFinish())
                {
                    TBScroe.Text = GetTotalScore() + "";
                    GridScore.Visibility = Visibility.Visible;
                    Storyboard sbd = (Storyboard)this.FindResource("abc");
                    sbd.Begin(this);

                    PlayMusic(Music.FINISH);
                }
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
                _questions = LocalLoader.LoadQA(mPath);
                ListCtrl.ItemsSource = _questions;
            }
            catch (Exception ex)
            {
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
                if (item.SelResult == 0)
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
                if (item.SelResult == 1)
                {
                    scroe += item.Score;
                }
            }
            return scroe;
        }
    }

    public enum Music
    { 
        RIGHT = 0,
        ERROR,
        FINISH
    }
}
