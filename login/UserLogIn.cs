using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MMAWPF
{
   class UserLogIn
   {
      private static string userName="";
      private static DateTime editTime;
      public static string UserName
      {
         get
         {
            return userName;
         }
         set
         {
            userName = value;
         }
      }
      public static DateTime EditTime
      {
         get
         {
            return editTime;
         }
         set
         {
            editTime = value;
         }
      }
   }
}
