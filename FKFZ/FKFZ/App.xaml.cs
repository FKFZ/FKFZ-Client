using FKFZ.Log;
using System;
using System.Windows;

namespace FKFZ
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is System.Exception)
            {
                RecordLog.RecordException(e.ExceptionObject as System.Exception);
            }
        }
        
        public static void HandleException(Exception ex)
        {
            RecordLog.RecordException(ex);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true; //值为true时，系统将继续运行，返回到异常前的状态。不为true，程序将崩溃退出
            RecordLog.RecordException(e.Exception);
        }
    }
}
