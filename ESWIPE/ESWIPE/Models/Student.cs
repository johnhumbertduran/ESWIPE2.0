using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using MvvmHelpers;

namespace ESWIPE.Models
{
    class Student
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        public int StudentNumber { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Section { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public string SubjectsCode { get; set; }
        public string QuizCode { get; set; }
        public string QuizScore { get; set; }

        public static implicit operator ObservableRangeCollection<object>(Student v)
        {
            throw new NotImplementedException();
        }
    }
}
