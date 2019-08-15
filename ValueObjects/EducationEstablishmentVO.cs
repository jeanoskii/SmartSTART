using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class EducationEstablishmentVO
    {
        private string educationEstablishmentCode;

        public string EducationEstablishmentCode
        {
            get { return educationEstablishmentCode; }
            set { educationEstablishmentCode = value; }
        }

        private string educationEstablishment;

        public string EducationEstablishment
        {
            get { return educationEstablishment; }
            set { educationEstablishment = value; }
        }
    }
}