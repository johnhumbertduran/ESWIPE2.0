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
        public StudentPage()
        {
            InitializeComponent();
            GetStudentInformation();
        }

        async private void GetStudentInformation()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
            try
            {
                //This is the saved firebaseauthentication that was saved during the time of login
                var savedfirebaseauth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));

                //Here we are Refreshin the token
                var RefreshedContent = await authProvider.RefreshAuthAsync(savedfirebaseauth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));

                //Now lets gradb user information
                MyUsername.Text = savedfirebaseauth.User.Email;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Token Expired!", "Ok");
            }
        }
    }
}