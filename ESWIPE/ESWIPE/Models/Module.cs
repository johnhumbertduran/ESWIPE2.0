using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using MvvmHelpers;

namespace ESWIPE.Models
{
    class Module
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        public string DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectQuizCode { get; set; }
        public string SubjectQuizQty { get; set; }

        public static implicit operator ObservableRangeCollection<object>(Module v)
        {
            throw new NotImplementedException();
        }
    }
}
