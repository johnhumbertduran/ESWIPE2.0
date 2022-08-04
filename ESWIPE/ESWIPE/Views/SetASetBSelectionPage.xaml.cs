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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetASetBSelectionPage : ContentPage
    {
        public SetASetBSelectionPage()
        {
            InitializeComponent();
        }
        private async void cq_Clicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"//{nameof(MultipleChoicePage)}");
            if (Preferences.ContainsKey("multiplechoice"))
            {
                Preferences.Remove("multiplechoice");
            }

            if (Preferences.ContainsKey("trueorfalse"))
            {
                Preferences.Remove("trueorfalse");
            }

            if (Preferences.ContainsKey("identification"))
            {
                Preferences.Remove("identification");
            }

            if (Preferences.ContainsKey("essay"))
            {
                Preferences.Remove("essay");
            }

            if (Preferences.ContainsKey("setasetb"))
            {
                Preferences.Remove("setasetb");
            }

            Preferences.Set("setasetb", "setasetbvalue");
            await Navigation.PushAsync(new SetASetBQuizPage());
            //await App.Current.MainPage.DisplayAlert("Create Question Check", "It's Okay", "OK");
        }

        private async void vq_Clicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"//{nameof(SetASetBPage)}");
            //await App.Current.MainPage.DisplayAlert("View Questions Check", "It's Okay", "OK");
            if (Preferences.ContainsKey("multiplechoice"))
            {
                Preferences.Remove("multiplechoice");
            }

            if (Preferences.ContainsKey("trueorfalse"))
            {
                Preferences.Remove("trueorfalse");
            }

            if (Preferences.ContainsKey("identification"))
            {
                Preferences.Remove("identification");
            }

            if (Preferences.ContainsKey("essay"))
            {
                Preferences.Remove("essay");
            }

            if (Preferences.ContainsKey("setasetb"))
            {
                Preferences.Remove("setasetb");
            }

            Preferences.Set("setasetb", "setasetbvalue");
            await Navigation.PushAsync(new SetASetBViewQuizPage());
        }
        private async void Cancel_Button(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"///{nameof(QuizTypePage)}", false);
        }
    }
}