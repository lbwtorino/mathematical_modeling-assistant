using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMAWPF.文档编辑模块
{
   class Table
   {
      public static bool isChanged;
      private static string tableTag;
      private static string tableName;
      private static int rowNum;
      private static int colNum;
      public static int TNum { get; set; }
      public static string TableTag
      {
         get
         {
            return tableTag;
         }
         set
         {
            tableTag = value;
         }
      }
      public static string TableName
      {
         get
         {
            return tableName;
         }
         set
         {
            tableName = value;
         }
      }
      public static int RowNum
      {
         get
         {
            return rowNum;
         }
         set
         {
            rowNum = value;
         }
      }
      public static int ColNum
      {
         get
         {
            return colNum;
         }
         set
         {
            colNum = value;
         }
      }
   }
}
