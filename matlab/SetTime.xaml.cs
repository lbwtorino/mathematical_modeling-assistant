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
using System.Data.SqlClient;
using System.Configuration;

namespace MMAWPF
{
   /// <summary>
   /// SetTime.xaml 的交互逻辑
   /// </summary>
   public partial class SetTime : Window
   {
      string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
      DateTime dt;
      bool isChanged;
      public SetTime()
      {
         InitializeComponent();
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         isChanged = false;
         hour.Items.Clear();
         minute.Items.Clear();
         for (int i = 0; i <= 23; i++)
         {
            hour.Items.Add(i);
         }
         for (int j = 0; j <= 59; j++)
         {
            minute.Items.Add(j);
         }
      }

      private void cancelBtn_Click(object sender, RoutedEventArgs e)
      {
         this.Close();
      }

      private void okBtn_Click(object sender, RoutedEventArgs e)
      {
         if (datePicker.Text != "")
         {
            if (hour.Text == "" || minute.Text == "")
            {
               MessageBox.Show("请选择完整的比赛结束时间");
            }
            else
            {
               int year = datePicker.SelectedDate.Value.Year;
               int month = datePicker.SelectedDate.Value.Month;
               int day = datePicker.SelectedDate.Value.Day;
               int h = int.Parse(hour.Text);
               int m = int.Parse(minute.Text);
               dt = new DateTime(year, month, day, h, m, 0, 0);
               SqlConnection conn = new SqlConnection();
               SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
               com.CommandText = "update TimeTable set OverTime=@OverTime where TimeID=1";
               com.Parameters.Clear();
               com.Parameters.AddWithValue("OverTime", dt);
               com.ExecuteNonQuery();
               DisposeClose.Disposeclose(com);
               DisposeClose.Disposeclose(conn);
               isChanged = true;
               this.Close();
            }
         }
         else
         {
            MessageBox.Show("请选择比赛结束日期！");
         }
      }

      private void Window_Closed(object sender, EventArgs e)
      {
         if (isChanged)
         {
            MathModeling.overtime = dt;
         }
      }
   }
}
