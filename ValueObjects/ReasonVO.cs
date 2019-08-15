using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class ReasonVO
    {
        private string reasonCode;

        public string ReasonCode
        {
            get { return reasonCode; }
            set { reasonCode = value; }
        }

        private string reason;

        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }
    }
}