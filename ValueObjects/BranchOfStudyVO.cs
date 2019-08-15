using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class BranchOfStudyVO
    {
        private string branchOfStudyCode;

        public string BranchOfStudyCode
        {
            get { return branchOfStudyCode; }
            set { branchOfStudyCode = value; }
        }

        private string branchOfStudy;

        public string BranchOfStudy
        {
            get { return branchOfStudy; }
            set { branchOfStudy = value; }
        }
    }
}