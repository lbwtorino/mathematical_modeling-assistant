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
using System.Web;
using System.Net.NetworkInformation;
using System.Diagnostics;
using MMAWPF.文档编辑模块;
using System.Data;
using System.IO;
using System.Net;

namespace MMAWPF
{
   /// <summary>
   /// Translation.xaml 的交互逻辑
   /// </summary>
   public partial class Translation : Window
   {
      string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
      public Translation()
      {
         InitializeComponent();
      }

      #region 窗体关闭响应事件
      private void Window_Closed(object sender, EventArgs e)
      {
         MainWindow.Translation_Object = null;
      }

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
      #endregion

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         //UserLogIn.UserName = "whutxuyang";
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         if (com != null)
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
            YouDaoTranslateTool("a");
         }
         else
         {
            this.Close();
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
               if (treeView.Nodes[0].Text == docHistory.Text)
               {
                  System.Windows.Forms.MessageBox.Show("该文档已载入，无需重载！");
               }
               else
               {
                  System.Windows.Forms.DialogResult dresult = System.Windows.Forms.MessageBox.Show("是否保存当前文档，并载入选定的文档？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                  if (dresult == System.Windows.Forms.DialogResult.OK)
                  {
                     int count = treeView.Nodes[0].Nodes.Count;
                     for (int i = 0; i < count; i++)
                     {
                        treeView.Nodes[0].Nodes[0].Remove();//每次调用时treeView.Nodes[0].Nodes.Count的值是不一样的，所以每次选择删除第0个节点。
                     }
                     treeView.Nodes[0].Text = "";
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

      private void InitTree()
      {
         TreeNode root = new TreeNode();
         root.Name = "root";
         root.Text = "root";
         treeView.Nodes.Add(root);
      }

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

      private void treeView_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
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
                        chineseTxt.Document = new FlowDocument(new Paragraph(new Run(dr[0].ToString())));
                     }
                     else
                     {
                        chineseTxt.Document.Blocks.Clear();
                     }
                  }
                  else
                  {
                     chineseTxt.Document.Blocks.Clear();
                  }
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
                        chineseTxt.Document = new FlowDocument(new Paragraph(new Run(dr[0].ToString())));
                     }
                     else
                     {
                        chineseTxt.Document.Blocks.Clear();
                     }
                  }
                  else
                  {
                     chineseTxt.Document.Blocks.Clear();
                  }
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
                        chineseTxt.Document = new FlowDocument(new Paragraph(new Run(dr[0].ToString())));
                     }
                     else
                     {
                        chineseTxt.Document.Blocks.Clear();
                     }
                  }
                  else
                  {
                     chineseTxt.Document.Blocks.Clear();
                  }
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
                        chineseTxt.Document = new FlowDocument(new Paragraph(new Run(dr[0].ToString())));
                     }
                     else
                     {
                        chineseTxt.Document.Blocks.Clear();
                     }
                  }
                  else
                  {
                     chineseTxt.Document.Blocks.Clear();
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

      #region 有道在线翻译

      Ping ping = new System.Net.NetworkInformation.Ping();
      PingReply res;
      public bool check(string url)
      {
         try
         {
            res = ping.Send(url);

            if (res.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
               return true;
            }
            else
            {
               return false;
            }
         }
         catch
         {
            return false;
         }
      }
      public string YouDaoTranslateTool(string sourceWord)
      {
         if (!check("www.baidu.com"))
         {
            string offLine = "未联网，不能进行在线翻译！";
            return offLine;
         }
         else
         {
            string serverUrl = @"http://fanyi.youdao.com/openapi.do?keyfrom=WHUTMMA&key=2045785969&type=data&doctype=json&version=1.1&q="
         + HttpUtility.UrlEncode(sourceWord);
            WebRequest request = WebRequest.Create(serverUrl);
            WebResponse response = request.GetResponse();
            string resJson = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            int textIndex = resJson.IndexOf("translation") + 15;
            int textLen = resJson.IndexOf("\"]", textIndex) - textIndex;
            return resJson.Substring(textIndex, textLen);
         }
      }
      #endregion

      private void translateBtn_Click(object sender, RoutedEventArgs e)
      {
         resultTxt.Document.Blocks.Clear();
         string content = YouDaoTranslateTool(contentTxt.Text.ToString());
         resultTxt.Document = new FlowDocument(new Paragraph(new Run(content)));
      }

   }
}
