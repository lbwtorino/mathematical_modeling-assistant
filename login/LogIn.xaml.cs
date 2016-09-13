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
using System.Configuration;
using System.Data.SqlClient;
using MMAWPF.登录模块;

namespace MMAWPF
{
   /// <summary>
   /// LogIn.xaml 的交互逻辑
   /// </summary>
   public partial class LogIn : Window
   {
      string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
      public LogIn()
      {
         InitializeComponent();
      }

      private void OKBtn_Click(object sender, RoutedEventArgs e)
      {
         if (UseName.Text == "" || Password.Password == "")
         {
            MessageBox.Show("请填写完整登陆信息", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                     cmd.CommandText = "select Name from AuthorTable where Name=@Name and AutherPWD=@AutherPWD";
                     cmd.Parameters.Clear();
                     cmd.Parameters.AddWithValue("Name", UseName.Text);
                     cmd.Parameters.AddWithValue("AutherPWD", AES.AESEncrypt(Password.Password));
                     SqlDataReader dr = cmd.ExecuteReader();
                     if (!dr.Read())
                     {
                        MessageBox.Show("输入的用户名或密码不正确！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                     }
                     else
                     {
                        UserLogIn.UserName = dr[0].ToString();
                        UserLogIn.EditTime = DateTime.Now;
                        this.Close();
                     }
                  }
               }
            }
            catch (Exception ex)
            {
               MessageBox.Show("无法连接数据库！");
               this.Close();
            }
         }

      }

      private void forgetPWD_Click(object sender, RoutedEventArgs e)
      {
         ForgetPWD fgpwd = new ForgetPWD();
         fgpwd.Show();
      }

      private void register_Click(object sender, RoutedEventArgs e)
      {
         Register reg = new Register();
         reg.Show();
      }
   }
}
