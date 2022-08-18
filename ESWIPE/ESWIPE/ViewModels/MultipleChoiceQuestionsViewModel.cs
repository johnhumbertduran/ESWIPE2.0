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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class MultipleChoiceQuestionsViewModel : ViewModelBase
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
        public MultipleChoiceQuestionsViewModel()
        {
            //Title = "Teacher's Data";
            _questionService = DependencyService.Resolve<IQuestionService>();
            IsRefreshing = true;
            GetAllMultipleChoiceQuestion();
            IsBusy = IsRefreshing = false;
        }
        #endregion

        #region Methods
        private void GetAllMultipleChoiceQuestion()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var questionsList = await _questionService.GetMultipleChoiceQuestions();

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
            GetAllMultipleChoiceQuestion();
        });


        public ICommand SelectedQuestionCommand => new Xamarin.Forms.Command<QuestionModel>(async (question) =>
        {
            if (question != null)
            {

                var AnswerData = await GetAnswer(question.CreatedBy, question.Quarters, question.Question);

                if (AnswerData != null)
                {
                    if (AnswerData.CorrectAnswer != null && AnswerData.Answer != null)
                    {
                        //Add Answer, View Answers and View Correct Answer since Correct Answer has a data

                        var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Question", "Add Answers", "View Answers", "View Correct Answer", "Delete Question");

                        if (response == "Update Question")
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoicePage(question));
                        }
                        else if (response == "Add Answers")
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

                            Preferences.Set("multipleChoiceQuestion", question.Question);

                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceAddAnswerPage());
                        }
                        else if (response == "View Answers")
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

                            Preferences.Set("multipleChoiceQuestion", question.Question);

                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceViewAnswerPage());
                        }
                        else if (response == "View Correct Answer")
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

                            Preferences.Set("multipleChoiceQuestion", question.Question);

                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceViewCorrectAnswerPage());
                        }
                        else if (response == "Delete Question")
                        {
                            IsBusy = true;
                            bool deleteResponse = await _questionService.DeleteQuestion(question.Key);
                            if (deleteResponse)
                            {
                                GetAllMultipleChoiceQuestion();
                            }
                        }
                    }
                    else if (AnswerData.Answer != null && AnswerData.CorrectAnswer == null)
                    {
                        // Add Answer and Add Correct Answer and View Answers here

                        var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Question", "Add Answers", "Add Correct Answer", "View Answers", "Delete Question");

                        if (response == "Update Question")
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoicePage(question));
                        }
                        else if (response == "Add Answers")
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

                            Preferences.Set("multipleChoiceQuestion", question.Question);

                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceAddAnswerPage());
                        }
                        else if (response == "Add Correct Answer")
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

                            Preferences.Set("multipleChoiceQuestion", question.Question);

                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceAddCorrectAnswerPage());
                        }
                        else if (response == "View Answers")
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

                            Preferences.Set("multipleChoiceQuestion", question.Question);

                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceViewAnswerPage());
                        }
                        else if (response == "Delete Question")
                        {
                            IsBusy = true;
                            bool deleteResponse = await _questionService.DeleteQuestion(question.Key);
                            if (deleteResponse)
                            {
                                GetAllMultipleChoiceQuestion();
                            }
                        }

                    }
                    else if (AnswerData.Answer == null && AnswerData.CorrectAnswer != null)
                    {
                        // Add Answer and Add Correct Answer and View Answers here

                        var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Question", "Add Answers", "View Correct Answer", "Delete Question");

                        if (response == "Update Question")
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoicePage(question));
                        }
                        else if (response == "Add Answers")
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

                            Preferences.Set("multipleChoiceQuestion", question.Question);

                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceAddAnswerPage());
                        }
                        else if (response == "View Correct Answer")
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

                            Preferences.Set("multipleChoiceQuestion", question.Question);

                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceViewCorrectAnswerPage());
                        }
                        else if (response == "Delete Question")
                        {
                            IsBusy = true;
                            bool deleteResponse = await _questionService.DeleteQuestion(question.Key);
                            if (deleteResponse)
                            {
                                GetAllMultipleChoiceQuestion();
                            }
                        }

                    }
                    else if (AnswerData.CorrectAnswer == null && AnswerData.Answer == null)
                    {
                        // Add Answer and Add Correct Answer here

                        var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Question", "Add Answers", "Add Correct Answers", "Delete Question");

                        if (response == "Update Question")
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoicePage(question));
                        }
                        else if (response == "Add Answers")
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

                            Preferences.Set("multipleChoiceQuestion", question.Question);

                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceAddAnswerPage());
                        }
                        else if (response == "Add Correct Answers")
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

                            Preferences.Set("multipleChoiceQuestion", question.Question);

                            await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceAddCorrectAnswerPage());
                        }
                        else if (response == "Delete Question")
                        {
                            IsBusy = true;
                            bool deleteResponse = await _questionService.DeleteQuestion(question.Key);
                            if (deleteResponse)
                            {
                                GetAllMultipleChoiceQuestion();
                            }
                        }
                    }
                }
                else
                {
                    // Both Add Answer and CorrectAnswer here with no conditions but ni View Answers since there is no data for this question

                    var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Question", "Add Answers", "Add Correct Answer", "Delete Question");

                    if (response == "Update Question")
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoicePage(question));
                    }
                    else if (response == "Add Answers")
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

                        Preferences.Set("multipleChoiceQuestion", question.Question);

                        await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceAddAnswerPage());
                    }
                    else if (response == "Add Correct Answer")
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

                        Preferences.Set("multipleChoiceQuestion", question.Question);

                        await Application.Current.MainPage.Navigation.PushAsync(new MultipleChoiceAddCorrectAnswerPage());
                    }
                    else if (response == "Delete Question")
                    {
                        IsBusy = true;
                        bool deleteResponse = await _questionService.DeleteQuestion(question.Key);
                        if (deleteResponse)
                        {
                            GetAllMultipleChoiceQuestion();
                        }
                    }
                }


                //############################################################################
                //############################################################################
                //############################################################################
                //############################################################################
                //############################################################################
                //############################################################################

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
