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
using System.Windows.Forms;
namespace MMAWPF
{
   /// <summary>
   /// Algorithm.xaml 的交互逻辑
   /// </summary>
   public partial class Algorithm : Window
   {
      string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

      public Algorithm()
      {
         InitializeComponent();
      }

      #region 关闭窗口响应事件
      private void algorithm_introduction_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (System.Windows.MessageBox.Show(this, "是否关闭文档编辑窗口？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
         {
            e.Cancel = false;
         }
         else
         {
            e.Cancel = true; ;
         }
      }

      private void algorithm_introduction_Closed(object sender, EventArgs e)
      {
         MainWindow.Algorithm_Object = null;
      }
      #endregion


      private void treeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         if (com != null)
         {
            SqlDataReader dr;
            TreeViewItem tvi = (TreeViewItem)treeView.SelectedItem;
            if (tvi.Header.ToString() != "算法" && tvi.Header.ToString() != "算法介绍")
            {
               if (tvi.Header.ToString() == "Dijkstra")
               {
                  com.CommandText = "select AIntroduce from AlgorithmTable where AName='Dijkstra'";
                  dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     string content = dr[0].ToString();
                     contentTextBox.Text = content;
                     label.Content = "当前算法:" + content;
                  }
                  DisposeClose.Disposeclose(dr);
               }
               else if (tvi.Header.ToString() == "Floyd")
               {
                  com.CommandText = "select AIntroduce from AlgorithmTable where AName='Floyd'";
                  dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     string content = dr[0].ToString();
                     contentTextBox.Text = content;
                     label.Content = "当前算法:" + content;
                  }
                  DisposeClose.Disposeclose(dr);
               }
               else if (tvi.Header.ToString() == "概率算法")
               {
                  com.CommandText = "select AIntroduce from AlgorithmTable where AName='概率算法'";
                  dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     string content = dr[0].ToString();
                     contentTextBox.Text = content;
                     label.Content = "当前算法:" + content;
                  }
                  DisposeClose.Disposeclose(dr);
               }
               else if (tvi.Header.ToString() == "灰色预测")
               {
                  com.CommandText = "select AIntroduce from AlgorithmTable where AName='灰色预测'";
                  dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     string content = dr[0].ToString();
                     contentTextBox.Text = content;
                     label.Content = "当前算法:" + content;
                  }
                  DisposeClose.Disposeclose(dr);
               }
               else if (tvi.Header.ToString() == "模拟退火算法")
               {
                  com.CommandText = "select AIntroduce from AlgorithmTable where AName='模拟退火算法'";
                  dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     string content = dr[0].ToString();
                     contentTextBox.Text = content;
                     label.Content = "当前算法:" + content;
                  }
                  DisposeClose.Disposeclose(dr);
               }
               else if (tvi.Header.ToString() == "搜索算法")
               {
                  com.CommandText = "select AIntroduce from AlgorithmTable where AName='搜索算法'";
                  dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     string content = dr[0].ToString();
                     contentTextBox.Text = content;
                     label.Content = "当前算法:" + content;
                  }
                  DisposeClose.Disposeclose(dr);
               }
               else if (tvi.Header.ToString() == "贪心算法")
               {
                  com.CommandText = "select AIntroduce from AlgorithmTable where AName='贪心算法'";
                  dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     string content = dr[0].ToString();
                     contentTextBox.Text = content;
                     label.Content = "当前算法:" + content;
                  }
                  DisposeClose.Disposeclose(dr);
               }
               else if (tvi.Header.ToString() == "遗传算法")
               {
                  com.CommandText = "select AIntroduce from AlgorithmTable where AName='遗传算法'";
                  dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     string content = dr[0].ToString();
                     contentTextBox.Text = content;
                     label.Content = "当前算法:" + content;
                  }
                  DisposeClose.Disposeclose(dr);
               }
               else if (tvi.Header.ToString() == "免疫算法")
               {
                  com.CommandText = "select AIntroduce from AlgorithmTable where AName='免疫算法'";
                  dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     string content = dr[0].ToString();
                     contentTextBox.Text = content;
                     label.Content = "当前算法:" + content;
                  }
                  DisposeClose.Disposeclose(dr);
               }
            }
         }
      }
   }
}
