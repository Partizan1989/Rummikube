using NLog;
using RummiServer.DAO.MySQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RummiServer.DAO.MySQL
{
    public static class Db
    {
        static MySqlWrap.MyWrap wraper = null;
        public static MySqlWrap.MyWrap Wraper
        {
            get
            {
                if (wraper == null) wraper = new MySqlWrap.MyWrap("MasterDB");
                return wraper;
            }
        }

        static Logger log = LogManager.GetCurrentClassLogger();

        public static PlayerInfoDb GetUserInfoDb(string login)
        {
            try
            {
                var dt = Wraper.GetDataTable($@"SELECT * FROM players p WHERE p.Login ='{login}'");
                if (dt.Rows.Count > 0)
                {
                    var resp = new PlayerInfoDb(dt.Rows[0]);
                    return resp;
                }
                return null;
            }
            catch (Exception ee)
            {
                log.Error(ee);
                return null;
            }
        }

        public static ErrorTextDb[] GetErrorTranslate()
        {
            try
            {
                var dt = Wraper.Procedure("Get_Error_Transate");
                var response = new List<ErrorTextDb>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        response.Add(new ErrorTextDb(row));
                    }
                }
                return response.ToArray();
            }
            catch (Exception ee)
            {
                log.Error(ee);
                return null;
            }
        }

        public static LanguageDb[] GetLanguagesDb()
        {
            try
            {
                var dt = Wraper.GetDataTable(@"SELECT * FROM languages");
                var response = new List<LanguageDb>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        response.Add(new LanguageDb(row));
                    }
                }
                return response.ToArray();
            }
            catch (Exception ee)
            {
                log.Error(ee);
                return null;
            }
        }

        public static TranslateDb[] GetTranslateDb()
        {
            try
            {
                var dt = Wraper.GetDataTable(@"SELECT * FROM translate_text");
                var response = new List<TranslateDb>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        response.Add(new TranslateDb(row));
                    }
                }
                return response.ToArray();
            }
            catch (Exception ee)
            {
                log.Error(ee);
                return null;
            }
        }
    }
}
