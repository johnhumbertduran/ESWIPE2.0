﻿using System;
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
        //public string WebAPIKey = "AIzaSyAZHeAFjNeVLmgTbIuu1SIfR07ZlJKBoR0";
        LoginViewModel loginViewModel;
        public LoginPage()
        {
            loginViewModel = new LoginViewModel();
            InitializeComponent();
            OnSignUpClickFunction();
            BindingContext = loginViewModel;
        }

        //private async void Button_Clicked(object sender, EventArgs e)
        //{

        //    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));

        //        var auth = await authProvider.SignInWithEmailAndPasswordAsync(Username.Text, Password.Text);
        //        var content = await auth.GetFreshAuthAsync();
        //        var serializedcontent = JsonConvert.SerializeObject(content);
        //        Preferences.Set("MyFirebaseRefreshToken", serializedcontent);

        //        await App.Current.MainPage.DisplayAlert("Login", "You have Logged in successfully!", "Ok");
        //        Username.Text = "";
        //        Password.Text = "";
        //        await Shell.Current.GoToAsync($"//{nameof(StudentPage)}");
        //        //await Navigation.PushAsync(new StudentPage());
        //        //await Shell.Current.GoToAsync($"//{nameof(AdminTeacherPage)}");

        //}

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

    }
}