using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class FamilyMemberVO
    {
        private string familyMemberCode;

        public string FamilyMemberCode
        {
            get { return familyMemberCode; }
            set { familyMemberCode = value; }
        }

        private string familyMember;

        public string FamilyMember
        {
            get { return familyMember; }
            set { familyMember = value; }
        }
    }
}