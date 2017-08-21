using System;
using System.ComponentModel;

namespace FKFZ.Models
{
    public class Subject : INotifyPropertyChanged
    {
        public Subject() { }
        public Subject(int id, string name)
        {
            this._name = name;
            this._id = id;
        }
        /// <summary>
        /// 排序依据
        /// </summary>
        protected int _id;
        public int ID 
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                Notify("ID");
            }
        }

        protected string _name;
        public String Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                Notify("Name");
            }
        }

        protected bool _IsChecked;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                if (_IsChecked == value) return;
                _IsChecked = value;
                Notify("IsChecked");
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
