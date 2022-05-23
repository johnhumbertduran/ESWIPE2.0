using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESWIPE.Views
{
    public partial class AdminPage : ContentPage
    {
        public AdminPage()
        {
            InitializeComponent();
        }
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var loggedIn = true;

            if (loggedIn)
            {
                await Shell.Current.GoToAsync($"//{nameof(AdminPage)}");
            }
        }
    }
}