using System;
using System.Collections.Generic;
using System.Text;

using Firebase.Database;
using Firebase.Database.Query;
using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ESWIPE.Services.Implementations
{
    public class QuizService : IQuizService
    {
        readonly FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        });

        //static int nextTeacherNumber = 20220000;

        public async Task<bool> AddorUpdateQuiz(QuizModel quizModel)
        {


            if (!string.IsNullOrWhiteSpace(quizModel.Key))
            {
                try
                {
                    await firebase.Child(nameof(QuizModel)).Child(quizModel.Key).PutAsync(quizModel);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                //nextTeacherNumber++;
                //teacherModel.TeacherNumber = nextTeacherNumber;
                //teacherModel.Username = teacherModel.Name;
                //teacherModel.Password = teacherModel.TeacherNumber.ToString();
                //quizModel.UserRole = "Teacher";
                var delay = TimeSpan.FromMinutes(480);
                var date_now = DateTime.UtcNow + delay;

                quizModel.DateCreated = date_now.ToString();
                var response = await firebase.Child(nameof(QuizModel)).PostAsync(quizModel);
                quizModel.Key = response.Key;
                await firebase.Child(nameof(QuizModel)).Child(quizModel.Key).PutAsync(quizModel);

                if (response.Key != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public async Task<bool> DeleteQuiz(string key)
        {
            try
            {
                await firebase.Child(nameof(QuizModel)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<QuizModel>> GetAllQuiz()
        {
            return (await firebase.Child(nameof(QuizModel)).OnceAsync<QuizModel>()).Select(f => new QuizModel
            {
                //TeacherNumber = f.Object.TeacherNumber,
                DateCreated = f.Object.DateCreated,
                CreatedBy = f.Object.CreatedBy,
                SubjectQuizCode = f.Object.SubjectQuizCode,
                SubjectQuizQty = f.Object.SubjectQuizQty,
                SubjectQuizAnswer = f.Object.SubjectQuizAnswer,
                SubjectQuizCorrectAnswer = f.Object.SubjectQuizCorrectAnswer,
                Key = f.Key
            }).ToList();
        }
    }
}
