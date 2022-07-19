using System;
using System.Collections.Generic;
using System.Text;

namespace ESWIPE.Models
{
    public class StudentModel
    {
        public string Key { get; set; }
        public string StudentName { get; set; }
        public string Year { get; set; }
        public string Section { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public string SubjectsCode { get; set; }
        public string QuizCode { get; set; }
        public string QuizScore { get; set; }
    }
}