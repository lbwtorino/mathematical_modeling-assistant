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

namespace MMAWPF.登录模块
{
   /// <summary>
   /// ForgetPWD.xaml 的交互逻辑
   /// </summary>
   public partial class ForgetPWD : Window
   {
      string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
      public ForgetPWD()
      {
         InitializeComponent();
      }

      private void OKBtn_Click(object sender, RoutedEventArgs e)
      {
         if (UseName.Text == "")
         {
            MessageBox.Show("请输入用户名");
         }
         else if (key1.Text == "" || key2.Text == "" || key3.Text == "" || key4.Text == "")
         {
            MessageBox.Show("请输入完整的注册号码！");
         }
         else
         {
            try
            {
               using (SqlConnection conn = new SqlConnection(conStr))
               {

                  conn.Open();
                  using (SqlCommand cmd = new SqlCommand("", conn))
                  {
                     string regID = key1.Text + key2.Text + key3.Text + key4.Text;
                     //string regID = "MMACS00500WH339GH68M";
                     byte[] bregID;
                     bregID = AES.AESEncrypt(regID);
                     cmd.CommandText = "select * from RegTable where RegID=@RegID";
                     cmd.Parameters.Clear();
                     cmd.Parameters.AddWithValue("RegID", bregID);
                     SqlDataReader dr = cmd.ExecuteReader();
                     if (!dr.Read())
                     {
                        MessageBox.Show("输入的注册码不正确！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                     }
                     else
                     {
                        dr.Close();
                        cmd.CommandText = "select AutherPWD from AuthorTable where Name=@Name";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("Name", UseName.Text);
                        dr = cmd.ExecuteReader();
                        if (!dr.Read())
                        {
                           MessageBox.Show("该用户不存在，请先注册或检查输入的用户名是否正确");
                        }
                        else
                        {

                           string pwd = AES.AESDecrypt((byte[])dr[0]);
                           string message = "您的登录密码为：" + pwd;
                           MessageBox.Show(message);
                           UseName.Clear();
                           key1.Clear();
                           key2.Clear();
                           key3.Clear();
                           key4.Clear();
                        }
                     }
                  }

               }
            }
            catch
            {
               MessageBox.Show("无法连接数据库！");
               this.Close();
            }
         }
      }
   }
}
