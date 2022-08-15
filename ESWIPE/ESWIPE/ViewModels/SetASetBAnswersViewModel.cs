using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class SetASetBAnswersViewModel : ViewModelBase
    {
        #region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly IAnswerService _answerService;
        public ObservableCollection<AnswerModel> Answers { get; set; } = new ObservableCollection<AnswerModel>();
        public static FirebaseClient firebase = new FirebaseClient("https://eswipe-37f7c-default-rtdb.asia-southeast1.firebasedatabase.app/");
        #endregion

        #region Constructor
        public SetASetBAnswersViewModel()
        {
            //Title = "Teacher's Data";
            _answerService = DependencyService.Resolve<IAnswerService>();
            IsRefreshing = true;
            GetAllSetASetBAnswer();
            IsBusy = IsRefreshing = false;
        }
        #endregion

        #region Methods
        private void GetAllSetASetBAnswer()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var answersList = await _answerService.GetSetASetBAnswer();

                Device.BeginInvokeOnMainThread(() =>
                {

                    Answers.Clear();
                    if (answersList?.Count > 0)
                    {
                        foreach (var answer in answersList)
                        {
                            Answers.Add(answer);
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
            GetAllSetASetBAnswer();
        });


        public ICommand SelectedAnswerCommand => new Xamarin.Forms.Command<AnswerModel>(async (answer) =>
        {
            if (answer != null)
            {
                var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Set Answer", "Delete Set Answer");

                if (response == "Update Set Answer")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new SetASetBAddAnswerPage(answer));
                }
                else if (response == "Delete Set Answer")
                {
                    IsBusy = true;
                    bool deleteResponse = await _answerService.DeleteAnswer(answer.Key);
                    if (deleteResponse)
                    {
                        GetAllSetASetBAnswer();
                    }
                }
            }
        });
        #endregion
    }
}
