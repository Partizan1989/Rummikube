using NLog;
using RummiServer.DAO.MySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RummiServer.Classes
{
    public class Transtator
    {
        static Transtator instance;
        static Logger log = LogManager.GetCurrentClassLogger();

        public static Transtator Instance
        {
            get { return instance ?? (instance = new Transtator()); }
        }

        public List<LanguageDb> Languages = new List<LanguageDb>();
        public List<ErrorTextDb> ErrorTransate = new List<ErrorTextDb>();
        public List<TranslateDb> TranslatePhrases = new List<TranslateDb>();

        private void Update()
        {
            try
            {
                lock (Languages)
                {
                    var langDb = DAO.MySQL.Db.GetLanguagesDb();
                    if (langDb != null && langDb.Length > 0)
                    {
                        Languages.Clear();
                        Languages.AddRange(langDb);
                    }
                }
                lock (ErrorTransate)
                {
                    var errorDb = DAO.MySQL.Db.GetErrorTranslate();
                    if (errorDb != null && errorDb.Length > 0)
                    {
                        ErrorTransate.Clear();
                        ErrorTransate.AddRange(errorDb);
                    }
                }
                lock (TranslatePhrases)
                {
                    var transtaleDb = DAO.MySQL.Db.GetTranslateDb();
                    if (transtaleDb != null && transtaleDb.Length > 0)
                    {
                        TranslatePhrases.Clear();
                        TranslatePhrases.AddRange(transtaleDb);
                    }
                }
            }
            catch (Exception ee)
            {
                log.Error(ee);
            }
        }
        public Transtator()
        {
            Update();
        }


        public string Transtale(int langId, int textId, string originRus)
        {
            try
            {
                var tr = TranslatePhrases.Where(w => w.PhraseId == textId).FirstOrDefault();
                if (string.IsNullOrEmpty(tr.Transtale))
                {
                    Update();
                    tr = TranslatePhrases.Where(w => w.PhraseId == textId).FirstOrDefault();
                    if (string.IsNullOrEmpty(tr.Transtale))
                    {
                        return originRus;
                    }
                }
                return tr.Transtale;
            }
            catch(Exception ee)
            {
                log.Error(ee);
                return originRus;
            }
        }
    }
}
