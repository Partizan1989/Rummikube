using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RummiServer.DAO.MySQL.Models
{
    public class ErrorTextDb
    {
        public int ErrorId { get; set; }
        public int LangId { get; set; }
        public string Transtale { get; set; }
        public int TextId { get; set; }

        public ErrorTextDb(DataRow row)
        {
            ErrorId = row.Table.Columns.Contains("ErrorId") && !Convert.IsDBNull(row["ErrorId"]) ? Convert.ToInt32(row["ErrorId"]) : 0;
            LangId = row.Table.Columns.Contains("langId") && !Convert.IsDBNull(row["langId"]) ? Convert.ToInt32(row["langId"]) : 0;
            TextId = row.Table.Columns.Contains("textId") && !Convert.IsDBNull(row["textId"]) ? Convert.ToInt32(row["textId"]) : 0;
            Transtale = row.Table.Columns.Contains("translate") && !Convert.IsDBNull(row["translate"]) ? Convert.ToString(row["translate"]) : null;
        }
    }
}
