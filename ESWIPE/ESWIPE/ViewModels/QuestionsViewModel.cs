using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class QuestionsViewModel : ViewModelBase
    {
        #region Constructor
        public QuestionsViewModel()
        {
            //Title = "Register Student";
            _questionService = DependencyService.Resolve<IQuestionService>();
        }

        public QuestionsViewModel(QuestionModel questionResponse)
        {
            //Title = "Update Student";
            _questionService = DependencyService.Resolve<IQuestionService>();
            QuestionDetail = new QuestionModel
            {
                CreatedBy = questionResponse.CreatedBy,
                DateCreated = questionResponse.DateCreated,
                Quarters = questionResponse.Quarters,
                Question = questionResponse.Question,
                QuizCode = questionResponse.QuizCode,
                QuizType = questionResponse.QuizType,
                Section = questionResponse.Section,
                Key = questionResponse.Key
            };
        }
        #endregion


        #region Properties
        private readonly IQuestionService _questionService;

        private QuestionModel _questionDetail = new QuestionModel();

        public QuestionModel QuestionDetail
        {
            get => _questionDetail;
            set => SetProperty(ref _questionDetail, value);
        }

        #endregion



        #region Commands

        public Command SaveQuestionCommand
        {
            get
            {
                return new Command(SaveQuestion);
            }
        }

        private async void SaveQuestion()
        {
            if (string.IsNullOrEmpty(_questionDetail.Question))
            {
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please input the empty values", "OK");
            }
            else
            {
                if (IsBusy) { return; }
                IsBusy = true;
                bool res = await _questionService.AddorUpdateQuestion(QuestionDetail);
                if (res)
                {
                    if (!string.IsNullOrWhiteSpace(QuestionDetail.Key))
                    {
                        await Application.Current.MainPage.DisplayAlert("Update Info", "Question Updated Succesfully!", "OK");

                        await Shell.Current.GoToAsync("..");
                        //await Application.Current.MainPage.Navigation.PushAsync(new AdminTeacherPage());
                    }
                    else
                    {
                        QuestionDetail = new QuestionModel() { };
                        await Application.Current.MainPage.DisplayAlert("Adding Question Info", "Question succesfully saved!", "OK");
                    }
                }
                IsBusy = false;
            }
        }

        #endregion
    }
}
