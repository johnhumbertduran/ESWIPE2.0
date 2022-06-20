using ESWIPE.ViewModels;
using ESWIPE.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ESWIPE
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
            Routing.RegisterRoute(nameof(TeacherCreateModulesPage), typeof(TeacherCreateModulesPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            //if (Preferences.ContainsKey("Key"))
            //{
            //    Preferences.Remove("Key");
            //}

            if (Preferences.ContainsKey("Username", ""))
            {
                Preferences.Remove("Username", "Username");
            }

            if (Preferences.ContainsKey("TeacherName", ""))
            {
                Preferences.Remove("TeacherName", "TeacherName");
            }

            if (Preferences.ContainsKey("StudentName", ""))
            {
                Preferences.Remove("StudentName", "StudentName");
            }

            if (Preferences.ContainsKey("Year", ""))
            {
                Preferences.Remove("Year", "Year");
            }

            if (Preferences.ContainsKey("Section", ""))
            {
                Preferences.Remove("Section", "Section");
            }

            Preferences.Clear();
            await Shell.Current.GoToAsync("//LoginPage");
            //await Navigation.PushAsync(new LoginPage());
        }

        private async void Admin_FlyoutItem_Appearing(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(AdminTeacherPage)}");
        }

        private async void Teacher_FlyoutItem_Appearing(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(TeacherStudentPage)}");
        }

        private async void Module_FlyoutItem_Appearing(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(Q1ModulePage)}");
        }
    }
}
