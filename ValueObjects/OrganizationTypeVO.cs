using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class OrganizationTypeVO
    {
        private string organizationTypeCode;

        public string OrganizationTypeCode
        {
            get { return organizationTypeCode; }
            set { organizationTypeCode = value; }
        }

        private string organizationType;

        public string OrganizationType
        {
            get { return organizationType; }
            set { organizationType = value; }
        }
    }
}