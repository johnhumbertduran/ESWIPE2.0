using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class MultipleChoiceViewModel : ViewModelBase
    {
        #region Constructor
        public MultipleChoiceViewModel()
        {
            //Title = "Register Student";
            _quizService = DependencyService.Resolve<IQuizService>();
        }

        public MultipleChoiceViewModel(QuizModel quizResponse)
        {
            Title = "Update Student";
            _quizService = DependencyService.Resolve<IQuizService>();
            QuizDetail = new QuizModel
            {
                CreatedBy = quizResponse.CreatedBy,
                DateCreated = quizResponse.DateCreated,
                Quarters = quizResponse.Quarters,
                Question = quizResponse.Question,
                QuizCode = quizResponse.QuizCode,
                QuizType = quizResponse.QuizType,
                Section = quizResponse.Section,
                Key = quizResponse.Key
            };
        }
        #endregion


        #region Properties
        private readonly IQuizService _quizService;

        private QuizModel _quizDetail = new QuizModel();

        public QuizModel QuizDetail
        {
            get => _quizDetail;
            set => SetProperty(ref _quizDetail, value);
        }

        #endregion



        #region Commands

        public Command SaveQuizCommand
        {
            get
            {
                return new Command(SaveQuiz);
            }
        }

        private async void SaveQuiz()
        {
            if (string.IsNullOrEmpty(_quizDetail.Question))
            {
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please input the empty values", "OK");
            }
            else
            {
                if (IsBusy) { return; }
                IsBusy = true;
                bool res = await _quizService.AddorUpdateQuiz(QuizDetail);
                if (res)
                {
                    if (!string.IsNullOrWhiteSpace(QuizDetail.Key))
                    {
                        await Application.Current.MainPage.DisplayAlert("Update Info", "Question Updated Succesfully!", "OK");

                        await Shell.Current.GoToAsync("..");
                        //await Application.Current.MainPage.Navigation.PushAsync(new AdminTeacherPage());
                    }
                    else
                    {
                        QuizDetail = new QuizModel() { };
                        await Application.Current.MainPage.DisplayAlert("Adding Question Info", "Question succesfully saved!", "OK");
                    }
                }
                IsBusy = false;
            }
        }

        #endregion
    }
}
