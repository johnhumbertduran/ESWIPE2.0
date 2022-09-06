using ESWIPE.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESWIPE.Services.Interfaces
{
    public interface IScoreService
    {
        Task<bool> AddorUpdateScore(ScoreModel scoreModel);
        Task<bool> DeleteScore(string key);
        Task<List<ScoreModel>> GetAllScore();
        Task<List<ScoreModel>> GetScore();
        Task<List<ScoreModel>> GetEssayScore();
        Task<List<ScoreModel>> GetIdentificationScore();
        Task<List<ScoreModel>> GetMultipleChoiceScore();
        Task<List<ScoreModel>> GetSetASetBScore();
        Task<List<ScoreModel>> GetTrueOrFalseScore();
    }
}
