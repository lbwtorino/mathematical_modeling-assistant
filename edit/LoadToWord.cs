using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MSWord = Microsoft.Office.Interop.Word;
using System.Collections;
using System.IO;

namespace MMAWPF.文档编辑模块
{
   class LoadToWord
   {
      string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

      MSWord.Application msapp = new MSWord.Application();
      MSWord.Document mydoc = new MSWord.Document();

      public void CreateWord()
      {
        try
         {
            CreateDoc(msapp, ref mydoc);
            InsertTitleBK(mydoc, msapp);
            InsertFromDocTable();
            InsertTitleName(mydoc);
            InsertContent();
            SetStyle();
            RemoveBK(mydoc);
            SaveAs(mydoc, DocAddress.DocPath);
            CloseAndSave(mydoc);
            CloseMSAPP(msapp);
        }
        catch (Exception ex)
         {
            
            MessageBox.Show("文档生成过程出现错误，请重新生成！");
         }
         finally
         {
            CloseMSAPP(msapp);
         }
      }

      private void CreateDoc(MSWord.Application msapp, ref MSWord.Document mydoc)
      {
         string basepath = System.Windows.Forms.Application.StartupPath;

         Object template =basepath+"\\template\\template.doc";
         Object newTemplate = Type.Missing;
         Object documentType = Type.Missing;
         Object visible = true;
         mydoc = msapp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
      }

      private void InsertFromDocTable()
      {
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         com.CommandText = "select Title from DocTable where DocID=@DocID";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         SqlDataReader dr = com.ExecuteReader();
         MSWord.Range range;
         if (dr.Read())
         {
            if (dr[0].ToString() != "")
            {
               string title = dr.GetString(0);
               range = GetBKRange(mydoc, "bk001_title");
               range.Select();
               range.InsertAfter(title);
            }
         }
         DisposeClose.Disposeclose(dr);
         range = GetBKRange(mydoc, "bk002_summary");
         range.Select();
         range.InsertAfter("摘要");

         com.CommandText = "select Summary from DocTable where DocID=@DocID";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         dr = com.ExecuteReader();
         if (dr.Read())
         {
            if (dr[0].ToString() != "")
            {
               range = GetBKRange(mydoc, "bk003_scontent");
               range.Select();
               range.InsertAfter(dr[0].ToString());
               DisposeClose.Disposeclose(dr);
            }
         }
         DisposeClose.Disposeclose(dr);

         range = GetBKRange(mydoc, "bk004_keyword");
         range.Select();
         com.CommandText = "select Keyword from DocTable where DocID=@DocID";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         dr = com.ExecuteReader();
         if (dr.Read())
         {
            if (dr[0].ToString() != "")
            {
               string keyword = "\n\n关键字：" + dr[0].ToString();
               range.InsertAfter(keyword);
               DisposeClose.Disposeclose(dr);
            }
            else
            {
               range.InsertAfter("\n\n关键字");
            }
         }
         DisposeClose.Disposeclose(dr);

         range = GetBKRangeByName(mydoc, "bibliography");
         range.Select();
         range.InsertAfter("参考文献");

         com.CommandText = "select Bibliography from DocTable where DocID=@DocID";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         dr = com.ExecuteReader();
         if (dr.Read())
         {
            if (dr[0].ToString() != "")
            {
               range = GetBKRangeByName(mydoc, "bcontent");
               range.Select();
               range.InsertAfter(dr[0].ToString());
               DisposeClose.Disposeclose(dr);
            }
         }
         DisposeClose.Disposeclose(dr);

      }

      private MSWord.Range GetBKRange(MSWord.Document doc, string bkmark)
      {
         int location;
         location = mydoc.Bookmarks[bkmark].Range.BookmarkID;
         return mydoc.Bookmarks[location].Range;
      }

      private MSWord.Range GetBKRangeByName(MSWord.Document doc, string name)
      {
         int location = 0;
         foreach (MSWord.Bookmark bk in doc.Bookmarks)
         {
            string bkname = bk.Name;
            string[] bkarray = bkname.Split('_');
            if (bkarray[1] == name)
            {
               location = mydoc.Bookmarks[bkname].Range.BookmarkID;
               break;
            }
         }
         return mydoc.Bookmarks[location].Range;

      }

