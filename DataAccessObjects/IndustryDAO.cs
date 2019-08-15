using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class IndustryDAO
    {
        DB db = new DB();

        public DataTable SelectAllIndustry()
        {
            return db.GetData("SELECT industry_code, industry FROM Industries");
        }

        public DataTable SelectIndustry(string industryCode)
        {
            return db.GetData("SELECT industry_code, industry FROM Industries "
                + "WHERE industry_code = '" + industryCode + "'");
        }

        public void InsertIndustry(string industryCode, string industry)
        {
            db.Execute("InsertIndustry", "INSERT INTO Industries (industry_code, "
                + "industry) VALUES ('" + industryCode + "','" + industry + "')");
        }

        public void UpdateIndustry(string industryCode, string industry)
        {
            db.Execute("UpdateIndustry", "UPDATE Industries SET industry = '"
                + industry + "' WHERE industry_code = '" + industryCode + "'");
        }

        public void DeleteIndustry(string industryCode)
        {
            db.Execute("DeleteIndustry", "DELETE FROM Industries WHERE industry_code = '"
                + industryCode + "'");
        }
    }
}