using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using MvvmHelpers;

namespace ESWIPE.Models
{
    class Quiz
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        public string DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string SubjectQuizCode { get; set; }
        public string SubjectQuizQty { get; set; }
        public string SubjectQuizAnswer { get; set; }
        public string SubjectQuizCorrectAnswer { get; set; }

        public static implicit operator ObservableRangeCollection<object>(Quiz v)
        {
            throw new NotImplementedException();
        }
    }
}
