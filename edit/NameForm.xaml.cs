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

namespace MMAWPF.文档编辑模块
{
   /// <summary>
   /// NameForm.xaml 的交互逻辑
   /// </summary>
   public partial class NameForm : Window
   {
      public NameForm()
      {
         InitializeComponent();
      }

      private void OKBtn_Click(object sender, RoutedEventArgs e)
      {
         if (titleTxt.Text == "")
         {
            MessageBox.Show("请输入论文标题", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
         }
         else
         {
            SaveTitle.TitleName = titleTxt.Text;
            this.Close();
         }
      }
   }
}
