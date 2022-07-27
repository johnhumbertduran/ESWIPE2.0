using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESWIPE.Services.Implementations
{
    public class AnswerService : IAnswerService
    {
        public Task<bool> AddorUpdateAnswer(AnswerModel answerModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAnswer(string key)
        {
            throw new NotImplementedException();
        }

        public Task<List<AnswerModel>> GetAllAnswer()
        {
            throw new NotImplementedException();
        }
    }
}
