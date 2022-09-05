using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class StudentViewAnswerViewModel : ViewModelBase
    {
        #region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly IAnswerService _answerService;
        public ObservableCollection<AnswerModel> Answer { get; set; } = new ObservableCollection<AnswerModel>();
        public static FirebaseClient firebase = new FirebaseClient("https://eswipe-37f7c-default-rtdb.asia-southeast1.firebasedatabase.app/");
        #endregion

        #region Constructor
        public StudentViewAnswerViewModel()
        {
            //Title = "Teacher's Data";
            _answerService = DependencyService.Resolve<IAnswerService>();
            IsRefreshing = true;
            GetAllAnswer();
            IsBusy = IsRefreshing = false;
        }
        #endregion

        #region Methods
        private void GetAllAnswer()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var answersList = await _answerService.GetAnswerForStudent();

                Device.BeginInvokeOnMainThread(() =>
                {

                    Answer.Clear();
                    if (answersList?.Count > 0)
                    {
                        foreach (var answer in answersList)
                        {
                            Answer.Add(answer);
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
            GetAllAnswer();
        });


        public ICommand SelectedAnswerCommand => new Xamarin.Forms.Command<AnswerModel>(async (answer) =>
        {
            if (answer != null)
            {
                var AnswerData = await GetAnswer(answer.CreatedBy, answer.Quarters, answer.Question);
                //await Application.Current.MainPage.Navigation.PushAsync(new Q1StudentQuestionPage());
                //var response = await Application.Current.MainPage.DisplayActionSheet(question.Question, "Cancel", null, AnswerData.Answer, AnswerData.CorrectAnswer);

                //if (response == "Answer")
                //{
                await Application.Current.MainPage.Navigation.PushAsync(new IdentificationQuizPage());
                //}
            }
        });

        //Read All Answers
        public static async Task<List<AnswerModel>> GetAllAnswers()
        {
            try
            {
                var answerlist = (await firebase
                .Child("AnswerModel")
                .OnceAsync<AnswerModel>()).Select(item =>
                new AnswerModel
                {
                    CreatedBy = item.Object.CreatedBy,
                    DateCreated = item.Object.DateCreated,
                    Quarters = item.Object.Quarters,
                    QuizType = item.Object.QuizType,
                    QuizCode = item.Object.QuizCode,
                    Section = item.Object.Section,
                    Question = item.Object.Question,
                    Answer = item.Object.Answer,
                    CorrectAnswer = item.Object.CorrectAnswer,
                    Key = item.Object.Key
                }).ToList();
                return answerlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read Answer
        public static async Task<AnswerModel> GetAnswer(string createdby, string quarters, string question)
        {
            try
            {
                var allAnswer = await GetAllAnswers();
                await firebase.Child("AnswerModel").OnceAsync<AnswerModel>();
                return allAnswer.Where(a => a.CreatedBy == createdby).Where(b => b.Quarters == quarters).Where(b => b.Question == question).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }

        #endregion
    }
}
