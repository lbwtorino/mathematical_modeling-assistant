using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MMAWPF
{
   class DisposeClose
   {
      public static void Disposeclose(SqlConnection connection)
      {
         connection.Dispose();
         connection.Close();
      }
      public static void Disposeclose(SqlDataReader dreader)
      {
         dreader.Dispose();
         dreader.Close();
      }
      public static void Disposeclose(SqlDataAdapter dadapter)
      {
         dadapter.Dispose();
      }
      public static void Disposeclose(SqlCommand command)
      {
         command.Dispose();
      }
   }
}
