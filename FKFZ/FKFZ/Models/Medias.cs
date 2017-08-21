using System;
using System.ComponentModel;

namespace FKFZ.Models
{
    public class Medias : INotifyPropertyChanged
    {
        public Medias() { }
        public Medias(int id, string name)
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
        protected string _FirstPicName;
        public String FirstPicName
        {
            get { return _FirstPicName; }
            set
            {
                if (_FirstPicName == value) return;
                _FirstPicName = value;
                Notify("Name");
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
