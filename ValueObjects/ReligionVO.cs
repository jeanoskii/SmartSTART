using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class ReligionVO
    {
        private string religionCode;

        public string ReligionCode
        {
            get { return religionCode; }
            set { religionCode = value; }
        }

        private string religion;

        public string Religion
        {
            get { return religion; }
            set { religion = value; }
        }
    }
}