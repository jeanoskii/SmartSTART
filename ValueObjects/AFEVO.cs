using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class AFEVO
    {
        private int afeId;

        public int AfeId
        {
            get { return afeId; }
            set { afeId = value; }
        }

        private string applicationDate;

        public string ApplicationDate
        {
            get { return applicationDate; }
            set { applicationDate = value; }
        }

        private string positionAppliedFor;

        public string PositionAppliedFor
        {
            get { return positionAppliedFor; }
            set { positionAppliedFor = value; }
        }

        private string interviewerName;

        public string InterviewerName
        {
            get { return interviewerName; }
            set { interviewerName = value; }
        }
    }
}