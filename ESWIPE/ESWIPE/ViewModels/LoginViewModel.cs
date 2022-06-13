using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using ESWIPE.Services.Implementations;
using ESWIPE.Services.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;
using ESWIPE.Views;
using ESWIPE.ViewModels;
using System.ComponentModel;

namespace ESWIPE.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        public LoginViewModel()
        {

        }
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public Command LoginCommand
        {
            get
            {
                return new Command(Login);
            }
        }

        private async void Login()
        {
            //null or empty field validation, check weather email and password is null or empty

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            }
            else
            {
                //call GetUser function which we define in Firebase helper class
                var user = await FirebaseHelper.GetTeacher(Username);
                //firebase return null valuse if user data not found in database
                if (user != null)
                {
                    if (Username == user.Username && Password == user.Password)
                    {
                        await App.Current.MainPage.DisplayAlert("Login Success", "", "Ok");
                        //Navigate to Wellcom page after successfuly login
                        //pass user email to welcom page
                        //await App.Current.MainPage.Navigation.PushAsync(new WelcomPage(Email));
                        await Shell.Current.GoToAsync($"//{nameof(TeacherStudentPage)}");
                    }
                    else
                    { 
                        await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");
                }
            }
        }
    }
}
