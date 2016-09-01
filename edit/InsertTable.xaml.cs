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

namespace MMAWPF.文档编辑模块
{
   /// <summary>
   /// InsertTable.xaml 的交互逻辑
   /// </summary>
   public partial class InsertTable : Window
   {
      string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
      public InsertTable()
      {
         InitializeComponent();
      }

      private void OKBtn_Click(object sender, RoutedEventArgs e)
      {
         if (tableTitleTxt.Text == "")
         {
            System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("未输入表名，确定使用默认名", "提示", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
               Table.isChanged = true;
               Table.RowNum = (int)rowNum.Value;
               Table.ColNum = (int)colNum.Value;
               Table.TNum = Table.TNum + 1;
               Table.TableName = "表格(" + Table.TNum.ToString() + ")";
               Table.TableTag = "<table><" + Table.TableName + ">";
               CreateTable();
            }
            else
            {
               tableTitleTxt.Focus();
            }
         }
         else
         {
            //以输入表格名创建表格
            Table.isChanged = true;
            Table.RowNum = (int)rowNum.Value;
            Table.ColNum = (int)colNum.Value;
            Table.TNum += 1;
            Table.TableName = tableTitleTxt.Text;
            Table.TableTag = "<table><" + Table.TableName + ">";
            CreateTable();
         }
      }

      private void CreateTable()
      {
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         string rowcol = Table.RowNum.ToString() + "|" + Table.ColNum.ToString();
         com.CommandText = "select * from TitleTable where DocID=@DocID and TitleNum=@TitleNum";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         com.Parameters.AddWithValue("TitleNum", IsEditing.ElementName);
         SqlDataReader dr = com.ExecuteReader();
         if (dr.Read())
         {
            DisposeClose.Disposeclose(dr);
            com.CommandText = "insert into LeafTableTag(DocID,LeafTitleNum,TTag,TRowCol) values(@DocID,@LeafTitleNum,@TTag,@TRowCol)";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
            com.Parameters.AddWithValue("TTag", Table.TableTag);
            com.Parameters.AddWithValue("TRowCol", rowcol);
            com.ExecuteNonQuery();
            MessageBox.Show("表格插入成功！");
         }
         else
         {
            DisposeClose.Disposeclose(dr);
         }
         DisposeClose.Disposeclose(com);
         DisposeClose.Disposeclose(conn);
      }

      private void CancelBtn_Click(object sender, RoutedEventArgs e)
      {
         Table.isChanged = false;
         this.Close();
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         Table.isChanged = false;
      }

      private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("确认退出插入表格操作", "警告", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Warning);
         if (dr == System.Windows.Forms.DialogResult.OK)
         {
            e.Cancel = false;
         }
         else
         {
            e.Cancel = true;
         }
      }
   }
}
