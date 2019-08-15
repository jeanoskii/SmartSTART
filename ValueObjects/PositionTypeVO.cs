using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class PositionTypeVO
    {
        private string positionTypeCode;

        public string PositionTypeCode
        {
            get { return positionTypeCode; }
            set { positionTypeCode = value; }
        }

        private string positionType;

        public string PositionType
        {
            get { return positionType; }
            set { positionType = value; }
        }
    }
}