      private void InsertTitleBK(MSWord.Document doc, MSWord.Application app)
      {
         int bknum = 6;
         string bknum_string;
         mydoc.Bookmarks.DefaultSorting = 0;
         mydoc.Bookmarks.ShowHidden = false;
         MSWord.Section mysec = mydoc.Sections.Add();
         object unit = MSWord.WdUnits.wdLine;
         object count = 1;
         object extend = MSWord.WdMovementType.wdMove;
         MSWord.Range r = mysec.Range;
         object rng = (object)r;
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         com.CommandText = "select TitleNum from TitleTable where DocID=@DocID order by TitleNum";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         SqlDataReader dr = com.ExecuteReader();
         while (dr.Read())
         {
            //word书签不支持符合“|”，因此在这里需要转换，使用字符“l”
            bknum_string = bknum.ToString();
            if (bknum_string.Length == 1)
            {
               bknum_string = "00" + bknum_string;
            }
            else if (bknum_string.Length == 2)
            {
               bknum_string = "0" + bknum_string;
            }
            string titlenum = dr[0].ToString();
            string[] title = titlenum.Split('|');
            titlenum = String.Join("l", title);
            string bkmark = "bk" + bknum_string + "_" + titlenum;
            mydoc.Bookmarks.Add(bkmark, ref rng);
            r = GetBKRange(mydoc, bkmark);
            r.Select();
            r.InsertAfter("\n");
            msapp.Selection.MoveDown(ref unit, ref count, ref extend);
            r = msapp.Selection.Range;
            rng = (object)r;
            bknum += 1;
         }
         DisposeClose.Disposeclose(dr);
         DisposeClose.Disposeclose(com);
         DisposeClose.Disposeclose(conn);
         //最后插入参考文献

         bknum_string = bknum.ToString();
         if (bknum_string.Length == 1)
         {
            bknum_string = "00" + bknum_string;
         }
         else if (bknum_string.Length == 2)
         {
            bknum_string = "0" + bknum_string;
         }

         string bk_bibliography = "bk" + bknum_string + "_bibliography";
         mydoc.Bookmarks.Add(bk_bibliography, ref rng);
         r = GetBKRange(mydoc, bk_bibliography);
         r.Select();
         r.InsertAfter("\n");
         msapp.Selection.MoveDown(ref unit, ref count, ref extend);
         r = msapp.Selection.Range;
         rng = (object)r;
         bknum += 1;
         bknum_string = bknum.ToString();
         if (bknum_string.Length == 1)
         {
            bknum_string = "00" + bknum_string;
         }
         else if (bknum_string.Length == 2)
         {
            bknum_string = "0" + bknum_string;
         }
         string bk_bcontent = "bk" + bknum_string + "_bcontent";
         mydoc.Bookmarks.Add(bk_bcontent, ref rng);
      }

      private void InsertTitleName(MSWord.Document doc)
      {
         MSWord.Range range;
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         com.CommandText = "select TitleNum,TitleName from TitleTable where DocID=@DocID order by TitleNum";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         SqlDataReader dr = com.ExecuteReader();
         while (dr.Read())
         {
            string titlename = dr[0].ToString();
            string[] titlearray = titlename.Split('|');
            string newtitlename = String.Join("l", titlearray);
            range = GetBKRangeByName(doc, newtitlename);
            range.Select();
            //range.InsertBefore(dr[1].ToString());
            range.Text = dr[1].ToString();
         }
      }

