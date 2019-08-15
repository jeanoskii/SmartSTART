using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class CivilStatusVO
    {
        private string civilStatusCode;

        public string CivilStatusCode
        {
            get { return civilStatusCode; }
            set { civilStatusCode = value; }
        }

        private string civilStatus;

        public string CivilStatus
        {
            get { return civilStatus; }
            set { civilStatus = value; }
        }
    }
}