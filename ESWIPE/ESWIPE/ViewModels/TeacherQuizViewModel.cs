using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers.Commands;

namespace ESWIPE.ViewModels
{
    class TeacherQuizViewModel : ViewModelBase
    {
        #region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly IQuizService _quizService;
        public ObservableCollection<QuizModel> Quiz { get; set; } = new ObservableCollection<QuizModel>();
        #endregion

        #region Constructor
        public TeacherQuizViewModel()
        {
            //Title = "Teacher's Data";
            _quizService = DependencyService.Resolve<IQuizService>();
            GetAllQuiz();
        }
        #endregion

        #region Methods
        private void GetAllQuiz()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var quizList = await _quizService.GetAllQuiz();

                Device.BeginInvokeOnMainThread(() =>
                {

                    Quiz.Clear();
                    if (quizList?.Count > 0)
                    {
                        foreach (var quiz in quizList)
                        {
                            Quiz.Add(quiz);
                        }
                    }
                    IsBusy = IsRefreshing = false;
                });

            });
        }
        #endregion

        #region Commands

        public ICommand RefreshCommand => new MvvmHelpers.Commands.Command(() =>
        {
            IsRefreshing = true;
            GetAllQuiz();
        });


        public ICommand SelectedQuizCommand => new Xamarin.Forms.Command<QuizModel>(async (quiz) =>
        {
            if (quiz != null)
            {
                var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Quiz", "Delete Quiz");

                if (response == "Update Quiz")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new TeacherCreateQuizPage(quiz));
                }
                else if (response == "Delete Quiz")
                {
                    IsBusy = true;
                    bool deleteResponse = await _quizService.DeleteQuiz(quiz.Key);
                    if (deleteResponse)
                    {
                        GetAllQuiz();
                    }
                }
            }
        });

        #endregion
    }
}