      private void InsertContent()
      {
         SqlConnection conn = new SqlConnection();
         SqlCommand com = DatabaseClass.ConnectionToCommad(conn, conStr);
         com.CommandText = "select LeafTitleNum,Content from LeafTitleTable where DocID=@DocID order by LeafTitleNum";
         com.Parameters.Clear();
         com.Parameters.AddWithValue("DocID", IsEditing.DOCID);
         SqlDataReader dr = com.ExecuteReader();
         object unit = MSWord.WdUnits.wdLine;
         object count = 1;
         object extend = MSWord.WdMovementType.wdMove;
         MSWord.Range range, crange;
         object rng;
         while (dr.Read())
         {
            string titlenum = dr[0].ToString();
            string[] titlearray = titlenum.Split('|');
            string newtitlenum = String.Join("l", titlearray);

            string content = dr[1].ToString();
            string[] contentarray = content.Split('\n');
            ArrayList al=new ArrayList();
            range = GetBKRangeByName(mydoc, newtitlenum);
            string bkmark = mydoc.Bookmarks[range.BookmarkID].Name;
            range.Select();
            msapp.Selection.EndKey(ref unit, ref extend);
            range = msapp.Selection.Range;
            range.InsertAfter("\n");
            for (int i = 0; i < contentarray.Length; i++)
            {
               msapp.Selection.MoveDown(ref unit, ref count, ref extend);
               range = msapp.Selection.Range;
               rng = (object)range;

               string number = "";
               if (i < 10)
               {
                  number = "00" + i.ToString();
               }
               else if (i < 100)
               {
                  number = "00" + i.ToString();
               }
               else
               {
                  number = i.ToString();
               }
               string cbkmark = bkmark + "_" + number;
               mydoc.Bookmarks.Add(cbkmark, ref rng);
               crange = GetBKRange(mydoc, cbkmark);
               range = GetBKRange(mydoc, cbkmark);
               range.Select();
               al.Add(cbkmark);
               if (i != contentarray.Length - 1)
               {
                  range.InsertAfter("\n");
               }
            }
            for (int j = 0; j < al.Count; j++)
            {
               string cbkmark =(string) al[j];
               range = GetBKRange(mydoc, cbkmark);
               if (contentarray[j].Contains("<picture>"))
               {
                  string ptag = contentarray[j].Trim();
                  object Nothing = System.Reflection.Missing.Value;
                  range.Select();
                  SqlConnection nconn = new SqlConnection();
                  SqlCommand ncom = DatabaseClass.ConnectionToCommad(conn, conStr);
                  ncom.CommandText = "select PAddress from LeafPictureTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum and PTag=@PTag";
                  ncom.Parameters.Clear();
                  ncom.Parameters.AddWithValue("DocID", IsEditing.DOCID);
                  ncom.Parameters.AddWithValue("LeafTitleNum", titlenum);
                  ncom.Parameters.AddWithValue("PTag", ptag);
                  SqlDataReader ndr = ncom.ExecuteReader();
                  ndr.Read();
                  string picpath = ndr[0].ToString();
                  DisposeClose.Disposeclose(ndr);
                  if (File.Exists(picpath))
                  {
                     MSWord.InlineShape shape = msapp.Selection.InlineShapes.AddPicture(picpath, ref Nothing, ref Nothing, ref Nothing);
                     float width = shape.Width;
                     float height = shape.Height;
                     float wrh = height / width;
                     shape.Width = 280;
                     shape.Height = wrh * 280;
                     shape.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                  }
                  DisposeClose.Disposeclose(ncom);
                  DisposeClose.Disposeclose(nconn);
               }
               else if (contentarray[j].Contains("<formula>"))
               {
                  string ftag = contentarray[j].Trim();
                  object Nothing = System.Reflection.Missing.Value;
                  range.Select();
                  SqlConnection nconn = new SqlConnection();
                  SqlCommand ncom = DatabaseClass.ConnectionToCommad(conn, conStr);
                  ncom.CommandText = "select FPicName from LeafFormulaTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum and FTag=@FTag";
                  ncom.Parameters.Clear();
                  ncom.Parameters.AddWithValue("DocID", IsEditing.DOCID);
                  ncom.Parameters.AddWithValue("LeafTitleNum", titlenum);
                  ncom.Parameters.AddWithValue("FTag", ftag);
                  SqlDataReader ndr = ncom.ExecuteReader();
                  ndr.Read();
                  string picpath = ndr[0].ToString();
                  DisposeClose.Disposeclose(ndr);
                  string basepath = System.Windows.Forms.Application.StartupPath;
                  string path = basepath + picpath;
                  if (File.Exists(path))
                  {
                     MSWord.InlineShape shape = msapp.Selection.InlineShapes.AddPicture(path, ref Nothing, ref Nothing, ref Nothing);
                     shape.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                  }
                  DisposeClose.Disposeclose(ncom);
                  DisposeClose.Disposeclose(nconn);
               }
               else if (contentarray[j].Contains("<table>"))
               {
                  string ttag = contentarray[j].Trim();
                  object Nothing = System.Reflection.Missing.Value;
                  range.Select();
                  SqlConnection nconn = new SqlConnection();
                  SqlCommand ncom = DatabaseClass.ConnectionToCommad(conn, conStr);
                  ncom.CommandText = "select TRowCol from LeafTableTag where DocID=@DocID and LeafTitleNum=@LeafTitleNum and TTag=@TTag";
                  ncom.Parameters.Clear();
                  ncom.Parameters.AddWithValue("DocID", IsEditing.DOCID);
                  ncom.Parameters.AddWithValue("LeafTitleNum", titlenum);
                  ncom.Parameters.AddWithValue("TTag", ttag);
                  SqlDataReader ndr = ncom.ExecuteReader();
                  ndr.Read();
                  string rowcol = ndr[0].ToString();
                  string[] splitrowcol = rowcol.Split('|');
                  int rows = int.Parse(splitrowcol[0]);
                  int cols = int.Parse(splitrowcol[1]);
                  MSWord.Table table = mydoc.Tables.Add(range, rows, cols, ref Nothing, ref Nothing);
                  table.Borders.InsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;
                  table.Borders.InsideLineWidth = MSWord.WdLineWidth.wdLineWidth050pt;
                  table.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;
                  table.Borders.OutsideLineWidth = MSWord.WdLineWidth.wdLineWidth050pt;
                  table.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                  DisposeClose.Disposeclose(ncom);
                  DisposeClose.Disposeclose(nconn);
               }
               else
               {
                  range.Text = contentarray[j];
               }
            }

         }
         DisposeClose.Disposeclose(dr);
         DisposeClose.Disposeclose(com);
         DisposeClose.Disposeclose(conn);
      }

