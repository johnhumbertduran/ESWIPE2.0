using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class EssayQuestionsViewModel : ViewModelBase
    {
        #region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly IQuizService _quizService;
        public ObservableCollection<QuizModel> Quizzes { get; set; } = new ObservableCollection<QuizModel>();
        #endregion

        #region Constructor
        public EssayQuestionsViewModel()
        {
            //Title = "Teacher's Data";
            _quizService = DependencyService.Resolve<IQuizService>();
            IsRefreshing = true;
            GetAllEssayQuiz();
        }
        #endregion

        #region Methods
        private void GetAllEssayQuiz()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var quizsList = await _quizService.GetEssayQuestions();

                Device.BeginInvokeOnMainThread(() =>
                {

                    Quizzes.Clear();
                    if (quizsList?.Count > 0)
                    {
                        foreach (var quiz in quizsList)
                        {
                            Quizzes.Add(quiz);
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
            GetAllEssayQuiz();
        });


        public ICommand SelectedQuizCommand => new Xamarin.Forms.Command<QuizModel>(async (quiz) =>
        {
            if (quiz != null)
            {
                var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Module", "Delete Module");

                if (response == "Update Module")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoicePage(quiz));
                }
                else if (response == "Delete Module")
                {
                    IsBusy = true;
                    bool deleteResponse = await _quizService.DeleteQuiz(quiz.Key);
                    if (deleteResponse)
                    {
                        GetAllEssayQuiz();
                    }
                }
            }
        });

        #endregion
    }
}
