using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.RichTextEditor;

using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Reflection;
using System.IO;
using ESWIPE.Services.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;


using Firebase.Storage;
using Xamarin.Essentials;
using ESWIPE.Models;
using ESWIPE.ViewModels;
using ESWIPE.Views;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TitleContentViewPage : ContentPage
    {

        public TitleContentViewPage()
        {
            InitializeComponent();
            //if (!string.IsNullOrEmpty(Rte.Text))
            //{
            //    Preferences.Set("RTEContent", Rte.HtmlText);
            //}

            BindingContext = new TitleContentViewViewModel();
        }
        
        public TitleContentViewPage(ContentModel content)
        {
            InitializeComponent();
            BindingContext = new TitleContentViewViewModel(content);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, message: "Teacher");
        }

        private async void CheckContent(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(CheckContent)}");
        }

        
    }
}