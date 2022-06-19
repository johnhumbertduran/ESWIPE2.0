using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

using ESWIPE.Models;
using ESWIPE.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ESWIPE.ViewModels
{
    public class StudentViewModel : ViewModelBase
    {
        private string key;
        public string Key
        {
            get { return key; }
            set { SetProperty(ref key, value); }
        }

        private string username;
        public string UserName
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string studentname;
        public string StudentName
        {
            get { return studentname; }
            set { SetProperty(ref studentname, value); }
        }

        private string year;
        public string Year
        {
            get { return year; }
            set { SetProperty(ref year, value); }
        }

        private string section;
        public string Section
        {
            get { return section; }
            set { SetProperty(ref section, value); }
        }

        private string subjectsCode;
        public string SubjectsCode
        {
            get { return subjectsCode; }
            set { SetProperty(ref subjectsCode, value); }
        }

        private string quizCode;
        public string QuizCode
        {
            get { return quizCode; }
            set { SetProperty(ref quizCode, value); }
        }

        private string quizScore;
        public string QuizScore
        {
            get { return quizScore; }
            set { SetProperty(ref quizScore, value); }
        }

        private string teacherName;
        public string TeacherName
        {
            get { return teacherName; }
            set { SetProperty(ref teacherName, value); }
        }


        public StudentViewModel()
        {
            Title = "Student";
            //CheckUserLoginDetails();
            //if (Preferences.ContainsKey("Key", ""))
            //{
            //    Key = Preferences.Get("Key", "");
            //}

            if (Preferences.ContainsKey("Username", ""))
            {
                UserName = Preferences.Get("Username", "");
            }

            if (Preferences.ContainsKey("StudentName", ""))
            {
                StudentName = Preferences.Get("StudentName", "");
            }

            if (Preferences.ContainsKey("Year", ""))
            {
                Year = Preferences.Get("Year", "");
            }

            if (Preferences.ContainsKey("Section", ""))
            {
                Section = Preferences.Get("Section", "");
            }

        }
    }
}
