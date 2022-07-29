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

        public MultipleChoiceViewModel(StudentModel quizResponse)
        {
            Title = "Update Student";
            _quizService = DependencyService.Resolve<IQuizService>();
            QuizDetail = new QuizModel
            {
                //StudentNumber = studentResponse.StudentNumber,
                DateCreated = quizResponse.Key,
                CreatedBy = quizResponse.Key,
                QuizCode = quizResponse.Key,
                QuizQuestionCode = quizResponse.Key,
                Question = quizResponse.Key,
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
            if (string.IsNullOrEmpty(_quizDetail.QuizCode))
            {
                await App.Current.MainPage.DisplayAlert("Empty Name Value", "Please enter Name", "OK");
            }
            else if (string.IsNullOrEmpty(_quizDetail.QuizQuestionCode))
            {
                await App.Current.MainPage.DisplayAlert("Empty Year Value", "Please enter Year", "OK");
            }
            else if (string.IsNullOrEmpty(_quizDetail.Question))
            {
                await App.Current.MainPage.DisplayAlert("Empty Section Value", "Please enter Section", "OK");
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
