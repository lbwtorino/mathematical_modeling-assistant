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
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
namespace MMAWPF.文档编辑模块
{
   /// <summary>
   /// InsertFormula.xaml 的交互逻辑
   /// </summary>
   public partial class InsertFormula : Window
   {
      string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
      public InsertFormula()
      {
         InitializeComponent();
      }

      private void OKBtn_Click(object sender, RoutedEventArgs e)
      {
         if (Formula.ModFName == "")
         {
            Formula.isChanged = true;
            Formula.FNumber += 1;
            Formula.FName = "公式(" + Formula.FNumber.ToString() + ")";
            Formula.FTag = "<formula><" + Formula.FName + ">";
            //将公式保存为图片，放入wordimage中。同时将图片地址保存起来。
            string[] titlearray = IsEditing.ElementName.Split('|');
            string newtitlenum = String.Join("l", titlearray);
            string picturename = "wordimage//" + IsEditing.DOCID.ToString() + "_" + newtitlenum + "_" + Formula.FName + ".jpg";
            string picname = "\\wordimage\\" + IsEditing.DOCID.ToString() + "_" + newtitlenum + "_" + Formula.FName + ".jpg";
            mathformula.MC_saveAsJPEG(picturename, 15, MathMLControl.enum_ImageResolution._120dpi);

            string xml = mathformula.MC_getXML();
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
               com.CommandText = "insert into LeafFormulaTag(DocID,LeafTitleNum,FTag,FXml,FPicName) values(@DocID,@LeafTitleNum,@FTag,@FXml,@FPicName)";
               com.Parameters.Clear();
               com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
               com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
               com.Parameters.AddWithValue("FTag", Formula.FTag);
               com.Parameters.AddWithValue("FXml", xml);
               com.Parameters.AddWithValue("FPicName", picname);
               com.ExecuteNonQuery();
               System.Windows.Forms.MessageBox.Show("公式插入成功");
            }
            else
            {
               DisposeClose.Disposeclose(dr);
            }
            DisposeClose.Disposeclose(com);
            DisposeClose.Disposeclose(conn);
         }
         else
         {
            string xml = mathformula.MC_getXML();
            string ftag = Formula.ModFName;
            string fpicname;
            SqlConnection conn = new SqlConnection();
            SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
            com.CommandText = "update LeafFormulaTag set FXml=@FXml where DocID=@DocID and LeafTitleNum=@LeafTitleNum and FTag=@FTag";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("FXml", xml);
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
            com.Parameters.AddWithValue("FTag", ftag);
            com.ExecuteNonQuery();

            com.CommandText = "select FPicName from LeafFormulaTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum and FTag=@FTag";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
            com.Parameters.AddWithValue("FTag", ftag);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
               fpicname = dr[0].ToString();
               string basepath = System.Windows.Forms.Application.StartupPath;
               string path = basepath + fpicname;
               mathformula.MC_saveAsJPEG(path, 15, MathMLControl.enum_ImageResolution._120dpi);
               System.Windows.Forms.MessageBox.Show("公式修改成功");               
            }
            DisposeClose.Disposeclose(dr);
            DisposeClose.Disposeclose(com);
            DisposeClose.Disposeclose(conn);
         }
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         Formula.isChanged = false;
         if (Formula.ModFName != "")
         {
            string ftag = Formula.ModFName;
            SqlConnection conn = new SqlConnection();
            SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
            com.CommandText = "select FXml from LeafFormulaTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum and FTag=@FTag";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
            com.Parameters.AddWithValue("FTag", ftag);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
               string fxml = dr[0].ToString();
               mathformula.MC_loadXML(fxml);
            }
            DisposeClose.Disposeclose(dr);
            DisposeClose.Disposeclose(com);
            DisposeClose.Disposeclose(conn);
         }
      }

      private void CancelBtn_Click(object sender, RoutedEventArgs e)
      {
         Formula.isChanged = false;
         this.Close();
      }

      private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("确认退出插入公式操作", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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
