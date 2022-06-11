using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public string WebAPIKey = "AIzaSyAZHeAFjNeVLmgTbIuu1SIfR07ZlJKBoR0";
        public SignupPage()
        {
            InitializeComponent();
            OnSignUpClickFunction();
        }


        private async void Signup_Button(object sender, EventArgs e)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(UserNewMail.Text, UserNewPassword.Text);
                string gettoken = auth.FirebaseToken;
                await App.Current.MainPage.DisplayAlert("Signup", "You have signed up successfully! You can log in now", "Ok");
                UserNewMail.Text = "";
                UserNewPassword.Text = "";
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert 2", ex.Message, "Ok");
            }
        }

        private void OnSignUpClickFunction()
        {
            Loginclick.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    //DisplayAlert("Login Info", "Admin", "ok");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                })
            });
        }
    }
}