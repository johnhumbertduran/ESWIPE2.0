using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class MultipleChoiceQuizViewModel : ViewModelBase
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
        public MultipleChoiceQuizViewModel()
        {
            //Title = "Teacher's Data";
            _quizService = DependencyService.Resolve<IQuizService>();
            IsRefreshing = true;
            GetAllMultipleChoiceQuiz();
        }
        #endregion

        #region Methods
        private void GetAllMultipleChoiceQuiz()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var quizzesList = await _quizService.GetMultipleChoiceQuizzes();

                Device.BeginInvokeOnMainThread(() =>
                {

                    Quizzes.Clear();
                    if (quizzesList?.Count > 0)
                    {
                        foreach (var quiz in quizzesList)
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
            GetAllMultipleChoiceQuiz();
        });


        public ICommand SelectedQuizCommand => new Xamarin.Forms.Command<QuizModel>(async (quiz) =>
        {
            if (quiz != null)
            {
                var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Quiz", "Add Question", "View Questions", "Delete Quiz");

                if (response == "Update Quiz")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceQuizPage(quiz));
                }
                else if (response == "Add Question")
                {
                    if (Preferences.ContainsKey("essayCode"))
                    {
                        Preferences.Remove("essayCode");
                    }

                    if (Preferences.ContainsKey("identificationCode"))
                    {
                        Preferences.Remove("identificationCode");
                    }

                    if (Preferences.ContainsKey("multipleChoiceCode"))
                    {
                        Preferences.Remove("multipleChoiceCode");
                    }

                    if (Preferences.ContainsKey("setASetBCode"))
                    {
                        Preferences.Remove("setASetBCode");
                    }

                    if (Preferences.ContainsKey("setACode"))
                    {
                        Preferences.Remove("setACode");
                    }

                    if (Preferences.ContainsKey("setBCode"))
                    {
                        Preferences.Remove("setBCode");
                    }

                    if (Preferences.ContainsKey("trueOrFalseCode"))
                    {
                        Preferences.Remove("trueOrFalseCode");
                    }

                    Preferences.Set("multipleChoiceCode", quiz.QuizCode);

                    await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoicePage());
                }
                else if (response == "View Questions")
                {
                    if (Preferences.ContainsKey("essayCode"))
                    {
                        Preferences.Remove("essayCode");
                    }

                    if (Preferences.ContainsKey("identificationCode"))
                    {
                        Preferences.Remove("identificationCode");
                    }

                    if (Preferences.ContainsKey("multipleChoiceCode"))
                    {
                        Preferences.Remove("multipleChoiceCode");
                    }

                    if (Preferences.ContainsKey("setASetBCode"))
                    {
                        Preferences.Remove("setASetBCode");
                    }

                    if (Preferences.ContainsKey("setACode"))
                    {
                        Preferences.Remove("setACode");
                    }

                    if (Preferences.ContainsKey("setBCode"))
                    {
                        Preferences.Remove("setBCode");
                    }

                    if (Preferences.ContainsKey("trueOrFalseCode"))
                    {
                        Preferences.Remove("trueOrFalseCode");
                    }

                    Preferences.Set("multipleChoiceCode", quiz.QuizCode);

                    await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceViewQuestionsPage());
                }
                else if (response == "Delete Quiz")
                {
                    IsBusy = true;
                    bool deleteResponse = await _quizService.DeleteQuiz(quiz.Key);
                    if (deleteResponse)
                    {
                        GetAllMultipleChoiceQuiz();
                    }
                }
            }
        });

        #endregion
    }
}
