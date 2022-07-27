using System;
using System.Collections.Generic;
using System.Text;

namespace ESWIPE.Models
{
    public class AnswerModel
    {
        public string Key { get; set; }
        public string DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string QuizQuestionCode { get; set; }
        public string QuizAnswer1 { get; set; }
        public string QuizAnswer2 { get; set; }
        public string QuizAnswer3 { get; set; }
        public string QuizCorrectAnswer { get; set; }
    }
}
