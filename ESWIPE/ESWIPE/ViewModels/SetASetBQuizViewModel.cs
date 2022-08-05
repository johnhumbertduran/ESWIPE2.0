﻿using ESWIPE.Models;
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
    public class SetASetBQuizViewModel : ViewModelBase
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
        public SetASetBQuizViewModel()
        {
            //Title = "Teacher's Data";
            _quizService = DependencyService.Resolve<IQuizService>();
            IsRefreshing = true;
            GetAllSetASetBQuiz();
        }
        #endregion

        #region Methods
        private void GetAllSetASetBQuiz()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var quizzesList = await _quizService.GetSetASetBQuizzes();

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
            GetAllSetASetBQuiz();
        });


        public ICommand SelectedQuizCommand => new Xamarin.Forms.Command<QuizModel>(async (quiz) =>
        {
            if (quiz != null)
            {
                var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Quiz", "Add Question", "View Question", "Delete Quiz");

                if (response == "Update Quiz")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new SetASetBQuizPage(quiz));
                }
                else if (response == "Add Question")
                {
                    if (Preferences.ContainsKey("setASetBCode"))
                    {
                        Preferences.Remove("setASetBCode");
                    }

                    Preferences.Set("setASetBCode", quiz.QuizCode);

                    await Application.Current.MainPage.Navigation.PushAsync(new SetASetBPage());
                }
                else if (response == "View Question")
                {
                    await Application.Current.MainPage.DisplayAlert("Adding Question", "Yes Please", "OK");
                }
                else if (response == "Delete Quiz")
                {
                    IsBusy = true;
                    bool deleteResponse = await _quizService.DeleteQuiz(quiz.Key);
                    if (deleteResponse)
                    {
                        GetAllSetASetBQuiz();
                    }
                }
            }
        });

        #endregion
    }
}
