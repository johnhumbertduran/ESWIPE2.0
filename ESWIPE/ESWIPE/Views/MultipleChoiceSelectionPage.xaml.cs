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
    public partial class MultipleChoiceSelectionPage : ContentPage
    {
        public MultipleChoiceSelectionPage()
        {
            InitializeComponent();
        }
        private async void cq_Clicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"//{nameof(MultipleChoicePage)}");
            await Navigation.PushAsync(new MultipleChoicePage());
            //await App.Current.MainPage.DisplayAlert("Create Question Check", "It's Okay", "OK");
        }

        private async void vq_Clicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"//{nameof(SetASetBPage)}");
            await App.Current.MainPage.DisplayAlert("View Questions Check", "It's Okay", "OK");
        }
        private async void Cancel_Button(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"///{nameof(QuizTypePage)}", false);
        }

    }
}