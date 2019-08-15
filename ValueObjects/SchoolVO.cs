using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class SchoolVO
    {
        private string schoolCode;

        public string SchoolCode
        {
            get { return schoolCode; }
            set { schoolCode = value; }
        }

        private string school;

        public string School
        {
            get { return school; }
            set { school = value; }
        }
    }
}