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
        public string QuizCode { get; set; }
        public string QuizQuestionCode { get; set; }
        public string Question { get; set; }
    }
}
