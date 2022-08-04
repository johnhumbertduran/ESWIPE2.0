using ESWIPE.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESWIPE.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<bool> AddorUpdateQuestion(QuestionModel questionModel);
        Task<bool> DeleteQuestion(string key);
        Task<List<QuestionModel>> GetAllQuestion();
        Task<List<QuestionModel>> GetEssayQuestions();
        Task<List<QuestionModel>> GetIdentificationQuestions();
        Task<List<QuestionModel>> GetMultipleChoiceQuestions();
        Task<List<QuestionModel>> GetSetASetBQuestions();
        Task<List<QuestionModel>> GetTrueOrFalseQuestions();
    }
}
