using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ESWIPE.Views;
using Firebase.Auth;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Essentials;
using ESWIPE.ViewModels;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        readonly LoginViewModel loginViewModel;
        public LoginPage()
        {
            loginViewModel = new LoginViewModel();
            InitializeComponent();
            OnSignUpClickFunction();
            BindingContext = loginViewModel;
        }

        private void OnSignUpClickFunction()
        {
            signupclick.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    //DisplayAlert("Login Info", "Admin", "ok");
                    await Shell.Current.GoToAsync($"//{nameof(SignupPage)}");
                })
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //if (Preferences.ContainsKey("Username", ""))
            //{
            //    if (Preferences.ContainsKey("TeacherName", ""))
            //    {
            //        await Shell.Current.GoToAsync($"//{nameof(TeacherStudentPage)}");
            //    }

            //    if (Preferences.ContainsKey("StudentName", ""))
            //    {
            //        await Shell.Current.GoToAsync($"//{nameof(StudentPage)}");
            //    }
            //}
            //else
            //{
            //    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            //}

        }

    }
}