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
    public partial class TrueOrFalseAddAnswerPage : ContentPage
    {
        public TrueOrFalseAddAnswerPage()
        {
            InitializeComponent();
            CreateAnswerLabel.Text = "Create Answer";
            BindingContext = new AnswersViewModel();
        }

        public TrueOrFalseAddAnswerPage(AnswerModel question)
        {
            InitializeComponent();
            CreateAnswerLabel.Text = "Update Answer";
            BindingContext = new AnswersViewModel(question);
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            Answer.Text = "";
            Answer.Focus();
        }
    }
}