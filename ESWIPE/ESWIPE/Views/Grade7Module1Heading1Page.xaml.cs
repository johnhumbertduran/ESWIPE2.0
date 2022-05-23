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
    [QueryProperty(nameof(HeadingId), nameof(HeadingId))]
    public partial class Grade7Module1Heading1Page : ContentPage
    {
        //public string HeadingId { get; set; }

        string headingId = "";
        public string HeadingId
        {
            get => headingId;
            set
            {
                headingId = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
            }
        }

        public Grade7Module1Heading1Page()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //int.TryParse(HeadingId, out var result);
        }

    }
}