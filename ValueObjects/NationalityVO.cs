using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class NationalityVO
    {
        private string nationalityCode;

        public string NationalityCode
        {
            get { return nationalityCode; }
            set { nationalityCode = value; }
        }

        private string nationality;

        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }
    }
}