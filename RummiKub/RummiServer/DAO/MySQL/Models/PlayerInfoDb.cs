using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RummiServer.DAO.MySQL.Models
{
    public class PlayerInfoDb
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int CntWins { get; set; }
        public int CntLosses { get; set; }
        public int Balance { get; set; }
        public bool Blocked { get; set; }

        public PlayerInfoDb(DataRow row)
        {
            Id = row.Table.Columns.Contains("id") && !Convert.IsDBNull(row["id"]) ? Convert.ToInt32(row["id"]) : 0;
            Login = row.Table.Columns.Contains("Login") && !Convert.IsDBNull(row["Login"]) ? Convert.ToString(row["Login"]) : null;
            CntWins = row.Table.Columns.Contains("Wins_cnt") && !Convert.IsDBNull(row["Wins_cnt"]) ? Convert.ToInt32(row["Wins_cnt"]) : 0;
            CntLosses = row.Table.Columns.Contains("Loss_cnt") && !Convert.IsDBNull(row["Loss_cnt"]) ? Convert.ToInt32(row["Loss_cnt"]) : 0;
            Balance = row.Table.Columns.Contains("Balance") && !Convert.IsDBNull(row["Balance"]) ? Convert.ToInt32(row["Balance"]) : 0;
            Blocked = row.Table.Columns.Contains("Block") && !Convert.IsDBNull(row["Block"]) ? Convert.ToBoolean(row["Block"]) : false;
        }

    }
}
