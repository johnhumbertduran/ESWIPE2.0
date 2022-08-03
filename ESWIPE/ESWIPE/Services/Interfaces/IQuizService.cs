using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using System.Threading.Tasks;

namespace ESWIPE.Services.Interfaces
{
    public interface IQuizService
    {
        Task<bool> AddorUpdateQuiz(QuizModel quizModel);
        Task<bool> DeleteQuiz(string key);
        Task<List<QuizModel>> GetAllQuiz();
        Task<List<QuizModel>> GetEssayQuestions();
        Task<List<QuizModel>> GetIdentificationQuestions();
        Task<List<QuizModel>> GetMultipleChoiceQuestions();
        Task<List<QuizModel>> GetSetASetBQuestions();
        Task<List<QuizModel>> GetTrueOrFalseQuestions();
    }
}
