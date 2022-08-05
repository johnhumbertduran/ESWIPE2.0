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
    public partial class MultipleChoicePage : ContentPage
    {
        public MultipleChoicePage()
        {
            InitializeComponent();
            CreateQuestionLabel.Text = "Create Module";
            BindingContext = new QuestionsViewModel();
        }

        public MultipleChoicePage(QuestionModel question)
        {
            InitializeComponent();
            CreateQuestionLabel.Text = "Update Module";
            BindingContext = new QuestionsViewModel(question);
        }

        private async void Cancel_Button(object sender, EventArgs e)
        {
            //QuizCode.Text = "";
            //QuestionCode.Text = "";
            Question.Text = "";
            await Shell.Current.GoToAsync($"///{nameof(MultipleChoiceSelectionPage)}");
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            //QuizCode.Text = "";
            //QuestionCode.Text = "";
            Question.Text = "";
            Question.Focus();
        }
    }
}