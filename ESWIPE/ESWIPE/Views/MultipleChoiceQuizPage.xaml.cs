using ESWIPE.Models;
using ESWIPE.ViewModels;
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
    public partial class MultipleChoiceQuizPage : ContentPage
    {
        public MultipleChoiceQuizPage()
        {
            InitializeComponent();
            CreateQuestionLabel.Text = "Create Instructions";
            BindingContext = new QuizViewModel();
        }

        public MultipleChoiceQuizPage(QuizModel quiz)
        {
            InitializeComponent();
            CreateQuestionLabel.Text = "Update Instructions";
            BindingContext = new QuizViewModel(quiz);
        }

        private async void Cancel_Button(object sender, EventArgs e)
        {
            QuizCode.Text = "";
            //QuestionCode.Text = "";
            Instructions.Text = "";
            await Shell.Current.GoToAsync($"///{nameof(MultipleChoiceSelectionPage)}");
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            QuizCode.Text = "";
            //QuestionCode.Text = "";
            Instructions.Text = "";
            QuizCode.Focus();
        }
    }
}