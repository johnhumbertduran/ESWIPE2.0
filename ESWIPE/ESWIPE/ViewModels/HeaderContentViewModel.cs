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

        private string studentname;
        public string StudentName
        {
            get { return studentname; }
            set { SetProperty(ref studentname, value); }
        }

        public HeaderContentViewModel()
        {
            //Title = "Student";
            //CheckUserLoginDetails();
            UserName = Preferences.Get("Username", "");
            StudentName = Preferences.Get("StudentName", "");
        }
    }
}
