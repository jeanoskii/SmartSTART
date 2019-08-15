using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class File4VO
    {
        private int file5Id;

        public int File5Id
        {
            get { return file5Id; }
            set { file5Id = value; }
        }

        private int candidateId;

        public int CandidateId
        {
            get { return candidateId; }
            set { candidateId = value; }
        }

        private string organizationType;

        public string OrganizationType
        {
            get { return organizationType; }
            set { organizationType = value; }
        }

        private string organizationName;

        public string OrganizationName
        {
            get { return organizationName; }
            set { organizationName = value; }
        }

        private string startDate;

        public string StarDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private string endDate;

        public string EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        private string positionType;

        public string PositionType
        {
            get { return positionType; }
            set { positionType = value; }
        }
    }
}