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
    public partial class QuizTypePage : ContentPage
    {
        public QuizTypePage()
        {
            InitializeComponent();
        }

        private async void mc_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(MultipleChoiceSelectionPage)}");
        }

        private async void tf_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(TrueOrFalseSelectionPage)}");
        }

        private async void id_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(IdentificationSelectionPage)}");
        }

        private async void essay_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(EssaySelectionPage)}");
        }

        private async void sasb_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(SetASetBSelectionPage)}");
        }
    }
}