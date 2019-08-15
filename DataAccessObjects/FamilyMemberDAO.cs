using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class FamilyMemberDAO
    {
        private DB db = new DB();

        public DataTable SelectAllFamilyMember()
        {
            return db.GetData("SELECT family_member_code, family_member FROM Family_Members");
        }

        public DataTable SelectFamilyMember(string familyMemberCode)
        {
            return db.GetData("SELECT family_member_code, family_member FROM "
                + "Family_Members WHERE family_member_code = '" + familyMemberCode + "'");
        }

        public void InsertFamilyMember(string familyMemberCode, string familyMember)
        {
            db.Execute("InsertFamilyMember", "INSERT INTO Family_Members (family_member_code, family_member) "
                + "VALUES ('" + familyMemberCode + "','" + familyMember + "')");
        }

        public void UpdateFamilyMember(string familyMemberCode, string familyMember)
        {
            db.Execute("UpdateFamilyMember", "UPDATE Family_Members SET family_member = '"
                + familyMember + "' WHERE family_member_code = '" + familyMemberCode + "'");
        }

        public void DeleteFamilyMember(string familyMemberCode)
        {
            db.Execute("DeleteFamilyMember", "DELETE FROM Family_Members WHERE "
                + "family_member_code = '" + familyMemberCode + "'");
        }
    }
}