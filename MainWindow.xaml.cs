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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MMAWPF.登录模块;
using MMAWPF.文档编辑模块;
using System.Windows.Threading;
namespace MMAWPF
{
   /// <summary>
   /// MainWindow.xaml 的交互逻辑
   /// </summary>
   public partial class MainWindow : Window
   {
       public static DocumentEditor DocumentEditor_Object { get; set; }
       public static Algorithm Algorithm_Object { get; set; }
       public static MathModeling MathModeling_Object { get; set; }
       public static Translation Translation_Object { get; set; }

       DispatcherTimer tm = new DispatcherTimer();

      public MainWindow()
      {
         InitializeComponent();
         tm.Tick += new EventHandler(tm_Tick);
         tm.Interval = TimeSpan.FromSeconds(1);
         tm.Start();
      }

      #region 时钟
      void tm_Tick(object sender, EventArgs e)
      {
         timelabel.Content = DateTime.Now.ToString("HH:mm:ss");
      }
      #endregion


      #region 图片按钮
      private void block_1_MouseEnter(object sender, MouseEventArgs e)
      {
         BitmapImage image = new BitmapImage(new Uri(@"/bin/Debug/images/leftup.gif", UriKind.Relative));
         block_1.Source = image;
      }

      private void block_1_MouseLeave(object sender, MouseEventArgs e)
      {
         BitmapImage image = new BitmapImage(new Uri(@"/bin/Debug/images/leftup_bottom.gif", UriKind.Relative));
         block_1.Source = image;
      }

      private void block_2_MouseEnter(object sender, MouseEventArgs e)
      {
         BitmapImage image = new BitmapImage(new Uri(@"/bin/Debug/images/rightup.gif", UriKind.Relative));
         block_2.Source = image;
      }

      private void block_2_MouseLeave(object sender, MouseEventArgs e)
      {
         BitmapImage image = new BitmapImage(new Uri(@"/bin/Debug/images/rightup_bottom.gif", UriKind.Relative));
         block_2.Source = image;
      }

      private void block_3_MouseEnter(object sender, MouseEventArgs e)
      {
         BitmapImage image = new BitmapImage(new Uri(@"/bin/Debug/images/rightdown.gif", UriKind.Relative));
         block_3.Source = image;
      }

      private void block_3_MouseLeave(object sender, MouseEventArgs e)
      {
         BitmapImage image = new BitmapImage(new Uri(@"/bin/Debug/images/rightdown_bottom.gif", UriKind.Relative));
         block_3.Source = image;
      }

      private void block_4_MouseEnter(object sender, MouseEventArgs e)
      {
         BitmapImage image = new BitmapImage(new Uri(@"/bin/Debug/images/leftdown.gif", UriKind.Relative));
         block_4.Source = image;
      }

      private void block_4_MouseLeave(object sender, MouseEventArgs e)
      {
         BitmapImage image = new BitmapImage(new Uri(@"/bin/Debug/images/leftdown_bottom.gif", UriKind.Relative));
         block_4.Source = image;
      }
      #endregion

      #region 软件信息介绍
      private void softname_Click(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("数模助理（Mathematical Modeling Assistant）\n提供算法介绍、matlab编程、文档模板化等功能\n方便学生进行数学建模", "产品信息", MessageBoxButton.OK, MessageBoxImage.Information);
      }

      private void version_Click(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("数模助理（Mathematical Modeling Assistant）\nVersion 1.0.0.0", "产品版本", MessageBoxButton.OK, MessageBoxImage.Information);
      }

      private void liujiawei_Click(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("姓      名：刘佳伟\n性      别：男\n所在学校：武汉理工大学\n专      业：物联网工程", "程序员基本信息", MessageBoxButton.OK, MessageBoxImage.Information);
      }

