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
    public partial class MultipleChoiceAddCorrectAnswerPage : ContentPage
    {
        public MultipleChoiceAddCorrectAnswerPage()
        {
            InitializeComponent();
            CreateAnswerLabel.Text = "Create Answer";
            BindingContext = new AnswersViewModel();
        }

        public MultipleChoiceAddCorrectAnswerPage(AnswerModel question)
        {
            InitializeComponent();
            CreateAnswerLabel.Text = "Update Answer";
            BindingContext = new AnswersViewModel(question);
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            //QuizCode.Text = "";
            //QuestionCode.Text = "";
            Answer.Text = "";
            Answer.Focus();
        }
    }
}