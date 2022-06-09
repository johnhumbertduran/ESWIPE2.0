using System;
using System.Collections.Generic;
using System.Text;

using MvvmHelpers;
using ESWIPE.Models;
using ESWIPE.Services;
using ESWIPE.ViewModels;
using MvvmHelpers.Commands;
using System.Threading.Tasks;

namespace ESWIPE.ViewModels
{
    class AdminQuizViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Quiz> Quiz { get; set; }
        public AsyncCommand RefreshCommandQuiz { get; }
        public AdminQuizViewModel()
        {
            Title = "Quiz's Data";
            Quiz = new ObservableRangeCollection<Quiz>();
            RefreshCommandQuiz = new AsyncCommand(Refresh);
            //_ = Task.Run(async () => await Refresh());
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Quiz.Clear();

            var quizzes = await QuizService1.GetQuiz();

            Quiz.AddRange(quizzes);

            IsBusy = false;
        }
    }
}
