using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMAWPF.文档编辑模块
{
   class Picture
   {
      public static bool isChanged;
      private static string picTag;
      private static string picName;
      private static string picAddress;
      public static int PNum { get; set; }
      public static string PicTag
      {
         get
         {
            return picTag;
         }
         set
         {
            picTag = value;
         }
      }
      public static string PicName
      {
         get
         {
            return picName;
         }
         set
         {
            picName = value;
         }
      }
      public static string PicAddress
      {
         get
         {
            return picAddress;
         }
         set
         {
            picAddress = value;
         }
      }
   }
}
