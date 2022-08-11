﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ESWIPE.Models
{
    public class AnswerModel
    {
        public string Key { get; set; }
        public string DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string Quarters { get; set; }
        public string QuizType { get; set; }
        public string QuizCode { get; set; }
        public string Section { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
