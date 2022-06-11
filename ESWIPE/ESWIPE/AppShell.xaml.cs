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
        public string WebAPIKey = "AIzaSyAZHeAFjNeVLmgTbIuu1SIfR07ZlJKBoR0";
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
            Routing.RegisterRoute(nameof(StudentPage), typeof(StudentPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            //Routing.RegisterRoute(nameof(AdminTeacherPage), typeof(AdminTeacherPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async void Admin_FlyoutItem_Appearing(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(AdminTeacherPage)}");
        }

        private async void Teacher_FlyoutItem_Appearing(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(TeacherStudentPage)}");
        }

        private void FlyoutItem_Appearing(object sender, EventArgs e)
        {

        }
    }
}
