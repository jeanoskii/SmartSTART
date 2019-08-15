using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class AffixDAO
    {
        private DB db = new DB();

        public DataTable SelectAllAffix()
        {
            return db.GetData("SELECT affix_code, affix FROM Affixes");
        }

        public DataTable SelectAffix(string affixCode)
        {
            return db.GetData("SELECT affix_code, affix FROM Affixes WHERE affix_code = '"
                + affixCode + "'");
        }

        public void InsertAffix(string affixCode, string affix)
        {
            db.Execute("InsertAffix", "INSERT INTO Affixes (affix_code, affix) VALUES ('"
                + affixCode + "','" + affix + "')");
        }

        public void UpdateAffix(string affixCode, string affix)
        {
            db.Execute("UpdateAffix", "UPDATE Affixes SET affix = '"
                + affix + "' WHERE affix_code = '" + affixCode + "'");
        }

        public void DeleteAffix(string affixCode)
        {
            db.Execute("DeleteAffix", "DELETE FROM Affixes WHERE affix_code = '" + affixCode + "'");
        }

    }
}