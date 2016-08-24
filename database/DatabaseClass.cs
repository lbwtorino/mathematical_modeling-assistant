using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MMAWPF
{
   class DatabaseClass
   {
      public static SqlConnection Conn { get; set; }
      public static string Constr { get; set; }
      public static SqlCommand ConnectionToCommad(SqlConnection con, string constr)
      {
         Conn = con;
         Constr = constr;
         Conn = new SqlConnection(Constr);
         try
         {
            Conn.Open();
            SqlCommand com = new SqlCommand("", Conn);
            return com;
         }
         catch (Exception e)
         {
            MessageBox.Show("数据库连接失败！");
            return null;
         }

      }
   }
}
