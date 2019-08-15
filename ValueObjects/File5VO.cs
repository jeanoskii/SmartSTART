using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class File5VO
    {
        private int file5Id;

        public int File5Id
        {
            get { return file5Id; }
            set { file5Id = value; }
        }

        private int candidateId;

        public int CandidateID
        {
            get { return candidateId; }
            set { candidateId = value; }
        }

        private string dependentsName;

        public string DependentsName
        {
            get { return dependentsName; }
            set { dependentsName = value; }
        }

        private string familyMember;

        public string FamilyMember
        {
            get { return familyMember; }
            set { familyMember = value; }
        }

        private string gender;

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private string birthdate;

        public string Birthdate
        {
            get { return birthdate; }
            set { birthdate = value; }
        }

        
    }
}