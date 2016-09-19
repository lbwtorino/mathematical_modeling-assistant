using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Threading;
using System.IO;
using CSharpWin;

namespace MMAWPF
{
   /// <summary>
   /// MathModeling.xaml 的交互逻辑
   /// </summary>
   public partial class MathModeling : Window
   {
      string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
      Process debug = new Process();
      public static DateTime overtime;
      DispatcherTimer tm = new DispatcherTimer();
      public MathModeling()
      {
         InitializeComponent();
         tm.Tick += new EventHandler(tm_Tick);
         tm.Interval = TimeSpan.FromSeconds(1);
         tm.Start();
      }

      private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (MessageBox.Show(this, "是否关闭数模版块窗口？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
         {
            Process[] myprc = Process.GetProcessesByName("java");
            if (myprc.Length > 0)
            {
               myprc[0].Kill();
            }
            myprc=Process.GetProcessesByName("cmd");
            if (myprc.Length > 0)
            {
               myprc[0].Kill();
            }        
            e.Cancel = false;
            //关闭进程
         }
         else
         {
            e.Cancel = true;
         }
      }

      private void Window_Closed(object sender, EventArgs e)
      {
         MainWindow.MathModeling_Object = null;
      }

      private void setBtn_Click(object sender, RoutedEventArgs e)
      {
         SetTime st = new SetTime();
         st.ShowDialog();
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         LoadTime();
      }

      private void LoadTime()
      {
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         com.CommandText = "select OverTime from TimeTable  where TimeID=1";
         SqlDataReader dr = com.ExecuteReader();
         dr.Read();
         overtime =(DateTime)dr[0];
         DisposeClose.Disposeclose(dr);
         DisposeClose.Disposeclose(com);
         DisposeClose.Disposeclose(conn);
      }

      void tm_Tick(object sender, EventArgs e)
      {
         DateTime now = DateTime.Now;
         if (overtime > now)
         {
            TimeSpan diffTime = overtime.Subtract(now);
            if (diffTime.Days < 10)
            {
               daytxt.Content = "0" + diffTime.Days.ToString();
            }
            else
            {
               daytxt.Content = diffTime.Days.ToString();
            }
            if (diffTime.Hours < 10)
            {
               hourtxt.Content = "0"+diffTime.Hours.ToString();
            }
            else
            {
               hourtxt.Content = diffTime.Hours.ToString();
            }
            if (diffTime.Minutes < 10)
            {
               minutetxt.Content = "0"+diffTime.Minutes.ToString();
            }
            else
            {
               minutetxt.Content = diffTime.Minutes.ToString();
            }
            if (diffTime.Seconds < 10)
            {
               secondtxt.Content = "0"+diffTime.Seconds.ToString();
            }
            else
            {
               secondtxt.Content = diffTime.Seconds.ToString();
            }
         }
         else
         {
            daytxt.Content = "00";
            hourtxt.Content = "00";
            minutetxt.Content = "00";
            secondtxt.Content = "00";
         }

      }

      private void stopBtn_Click(object sender, RoutedEventArgs e)
      {
         tm.Stop();
      }

      private void startBtn_Click(object sender, RoutedEventArgs e)
      {
         tm.Start();
      }

      private void saveBtn_Click(object sender, RoutedEventArgs e)
      {
         Microsoft.Win32.SaveFileDialog savefile = new Microsoft.Win32.SaveFileDialog();
         savefile.Filter = "m文件(*.m)|*.m|所有文件(*.*)|*.*";

         Nullable<bool> result = savefile.ShowDialog();

         if (result == true)
         {
            string path = savefile.FileName;
            string text = mtxt.Text;
            using (StreamWriter sw = File.CreateText(path))
            {
               string[] splittext=text.Split('\r','\n');
               foreach (string s in splittext)
               {
                  if (s != "")
                  {
                     sw.WriteLine(s);
                  }
               }
            } 
         }
      }

      private void loadBtn_Click(object sender, RoutedEventArgs e)
      {
         Microsoft.Win32.OpenFileDialog openfile = new Microsoft.Win32.OpenFileDialog();
         openfile.Filter = "m文件(*.m)|*.m";

         Nullable<bool> result = openfile.ShowDialog();

         if (result == true)
         {
            mtxt.Clear();
            string path = openfile.FileName;
            using (StreamReader sr = File.OpenText(path))
            {
               string s;
               if ((s = sr.ReadLine()) != null)
               {
                  mtxt.AppendText(s);
                  while ((s = sr.ReadLine()) != null)
                  {
                     mtxt.AppendText("\n" + s);
                  }
               }
              
            }
         }
      }

      private void test_Click(object sender, RoutedEventArgs e)
      {
         this.WindowState =WindowState.Minimized;
         debug = new Process();   //创建新的进程                      
         //设置进程信息                                               
         debug.StartInfo.FileName = (string)@"GidenV4a\GIDEN.bat";//程序路径                        
         debug.StartInfo.Arguments = null;//程序参数                        
         debug.StartInfo.UseShellExecute = false;//                        
         debug.StartInfo.CreateNoWindow = true;//是否创建新窗口true为不创建                       
         debug.Start();//启动进程 
      }

      private void capture_Click(object sender, RoutedEventArgs e)
      {
         this.Hide();
         System.Threading.Thread.Sleep(30);
         CaptureImageTool capture = new CaptureImageTool();

         capture.SelectCursor = CursorManager.Arrow;
         capture.DrawCursor = CursorManager.Cross;

         if (capture.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
         {
            this.Show();
         }
      }

      private void matlabBtn_Click(object sender, RoutedEventArgs e)
      {
         this.WindowState = WindowState.Minimized;
         debug = new Process();   //创建新的进程                      
         //设置进程信息                                   
         debug.StartInfo.FileName = (string)@"matlab.exe";//程序路径                        
         debug.StartInfo.Arguments = null;//程序参数                        
         debug.StartInfo.UseShellExecute = false;//                        
         debug.StartInfo.CreateNoWindow = true;//是否创建新窗口true为不创建                       
         debug.Start();//启动进程 
      }
   }
}
