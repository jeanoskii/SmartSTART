using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class File2VO
    {
        private int file2Id;

        public int File2Id
        {
            get { return file2Id; }
            set { file2Id = value; }
        }

        private int candidateId;

        public int CandidateId
        {
            get { return candidateId; }
            set { candidateId = value; }
        }

        private string educationEstablishment;

        public string EducationEstablishment
        {
            get { return educationEstablishment; }
            set { educationEstablishment = value; }
        }

        private string school;

        public string School
        {
            get { return school; }
            set { school = value; }
        }

        private string schoolOthers;

        public string SchoolOthers
        {
            get { return schoolOthers; }
            set { schoolOthers = value; }
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

        private string branchOfStudy1;

        public string BracnhOfStudy1
        {
            get { return branchOfStudy1; }
            set { branchOfStudy1 = value; }
        }

        private string certificate;

        public string Certificate
        {
            get { return certificate; }
            set { certificate = value; }
        }

        private string courseAppraisal;

        public string CourseAppraisal
        {
            get { return courseAppraisal; }
            set { courseAppraisal = value; }
        }

        private string branchOfStudy2;

        public string BranchOfStudy2
        {
            get { return branchOfStudy2; }
            set { branchOfStudy2 = value; }
        }
    }
}