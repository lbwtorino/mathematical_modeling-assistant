using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMAWPF.文档编辑模块
{
   class SaveTitle
   {
      private static string titleName = "";
      public static string TitleName
      {
         get
         {
            return titleName;
         }
         set
         {
            titleName = value;
         }
      }
   }
}
