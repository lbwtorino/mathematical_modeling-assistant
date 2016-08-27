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
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.IO;
namespace MMAWPF.文档编辑模块
{
   /// <summary>
   /// DocumentEditer.xaml 的交互逻辑
   /// </summary>
   public partial class DocumentEditor : Window
   {
      bool modMark = false;
      string odlname;
      string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
      private ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
      private ContextMenuStrip contextMenuStrip2 = new ContextMenuStrip();
      public DocumentEditor()
      {
         InitializeComponent();
         //InitTree();
         InitContextMenuStrip();
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         IsEditing.DOCID = -1;
         IsEditing.ElementName = "";
         SaveTitle.TitleName = "";
         UserLogIn.EditTime = DateTime.Now;
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         if(com!=null)
         {
            com.CommandText = "select Title from DocView where Name=@Name";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("Name", UserLogIn.UserName);
            SqlDataReader dr = com.ExecuteReader();
            docHistory.Items.Clear();
            while (dr.Read())
            {
               string titleName = dr.GetString(0);
               docHistory.Items.Add(titleName);
            }
            DisposeClose.Disposeclose(dr);
            DisposeClose.Disposeclose(conn);
         }
         else
         {
            this.Close();
         }
      }

      private void InitTree()
      {
         TreeNode root = new TreeNode();
         root.Name = "root";
         root.Text = "root";
         treeView.Nodes.Add(root);
      }
      private void InitContextMenuStrip()
      {         
         ToolStripMenuItem tool1 = new ToolStripMenuItem();
         tool1.Name = "reName";
         tool1.Text = "更改成员名称";
         contextMenuStrip.Items.Add(tool1);
         ToolStripMenuItem tool2 = new ToolStripMenuItem();
         tool2.Name = "insertAfter";
         tool2.Text = "添加同级成员";
         contextMenuStrip.Items.Add(tool2);
         ToolStripMenuItem tool3 = new ToolStripMenuItem();
         tool3.Name = "insertChild";
         tool3.Text = "添加子成员";
         contextMenuStrip.Items.Add(tool3);
         ToolStripMenuItem tool4 = new ToolStripMenuItem();
         tool4.Name = "delete";
         tool4.Text = "删除成员";
         contextMenuStrip.Items.Add(tool4);

         tool1.Click += new System.EventHandler(this.reName_Click);
         tool2.Click += new System.EventHandler(this.insertAfter_Click);
         tool3.Click += new System.EventHandler(this.insertChild_Click);
         tool4.Click += new System.EventHandler(this.delete_Click);

         ToolStripMenuItem ctool1 = new ToolStripMenuItem();
         ctool1.Name = "cut";
         ctool1.Text = "剪切";
         contextMenuStrip2.Items.Add(ctool1);
         ToolStripMenuItem ctool2 = new ToolStripMenuItem();
         ctool2.Name = "copy";
         ctool2.Text = "复制";
         contextMenuStrip2.Items.Add(ctool2);
         ToolStripMenuItem ctool3 = new ToolStripMenuItem();
         ctool3.Name = "paste";
         ctool3.Text = "粘贴";
         contextMenuStrip2.Items.Add(ctool3);
         ToolStripMenuItem ctool4 = new ToolStripMenuItem();
         ctool4.Name = "delete";
         ctool4.Text = "删除";
         contextMenuStrip2.Items.Add(ctool4);

         ctool1.Click += new System.EventHandler(this.ccut_Click);
         ctool2.Click += new System.EventHandler(this.ccopy_Click);
         ctool3.Click += new System.EventHandler(this.cpaste_Click);
         ctool4.Click += new System.EventHandler(this.cdelete_Click);
         contentTextBox.ContextMenuStrip = contextMenuStrip2;
         contextMenuStrip2.Text = "数模助理";
      }
    
      #region 右键响应事件
      private void ccut_Click(object sender, EventArgs e)
      {
         System.Windows.Forms.Clipboard.SetText(contentTextBox.SelectedText);
         contentTextBox.SelectedText = "";
      }

      private void ccopy_Click(object sender, EventArgs e)
      {
         System.Windows.Forms.Clipboard.SetText(contentTextBox.SelectedText);
      }

      private void cpaste_Click(object sender, EventArgs e)
      {
         contentTextBox.SelectedText = System.Windows.Forms.Clipboard.GetText();
      }

      private void cdelete_Click(object sender, EventArgs e)
      {
         contentTextBox.SelectedText = "";
      }

      #region 重命名
      private void reName_Click(object sender, EventArgs e)
      {
         treeView.LabelEdit = true;
         if (!treeView.SelectedNode.IsEditing)
         {
            treeView.SelectedNode.BeginEdit();
         }
      }
      private void treeView_BeforeLabelEdit_1(object sender, NodeLabelEditEventArgs e)
      {
         odlname = e.Node.Text;
      }

      private void treeView_AfterLabelEdit_1(object sender, NodeLabelEditEventArgs e)
      {
         if (e.Label == "")
         {
            e.CancelEdit = true;
            System.Windows.Forms.MessageBox.Show("名称不能为空！");
            e.Node.BeginEdit();
         }
         else if (e.Label != null)
         {
            SqlConnection conn = new SqlConnection();
            SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
            if (e.Label != e.Node.Text)
            {
               if (e.Node.Name == "title")
               {
                  com.CommandText = "select * from DocView where Name=@Name and Title=@Title";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("Name", UserLogIn.UserName);
                  com.Parameters.AddWithValue("Title", e.Label);
                  SqlDataReader dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     System.Windows.Forms.MessageBox.Show("该文档名已存在，请重新输入新的文档名！");
                     DisposeClose.Disposeclose(dr);
                     e.CancelEdit = true;
                     e.Node.BeginEdit();
                  }
                  else
                  {
                     DisposeClose.Disposeclose(dr);
                     SaveTitle.TitleName = e.Label;
                     e.Node.Text = e.Label;
                     treeView.Nodes["root"].Text = e.Label;
                     com.CommandText = "update DocTable set Title=@Title where DocID=@DocID";
                     com.Parameters.Clear();
                     com.Parameters.AddWithValue("Title", e.Node.Text);
                     com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
                     com.ExecuteNonQuery();

                     com.CommandText = "select Title from DocView where Name=@Name";
                     com.Parameters.Clear();
                     com.Parameters.AddWithValue("Name", UserLogIn.UserName);
                     dr = com.ExecuteReader();
                     docHistory.Items.Clear();
                     while (dr.Read())
                     {
                        string titleName = dr.GetString(0);
                        docHistory.Items.Add(titleName);
                     }
                     DisposeClose.Disposeclose(dr);
                     e.Node.EndEdit(false);

                  }
               }
               else
               {
                  e.Node.Text = e.Label;
                  com.CommandText = "update TitleTable set TitleName=@TitleName where DocID=@DocID and TitleNum=@TitleNum";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("TitleName", e.Node.Text);
                  com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
                  com.Parameters.AddWithValue("TitleNum", e.Node.Name);
                  com.ExecuteNonQuery();
                  e.Node.EndEdit(false);
               }
            }
            DisposeClose.Disposeclose(com);
            DisposeClose.Disposeclose(conn);
            this.treeView.LabelEdit = false;
         }
      }
      #endregion
      
