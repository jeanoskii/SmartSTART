using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class OrganizationTypeDAO
    {
        private DB db = new DB();

        public DataTable SelectAllOrganizationType()
        {
            return db.GetData("SELECT organization_type_code, organization_type "
                + "FROM Organization_Types");
        }

        public DataTable SelectOrganizationType(string organizationTypeCode)
        {
            return db.GetData("SELECT organization_type_code, organization_type "
                + " FROM Organization_Types WHERE organization_type_code = '"
                + organizationTypeCode + "'");
        }

        public void InsertOrganizationType(string organizationTypeCode, string organizationType)
        {
            db.Execute("InsertOrganizationType", "INSERT INTO Organization_Types "
                + "(organization_type_code, organization_type) VALUES ('"
                + organizationTypeCode + "','" + organizationType + "')");
        }

        public void UpdateOrganizationType(string organizationTypeCode, string organizationType)
        {
            db.Execute("UpdateOrganizationType", "UPDATE Organization_Types SET "
                + "organization_type = '" + organizationType + "' WHERE "
                + "organization_type_code = '" + organizationTypeCode + "'");
        }

        public void DeleteOrganizationType(string organizationTypeCode)
        {
            db.Execute("DeleteOrganizationType", "DELETE FROM Organization_Types "
                + "WHERE organization_type_code = '" + organizationTypeCode + "'");
        }
    }
}