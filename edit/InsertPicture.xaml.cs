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
   /// InsertPicture.xaml 的交互逻辑
   /// </summary>
   public partial class InsertPicture : Window
   {
      string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
      public InsertPicture()
      {
         InitializeComponent();
      }

      private void liulanBtn_Click(object sender, RoutedEventArgs e)
      {
         Microsoft.Win32.OpenFileDialog picfile = new Microsoft.Win32.OpenFileDialog();
         picfile.Filter = "图像文件(*.jpg;*.jpg;*.jpeg;*.gif;*.png;*.bmp)|*.jpg;*.jpeg;*.gif;*.png;*.bmp";

         Nullable<bool> result = picfile.ShowDialog();

         if (result == true)
         {
            string filename = picfile.FileName;
            picaddress.Text = filename;
            BitmapImage image = new BitmapImage(new Uri(filename, UriKind.RelativeOrAbsolute));
            previewimage.Source = image;
         }
         else
         {
            previewimage.Source = null;
         }
      }

      private void OKBtn_Click(object sender, RoutedEventArgs e)
      {
         if (picaddress.Text != "")
         {
            if (pictureTxt.Text == "")
            {
               System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("未输入图片名，确定使用默认名", "提示", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);
               if (dr == System.Windows.Forms.DialogResult.OK)
               {
                  Picture.isChanged = true;
                  Picture.PicAddress = picaddress.Text;
                  Picture.PNum = +1;
                  Picture.PicName = "图片(" + Picture.PNum.ToString() + ")";
                  Picture.PicTag = "<picture><" + Picture.PicName + ">";
                  CreatePicture();
               }
               else
               {
                  pictureTxt.Focus();
               }
            }
            else
            {
               //以输入图片名创建表格
               Picture.isChanged = true;
               Picture.PicAddress = picaddress.Text;
               Picture.PNum += 1;
               Picture.PicName = pictureTxt.Text;
               Picture.PicTag = "<picture><" + Picture.PicName + ">";
               CreatePicture();
            }
         }
         else
         {
            MessageBox.Show("请先选择待插入的图片！");
         }
      }

      private void CancelBtn_Click(object sender, RoutedEventArgs e)
      {
         Picture.isChanged = false;
         this.Close();
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         Picture.isChanged = false;
      }

      private void CreatePicture()
      {
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         com.CommandText = "select * from TitleTable where DocID=@DocID and TitleNum=@TitleNum";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         com.Parameters.AddWithValue("TitleNum", IsEditing.ElementName);
         SqlDataReader dr = com.ExecuteReader();
         if (dr.Read())
         {
            DisposeClose.Disposeclose(dr);
            com.CommandText = "insert into LeafPictureTag(DocID,LeafTitleNum,PTag,PAddress) values(@DocID,@LeafTitleNum,@PTag,@PAddress)";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
            com.Parameters.AddWithValue("PTag", Picture.PicTag);
            com.Parameters.AddWithValue("PAddress", Picture.PicAddress);
            com.ExecuteNonQuery();
            MessageBox.Show("图片插入成功");
         }
         else
         {
            DisposeClose.Disposeclose(dr);
         }
         DisposeClose.Disposeclose(com);
         DisposeClose.Disposeclose(conn);
      }

      private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("确认退出插入图片操作", "警告", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Warning);
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