      private void insertAfter_Click(object sender, EventArgs e)
      {
         if (treeView.SelectedNode.Parent.Name == "root")
         {
            int num = treeView.SelectedNode.Parent.Nodes.Count;
            string selectedName = treeView.SelectedNode.Name;
            string[] splitStr = selectedName.Split('|');
            int length = splitStr.Length;
            splitStr[length - 1] = (num - 2).ToString();
            string newNodeName = String.Join("|", splitStr);
            string newNodeText = String.Join(".", splitStr) + "标题名";

            TreeNode tNode = new TreeNode();
            tNode.Name = newNodeName;
            tNode.Text = newNodeText;
            treeView.SelectedNode.Parent.Nodes.Insert(num - 1, tNode);
            SqlConnection conn = new SqlConnection();
            SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);

            com.CommandText="insert into TitleTable(DocID,TitleNum,TitleName) values(@DocID,@TitleNum,@TitleName)";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID",IsEditing.DOCID);
            com.Parameters.AddWithValue("TitleNum", newNodeName);
            com.Parameters.AddWithValue("TitleName", newNodeText);
            com.ExecuteNonQuery();

            com.CommandText = "insert into LeafTitleTable(DocID,LeafTitleNum) values(@DocID,@LeafTitleNum)";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", newNodeName);
            com.ExecuteNonQuery();

            DisposeClose.Disposeclose(com);
            DisposeClose.Disposeclose(conn);
         }
         else
         {
            int num = treeView.SelectedNode.Parent.Nodes.Count;
            string selectedName = treeView.SelectedNode.Name;
            string[] splitStr = selectedName.Split('|');
            int length = splitStr.Length;
            splitStr[length - 1] = (num+1).ToString();
            string newNodeName = String.Join("|", splitStr);
            string newNodeText = String.Join(".", splitStr) + "标题名";

            TreeNode tNode = new TreeNode();
            tNode.Name = newNodeName;
            tNode.Text = newNodeText;
            treeView.SelectedNode.Parent.Nodes.Add(tNode);

            SqlConnection conn = new SqlConnection();
            SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
            com.CommandText = "insert into TitleTable(DocID,TitleNum,TitleName) values(@DocID,@TitleNum,@TitleName)";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("TitleNum", newNodeName);
            com.Parameters.AddWithValue("TitleName", newNodeText);
            com.ExecuteNonQuery();

            com.CommandText = "insert into LeafTitleTable(DocID,LeafTitleNum) values(@DocID,@LeafTitleNum)";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", newNodeName);
            com.ExecuteNonQuery();

            DisposeClose.Disposeclose(com);
            DisposeClose.Disposeclose(conn);
         }
        
      }
      
      private void insertChild_Click(object sender, EventArgs e)
      {

         if (treeView.SelectedNode.Nodes.Count == 0)
         {
            string selectedName = treeView.SelectedNode.Name;
            string[] splitStr = selectedName.Split('|');

            string newNodeName = selectedName + "|1";
            string newNodeText = String.Join(".", splitStr) + ".1" + "标题名";
            TreeNode tNode = new TreeNode();
            tNode.Name = newNodeName;
            tNode.Text = newNodeText;
            treeView.SelectedNode.Nodes.Add(tNode);

            SqlConnection conn = new SqlConnection();
            SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
            com.CommandText = "delete from LeafTitleTable where DocID=@DocID and LeafTitleNum=@LeafTitleNum";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID",IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum",treeView.SelectedNode.Name);
            com.ExecuteNonQuery();

            com.CommandText = "insert into TitleTable(DocID,TitleNum,TitleName) values(@DocID,@TitleNum,@TitleName)";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID",IsEditing.DOCID);
            com.Parameters.AddWithValue("TitleNum",newNodeName);
            com.Parameters.AddWithValue("TitleName", newNodeText);
            com.ExecuteNonQuery();

            com.CommandText = "insert into LeafTitleTable(DocID,LeafTitleNum) values(@DocID,@LeafTitleNum)";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", newNodeName);
            com.ExecuteNonQuery();

            DisposeClose.Disposeclose(com);
            DisposeClose.Disposeclose(conn);

         }
         else
         {
            string selectedName = treeView.SelectedNode.Name;
            string[] splitStr = selectedName.Split('|');
            int num = treeView.SelectedNode.Nodes.Count;
            string newNodeName = selectedName + "|"+(num+1).ToString();
            string newNodeText = String.Join(".", splitStr) + "." + (num + 1).ToString() + "标题名";
            TreeNode tNode = new TreeNode();
            tNode.Name = newNodeName;
            tNode.Text = newNodeText;
            treeView.SelectedNode.Nodes.Add(tNode);

            SqlConnection conn = new SqlConnection();
            SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
            com.CommandText = "insert into TitleTable(DocID,TitleNum,TitleName) values(@DocID,@TitleNum,@TitleName)";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("TitleNum", newNodeName);
            com.Parameters.AddWithValue("TitleName", newNodeText);
            com.ExecuteNonQuery();

            com.CommandText = "insert into LeafTitleTable(DocID,LeafTitleNum) values(@DocID,@LeafTitleNum)";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", newNodeName);
            com.ExecuteNonQuery();

            DisposeClose.Disposeclose(com);
            DisposeClose.Disposeclose(conn);
         }
      }

      private void delete_Click(object sender, EventArgs e)
      {

         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         TreeNode parentnode = treeView.SelectedNode.Parent;
         if (treeView.SelectedNode.Nodes.Count == 0)
         {
            com.CommandText = "delete from LeafTitleTable where LeafTitleNum=@LeafTitleNum and DocID=@DocID";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("LeafTitleNum", treeView.SelectedNode.Name);
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.ExecuteNonQuery();

            com.CommandText = "delete from TitleTable where TitleNum=@TitleNum and DocID=@DocID";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("TitleNum", treeView.SelectedNode.Name);
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.ExecuteNonQuery();
            treeView.Nodes.Remove(treeView.SelectedNode);
         }
         if (parentnode.Nodes.Count == 0)
         {
            com.CommandText = "insert into LeafTitleTable(DocID,LeafTitleNum) values(@DocID,@LeafTitleNum)";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", parentnode.Name);
            com.ExecuteNonQuery();
         }
         DisposeClose.Disposeclose(com);
         DisposeClose.Disposeclose(conn);


      }
      #endregion

      #region 窗体关闭响应事件
      private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

      private void Window_Closed(object sender, EventArgs e)
      {
         MainWindow.DocumentEditor_Object = null;
      }
      #endregion

      #region 插入图片 表格 公式按钮
      private void picBtn_Click(object sender, RoutedEventArgs e)
      {
         if (treeView.Nodes.Count != 0 && IsEditing.ElementName != "")
         {
            image6.Visibility = Visibility.Hidden;
            InsertPicture iPicture = new InsertPicture();
            iPicture.ShowDialog();
            if (Picture.isChanged )
            {
               contentTextBox.AppendText("\n" + Picture.PicTag + "\n");
               LoadPicture();
               SaveDoc();
            }
            image6.Visibility = Visibility.Visible;
         }
      }

      private void tableBtn_Click(object sender, RoutedEventArgs e)
      {
         if (treeView.Nodes.Count != 0 && IsEditing.ElementName != "")
         {
            image7.Visibility = Visibility.Hidden;
            InsertTable iTable = new InsertTable();
            iTable.ShowDialog();
            if (Table.isChanged)
            {
               contentTextBox.AppendText("\n" + Table.TableTag + "\n");
               LoadTable();
               SaveDoc();
            }
            image7.Visibility = Visibility.Visible;
         }
      }

      private void button1_Click(object sender, RoutedEventArgs e)
      {
         if (treeView.Nodes.Count != 0 && IsEditing.ElementName != "")
         {
            image4.Visibility = Visibility.Hidden;
            Formula.ModFName = "";
            InsertFormula iFormula = new InsertFormula();
            iFormula.ShowDialog();
            if (Formula.isChanged)
            {
               contentTextBox.AppendText("\n" + Formula.FTag + "\n");
               LoadFormula();
               SaveDoc();
            }
            image4.Visibility = Visibility.Visible;
         }
      }
      #endregion

      #region 常用符号
      private void jia_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(jia.Content.ToString());
      }

      private void jian_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(jian.Content.ToString());
      }

      private void cheng_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(cheng.Content.ToString());
      }

      private void chu_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(chu.Content.ToString());
      }

      private void budengyu_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(budengyu.Content.ToString());
      }

      private void genhao_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(genhao.Content.ToString());
      }

      private void jifen_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(jifen.Content.ToString());
      }

      private void wuqiongda_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(wuqiongda.Content.ToString());
      }

      private void xiaoyudengyu_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(xiaoyudengyu.Content.ToString());
      }

      private void dayudengyu_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(dayudengyu.Content.ToString());
      }

      private void pi_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(pi.Content.ToString());
      }

      private void qiuhe_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(qiuhe.Content.ToString());
      }

      private void log_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(log.Content.ToString());
      }

      private void lg_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(lg.Content.ToString());
      }

      private void ln_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(ln.Content.ToString());
      }

      private void shuyu_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(shuyu.Content.ToString());
      }

      private void jiao_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(jiao.Content.ToString());
      }

      private void bing_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(bing.Content.ToString());
      }

      private void luojijiao_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(luojijiao.Content.ToString());
      }

      private void luojibing_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(luojibing.Content.ToString());
      }

      private void α_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(α.Content.ToString());
      }

      private void β_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(β.Content.ToString());
      }

      private void γ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(γ.Content.ToString());
      }

      private void δ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(δ.Content.ToString());
      }

      private void ε_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(ε.Content.ToString());
      }

      private void ζ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(ζ.Content.ToString());
      }

      private void η_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(η.Content.ToString());
      }

      private void θ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(θ.Content.ToString());
      }

      private void l_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(l.Content.ToString());
      }

      private void κ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(κ.Content.ToString());
      }

      private void λ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(λ.Content.ToString());
      }

      private void μ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(μ.Content.ToString());
      }

      private void ν_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(ν.Content.ToString());
      }

      private void ξ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(ξ.Content.ToString());
      }

      private void ο_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(ο.Content.ToString());
      }

      private void π_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(π.Content.ToString());
      }

      private void ρ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(ρ.Content.ToString());
      }

      private void σ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(σ.Content.ToString());
      }

      private void τ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(τ.Content.ToString());
      }

      private void υ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(υ.Content.ToString());
      }

      private void φ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(φ.Content.ToString());
      }

      private void χ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(χ.Content.ToString());
      }

      private void ψ_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(ψ.Content.ToString());
      }

      private void ω_Click(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(ω.Content.ToString());
      }

      private void Γ_Click_1(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(Γ.Content.ToString());
      }

      private void Δ_Click_1(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(Δ.Content.ToString());
      }

      private void Θ_Click_1(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(Θ.Content.ToString());
      }

      private void Λ_Click_1(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(Λ.Content.ToString());
      }

      private void Ξ_Click_1(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(Ξ.Content.ToString());
      }

      private void Ο_Click_1(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(Ο.Content.ToString());
      }

      private void Φ_Click_1(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(Φ.Content.ToString());
      }

      private void Χ_Click_1(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(Χ.Content.ToString());
      }

      private void Ψ_Click_1(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(Ψ.Content.ToString());
      }

      private void Ω_Click_1(object sender, RoutedEventArgs e)
      {
         contentTextBox.AppendText(Ω.Content.ToString());
      }
      #endregion

      #region 符号窗口打开或关闭
      private void signal_expander_Expanded(object sender, RoutedEventArgs e)
      {
         textboxgrid.ColumnDefinitions[1].Width = new GridLength(220, GridUnitType.Pixel);
      }

      private void signal_expander_Collapsed(object sender, RoutedEventArgs e)
      {
         textboxgrid.ColumnDefinitions[1].Width = new GridLength(30, GridUnitType.Pixel);
      }
      #endregion

      #region 控制修改文档结构树
      private void modBtn_Click(object sender, RoutedEventArgs e)
      {
         if (treeView.Nodes.Count == 0)
         {
            System.Windows.Forms.MessageBox.Show("暂无可修改的文档结构");
         }
         else
         {
            modMark = true;
            topgrid.IsEnabled = false;
            middlegrid.IsEnabled = false;
            textboxgrid.IsEnabled = false;
            rightgrid.IsEnabled = false;
         }
      }

      private void comBtn_Click(object sender, RoutedEventArgs e)
      {
         if (treeView.Nodes.Count == 0)
         {
            System.Windows.Forms.MessageBox.Show("暂无可修改的文档结构");
         }
         else
         {
            modMark = false;
            topgrid.IsEnabled = true;
            middlegrid.IsEnabled = true;
            textboxgrid.IsEnabled = true;
            rightgrid.IsEnabled = true;
         }
      }
      #endregion

      private void CreateInitTree(SqlCommand com, SqlDataReader dr, string title)
      {
         InitTree();
         treeView.Nodes["root"].Text = title;
         TreeNode tNode = new TreeNode();
         tNode.Name = "title";
         tNode.Text = SaveTitle.TitleName;
         treeView.Nodes["root"].Nodes.Add(tNode);

         TreeNode tNode1 = new TreeNode();
         tNode1.Name = "summary";
         tNode1.Text = "摘要";
         treeView.Nodes["root"].Nodes.Add(tNode1);

         TreeNode tNode2 = new TreeNode();
         tNode2.Name = "scontent";
         tNode2.Text = "摘要内容";
         tNode1.Nodes.Add(tNode2);

         TreeNode tNode3 = new TreeNode();
         tNode3.Name = "keyword";
         tNode3.Text = "关键字";
         tNode1.Nodes.Add(tNode3);

         TreeNode tNode4 = new TreeNode();
         tNode4.Name = "1";
         tNode4.Text = "1标题名";
         treeView.Nodes["root"].Nodes.Add(tNode4);

         TreeNode tNode4_1 = new TreeNode();
         tNode4_1.Name = "1|1";
         tNode4_1.Text = "1.1标题名";
         tNode4.Nodes.Add(tNode4_1);

         TreeNode tNode5 = new TreeNode();
         tNode5.Name = "bibliography";
         tNode5.Text = "参考文献";
         treeView.Nodes["root"].Nodes.Add(tNode5);

         //treeView.Visible = true;
         treeView.ExpandAll();
         int docID;
         com.CommandText = "select top 1 DocID from DocTable order by DocID desc";
         dr = com.ExecuteReader();
         if (!dr.Read())
         {
            docID = 0;
         }
         else
         {
            docID = dr.GetInt32(0) + 1;
         }         
         DisposeClose.Disposeclose(dr);
         com.CommandText = "select AutherID from AuthorTable where Name='" + UserLogIn.UserName + "'";
         dr = com.ExecuteReader();
         dr.Read();
         int autherID = dr.GetInt32(0);
         DisposeClose.Disposeclose(dr);
         com.CommandText = "insert into DocTable(DocID,AutherID,Title) values(@DocID,@AutherID,@Title)";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", docID);
         com.Parameters.AddWithValue("AutherID", autherID);
         com.Parameters.AddWithValue("Title", treeView.Nodes["root"].Text);
         com.ExecuteNonQuery();

         com.CommandText = "insert into TitleTable(DocID,TitleNum,TitleName) values(@DocID,@TitleNum,@TitleName)";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", docID);
         com.Parameters.AddWithValue("TitleNum", "1");
         com.Parameters.AddWithValue("TitleName", "1标题名");
         com.ExecuteNonQuery();
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", docID);
         com.Parameters.AddWithValue("TitleNum", "1|1");
         com.Parameters.AddWithValue("TitleName", "1.1标题名");
         com.ExecuteNonQuery();

         com.CommandText = "insert into LeafTitleTable(DocID,LeafTitleNum) values(@DocID,@LeafTitleNum)";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", docID);
         com.Parameters.AddWithValue("LeafTitleNum", "1|1");
         com.ExecuteNonQuery();
         IsEditing.DOCID = docID;
      }

      #region 新建、选择、删除
      private void newBtn_Click(object sender, RoutedEventArgs e)
      {
         SaveTitle.TitleName = "";

         if (treeView.Nodes.Count != 0)
         {
            DialogResult dresult = System.Windows.Forms.MessageBox.Show("是否保存当前文档，并新建另一文档？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dresult == System.Windows.Forms.DialogResult.OK)
            {
               //清除文档结构树的信息
               treeView.Nodes[0].Remove();
               contentTextBox.Clear();
               NameForm nf = new NameForm();
               nf.ShowDialog();

               if (SaveTitle.TitleName != "")
               {
                  SqlConnection conn = new SqlConnection();
                  SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
                  com.CommandText = "select count(Title) from DocView where Name='" + UserLogIn.UserName + "' and Title= '" + SaveTitle.TitleName + "'";
                  SqlDataReader dr = com.ExecuteReader();
                  dr.Read();
                  int number = dr.GetInt32(0);
                  DisposeClose.Disposeclose(dr);

                  if (number == 0)
                  {                     
                     CreateInitTree(com, dr, SaveTitle.TitleName);

                     com.CommandText = "select Title from DocView where Name=@Name";
                     com.Parameters.Clear();
                     com.Parameters.AddWithValue("Name", UserLogIn.UserName);
                     dr = com.ExecuteReader();
                     docHistory.Items.Clear();
                     while (dr.Read())
                     {
                        string titleName = dr.GetString(0);
                        docHistory.Items.Add(titleName);
                     }
                     DisposeClose.Disposeclose(dr);

                     DisposeClose.Disposeclose(com);
                     DisposeClose.Disposeclose(conn);

                  }
                  else
                  {
                     CreateInitTree(com, dr, SaveTitle.TitleName + "(" + number.ToString() + ")");

                     com.CommandText = "select Title from DocView where Name=@Name";
                     com.Parameters.Clear();
                     com.Parameters.AddWithValue("Name", UserLogIn.UserName);
                     dr = com.ExecuteReader();
                     docHistory.Items.Clear();
                     while (dr.Read())
                     {
                        string titleName = dr.GetString(0);
                        docHistory.Items.Add(titleName);
                     }
                     DisposeClose.Disposeclose(dr);

                     DisposeClose.Disposeclose(com);
                     DisposeClose.Disposeclose(conn);
                  }
               }
            }

         }
         else
         {
            NameForm nf = new NameForm();
            nf.ShowDialog();

            if (SaveTitle.TitleName != "")
            {
               SqlConnection conn = new SqlConnection();
               SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
               com.CommandText = "select count(Title) from DocView where Name='" + UserLogIn.UserName + "' and Title= '" + SaveTitle.TitleName + "'";
               SqlDataReader dr = com.ExecuteReader();
               dr.Read();
               int number = dr.GetInt32(0);
               DisposeClose.Disposeclose(dr);
               if (number == 0)
               {
                  CreateInitTree(com, dr, SaveTitle.TitleName);

                  com.CommandText = "select Title from DocView where Name=@Name";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("Name", UserLogIn.UserName);
                  dr = com.ExecuteReader();
                  docHistory.Items.Clear();
                  while (dr.Read())
                  {
                     string titleName = dr.GetString(0);
                     docHistory.Items.Add(titleName);
                  }
                  DisposeClose.Disposeclose(dr);

                  DisposeClose.Disposeclose(com);
                  DisposeClose.Disposeclose(conn);
               }
               else
               {
                  CreateInitTree(com, dr, SaveTitle.TitleName + "(" + number.ToString() + ")");

                  com.CommandText = "select Title from DocView where Name=@Name";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("Name", UserLogIn.UserName);
                  dr = com.ExecuteReader();
                  docHistory.Items.Clear();
                  while (dr.Read())
                  {
                     string titleName = dr.GetString(0);
                     docHistory.Items.Add(titleName);
                  }
                  DisposeClose.Disposeclose(dr);

                  DisposeClose.Disposeclose(com);
                  DisposeClose.Disposeclose(conn);
               }
            }
         }
      }

      private void selectBtn_Click(object sender, RoutedEventArgs e)
      {
         if (docHistory.Text == "")
         {
            System.Windows.Forms.MessageBox.Show("请选择要载入的文档！");
         }
         else
         {
            if (treeView.Nodes.Count != 0)
            {
               if (SaveTitle.TitleName == docHistory.Text)
               {
                  System.Windows.Forms.MessageBox.Show("该文档已载入，无需重载！");
               }
               else
               {
                  DialogResult dresult = System.Windows.Forms.MessageBox.Show("是否保存当前文档，并载入选定的文档？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                  if (dresult == System.Windows.Forms.DialogResult.OK)
                  {
                     contentTextBox.Clear();
                     int count = treeView.Nodes[0].Nodes.Count;
                     for (int i = 0; i < count; i++)
                     {
                        treeView.Nodes[0].Nodes[0].Remove();//每次调用时treeView.Nodes[0].Nodes.Count的值是不一样的，所以每次选择删除第0个节点。
                     }
                     treeView.Nodes[0].Remove();
                     //treeView.Visible = false;

                     LoadTree();
                  }
               }
            }
            else
            {
               LoadTree();
            }
         }
      }

      private void deleteBtn_Click(object sender, RoutedEventArgs e)
      {
         if (docHistory.Text == "")
         {
            System.Windows.Forms.MessageBox.Show("请选择要删除的文档！");
         }
         else
         {
            if (treeView.Nodes.Count != 0 && treeView.Nodes[0].Text == docHistory.Text)
            {
               DialogResult dresult = System.Windows.Forms.MessageBox.Show("当前选择的文档已载入，是否继续删除？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

               if (dresult == System.Windows.Forms.DialogResult.OK)
               {
                  treeView.Nodes[0].Remove();
                  SqlConnection conn = new SqlConnection();
                  SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
                  com.CommandText = "select DocID from DocTable where Title=@Title";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("Title", docHistory.Text);
                  SqlDataReader dr = com.ExecuteReader();
                  dr.Read();
                  int docID = dr.GetInt32(0);
                  DisposeClose.Disposeclose(dr);
                  com.CommandText = "delete from LeafFormulaTag where DocID=@DocID";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("DocID", docID);
                  com.ExecuteNonQuery();

                  com.CommandText = "delete from LeafPictureTag where DocID=@DocID";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("DocID", docID);
                  com.ExecuteNonQuery();

                  com.CommandText = "delete from LeafTableTag where DocID=@DocID";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("DocID", docID);
                  com.ExecuteNonQuery();
                  //删除LeafTitleTable文档信息                  
                  com.CommandText = "delete from LeafTitleTable where DocID=@DocID";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("DocID", docID);
                  com.ExecuteNonQuery();
                  //删除TitleTable文档信息
                  com.CommandText = "delete from TitleTable where DocID=@DocID";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("DocID", docID);
                  com.ExecuteNonQuery();
                  //删除DocTable文档信息
                  com.CommandText = "delete from DocTable where DocID=@DocID";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("DocID", docID);
                  com.ExecuteNonQuery();

                  docHistory.Text = "";
                  com.CommandText = "select Title from DocView where Name=@Name";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("Name", UserLogIn.UserName);
                  dr = com.ExecuteReader();
                  docHistory.Items.Clear();
                  while (dr.Read())
                  {
                     string titleName = dr.GetString(0);
                     docHistory.Items.Add(titleName);
                  }
                  DisposeClose.Disposeclose(dr);
                  DisposeClose.Disposeclose(conn);
               }
            }
            else
            {
               SqlConnection conn = new SqlConnection();
               SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
               com.CommandText = "select DocID from DocTable where Title=@Title";
               com.Parameters.Clear();
               com.Parameters.AddWithValue("Title", docHistory.Text);
               SqlDataReader dr = com.ExecuteReader();
               dr.Read();
               int docID = dr.GetInt32(0);
               DisposeClose.Disposeclose(dr);

               com.CommandText = "delete from LeafFormulaTag where DocID=@DocID";
               com.Parameters.Clear();
               com.Parameters.AddWithValue("DocID", docID);
               com.ExecuteNonQuery();

               com.CommandText = "delete from LeafPictureTag where DocID=@DocID";
               com.Parameters.Clear();
               com.Parameters.AddWithValue("DocID", docID);
               com.ExecuteNonQuery();

               com.CommandText = "delete from LeafTableTag where DocID=@DocID";
               com.Parameters.Clear();
               com.Parameters.AddWithValue("DocID", docID);
               com.ExecuteNonQuery();

               //删除LeafTitleTable文档信息
               com.CommandText = "delete from LeafTitleTable where DocID=@DocID";
               com.Parameters.Clear();
               com.Parameters.AddWithValue("DocID", docID);
               com.ExecuteNonQuery();
               //删除TitleTable文档信息
               com.CommandText = "delete from TitleTable where DocID=@DocID";
               com.Parameters.Clear();
               com.Parameters.AddWithValue("DocID", docID);
               com.ExecuteNonQuery();
               //删除DocTable文档信息
               com.CommandText = "delete from DocTable where DocID=@DocID";
               com.Parameters.Clear();
               com.Parameters.AddWithValue("DocID", docID);
               com.ExecuteNonQuery();

               docHistory.Text = "";
               com.CommandText = "select Title from DocView where Name=@Name";
               com.Parameters.Clear();
               com.Parameters.AddWithValue("Name", UserLogIn.UserName);
               dr = com.ExecuteReader();
               docHistory.Items.Clear();
               while (dr.Read())
               {
                  string titleName = dr.GetString(0);
                  docHistory.Items.Add(titleName);
               }
               DisposeClose.Disposeclose(dr);
               DisposeClose.Disposeclose(conn);
            }
         }
      }
      #endregion

      private void LoadTree()
      {
         InitTree();
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         com.CommandText = "select DocID  from DocView where Title=@Title and Name=@Name";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("Title", docHistory.Text);
         com.Parameters.AddWithValue("Name", UserLogIn.UserName);
         SqlDataReader dr = com.ExecuteReader();
         int docID;
         if (dr.Read())
         {
            docID = dr.GetInt32(0);
         }
         else
         {
            docID = -1;
         }
         DisposeClose.Disposeclose(dr);
         SaveTitle.TitleName = docHistory.Text;
         treeView.Nodes["root"].Text = SaveTitle.TitleName;
         TreeNode tNode = new TreeNode();
         tNode.Name = "title";
         tNode.Text = SaveTitle.TitleName;
         treeView.Nodes["root"].Nodes.Add(tNode);

         TreeNode tNode1 = new TreeNode();
         tNode1.Name = "summary";
         tNode1.Text = "摘要";
         treeView.Nodes["root"].Nodes.Add(tNode1);

         TreeNode tNode2 = new TreeNode();
         tNode2.Name = "scontent";
         tNode2.Text = "摘要内容";
         tNode1.Nodes.Add(tNode2);

         TreeNode tNode3 = new TreeNode();
         tNode3.Name = "keyword";
         tNode3.Text = "关键词";
         tNode1.Nodes.Add(tNode3);

         TreeNode tNode4 = new TreeNode();
         tNode4.Name = "bibliography";
         tNode4.Text = "参考文献";
         treeView.Nodes["root"].Nodes.Add(tNode4);

         SqlDataAdapter da = new SqlDataAdapter(com);
         da.SelectCommand.CommandText = "select TitleNum,TitleName from TitleView where Title=@Title and DocID=@DocID order by TitleNum";
         da.SelectCommand.Parameters.Clear();
         da.SelectCommand.Parameters.AddWithValue("Title", SaveTitle.TitleName);
         da.SelectCommand.Parameters.AddWithValue("DocID", docID);
         DataSet ds = new DataSet();
         da.Fill(ds);

         DataTable dtable = ds.Tables[0];

         int rowNum = dtable.Rows.Count;
         for (int i = 0; i < rowNum; i++)
         {
            string titlenum = dtable.Rows[i][0].ToString();
            string titletxt = dtable.Rows[i][1].ToString();
            string[] splittitle = titlenum.Split('|');
            if (splittitle.Length == 1)
            {
               int num = treeView.Nodes["root"].Nodes.Count;
               TreeNode tn = new TreeNode();
               tn.Name = titlenum;
               tn.Text = titletxt;
               treeView.Nodes["root"].Nodes.Insert(num - 1, tn);
            }
            else
            {
               string[] newsplittile = new string[splittitle.Length - 1];
               for (int j = 0; j < splittitle.Length - 1; j++)
               {
                  newsplittile[j] = splittitle[j];
               }
               string newtitlenum = String.Join("|", newsplittile);
               TreeNode tn = new TreeNode();
               tn.Name = titlenum;
               tn.Text = titletxt;
               TreeNode ptn = new TreeNode();
               GetNode(treeView.Nodes, newtitlenum, ref ptn);
               ptn.Nodes.Add(tn);
            }
         }
         //treeView.Visible = true;
         treeView.ExpandAll();
         DisposeClose.Disposeclose(com);
         DisposeClose.Disposeclose(conn);
         IsEditing.DOCID = docID;
      }

      private void GetNode(TreeNodeCollection tnc, string nodename, ref TreeNode tn)
      {
         foreach (TreeNode node in tnc)
         {
            if (node.Name == nodename)
            {
               tn = node;
               break;
            }
            GetNode(node.Nodes, nodename, ref tn);
         }
      }

      private void SaveDoc()
      {
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         if (IsEditing.ElementName == "scontent")
         {
            com.CommandText = "update DocTable set Summary=@Summary where DocID=@DocID";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("Summary", contentTextBox.Text);
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.ExecuteNonQuery();
         }
         else if (IsEditing.ElementName == "keyword")
         {
            com.CommandText = "update DocTable set keyword=@keyword where DocID=@DocID";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("keyword", contentTextBox.Text);
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.ExecuteNonQuery();
         }
         else if (IsEditing.ElementName == "bibliography")
         {
            com.CommandText = "update DocTable set bibliography=@bibliography where DocID=@DocID";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("bibliography", contentTextBox.Text);
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.ExecuteNonQuery();
         }
         else if (IsEditing.ElementName != "")
         {
            com.CommandText = "update LeafTitleTable set Content=@Content where DocID=@DocID and LeafTitleNum=@LeafTitleNum";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("Content", contentTextBox.Text);
            com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.ExecuteNonQuery();
         }
      }

      private void inputBtn_Click(object sender, RoutedEventArgs e)
      {
         if (treeView.Nodes.Count != 0)
         {
            SaveDoc();
         }
      }

      #region 控制右键响应事件
      private void treeView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Right)
         {
            System.Drawing.Point clickPoint = new System.Drawing.Point(e.X, e.Y);
            TreeNode currentNode = treeView.GetNodeAt(clickPoint);

            if (currentNode != null && modMark)
            {
               treeView.SelectedNode = currentNode;
               treeView.ContextMenuStrip = contextMenuStrip;
               if (currentNode.Name == "root")
               {
                  contextMenuStrip.Items[0].Enabled = false;
                  contextMenuStrip.Items[1].Enabled = false;
                  contextMenuStrip.Items[2].Enabled = false;
                  contextMenuStrip.Items[3].Enabled = false;
               }
               else if (currentNode.Name == "title")
               {
                  contextMenuStrip.Items[0].Enabled = true;
                  contextMenuStrip.Items[1].Enabled = false;
                  contextMenuStrip.Items[2].Enabled = false;
                  contextMenuStrip.Items[3].Enabled = false;
               }
               else if (currentNode.Name == "summary" || currentNode.Name == "bibliography" || currentNode.Name == "scontent" || currentNode.Name == "keyword")
               {
                  contextMenuStrip.Items[0].Enabled = false;
                  contextMenuStrip.Items[1].Enabled = false;
                  contextMenuStrip.Items[2].Enabled = false;
                  contextMenuStrip.Items[3].Enabled = false;
               }
               else if ((currentNode.Level == 1 && currentNode.Name == "1") || (currentNode.Level == 1 && currentNode.NextNode.Name != "bibliography") || (currentNode.Level > 1 && currentNode.NextNode != null) || currentNode.Nodes.Count != 0)
               {
                  contextMenuStrip.Items[0].Enabled = true;
                  contextMenuStrip.Items[1].Enabled = true;
                  contextMenuStrip.Items[2].Enabled = true;
                  contextMenuStrip.Items[3].Enabled = false;
               }
               else
               {
                  contextMenuStrip.Items[0].Enabled = true;
                  contextMenuStrip.Items[1].Enabled = true;
                  contextMenuStrip.Items[2].Enabled = true;
                  contextMenuStrip.Items[3].Enabled = true;
               }
            }
            else
            {
               treeView.ContextMenuStrip = null;
            }
         }
      }
      private void contentTextBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Right)
         {
            if (contentTextBox.SelectedText != "")
            {
               contextMenuStrip2.Items[0].Enabled = true;
               contextMenuStrip2.Items[1].Enabled = true;
               contextMenuStrip2.Items[2].Enabled = true;
               contextMenuStrip2.Items[3].Enabled = true;
            }
            else
            {
               contextMenuStrip2.Items[0].Enabled = false;
               contextMenuStrip2.Items[1].Enabled = false;
               contextMenuStrip2.Items[2].Enabled = true;
               contextMenuStrip2.Items[3].Enabled = false;
            }
         }
      }
      #endregion

      #region 更新元素记录
      private void LoadTable()
      {
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         com.CommandText = "select count(TTag) from LeafTableTag where DocID=@DocID";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         SqlDataReader dr = com.ExecuteReader();
         if (dr.Read())
         {
            Table.TNum = dr.GetInt32(0);
            DisposeClose.Disposeclose(dr);
         }
         else
         {
            DisposeClose.Disposeclose(dr);
            Table.TNum = 0;
         }
         com.CommandText = "select TTag from LeafTableTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum order by TTag";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
         dr = com.ExecuteReader();
         tablemarkbox.Items.Clear();
         while (dr.Read())
         {
            string ttag = dr.GetString(0);
            ttag = ttag.Replace("<table>", "");
            tablemarkbox.Items.Add(ttag);
         }
         DisposeClose.Disposeclose(dr);
         DisposeClose.Disposeclose(com);
         DisposeClose.Disposeclose(conn);
      }

      private void LoadPicture()
      {
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         com.CommandText = "select count(PTag) from LeafPictureTag where DocID=@DocID";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         SqlDataReader dr = com.ExecuteReader();
         if (dr.Read())
         {
            Picture.PNum = dr.GetInt32(0);
            DisposeClose.Disposeclose(dr);
         }
         else
         {
            DisposeClose.Disposeclose(dr);
            Picture.PNum = 0;
         }

         com.CommandText = "select PTag from LeafPictureTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum order by PTag";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
         dr = com.ExecuteReader();
         picmarkbox.Items.Clear();
         while (dr.Read())
         {
            string ttag = dr.GetString(0);
            ttag = ttag.Replace("<picture>", "");
            picmarkbox.Items.Add(ttag);
         }
         DisposeClose.Disposeclose(dr);
         DisposeClose.Disposeclose(com);
         DisposeClose.Disposeclose(conn);

      }

      private void LoadFormula()
      {
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         com.CommandText = "select count(FTag) from LeafFormulaTag where DocID=@DocID";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         SqlDataReader dr = com.ExecuteReader();
         if (dr.Read())
         {
            Formula.FNumber = dr.GetInt32(0);
            DisposeClose.Disposeclose(dr);
         }
         else
         {
            DisposeClose.Disposeclose(dr);
            Formula.FNumber = 0;
         }

         com.CommandText = "select FTag from LeafFormulaTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum order by FTag";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
         dr = com.ExecuteReader();
         formarkbox.Items.Clear();
         while (dr.Read())
         {
            string ttag = dr.GetString(0);
            ttag = ttag.Replace("<formula>", "");
            formarkbox.Items.Add(ttag);
         }
         DisposeClose.Disposeclose(dr);
         DisposeClose.Disposeclose(com);
         DisposeClose.Disposeclose(conn);
      }

      private void LoadAll()
      {
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         com.CommandText = "select count(FTag) from LeafFormulaTag where DocID=@DocID";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         SqlDataReader dr = com.ExecuteReader();
         if (dr.Read())
         {
            Formula.FNumber = dr.GetInt32(0);
            DisposeClose.Disposeclose(dr);
         }
         else
         {
            DisposeClose.Disposeclose(dr);
            Formula.FNumber = 0;
         }

         com.CommandText = "select FTag from LeafFormulaTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum order by FTag";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
         dr = com.ExecuteReader();
         formarkbox.Items.Clear();
         while (dr.Read())
         {
            string ttag = dr.GetString(0);
            ttag = ttag.Replace("<formula>", "");
            ttag = ttag.Replace("</formula>", "");
            formarkbox.Items.Add(ttag);
         }
         DisposeClose.Disposeclose(dr);


         com.CommandText = "select count(PTag) from LeafPictureTag where DocID=@DocID";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         dr = com.ExecuteReader();
         if (dr.Read())
         {
            Picture.PNum = dr.GetInt32(0);
            DisposeClose.Disposeclose(dr);
         }
         else
         {
            DisposeClose.Disposeclose(dr);
            Picture.PNum = 0;
         }

         com.CommandText = "select PTag from LeafPictureTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum order by PTag";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
         dr = com.ExecuteReader();
         picmarkbox.Items.Clear();
         while (dr.Read())
         {
            string ttag = dr.GetString(0);
            ttag = ttag.Replace("<picture>", "");
            ttag = ttag.Replace("</picture>", "");
            picmarkbox.Items.Add(ttag);
         }
         DisposeClose.Disposeclose(dr);


         com.CommandText = "select count(TTag) from LeafTableTag where DocID=@DocID";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         dr = com.ExecuteReader();
         if (dr.Read())
         {
            Table.TNum = dr.GetInt32(0);
            DisposeClose.Disposeclose(dr);
         }
         else
         {
            DisposeClose.Disposeclose(dr);
            Table.TNum = 0;
         }
         com.CommandText = "select TTag from LeafTableTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum order by TTag";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
         dr = com.ExecuteReader();
         tablemarkbox.Items.Clear();
         while (dr.Read())
         {
            string ttag = dr.GetString(0);
            ttag = ttag.Replace("<table>", "");
            ttag = ttag.Replace("</table>", "");
            tablemarkbox.Items.Add(ttag);
         }
         DisposeClose.Disposeclose(dr);
         DisposeClose.Disposeclose(com);
         DisposeClose.Disposeclose(conn);

      }
      #endregion

      private void treeView_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         if (!modMark)
         {
            SqlConnection conn = new SqlConnection();
            SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
            com.CommandText = "select DocID from DocView where Title=@Title and Name=@Name";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("Title", SaveTitle.TitleName);
            com.Parameters.AddWithValue("Name", UserLogIn.UserName);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            int docID = dr.GetInt32(0);
            DisposeClose.Disposeclose(dr);
            //判断是否为叶子节点
            if (treeView.SelectedNode.Nodes.Count == 0)
            {
               IsEditing.ElementName = treeView.SelectedNode.Name;
               IsEditing.DOCID = docID;
               if (treeView.SelectedNode.Name == "title")
               {
                  //do nothing 
               }
               else if (treeView.SelectedNode.Name == "scontent")
               {
                  com.CommandText = "select Summary from DocTable where DocID=@DocID";
                  com.Parameters.AddWithValue("DocID", docID);
                  dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     if (dr[0].ToString() != "")
                     {
                        contentTextBox.Text = dr[0].ToString();
                     }
                     else
                     {
                        contentTextBox.Text="";
                     }
                  }
                  else
                  {
                     contentTextBox.Text = "";
                  }
                  tablemarkbox.Items.Clear();
                  picmarkbox.Items.Clear();
                  formarkbox.Items.Clear();
               }
               else if (treeView.SelectedNode.Name == "keyword")
               {
                  com.CommandText = "select Keyword from DocTable where DocID=@DocID";
                  com.Parameters.AddWithValue("DocID", docID);
                  dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     if (dr[0].ToString() != "")
                     {
                        contentTextBox.Text =dr[0].ToString();
                     }
                     else
                     {
                         contentTextBox.Text="";
                     }
                  }
                  else
                  {
                     contentTextBox.Text = "";
                  }
                  tablemarkbox.Items.Clear();
                  picmarkbox.Items.Clear();
                  formarkbox.Items.Clear();
               }
               else if (treeView.SelectedNode.Name == "bibliography")
               {
                  com.CommandText = "select Bibliography from DocTable where DocID=@DocID";
                  com.Parameters.AddWithValue("DocID", docID);
                  dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     if (dr[0].ToString() != "")
                     {
                        contentTextBox.Text = dr[0].ToString();
                     }
                     else
                     {
                         contentTextBox.Text="";
                     }
                  }
                  else
                  {
                     contentTextBox.Text = "";
                  }
                  tablemarkbox.Items.Clear();
                  picmarkbox.Items.Clear();
                  formarkbox.Items.Clear();
               }
               else
               {
                  com.CommandText = "select Content from LeafTitleView where DocID=@DocID and LeafTitleNum=@LeafTitleNum";
                  com.Parameters.AddWithValue("DocID", docID);
                  com.Parameters.AddWithValue("LeafTitleNum", treeView.SelectedNode.Name);
                  dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     if (dr[0].ToString() != "")
                     {
                        contentTextBox.Text = dr[0].ToString();
                     }
                     else
                     {
                        contentTextBox.Text = "";
                     }
                     LoadAll();
                  }
                  else
                  {
                     contentTextBox.Text = "";
                  }
               }
               DisposeClose.Disposeclose(dr);


               if (treeView.SelectedNode.Name == "title")
               {
                  status.Content = "当前编辑区：";
               }
               else
               {
                  status.Content = "当前编辑区：" + treeView.SelectedNode.FullPath;
               }
            }
            else
            {
               status.Content = "当前编辑区：";
               IsEditing.ElementName = "";
            }
            DisposeClose.Disposeclose(com);
            DisposeClose.Disposeclose(conn);
         }
      }

      #region 生成word文档
      private void wordimage_MouseDown(object sender, MouseButtonEventArgs e)
      {
         if (e.LeftButton == MouseButtonState.Pressed)
         {
            if (treeView.Nodes.Count == 0)
            {
               System.Windows.Forms.MessageBox.Show("当前没有可生成word的文档！");
            }
            else
            {
               DocAddress.DocPath = "";
               Microsoft.Win32.SaveFileDialog savefile = new Microsoft.Win32.SaveFileDialog();
               savefile.Filter = "Word97~2003(*.doc)|*.doc|PDF(*.pdf)|*.pdf|所有文件(*.*)|*.*";

               Nullable<bool> result = savefile.ShowDialog();

               if (result == true)
               {
                  DocAddress.DocPath = savefile.FileName;
                  LoadToWord ltw = new LoadToWord();
                  ltw.CreateWord();
               }
            }
         }
      }
      #endregion

      #region 图片更新操作
      private void picmodBtn_Click(object sender, RoutedEventArgs e)
      {
         if (picmarkbox.Text != "")
         {
            Microsoft.Win32.OpenFileDialog picfile = new Microsoft.Win32.OpenFileDialog();
            picfile.Filter = "图像文件(*.jpg;*.jpg;*.jpeg;*.gif;*.png;*.bmp)|*.jpg;*.jpeg;*.gif;*.png;*.bmp";

            Nullable<bool> result = picfile.ShowDialog();

            if (result == true)
            {
               string filename = picfile.FileName;
               string ptag = "<picture>" + picmarkbox.Text;
               SqlConnection conn = new SqlConnection();
               SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
               com.CommandText = "update LeafPictureTag set PAddress=@PAddress where DocID=@DocID and LeafTitleNum=@LeafTitleNum and PTag=@PTag";
               com.Parameters.Clear();
               com.Parameters.AddWithValue("PAddress", filename);
               com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
               com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
               com.Parameters.AddWithValue("PTag", ptag);
               com.ExecuteNonQuery();
               DisposeClose.Disposeclose(com);
               DisposeClose.Disposeclose(conn);
               BitmapImage image = new BitmapImage(new Uri(filename, UriKind.RelativeOrAbsolute));
               previewimage.Source = image;
            }
         }
      }

      private void picdelBtn_Click(object sender, RoutedEventArgs e)
      {
         if (picmarkbox.Text != "")
         {            
            System.Windows.Forms.DialogResult drs = System.Windows.Forms.MessageBox.Show("确认删除该图片", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (drs == System.Windows.Forms.DialogResult.OK)
            {
               string ptag = "<picture>" + picmarkbox.Text;
               SqlConnection conn = new SqlConnection();
               SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
               com.CommandText = "delete from LeafPictureTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum and PTag=@PTag";
               com.Parameters.Clear();
               com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
               com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
               com.Parameters.AddWithValue("PTag", ptag);
               com.ExecuteNonQuery();
               DisposeClose.Disposeclose(com);
               DisposeClose.Disposeclose(conn);
               contentTextBox.Text=contentTextBox.Text.Replace(ptag, "");
               SaveDoc();
               picmarkbox.Text = "";
               LoadPicture();
            }
         }
      }

      private void picrenameBtn_Click(object sender, RoutedEventArgs e)
      {
         if (picmarkbox.Text != "")
         {
            if (picnewname.Text == "")
            {
               System.Windows.Forms.MessageBox.Show("请先输入新表名！");
            }
            else
            {
               System.Windows.Forms.DialogResult drs = System.Windows.Forms.MessageBox.Show("确认更改该图片名称", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
               if (drs == System.Windows.Forms.DialogResult.OK)
               {
                  string ptag = "<picture><" + picnewname.Text+">";
                  string oldptag = "<picture>" + picmarkbox.Text;
                  SqlConnection conn = new SqlConnection();
                  SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
                  com.CommandText = "select * from LeafPictureTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum and PTag=@PTag";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
                  com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
                  com.Parameters.AddWithValue("PTag", ptag);
                  SqlDataReader dr = com.ExecuteReader();
                  if (dr.Read())
                  {
                     System.Windows.Forms.MessageBox.Show("该图片名已存在，请重新输入！");
                     picnewname.Clear();
                     DisposeClose.Disposeclose(dr);
                  }
                  else
                  {
                     DisposeClose.Disposeclose(dr);
                     com.CommandText = "update LeafPictureTag set PTag=@PTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum and PTag=@OldPTag";
                     com.Parameters.Clear();
                     com.Parameters.AddWithValue("PTag", ptag);
                     com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
                     com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
                     com.Parameters.AddWithValue("OldPTag", oldptag);
                     com.ExecuteNonQuery();
                     picmarkbox.Text = "";
                     contentTextBox.Text = contentTextBox.Text.Replace(oldptag, ptag);
                     SaveDoc();
                     LoadPicture();
                  }
               }
            }
         }
      }

      private void picmarkbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         System.Windows.Controls.ComboBox box = sender as System.Windows.Controls.ComboBox;
         this.picmarkbox.DataContext = box.SelectedItem;
         string spname = (string)picmarkbox.DataContext;
         if (spname != "")
         {
            string ptag = "<picture>" + spname;
            SqlConnection conn = new SqlConnection();
            SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
            com.CommandText = "select PAddress from LeafPictureTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum and PTag=@PTag";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
            com.Parameters.AddWithValue("PTag", ptag);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
               string ppath = dr[0].ToString();
               if(File.Exists(ppath))
               {
                  BitmapImage image = new BitmapImage(new Uri(ppath, UriKind.RelativeOrAbsolute));
                  previewimage.Source = image;
               }
            }
         }
      }

      #endregion

      #region 表格更新
      private void tablemodBtn_Click(object sender, RoutedEventArgs e)
      {
         if (tablemarkbox.Text != "")
         {
            System.Windows.Forms.DialogResult drs = System.Windows.Forms.MessageBox.Show("是否对当前选定表格进行修改？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (drs == System.Windows.Forms.DialogResult.OK)
            {
               if (tablename.Text != "")
               {
                  string tnewtag = "<table><" + tablename.Text + ">";
                  string rc = oldrowNum.Value.ToString() + "|" + oldcolNum.Value.ToString();
                  string ttag = "<table>" + tablemarkbox.Text;
                  SqlConnection conn = new SqlConnection();
                  SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
                  com.CommandText = "update LeafTableTag set TTag=@TTag,TRowCol=@TRowCol where DocID=@DocID and LeafTitleNum=@LeafTitleNum and TTag=@OldTTag";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("TTag", tnewtag);
                  com.Parameters.AddWithValue("TRowCol", rc);
                  com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
                  com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
                  com.Parameters.AddWithValue("OldTTag", ttag);
                  com.ExecuteNonQuery();
                  LoadTable();
                  contentTextBox.Text = contentTextBox.Text.Replace(ttag, tnewtag);
               }
               else
               {
                  string rc = oldrowNum.Value.ToString() + "|" + oldcolNum.Value.ToString();
                  string ttag = "<table>" + tablemarkbox.Text;
                  SqlConnection conn = new SqlConnection();
                  SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
                  com.CommandText = "update LeafTableTag set TRowCol=@TRowCol whaere DocID=@DocID and LeafTitleNum=@LeafTitleNum and TTag=@OldTTag";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("TRowCol", rc);
                  com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
                  com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
                  com.Parameters.AddWithValue("OldTTag", ttag);
                  com.ExecuteNonQuery();
                  LoadTable();
               }
               tablename.Text = "";
               oldrowNum.Value = 1;
               oldcolNum.Value = 1;
            }
         }
      }

      private void tabledelBtn_Click(object sender, RoutedEventArgs e)
      {
         if (tablemarkbox.Text != "")
         {
            System.Windows.Forms.DialogResult drs = System.Windows.Forms.MessageBox.Show("是否删除当前选定表格？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (drs == System.Windows.Forms.DialogResult.OK)
            {
               if (tablename.Text != "")
               {
                  string ttag = "<table>" + tablemarkbox.Text;
                  SqlConnection conn = new SqlConnection();
                  SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
                  com.CommandText = "delete from LeafTableTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum and TTag=@TTag";
                  com.Parameters.Clear();
                  com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
                  com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
                  com.Parameters.AddWithValue("TTag", ttag);
                  com.ExecuteNonQuery();
                  LoadTable();
                  contentTextBox.Text = contentTextBox.Text.Replace(ttag, "");
                  SaveDoc();
               }
            }
         }
      }

      private void tablemarkbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         System.Windows.Controls.ComboBox box = sender as System.Windows.Controls.ComboBox;
         this.tablemarkbox.DataContext = box.SelectedItem;
         string stname = (string)tablemarkbox.DataContext;
         if (stname != "")
         {
            string ttag = "<table>" + stname;
            SqlConnection conn = new SqlConnection();
            SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
            com.CommandText = "select TRowCol from LeafTableTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum and TTag=@TTag";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
            com.Parameters.AddWithValue("TTag", ttag);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
               string tname = stname.Substring(1, stname.Length - 2);
               string rc = dr[0].ToString();
               string[] rcsplit = rc.Split('|');
               oldrowNum.Value = int.Parse(rcsplit[0]);
               oldcolNum.Value = int.Parse(rcsplit[1]);
               tablename.Text = tname;
            }
         }
      }
      #endregion

      private void formarkbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         System.Windows.Controls.ComboBox box = sender as System.Windows.Controls.ComboBox;
         this.formarkbox.DataContext = box.SelectedItem;
         string ftname = (string)formarkbox.DataContext;
         if (ftname != "")
         {
            string basepath = System.Windows.Forms.Application.StartupPath;
            string[] leaftitle = IsEditing.ElementName.Split('|');
            string newleaftitle = String.Join("l", leaftitle);
            string forname = ftname.Substring(1, ftname.Length - 2);
            string forpic = IsEditing.DOCID.ToString() + "_" + newleaftitle + "_" + forname + ".jpg";
            string fpath = basepath +@"\wordimage\"+forpic;
            if (File.Exists(fpath))
            {
               BitmapImage image = new BitmapImage(new Uri(fpath, UriKind.RelativeOrAbsolute));
               previewimage.Source = image;
            }
         }
      }

      private void formodBtn_Click(object sender, RoutedEventArgs e)
      {
         string ftname = formarkbox.Text;
         string basepath = System.Windows.Forms.Application.StartupPath;
         string blankpath = basepath + "\\wordimage\\blank.gif";
         BitmapImage image = new BitmapImage(new Uri(blankpath, UriKind.RelativeOrAbsolute));
         previewimage.Source = image;

         Formula.ModFName = "<formula>" + formarkbox.Text;
         InsertFormula iFormula = new InsertFormula();
         iFormula.ShowDialog();
         string[] leaftitle = IsEditing.ElementName.Split('|');
         string newleaftitle = String.Join("l", leaftitle);
         string forname = ftname.Substring(1, ftname.Length - 2);
         string forpic = IsEditing.DOCID.ToString() + "_" + newleaftitle + "_" + forname + ".jpg";
         string fpath = basepath + @"\wordimage\" + forpic;
         if (File.Exists(fpath))
         {
            image = new BitmapImage(new Uri(fpath, UriKind.RelativeOrAbsolute));
            previewimage.Source = image;
         }
      }

      private void fordelBtn_Click(object sender, RoutedEventArgs e)
      {
         if (formarkbox.Text != "")
         {
            string ftname = formarkbox.Text;
            string basepath = System.Windows.Forms.Application.StartupPath;
            string blankpath = basepath + "\\wordimage\\blank.gif";
            BitmapImage image = new BitmapImage(new Uri(blankpath, UriKind.RelativeOrAbsolute));
            previewimage.Source = image;

            string ftag = "<formula>" + formarkbox.Text;
            SqlConnection conn = new SqlConnection();
            SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
            com.CommandText = "select FPicName from LeafFormulaTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum and FTag=@FTag";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
            com.Parameters.AddWithValue("FTag", ftag);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
               string fpath = dr[0].ToString();
               if (File.Exists(fpath))
               {
                  File.Delete(fpath);
               }
            }
            DisposeClose.Disposeclose(dr);
            com.CommandText = "delete from LeafFormulaTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum and FTag=@FTag";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
            com.Parameters.AddWithValue("LeafTitleNum", IsEditing.ElementName);
            com.Parameters.AddWithValue("FTag", ftag);
            com.ExecuteNonQuery();
            DisposeClose.Disposeclose(com);
            DisposeClose.Disposeclose(conn);

            contentTextBox.Text = contentTextBox.Text.Replace(ftag, "");
            SaveDoc();
         }
      }

   }
}
