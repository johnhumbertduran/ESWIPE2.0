using ESWIPE.Models;
using ESWIPE.ViewModels;
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
    public partial class SetASetBPage : ContentPage
    {
        public SetASetBPage()
        {
            InitializeComponent();
            //CreateQuestionLabel.Text = "Create Set";

            if (Preferences.ContainsKey("setACode"))
            {
                CreateQuestionLabel.Text = "Create Set A";
            }

            if (Preferences.ContainsKey("setBCode"))
            {
                CreateQuestionLabel.Text = "Create Set B";
            }

            BindingContext = new QuestionsViewModel();
        }

        public SetASetBPage(QuestionModel question)
        {
            InitializeComponent();
            //CreateQuestionLabel.Text = "Update Set";

            if (Preferences.ContainsKey("setACode"))
            {
                CreateQuestionLabel.Text = "Update Set A";
            }

            if (Preferences.ContainsKey("setBCode"))
            {
                CreateQuestionLabel.Text = "Update Set B";
            }

            BindingContext = new QuestionsViewModel(question);
        }

        private async void Cancel_Button(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"///{nameof(QuizTypePage)}", false);
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            //QuizCode.Text = "";
            //QuestionCode.Text = "";
            Question.Text = "";
            Question.Focus();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Preferences.ContainsKey("setACode"))
            {
                EditorLabel.Text = "Set A";
            }
            
            if (Preferences.ContainsKey("setBCode"))
            {
                EditorLabel.Text = "Set B";
            }

        }
    }
}