      #region 保存
      private void Save(MSWord.Document mydoc)
      {
         mydoc.Save();
      }

      private void SaveAs(MSWord.Document mydoc, string filename)
      {
         Object fileName = filename;
         Object fileFormat = Type.Missing;
         Object lockComments = Type.Missing;
         Object password = Type.Missing;
         Object addToRecentFiles = Type.Missing;
         Object writePassword = Type.Missing;
         Object readOnlyRecommended = Type.Missing;
         Object embedTrueTypeFonts = Type.Missing;
         Object saveNativePictureFormat = Type.Missing;
         Object saveFormsData = Type.Missing;
         Object saveAsAOCELetter = Type.Missing;
         Object encoding = Type.Missing;
         Object insertLineBreaks = Type.Missing;
         Object allowSubstitutions = Type.Missing;
         Object lineEnding = Type.Missing;
         Object addBiDiMarks = Type.Missing;
         Object noPrompt = true;
         Object originalFormat = Type.Missing;

         mydoc.SaveAs(ref fileName, ref fileFormat, ref lockComments,
         ref password, ref addToRecentFiles, ref writePassword,
         ref readOnlyRecommended, ref embedTrueTypeFonts,
         ref saveNativePictureFormat, ref saveFormsData,
         ref saveAsAOCELetter, ref encoding, ref insertLineBreaks,
         ref allowSubstitutions, ref lineEnding, ref addBiDiMarks);
      }
      #endregion

      #region 退出word
      private void CloseNotSave(MSWord.Document mydoc)
      {
         Object saveChanges = MSWord.WdSaveOptions.wdDoNotSaveChanges;
         Object originalFormat = Type.Missing;
         Object routeDocument = Type.Missing;
         mydoc.Close(ref saveChanges, ref originalFormat, ref routeDocument);
      }

