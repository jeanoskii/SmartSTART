using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class CertificateVO
    {
        private string certificateCode;

        public string CertificateCode
        {
            get { return certificateCode; }
            set { certificateCode = value; }
        }
        private string certificate;

        public string Certificate
        {
            get { return certificate; }
            set { certificate = value; }
        }
    }
}