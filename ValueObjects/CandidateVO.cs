using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class CandidateVO
    {
        private int candidateId;

        public int CandidateId
        {
            get { return candidateId; }
            set { candidateId = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}