using System;
using System.Collections.Generic;
using System.Text;

using Firebase.Database;
using Firebase.Database.Query;
using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ESWIPE.Services.Implementations
{
    public class QuizService : IQuizService
    {
        readonly FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        });

        //static int nextTeacherNumber = 20220000;
        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;
        public string Quarters;

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
                if (Preferences.ContainsKey("Key"))
                {
                    Key = Preferences.Get("Key", "KeyValue");
                }

                if (Preferences.ContainsKey("Username"))
                {
                    UserName = Preferences.Get("Username", "UsernameValue");
                }

                if (Preferences.ContainsKey("TeacherName"))
                {
                    TeacherName = Preferences.Get("TeacherName", "TeacherNameValue");
                }

                if (Preferences.ContainsKey("Section"))
                {
                    Section = Preferences.Get("Section", "SectionValue");
                }

                if (Preferences.ContainsKey("quarter1pass"))
                {
                    Quarters = "quarter1";
                }

                if (Preferences.ContainsKey("quarter2pass"))
                {
                    Quarters = "quarter2";
                }

                if (Preferences.ContainsKey("quarter3pass"))
                {
                    Quarters = "quarter3";
                }

                if (Preferences.ContainsKey("quarter4pass"))
                {
                    Quarters = "quarter4";
                }

                var delay = TimeSpan.FromMinutes(480);
                var date_now = DateTime.UtcNow + delay;

                quizModel.DateCreated = date_now.ToString();
                quizModel.CreatedBy = TeacherName;
                quizModel.Quarters = Quarters;
                var response = await firebase.Child(nameof(QuizModel)).PostAsync(quizModel);
                //quizModel.Key = response.Key;
                //await firebase.Child(nameof(QuizModel)).Child(quizModel.Key).PutAsync(quizModel);

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

                DateCreated = f.Object.DateCreated,
                CreatedBy = f.Object.CreatedBy,
                QuizCode = f.Object.QuizCode,
                //QuizQty = f.Object.QuizQty,
                //QuizAnswer = f.Object.QuizAnswer,
                //QuizCorrectAnswer = f.Object.QuizCorrectAnswer,
                Key = f.Key
            }).ToList();
        }
    }
}
