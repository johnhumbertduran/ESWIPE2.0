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
    public partial class MultipleChoicePage : ContentPage
    {
        public MultipleChoicePage()
        {
            InitializeComponent();
        }

        private async void Cancel_Button(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"///{nameof(QuizTypePage)}", false);
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            StudentName.Text = "";
            Year.Text = "";
            Section.Text = "";
            Username.Text = "";
            Password.Text = "";
            StudentName.Focus();
        }
    }
}