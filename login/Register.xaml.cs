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
   /// Register.xaml 的交互逻辑
   /// </summary>
   public partial class Register : Window
   {
      string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
      public Register()
      {
         InitializeComponent();
      }

      private void regBtn_Click(object sender, RoutedEventArgs e)
      {
         if (username.Text == "" || (man.IsChecked == false && woman.IsChecked == false) || password1.Password == "" || password2.Password == "")
         {
            MessageBox.Show("请填写完整必要的信息", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
         }
         else if (password1.Password  != password2.Password )
         {
            MessageBox.Show("两次输入的密码不相等，请重新输入！");
         }
         else if (key1.Text == "" || key2.Text == "" || key3.Text == "" || key4.Text == "")
         {
            MessageBox.Show("请输入完整的注册号码");
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
                        string cipherpwd = password1.Password;
                        byte[] bcipherpwd;
                        bcipherpwd = AES.AESEncrypt(cipherpwd);
                        cmd.CommandText = "insert into AuthorTable(Name,Sex,AutherPWD,RegTime) values(@Name,@Sex,@AutherPWD,@RegTime)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("Name", username.Text);
                        if (man.IsChecked == true)
                        {
                           cmd.Parameters.AddWithValue("Sex", "男");
                        }
                        else
                        {
                           cmd.Parameters.AddWithValue("Sex", "女");
                        }
                        cmd.Parameters.AddWithValue("AutherPWD", bcipherpwd);
                        cmd.Parameters.AddWithValue("RegTime", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("注册成功！欢迎使用数模助理");
                        username.Clear();
                        man.IsChecked = false;
                        woman.IsChecked = false;
                        password1.Clear();
                        password2.Clear();
                        key1.Clear();
                        key2.Clear();
                        key3.Clear();
                        key4.Clear();
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

      private void username_LostFocus(object sender, RoutedEventArgs e)
      {
         try
         {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
               conn.Open();
               using (SqlCommand cmd = new SqlCommand("", conn))
               {
                  cmd.CommandText = "select * from AuthorTable where Name=@Name";
                  cmd.Parameters.Clear();
                  cmd.Parameters.AddWithValue("Name", username.Text);
                  SqlDataReader dr = cmd.ExecuteReader();
                  if (dr.Read())
                  {
                     MessageBox.Show("该用户名已被注册，请重新输入新的用户名！");
                     username.Clear();
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
