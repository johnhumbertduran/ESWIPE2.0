﻿using ESWIPE.Models;
using ESWIPE.Services.Implementations;
using ESWIPE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class AnswersViewModel : ViewModelBase
    {
        #region Constructor
        public AnswersViewModel()
        {
            //Title = "Register Student";
            _answerService = DependencyService.Resolve<IAnswerService>();
        }

        public AnswersViewModel(AnswerModel answerResponse)
        {
            //Title = "Update Student";
            _answerService = DependencyService.Resolve<IAnswerService>();
            AnswerDetail = new AnswerModel
            {
                CreatedBy = answerResponse.CreatedBy,
                DateCreated = answerResponse.DateCreated,
                Quarters = answerResponse.Quarters,
                QuizType = answerResponse.QuizType,
                QuizCode = answerResponse.QuizCode,
                Section = answerResponse.Section,
                Question = answerResponse.Question,
                Answer = answerResponse.Answer,
                CorrectAnswer = answerResponse.CorrectAnswer,
                Key = answerResponse.Key
            };
        }
        #endregion


        #region Properties
        private readonly IAnswerService _answerService;

        private AnswerModel _answerDetail = new AnswerModel();

        public AnswerModel AnswerDetail
        {
            get => _answerDetail;
            set => SetProperty(ref _answerDetail, value);
        }

        #endregion



        #region Commands

        public Command SaveCorrectAnswerCommand
        {
            get
            {
                return new Command(SaveCorrectAnswer);
            }
        }

        // Correct Answer Command
        private async void SaveCorrectAnswer()
        {
            if (string.IsNullOrEmpty(_answerDetail.CorrectAnswer))
            {
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please input the empty values", "OK");
            }
            else
            {
                if (IsBusy) { return; }
                IsBusy = true;
                bool res = await _answerService.AddorUpdateAnswer(AnswerDetail);
                if (res)
                {
                    if (!string.IsNullOrWhiteSpace(AnswerDetail.Key))
                    {
                        await Application.Current.MainPage.DisplayAlert("Update Info", "Answer updated Succesfully!", "OK");

                        await Shell.Current.GoToAsync("..");
                        //await Application.Current.MainPage.Navigation.PushAsync(new AdminTeacherPage());
                    }
                    else
                    {
                        AnswerDetail = new AnswerModel() { };
                        await Application.Current.MainPage.DisplayAlert("Adding Answer Info", "Answer succesfully saved!", "OK");
                        await Shell.Current.GoToAsync("..");
                    }
                }
                IsBusy = false;
            }
        }
        
        // Answer Command
        public Command SaveAnswerCommand
        {
            get
            {
                return new Command(SaveAnswer);
            }
        }

        private async void SaveAnswer()
        {
            if (string.IsNullOrEmpty(_answerDetail.Answer))
            {
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please input the empty values", "OK");
            }
            else
            {
                if (IsBusy) { return; }
                IsBusy = true;
                bool res = await _answerService.AddorUpdateNotCorrectAnswer(AnswerDetail);
                if (res)
                {
                    if (!string.IsNullOrWhiteSpace(AnswerDetail.Key))
                    {
                        await Application.Current.MainPage.DisplayAlert("Update Info", "Answer updated Succesfully!", "OK");

                        await Shell.Current.GoToAsync("..");
                        //await Application.Current.MainPage.Navigation.PushAsync(new AdminTeacherPage());
                    }
                    else
                    {
                        AnswerDetail = new AnswerModel() { };
                        await Application.Current.MainPage.DisplayAlert("Adding Answer Info", "Answer succesfully saved!", "OK");
                        await Shell.Current.GoToAsync("..");
                    }
                }
                IsBusy = false;
            }
        }

        #endregion
    }
}
