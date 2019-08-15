using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class File3VO
    {
        private int file3Id;

        public int File4Id
        {
            get { return file3Id; }
            set { file3Id = value; }
        }

        private int candidateId;

        public int CandidateId
        {
            get { return candidateId; }
            set { candidateId = value; }
        }

        private string previousEmployer;

        public string PreviousEmployer
        {
            get { return previousEmployer; }
            set { previousEmployer = value; }
        }

        private string city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        private string positionHeld;

        public string PositionHeld
        {
            get { return positionHeld; }
            set { positionHeld = value; }
        }

        private string specializationCode;

        public string SpecializationCode
        {
            get { return specializationCode; }
            set { specializationCode = value; }
        }

        private string immediateSuperior;

        public string ImmediateSuperior
        {
            get { return immediateSuperior; }
            set { immediateSuperior = value; }
        }

        private string previousSalary;

        public string PreviousSalary
        {
            get { return previousSalary; }
            set { previousSalary = value; }
        }

        private string industryCode;

        public string IndustryCode
        {
            get { return industryCode; }
            set { industryCode = value; }
        }

        private string reasonCode;

        public string ReasonCode
        {
            get { return reasonCode; }
            set { reasonCode = value; }
        }

        private string startDate;

        public string StartDate
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
    }
}