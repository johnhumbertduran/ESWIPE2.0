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
    public partial class IdentificationPage : ContentPage
    {
        public IdentificationPage()
        {
            InitializeComponent();
            CreateQuestionLabel.Text = "Create Module";
            BindingContext = new MultipleChoiceViewModel();
        }

        public IdentificationPage(QuizModel quiz)
        {
            InitializeComponent();
            CreateQuestionLabel.Text = "Update Module";
            BindingContext = new MultipleChoiceViewModel(quiz);
        }

        private async void Cancel_Button(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"///{nameof(QuizTypePage)}", false);
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            QuizCode.Text = "";
            //QuestionCode.Text = "";
            Question.Text = "";
            QuizCode.Focus();
        }

    }
}