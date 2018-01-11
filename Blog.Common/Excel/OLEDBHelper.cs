using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.Excel
{
    public static class OLEDBHelper
    {
        public static DataTable ReadExcelToTable(string path, string filePostfixName)
        {
            return ReadExcelToTable(path, filePostfixName, "Sheet1");
        }

        public static DataTable ReadExcelToTable(string path, string filePostfixName, string sheetName)
        {
            try
            {
                //连接字符串
                string connstring = "";
                connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";
                using (OleDbConnection conn = new OleDbConnection(connstring))
                {
                    conn.Open();

                    //DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字
                    string sql = string.Format("SELECT * FROM [{0}]", sheetName); //查询字符串

                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                    DataSet set = new DataSet();
                    ada.Fill(set);
                    if (set.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = set.Tables[0].Rows[0];

                        for (int col = 0; col < set.Tables[0].Columns.Count; col++)
                        {
                            set.Tables[0].Columns[col].ColumnName = dr[col].ToString();
                        }

                        set.Tables[0].Rows[0].Delete();
                        set.Tables[0].AcceptChanges();
                    }
                    return set.Tables[0];
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
