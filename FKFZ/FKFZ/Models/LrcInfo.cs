using System;
using System.ComponentModel;

namespace FKFZ.Models
{
    public class LrcInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line">当前行号</param>
        /// <param name="time">时间</param>
        /// <param name="lrc">内容</param>
        public LrcInfo(int line,TimeSpan time, String lrc)
        {
            this.Line = line;
            this.Time = time;
            this.Lrc = lrc;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private TimeSpan _Time;
        public TimeSpan Time 
        {
            get { return _Time; }
            set
            {
                if (_Time == value) return;
                _Time = value;
                Notify("Time");
            }
        }

        private String _Lrc;
        public String Lrc
        {
            get { return _Lrc; }
            set
            {
                if (_Lrc == value) return;
                _Lrc = value;
                Notify("Lrc");
            }
        }

        private int _Line;
        public int Line
        {
            get { return _Line; }
            set
            {
                if (_Line == value) return;
                _Line = value;
                Notify("Line");
            }
        }
        
    }
}
