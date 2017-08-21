using FKFZ.Controls;
using FKFZ.DataStore;
using FKFZ.Log;
using FKFZ.Utils;
using FKFZ.XmlModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace FKFZ.ViewModel
{
    public class QAViewModel : NotificationObject
    {
        public ObservableCollection<QAModel> _questions = null;

        public QAViewModel(String path)
        {
            Result = new DataResult<QAModel>();
            _questions = LocalLoader.LoadQA(path);
            Query(1, 1);
        }


        private DataResult<QAModel> _result;
        public DataResult<QAModel> Result
        {
            get { return _result; }
            set
            {
                _result = value;
                RaisePropertyChanged("Result");
            }
        }

        public void Query(int size, int pageIndex)
        {
            try
            {
                Result.Total = _questions.Count;//给页总数赋值
                Result.DataSource = _questions.Skip((pageIndex - 1) * size).Take(size).ToList();//改变数据源赋值
            }
            catch (Exception e)
            {
                RecordLog.RecordException(e);
            }
        }
    }
}
