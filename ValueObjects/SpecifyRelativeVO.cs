using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class SpecifyRelativeVO
    {
        private int specifyRelativeId;

        public int SpecifyRelativeId
        {
            get { return specifyRelativeId; }
            set { specifyRelativeId = value; }
        }

        private int answerId;

        public int AnswerId
        {
            get { return answerId; }
            set { answerId = value; }
        }

        private string companyName;

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        private string relativeName;

        public string RelativeName
        {
            get { return relativeName; }
            set { relativeName = value; }
        }
    }
}