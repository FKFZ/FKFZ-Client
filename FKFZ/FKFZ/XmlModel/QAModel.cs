using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FKFZ.XmlModel
{
    public class QAModel : INotifyPropertyChanged
    {
        public QAModel() { }

        public QAModel(int id, String title, String answerId, ObservableCollection<OptionModel> options)
        {
            this.Id = id;
            this.Title = title;
            this.AnswerId = answerId;
            this.Options = options;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        private String _AnswerId;
        public String AnswerId
        {
            get { return _AnswerId; }
            set
            {
                if (_AnswerId == value) return;
                _AnswerId = value;
                Notify("AnswerId");
            }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title == value) return;
                _Title = value;
                Notify("Title");
            }
        }

        private int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id == value) return;
                _Id = value;
                Notify("Id");
            }
        }

        private ObservableCollection<OptionModel> _Options;
        public ObservableCollection<OptionModel> Options
        {
            get { return _Options; }
            set
            {
                if (_Options == value) return;
                _Options = value;
                Notify("Options");
            }
        }

        private int _Result;
        /// <summary>
        /// 0：未选择，1：正确，2：错误
        /// </summary>
        public int SelResult
        {
            get { return _Result; }
            set
            {
                if (_Result == value) return;
                _Result = value;
                Notify("SelResult");
            }
        }

        private int _Score;
        /// <summary>
        /// 分数
        /// </summary>
        public int Score
        {
            get { return _Score; }
            set
            {
                if (_Score == value) return;
                _Score = value;
                Notify("Score");
            }
        }
    }

    public class OptionModel : INotifyPropertyChanged
    {
        private String _OptionId;
        public String OptionId
        {
            get { return _OptionId; }
            set
            {
                if (_OptionId == value) return;
                _OptionId = value;
                Notify("OptionId");
            }
        }
        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (_IsSelected == value) return;
                _IsSelected = value;
                Notify("IsSelected");
            }
        }
        private String _Value;
        public String Value
        {
            get { return _Value; }
            set
            {
                if (_Value == value) return;
                _Value = value;
                Notify("Value");
            }
        }

        //分组
        private String _Group;
        public String Group
        {
            get { return _Group; }
            set
            {
                if (_Group == value) return;
                _Group = value;
                Notify("Group");
            }
        }

        private int _Result;
        /// <summary>
        /// 0：未选择，1：正确，2：错误
        /// </summary>
        public int Result
        {
            get { return _Result; }
            set
            {
                if (_Result == value) return;
                _Result = value;
                Notify("Result");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
