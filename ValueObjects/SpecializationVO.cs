using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class SpecializationVO
    {
        private string specializationCode;

        public string SpecializationCode
        {
            get { return specializationCode; }
            set { specializationCode = value; }
        }

        private string specialization;

        public string Specialziation
        {
            get { return specialization; }
            set { specialization = value; }
        }
    }
}