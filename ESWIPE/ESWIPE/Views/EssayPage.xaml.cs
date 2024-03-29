﻿using ESWIPE.Models;
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
    public partial class EssayPage : ContentPage
    {
        public EssayPage()
        {
            InitializeComponent();
            CreateQuestionLabel.Text = "Create Question";
            BindingContext = new QuestionsViewModel();
        }

        public EssayPage(QuestionModel question)
        {
            InitializeComponent();
            CreateQuestionLabel.Text = "Update Question";
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
    }
}