      private void CloseAndSave(MSWord.Document mydoc)
      {
         Object saveChanges = MSWord.WdSaveOptions.wdSaveChanges;
         Object originalFormat = Type.Missing;
         Object routeDocument = Type.Missing;
         mydoc.Close(ref saveChanges, ref originalFormat, ref routeDocument);
      }
      #endregion

      private void SetStyle()
      {
         object unit = MSWord.WdUnits.wdLine;
         object extend = MSWord.WdMovementType.wdExtend;
         MSWord.Range range = mydoc.Bookmarks[1].Range;
         range.Select();
         MSWord.Range rng = mydoc.Range(range.Start, mydoc.Content.End);
         rng.Font.Size = 12;
         foreach (MSWord.Bookmark bk in mydoc.Bookmarks)
         {
            string bkname = bk.Name;
            string[] bknamearray = bkname.Split('_');
            if (bknamearray.Length == 2)
            {
               if (bknamearray[1] == "title")
               {
                  range = bk.Range;
                  range.Select();
                  msapp.Selection.EndKey(ref unit, ref extend);
                  range = msapp.Selection.Range;
                  object style = MSWord.WdBuiltinStyle.wdStyleHeading1;
                  range.set_Style(style);
                  range.Font.Size = 16;
                  range.Font.Name = "黑体";
                  range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
               }
               else if (bknamearray[1] == "summary")
               {
                  range = bk.Range;
                  range.Select();
                  msapp.Selection.EndKey(ref unit, ref extend);
                  range = msapp.Selection.Range;
                  object style = MSWord.WdBuiltinStyle.wdStyleHeading1;
                  range.set_Style(style);
                  range.Font.Size = 14;
                  range.Font.Name = "黑体";
                  range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
               }
               /*else if (bknamearray[1] == "keyword")
               {
                  range = bk.Range;
                  range.Select();
                  msapp.Selection.EndKey(ref unit, ref extend);
                  range = msapp.Selection.Range;
                  range.Font.Size = 14;
                  range.Font.Name = "黑体";
                  range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
               }*/
               else if (bknamearray[1] == "scontent" || bknamearray[1] == "keyword" || bknamearray[1] == "bcontent")
               {

               }
               else if (bknamearray[1] == "bibliography")
               {
                  range = bk.Range;
                  range.Select();
                  msapp.Selection.EndKey(ref unit, ref extend);
                  range = msapp.Selection.Range;
                  object style = MSWord.WdBuiltinStyle.wdStyleHeading1;
                  range.set_Style(style);
                  range.Font.Size = 14;
                  range.Font.Name = "黑体";
                  range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
               }
               else
               {
                  string sbk = bknamearray[1];
                  string[] sbkarray = sbk.Split('l');
                  if (sbkarray.Length == 1)
                  {
                     range = bk.Range;
                     range.Select();
                     msapp.Selection.EndKey(ref unit, ref extend);
                     range = msapp.Selection.Range;
                     object style = MSWord.WdBuiltinStyle.wdStyleHeading1;
                     range.set_Style(style);
                     range.Font.Size = 14;
                     range.Font.Name = "黑体";
                     range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                  }
                  else if (sbkarray.Length == 2)
                  {
                     range = bk.Range;
                     range.Select();
                     msapp.Selection.EndKey(ref unit, ref extend);
                     range = msapp.Selection.Range;
                     object style = MSWord.WdBuiltinStyle.wdStyleHeading2;
                     range.set_Style(style);
                     range.Font.Size = 12;
                     range.Font.Name = "黑体";
                     range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                  }
                  else
                  {
                     range = bk.Range;
                     range.Select();
                     msapp.Selection.EndKey(ref unit, ref extend);
                     range = msapp.Selection.Range;
                     object style = MSWord.WdBuiltinStyle.wdStyleHeading3;
                     range.set_Style(style);
                     range.Font.Size = 12;
                     range.Font.Name = "黑体";
                     range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                  }
               }
            }

         }
      }

      private void CloseMSAPP(MSWord.Application msapp)
      {
         msapp.Quit();
      }

      private void RemoveBK(MSWord.Document doc)
      {
         foreach (MSWord.Bookmark bk in doc.Bookmarks)
         {
            bk.Delete();
         }
      }

   }
}
