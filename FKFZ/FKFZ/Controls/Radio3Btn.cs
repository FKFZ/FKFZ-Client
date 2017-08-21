using FKFZ.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FKFZ.Controls
{
     public class Radio3Btn:RadioButton
    {
         public Radio3Btn()
        {
            this.DefaultStyleKey = typeof(Radio3Btn);
        }
         public static readonly DependencyProperty StateProperty = DependencyProperty.Register("SelectState", typeof(ResultState),
            typeof(Radio3Btn), new PropertyMetadata(ResultState.UNSELECT, new PropertyChangedCallback(OnStatePropertyChanged)));

         private static void OnStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Radio3Btn rb = (Radio3Btn)d;
            rb.SelectState = (ResultState)e.NewValue;
        }

        [Category("Common Properties")]
         public ResultState SelectState
        {
            get { return (ResultState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(String),
           typeof(Radio3Btn), new PropertyMetadata("", new PropertyChangedCallback(OnIdPropertyChanged)));

        private static void OnIdPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Radio3Btn rb = (Radio3Btn)d;
            rb.Id = (String)e.NewValue;
        }

        public String Id
        {
            get { return (String)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        protected override void OnClick()
        {
            try
            {
                ItemsControl c = FindParent<ItemsControl>(this);
                if (null != c)
                {
                    List<Radio3Btn> list = GetChildObjects<Radio3Btn>(c, "RdoBtn");
                    foreach (Radio3Btn btn in list)
                    {
                        if (true == btn.IsChecked)
                        {
                            btn.IsChecked = false;
                            btn.SelectState = ResultState.UNSELECT;
                        }
                    }
                    IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException(ex);
            }
            base.OnClick();
        }

        /// <summary>
        /// WPF中查找元素的父元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i_dp"></param>
        /// <returns></returns>
        public static T FindParent<T>(DependencyObject i_dp) where T : DependencyObject
        {
            DependencyObject dobj = (DependencyObject)VisualTreeHelper.GetParent(i_dp);
            if (dobj != null)
            {
                if (dobj is T)
                {
                    return (T)dobj;
                }
                else
                {
                    dobj = FindParent<T>(dobj);
                    if (dobj != null && dobj is T)
                    {
                        return (T)dobj;
                    }
                }
            }
            return null;
        }

        /// <summary>
        ///WPF查找元素的子元素
        /// </summary>
        /// <typeparam name="childItem"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private childItem FindVisualChild<childItem>(DependencyObject obj)
                                                where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public List<T> GetChildObjects<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();
            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (((T)child).Name == name || string.IsNullOrEmpty(name)))
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child, ""));//指定集合的元素添加到List队尾  
            }
            return childList;
        }  
    }

     public enum ResultState
     { 
       UNSELECT = 0, RIGHT,ERROR,
     }


     public class ResultCommand : ICommand
     {

         public bool CanExecute(object parameter)
         {
             return true;
         }

         public event EventHandler CanExecuteChanged;

         public void Execute(object parameter)
         {
             throw new NotImplementedException();
         }
     }
}
