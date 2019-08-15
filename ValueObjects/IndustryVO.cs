using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class IndustryVO
    {
        private string industryCode;

        public string IndustryCode
        {
            get { return industryCode; }
            set { industryCode = value; }
        }

        private string industry;

        public string Industry
        {
            get { return industry; }
            set { industry = value; }
        }
    }
}