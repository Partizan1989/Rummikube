using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RummiServer.DAO.MySQL.Models
{
    public class TranslateDb
    {
        public int PhraseId { get; set; }
        public int LangId { get; set; }
        public string Transtale { get; set; }

        public TranslateDb(DataRow row)
        {
            PhraseId = row.Table.Columns.Contains("textId") && !Convert.IsDBNull(row["textId"]) ? Convert.ToInt32(row["textId"]) : 0;
            LangId = row.Table.Columns.Contains("langId") && !Convert.IsDBNull(row["langId"]) ? Convert.ToInt32(row["langId"]) : 0;
            Transtale = row.Table.Columns.Contains("translate") && !Convert.IsDBNull(row["translate"]) ? Convert.ToString(row["translate"]) : null;
        }
    }
}
