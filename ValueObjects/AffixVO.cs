using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class AffixVO
    {
        private string affixCode;

        public string AffixCode
        {
            get { return affixCode; }
            set { affixCode = value; }
        }

        private string affix;

        public string Affix
        {
            get { return affix; }
            set { affix = value; }
        }
    }
}