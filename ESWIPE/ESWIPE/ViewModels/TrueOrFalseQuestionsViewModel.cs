﻿using ESWIPE.Models;
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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class TrueOrFalseQuestionsViewModel : ViewModelBase
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
        public static FirebaseClient firebase = new FirebaseClient("https://eswipe-37f7c-default-rtdb.asia-southeast1.firebasedatabase.app/");
        #endregion

        #region Constructor
        public TrueOrFalseQuestionsViewModel()
        {
            //Title = "Teacher's Data";
            _questionService = DependencyService.Resolve<IQuestionService>();
            IsRefreshing = true;
            GetAllTrueOrFalseQuestion();
            IsBusy = IsRefreshing = false;
        }
        #endregion

        #region Methods
        private void GetAllTrueOrFalseQuestion()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var questionsList = await _questionService.GetTrueOrFalseQuestions();

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
            GetAllTrueOrFalseQuestion();
        });


        public ICommand SelectedQuestionCommand => new Xamarin.Forms.Command<QuestionModel>(async (question) =>
        {
            if (question != null)
            {
                var AnswerData = await GetAnswer(question.CreatedBy, question.Quarters, question.Question);
                if (AnswerData != null)
                {
                    if (AnswerData.CorrectAnswer != "")
                    {
                        var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Question", "View Answer", "Delete Question");

                        if (response == "Update Question")
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new TrueOrFalsePage(question));
                        }
                        else if (response == "View Answer")
                        {
                            if (Preferences.ContainsKey("essayQuestion"))
                            {
                                Preferences.Remove("essayQuestion");
                            }

                            if (Preferences.ContainsKey("identificationQuestion"))
                            {
                                Preferences.Remove("identificationQuestion");
                            }

                            if (Preferences.ContainsKey("multipleChoiceQuestion"))
                            {
                                Preferences.Remove("multipleChoiceQuestion");
                            }

                            if (Preferences.ContainsKey("setASetBQuestion"))
                            {
                                Preferences.Remove("setASetBQuestion");
                            }

                            if (Preferences.ContainsKey("trueOrFalseQuestion"))
                            {
                                Preferences.Remove("trueOrFalseQuestion");
                            }

                            Preferences.Set("trueOrFalseQuestion", question.Question);

                            await Application.Current.MainPage.Navigation.PushAsync(new TrueOrFalseViewAnswerPage());

                        }
                        else if (response == "Delete Question")
                        {
                            IsBusy = true;
                            bool deleteResponse = await _questionService.DeleteQuestion(question.Key);
                            if (deleteResponse)
                            {
                                GetAllTrueOrFalseQuestion();
                            }
                        }
                    }
                    else
                    {
                        var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Question", "Add Answer", "Delete Question");

                        if (response == "Update Question")
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new TrueOrFalsePage(question));
                        }
                        else if (response == "Add Answer")
                        {
                            if (Preferences.ContainsKey("essayQuestion"))
                            {
                                Preferences.Remove("essayQuestion");
                            }

                            if (Preferences.ContainsKey("identificationQuestion"))
                            {
                                Preferences.Remove("identificationQuestion");
                            }

                            if (Preferences.ContainsKey("multipleChoiceQuestion"))
                            {
                                Preferences.Remove("multipleChoiceQuestion");
                            }

                            if (Preferences.ContainsKey("setASetBQuestion"))
                            {
                                Preferences.Remove("setASetBQuestion");
                            }

                            if (Preferences.ContainsKey("trueOrFalseQuestion"))
                            {
                                Preferences.Remove("trueOrFalseQuestion");
                            }

                            Preferences.Set("trueOrFalseQuestion", question.Question);

                            await Application.Current.MainPage.Navigation.PushAsync(new TrueOrFalseAddAnswerPage());
                        }
                        else if (response == "Delete Question")
                        {
                            IsBusy = true;
                            bool deleteResponse = await _questionService.DeleteQuestion(question.Key);
                            if (deleteResponse)
                            {
                                GetAllTrueOrFalseQuestion();
                            }
                        }
                    }
                }
                else
                {
                    var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Question", "Add Answer", "Delete Question");

                    if (response == "Update Question")
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new TrueOrFalsePage(question));
                    }
                    else if (response == "Add Answer")
                    {
                        if (Preferences.ContainsKey("essayQuestion"))
                        {
                            Preferences.Remove("essayQuestion");
                        }

                        if (Preferences.ContainsKey("identificationQuestion"))
                        {
                            Preferences.Remove("identificationQuestion");
                        }

                        if (Preferences.ContainsKey("multipleChoiceQuestion"))
                        {
                            Preferences.Remove("multipleChoiceQuestion");
                        }

                        if (Preferences.ContainsKey("setASetBQuestion"))
                        {
                            Preferences.Remove("setASetBQuestion");
                        }

                        if (Preferences.ContainsKey("trueOrFalseQuestion"))
                        {
                            Preferences.Remove("trueOrFalseQuestion");
                        }

                        Preferences.Set("trueOrFalseQuestion", question.Question);

                        await Application.Current.MainPage.Navigation.PushAsync(new TrueOrFalseAddAnswerPage());
                    }
                    else if (response == "Delete Question")
                    {
                        IsBusy = true;
                        bool deleteResponse = await _questionService.DeleteQuestion(question.Key);
                        if (deleteResponse)
                        {
                            GetAllTrueOrFalseQuestion();
                        }
                    }
                }
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
