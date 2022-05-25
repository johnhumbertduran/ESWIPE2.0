using ESWIPE.Models;
using ESWIPE.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESWIPE.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminTeacherPage : ContentPage
    {
        public AdminTeacherPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

    }
}