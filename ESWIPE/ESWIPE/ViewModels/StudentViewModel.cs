﻿using System;
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
        public StudentViewModel()
        {
            Title = "Student";
            CheckUserLoginDetails();
        }

        private async void CheckUserLoginDetails()
        {
            string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");

            if (string.IsNullOrWhiteSpace(userDetailsStr))
            {
                await App.Current.MainPage.DisplayAlert("Check Deserialize", "Null", "OK");
            }
            else
            {
                var userInfo = JsonConvert.DeserializeObject<StudentModel>(userDetailsStr);
                App.UserDetails = userInfo;
                await Shell.Current.GoToAsync($"//{nameof(StudentPage)}");
                await App.Current.MainPage.DisplayAlert("Check Deserialize", "Not Null", "OK");
            }
        }
    }
}