      private void xuyang_Click(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("姓      名：许 杨\n性      别：男\n所在学校：武汉理工大学\n专      业：物联网工程", "程序员基本信息", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      private void liubowen_Click(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("姓      名：刘博文\n性      别：男\n所在学校：武汉理工大学\n专      业：物联网工程", "程序员基本信息", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      #endregion

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         UserLogIn.UserName = "";
         DocumentEditor_Object = null;
         Algorithm_Object = null;
         MathModeling_Object = null;
         Translation_Object = null;

      }

      #region 登录—注销—注册
      private void login_MouseDown(object sender, MouseButtonEventArgs e)
      {
         if (e.LeftButton == MouseButtonState.Pressed)
         {
            LogIn log = new LogIn();
            log.ShowDialog();
            if (UserLogIn.UserName != "")
            {
               BitmapImage image = new BitmapImage(new Uri(@"/bin/Debug/images/dengluafter.jpg", UriKind.Relative));
               login.Source = image;
               login.MouseDown -= login_MouseDown;                  
            }
            else
            {
               BitmapImage image = new BitmapImage(new Uri(@"/bin/Debug/images/denglu.jpg", UriKind.Relative));
               login.Source = image;
            }
         }
      }

      private void register_MouseDown(object sender, MouseButtonEventArgs e)
      {
         if (e.LeftButton == MouseButtonState.Pressed)
         {
            Register reg = new Register();
            reg.Show();
         }
      }

      private void zhuxiao_MouseDown(object sender, MouseButtonEventArgs e)
      {
         if (e.LeftButton == MouseButtonState.Pressed)
         {
            if(UserLogIn.UserName != "")
            {
               UserLogIn.UserName = "";
               BitmapImage image = new BitmapImage(new Uri(@"/bin/Debug/images/denglu.jpg", UriKind.Relative));
               login.Source = image;
               login.MouseDown += login_MouseDown;
            }
         }
      }
      #endregion

      #region 打开各个模块
      private void block_1_MouseDown(object sender, MouseButtonEventArgs e)
      {
         if (e.LeftButton == MouseButtonState.Pressed)
         {
            if (MathModeling_Object == null)
            {
               MathModeling math = new MathModeling();
               MathModeling_Object = math;
               math.Show();
            }
            else
            {
               MathModeling_Object.Activate();
            }
         }
      }

      private void block_2_MouseDown(object sender, MouseButtonEventArgs e)
      {
         if (UserLogIn.UserName == "")
         {
            MessageBox.Show("请先登录系统。若为新用户，请先注册。");
         }
         else
         {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
               if (DocumentEditor_Object == null)
               {
                  DocumentEditor doc = new DocumentEditor();
                  DocumentEditor_Object = doc;
                  doc.Show();
               }
               else
               {
                  DocumentEditor_Object.Activate();
               }
            }
         }
      }

      private void block_3_MouseDown(object sender, MouseButtonEventArgs e)
      {
         if (e.LeftButton == MouseButtonState.Pressed)
         {
            if (UserLogIn.UserName == "")
            {
               MessageBox.Show("请先登录系统。若为新用户，请先注册。");
            }
            else
            {
               if (Translation_Object == null)
               {
                  Translation tran = new Translation();
                  Translation_Object = tran;
                  tran.Show();
               }
               else
               {
                  Translation_Object.Activate();
               }
            }
         }
      }

      private void block_4_MouseDown(object sender, MouseButtonEventArgs e)
      {
         if (e.LeftButton == MouseButtonState.Pressed)
         {

            if (Algorithm_Object == null)
            {
               Algorithm alg = new Algorithm();
               Algorithm_Object = alg;
               alg.Show();
            }
            else
            {
               Algorithm_Object.Activate();
            }
         }
      }
      #endregion

      private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (MessageBox.Show(this, "是否退出程序？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
         {
            e.Cancel = false;
         }
         else
         {
            e.Cancel = true; ;
         }
      }

      private void timelabel_MouseEnter(object sender, MouseEventArgs e)
      {
         DateTime dt = DateTime.Now;
         string dtmesg = dt.ToString("日期：yyyy/MM/dd ddd");
         timelabel.ToolTip = dtmesg;
      }


   }
}
