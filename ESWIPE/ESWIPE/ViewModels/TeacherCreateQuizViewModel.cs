using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using ESWIPE.Services.Implementations;
using ESWIPE.Services.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;
using ESWIPE.Views;

namespace ESWIPE.ViewModels
{
    class TeacherCreateQuizViewModel : ViewModelBase
    {
        #region Properties
        private readonly IQuizService _quizService;

        private QuizModel _quizDetail = new QuizModel();

        public QuizModel QuizDetail
        {
            get => _quizDetail;
            set => SetProperty(ref _quizDetail, value);
        }
        #endregion

        #region Constructor
        public TeacherCreateQuizViewModel()
        {
            //Title = "Register Teacher";
            _quizService = DependencyService.Resolve<IQuizService>();
        }

        public TeacherCreateQuizViewModel(QuizModel quizResponse)
        {
            _quizService = DependencyService.Resolve<IQuizService>();
            QuizDetail = new QuizModel
            {
                DateCreated = quizResponse.DateCreated,
                CreatedBy = quizResponse.CreatedBy,
                SubjectQuizCode = quizResponse.SubjectQuizCode,
                SubjectQuizQty = quizResponse.SubjectQuizQty,
                SubjectQuizAnswer = quizResponse.SubjectQuizAnswer,
                SubjectQuizCorrectAnswer = quizResponse.SubjectQuizCorrectAnswer,
                Key = quizResponse.Key
            };
        }
        #endregion

        #region Commands
        public ICommand SaveQuizCommand => new Command(async () =>
        {
            if (IsBusy) { return; }
            IsBusy = true;
            bool res = await _quizService.AddorUpdateQuiz(QuizDetail);
            if (res)
            {
                if (!string.IsNullOrWhiteSpace(QuizDetail.Key))
                {
                    await Application.Current.MainPage.DisplayAlert("Update Info", "Records Updated Succesfully!", "OK");

                    await Shell.Current.GoToAsync("..");
                    //await Application.Current.MainPage.Navigation.PushAsync(new AdminTeacherPage());
                }
                else
                {
                    QuizDetail = new QuizModel() { };
                    await Application.Current.MainPage.DisplayAlert("Registration Info", "Succesfully Registered!", "OK");
                }
            }
            IsBusy = false;

        });
        #endregion
    }
}
