using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class GenderVO
    {
        private string genderCode;

        public string GenderCode
        {
            get { return genderCode; }
            set { genderCode = value; }
        }

        private string gender;

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

    }
}