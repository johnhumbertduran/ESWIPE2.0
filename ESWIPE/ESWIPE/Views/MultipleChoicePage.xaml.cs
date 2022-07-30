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
            QuizCode.Text = "";
            QuestionCode.Text = "";
            Question.Text = "";
            await Shell.Current.GoToAsync($"///{nameof(MultipleChoiceSelectionPage)}", false);
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            QuizCode.Text = "";
            QuestionCode.Text = "";
            Question.Text = "";
            QuizCode.Focus();
        }
    }
}