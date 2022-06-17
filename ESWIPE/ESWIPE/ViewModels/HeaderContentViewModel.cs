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
    public class HeaderContentViewModel : ViewModelBase
    {
        private string username;
        public string UserName
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }


        public HeaderContentViewModel()
        {
            //Title = "Student";

            // Check if the user is Student
            if (Preferences.ContainsKey("StudentName", ""))
            {
                UserName = Preferences.Get("StudentName", "");
            }

            // Check if the user is Student
            if (Preferences.ContainsKey("TeacherName", ""))
            {
                UserName = Preferences.Get("TeacherName", "");
            }



        }
    }
}
