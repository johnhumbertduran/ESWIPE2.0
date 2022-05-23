using System;
using System.Collections.Generic;
using System.Text;

using MvvmHelpers;
using MvvmHelpers.Commands;
using ESWIPE.Views;
using ESWIPE.Models;
using ESWIPE.Services;
using ESWIPE.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    class TeacherCreateQuizViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Quiz> Quiz { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Quiz> RemoveCommand { get; }
        public TeacherCreateQuizViewModel()
        {
            Title = "Register Student";
            Quiz = new ObservableRangeCollection<Quiz>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Quiz>(Remove);
        }

        string emptyCreatedByString = "";
        string emptySubjectQuizCodeString = "";
        string emptySubjectQuizQtyString = "";
        string emptySubjectQuizAnswerString = "";
        string emptySubjectQuizCorrectAnswerString = "";

        public string CreatedBy
        {
            get => emptyCreatedByString;
            set
            {
                if (value == emptyCreatedByString)
                    return;
                emptyCreatedByString = value;
                OnPropertyChanged();
            }
        }

        public string SubjectQuizCode
        {
            get => emptySubjectQuizCodeString;
            set
            {
                if (value == emptySubjectQuizCodeString)
                    return;
                emptySubjectQuizCodeString = value;
                OnPropertyChanged();
            }
        }

        public string SubjectQuizQty
        {
            get => emptySubjectQuizQtyString;
            set
            {
                if (value == emptySubjectQuizQtyString)
                    return;
                emptySubjectQuizQtyString = value;
                OnPropertyChanged();
            }
        }
        
        public string SubjectQuizAnswer
        {
            get => emptySubjectQuizAnswerString;
            set
            {
                if (value == emptySubjectQuizAnswerString)
                    return;
                emptySubjectQuizAnswerString = value;
                OnPropertyChanged();
            }
        }

        public string SubjectQuizCorrectAnswer
        {
            get => emptySubjectQuizCorrectAnswerString;
            set
            {
                if (value == emptySubjectQuizCorrectAnswerString)
                    return;
                emptySubjectQuizCorrectAnswerString = value;
                OnPropertyChanged();
            }
        }


        async Task Add()
        {
            var createdByText = CreatedBy;
            var subjectQuizCodeText = SubjectQuizCode;
            var subjectQuizQtyText = SubjectQuizQty;
            var subjectQuizAnswerText = SubjectQuizAnswer;
            var subjectQuizCorrectAnswerText = SubjectQuizCorrectAnswer;


            if (SubjectQuizCode == "")
            {
                await Application.Current.MainPage.DisplayAlert("Subject Quiz Code empty!", "Please input Subject Quiz Code", "OK");
            }

            if (SubjectQuizQty == "")
            {
                await Application.Current.MainPage.DisplayAlert("Subject Quiz Qty empty!", "Please input Subject Quiz Qty", "OK");
            }

            if (SubjectQuizAnswer == "")
            {
                await Application.Current.MainPage.DisplayAlert("Subject Quiz Answer empty!", "Please input Subject Quiz Answer", "OK");
            }

            if (SubjectQuizCorrectAnswer == "")
            {
                await Application.Current.MainPage.DisplayAlert("Subject Quiz Correct Answer empty!", "Please input Subject Quiz Correct Answer", "OK");
            }

            if ((SubjectQuizCode != "") && (SubjectQuizQty != "") && (SubjectQuizAnswer != "") && (SubjectQuizCorrectAnswer != ""))
            {
                await QuizService.AddQuiz(createdByText, subjectQuizCodeText, subjectQuizQtyText, subjectQuizAnswerText, subjectQuizCorrectAnswerText);

                await Application.Current.MainPage.DisplayAlert("Create Quiz Info", "Succesfully Created Quiz!", "OK");

                SubjectQuizCode = "";
                SubjectQuizQty = "";
                SubjectQuizAnswer = "";
                SubjectQuizCorrectAnswer = "";
                await Refresh();
            }
        }

        async Task Remove(Quiz quiz)
        {
            await QuizService.RemoveQuiz(quiz.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Quiz.Clear();

            var quizzes = await QuizService.GetQuiz();

            Quiz.AddRange(quizzes);

            IsBusy = false;
        }
    }
}
