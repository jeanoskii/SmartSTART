using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class SpecifyGeneralVO
    {
        private int specifyGeneralId;

        public int SpecifyGeneralId
        {
            get { return specifyGeneralId; }
            set { specifyGeneralId = value; }
        }

        private int answerId;

        public int AnswerId
        {
            get { return answerId; }
            set { answerId = value; }
        }

        private string specifyValue;

        public string SpecifyValue
        {
            get { return specifyValue; }
            set { specifyValue = value; }
        }
    }
}