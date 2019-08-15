using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPRES.ValueObjects
{
    public class AnswerVO
    {
        private int answerId;

        public int AnswerId
        {
            get { return answerId; }
            set { answerId = value; }
        }

        private int afeId;

        public int AfeId
        {
            get { return afeId; }
            set { afeId = value; }
        }

        private string questionCode;

        public string QuestionCode
        {
            get { return questionCode; }
            set { questionCode = value; }
        }

        private string answer;

        public string Answer
        {
            get { return answer; }
            set { answer = value; }
        }
    }
}