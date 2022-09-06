using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESWIPE.Services.Implementations
{
    public class ScoreService : IScoreService
    {
        readonly FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        });

        //static int nextTeacherNumber = 20220000;
        public string Key;
        public string UserName;
        public string TeacherName;
        public string Teacher;
        public string StudentName;
        public string Section;
        public string Quarters;
        public string QuizTypes;
        public string QuizCode;
        public string QuizQuestion;
        public string Sets;
        public string StudentQuizCode;
        public string StudentQuestion;

        public Task<bool> AddorUpdateScore(ScoreModel scoreModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteScore(string key)
        {
            throw new NotImplementedException();
        }

        public Task<List<ScoreModel>> GetAllScore()
        {
            throw new NotImplementedException();
        }

        public Task<List<ScoreModel>> GetEssayScore()
        {
            throw new NotImplementedException();
        }

        public Task<List<ScoreModel>> GetIdentificationScore()
        {
            throw new NotImplementedException();
        }

        public Task<List<ScoreModel>> GetMultipleChoiceScore()
        {
            throw new NotImplementedException();
        }

        public Task<List<ScoreModel>> GetScore()
        {
            throw new NotImplementedException();
        }

        public Task<List<ScoreModel>> GetSetASetBScore()
        {
            throw new NotImplementedException();
        }

        public Task<List<ScoreModel>> GetTrueOrFalseScore()
        {
            throw new NotImplementedException();
        }
    }
}
