using System;
using System.Collections.Generic;
using System.Text;

namespace ESWIPE.Models
{
    public class QuizModel
    {
        public string Key { get; set; }
        public string DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string SubjectQuizCode { get; set; }
        public string SubjectQuizQty { get; set; }
        public string SubjectQuizAnswer { get; set; }
        public string SubjectQuizCorrectAnswer { get; set; }
    }
}
