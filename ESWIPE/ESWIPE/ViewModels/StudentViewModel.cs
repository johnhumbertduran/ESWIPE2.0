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
    [QueryProperty(nameof(Creds), nameof(Creds))]
    public class StudentViewModel : ViewModelBase
    {
        string creds = "";
        
        public string Creds
        {
            get => creds;
            set
            {
                creds = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
                PerformOperation(creds);
            }
        }

        private string Username { get; set; }
        private string Studentname { get; set; }
        private string Studentyear { get; set; }
        private string Studentsection { get; set; }
        private string Subjectscode { get; set; }
        private string Quizcode { get; set; }
        private string Quizscore { get; set; }

        public string UserName
        {
            get { return Username; }
            set
            {
                Username = value;
                OnPropertyChanged("UserName");
            }
        }

        public string StudentName
        {
            get { return Studentname; }
            set
            {
                Studentname = value;
                OnPropertyChanged("StudentName");
            }
        }
        public string StudentYear
        {
            get { return Studentyear; }
            set
            {
                Studentyear = value;
                OnPropertyChanged("StudentYear");
            }
        }

        public string StudentSection
        {
            get { return Studentsection; }
            set
            {
                Studentsection = value;
                OnPropertyChanged("StudentSection");
            }
        }
        public string SubjectsCode
        {
            get { return Subjectscode; }
            set
            {
                Subjectscode = value;
                OnPropertyChanged("SubjectsCode");
            }
        }

        public string QuizCode
        {
            get { return Quizcode; }
            set
            {
                Quizcode = value;
                OnPropertyChanged("QuizCode");
            }
        }
        public string QuizScore
        {
            get { return Quizscore; }
            set
            {
                Quizscore = value;
                OnPropertyChanged("QuizScore");
            }
        }

        private void PerformOperation(string userInfo)
        {
            var creds = JsonConvert.DeserializeObject<StudentModel>(userInfo);
            UserName = creds.Username;
            StudentName = creds.StudentName;
            StudentYear = creds.Year;
            StudentSection = creds.Section;
            SubjectsCode = creds.SubjectsCode;
            QuizCode = creds.QuizCode;
            QuizScore = creds.QuizScore;
            //StudentName = creds.StudentName;
            //Year = creds.Year;
        }


        public StudentViewModel()
        {
            Title = "Student";
            //CheckUserLoginDetails();
        }

        //private async void CheckUserLoginDetails()
        //{
        //    string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");

        //    if (string.IsNullOrWhiteSpace(userDetailsStr))
        //    {
        //        await App.Current.MainPage.DisplayAlert("Check Deserialize", "Null", "OK");
        //    }
        //    else
        //    {
        //        var userInfo = JsonConvert.DeserializeObject<StudentModel>(userDetailsStr);
        //        App.UserDetails = userInfo;
        //        await Shell.Current.GoToAsync($"//{nameof(StudentPage)}");
        //        await App.Current.MainPage.DisplayAlert("Check Deserialize", "Not Null", "OK");
        //    }
        //}
    }
}
