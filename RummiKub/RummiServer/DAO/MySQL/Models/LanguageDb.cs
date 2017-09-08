using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RummiServer.DAO.MySQL.Models
{
    public class LanguageDb
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public LanguageDb(DataRow row)
        {
            Id = row.Table.Columns.Contains("Id") && !Convert.IsDBNull(row["Id"]) ? Convert.ToInt32(row["Id"]) : 0;
            Name = row.Table.Columns.Contains("name") && !Convert.IsDBNull(row["name"]) ? Convert.ToString(row["name"]) : null;
        }

    }
}
