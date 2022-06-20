using ESWIPE.ViewModels;
using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESWIPE.Views
{
    public partial class StudentPage : ContentPage
    {
        public string WebAPIKey = "AIzaSyAZHeAFjNeVLmgTbIuu1SIfR07ZlJKBoR0";

        public string Key;
        public string UserName;
        public string StudentName;
        public string Year;
        public string Section;
        public StudentPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Preferences.ContainsKey("Key", ""))
            {
                Key = Preferences.Get("Key", "");
            }

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

            Title = StudentName;
            MyUsername.Text = Key;
            MyStudentName.Text = StudentName;
        }
    }
}