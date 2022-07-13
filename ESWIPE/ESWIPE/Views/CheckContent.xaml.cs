using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ESWIPE.Views;


using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using System.Collections;
using Firebase.Database;
using ESWIPE.Models;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckContent : ContentPage
    {
        //public string TitleContent { set; get; }
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public CheckContent()
        {
            InitializeComponent();

            
        }

        private async void Cancel_Button(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(ModuleViewPage)}", false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var myContents = await firebaseHelper.GetAllContent();
            myListView.ItemsSource = myContents;
        }


        public class FirebaseHelper
        {


            public static string FirebaseClient = "https://eswipe-37f7c-default-rtdb.asia-southeast1.firebasedatabase.app/";
            public static string FrebaseSecret = "IqvYgbWiwScAbg5vnNNUkKlVeGc5NkzgZFYIDfnl";
            public FirebaseClient firebase = new FirebaseClient(FirebaseClient,
                           new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FrebaseSecret) });

            public async Task<List<ContentModel>> GetAllContent()
            {

                return (await firebase.Child(nameof(ContentModel)).OnceAsync<ContentModel>()).
                Select(f => new ContentModel
                {
                    TitleContent = f.Object.TitleContent
                }).ToList();
            }
        }
    }
}