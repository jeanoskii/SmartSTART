using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class BloodTypeVO
    {
        private string bloodType;

        public string BloodType
        {
            get { return bloodType; }
            set { bloodType = value; }
        }
    }
}