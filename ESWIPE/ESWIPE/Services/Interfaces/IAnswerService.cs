using ESWIPE.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESWIPE.Services.Interfaces
{
    public interface IAnswerService
    {
        Task<bool> AddorUpdateAnswer(AnswerModel answerModel);
        Task<bool> AddorUpdateNotCorrectAnswer(AnswerModel answerModel);
        Task<bool> DeleteAnswer(string key);
        Task<List<AnswerModel>> GetAllAnswer();
        Task<List<AnswerModel>> GetEssayAnswer();
        Task<List<AnswerModel>> GetIdentificationAnswer();
        Task<List<AnswerModel>> GetMultipleChoiceAnswer();
        Task<List<AnswerModel>> GetMultipleChoiceCorrectAnswer();
        Task<List<AnswerModel>> GetSetASetBAnswer();
        Task<List<AnswerModel>> GetTrueOrFalseAnswer();
    }
}
