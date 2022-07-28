using System;
using System.Collections.Generic;
using System.Text;

namespace ESWIPE.Models
{
    public class ModuleModel
    {
        public string Key { get; set; }
        public string DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string Quarter { get; set; }
        public string ActiveQuarter { get; set; }
        public string SubjectCode { get; set; }
        //public string SubjectQuizCode { get; set; }
        //public string SubjectQuizQty { get; set; }
    }
}