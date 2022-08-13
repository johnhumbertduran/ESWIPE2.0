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

        private readonly IQuestionService _questionService;
        public ObservableCollection<QuestionModel> Questions { get; set; } = new ObservableCollection<QuestionModel>();
        #endregion

        #region Constructor
        public EssayQuestionsViewModel()
        {
            //Title = "Teacher's Data";
            _questionService = DependencyService.Resolve<IQuestionService>();
            IsRefreshing = true;
            GetAllEssayQuestion();
            IsBusy = IsRefreshing = false;
        }
        #endregion

        #region Methods
        private void GetAllEssayQuestion()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var questionsList = await _questionService.GetEssayQuestions();

                Device.BeginInvokeOnMainThread(() =>
                {

                    Questions.Clear();
                    if (questionsList?.Count > 0)
                    {
                        foreach (var question in questionsList)
                        {
                            Questions.Add(question);
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
            GetAllEssayQuestion();
        });


        public ICommand SelectedQuestionCommand => new Xamarin.Forms.Command<QuestionModel>(async (question) =>
        {
            if (question != null)
            {
                var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Question", "Delete Question");

                if (response == "Update Question")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new EssayPage(question));
                }
                else if (response == "Delete Question")
                {
                    IsBusy = true;
                    bool deleteResponse = await _questionService.DeleteQuestion(question.Key);
                    if (deleteResponse)
                    {
                        GetAllEssayQuestion();
                    }
                }
            }
        });

        #endregion
    }
}
