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
        public string QuizTypes;

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

                if (Preferences.ContainsKey("multiplechoice"))
                {
                    QuizTypes = "multiplechoice";
                }

                if (Preferences.ContainsKey("trueorfalse"))
                {
                    QuizTypes = "trueorfalse";
                }

                if (Preferences.ContainsKey("identification"))
                {
                    QuizTypes = "identification";
                }

                if (Preferences.ContainsKey("essay"))
                {
                    QuizTypes = "essay";
                }

                if (Preferences.ContainsKey("setasetb"))
                {
                    QuizTypes = "setasetb";
                }

                var delay = TimeSpan.FromMinutes(480);
                var date_now = DateTime.UtcNow + delay;

                quizModel.DateCreated = date_now.ToString();
                quizModel.CreatedBy = TeacherName;
                quizModel.Quarters = Quarters;
                quizModel.Section = Section;
                quizModel.QuizType = QuizTypes;
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

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                Instructions = f.Object.Instructions,
                QuizCode = f.Object.QuizCode,
                QuizType = f.Object.QuizType,
                Section = f.Object.Section,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<QuizModel>> GetEssayQuizzes()
        {
            //throw new NotImplementedException();
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

            return (await firebase.Child(nameof(QuizModel)).OnceAsync<QuizModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(b => b.Object.QuizType == "essay").Select(f => new QuizModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                Instructions = f.Object.Instructions,
                QuizCode = f.Object.QuizCode,
                QuizType = f.Object.QuizType,
                Section = f.Object.Section,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<QuizModel>> GetIdentificationQuizzes()
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

            return (await firebase.Child(nameof(QuizModel)).OnceAsync<QuizModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(b => b.Object.QuizType == "identification").Select(f => new QuizModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                Instructions = f.Object.Instructions,
                QuizCode = f.Object.QuizCode,
                QuizType = f.Object.QuizType,
                Section = f.Object.Section,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<QuizModel>> GetMultipleChoiceQuizzes()
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

            return (await firebase.Child(nameof(QuizModel)).OnceAsync<QuizModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(b => b.Object.QuizType == "multiplechoice").Select(f => new QuizModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                Instructions= f.Object.Instructions,
                QuizCode = f.Object.QuizCode,
                QuizType = f.Object.QuizType,
                Section = f.Object.Section,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<QuizModel>> GetSetASetBQuizzes()
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

            return (await firebase.Child(nameof(QuizModel)).OnceAsync<QuizModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(b => b.Object.QuizType == "setasetb").Select(f => new QuizModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                Instructions= f.Object.Instructions,
                QuizCode = f.Object.QuizCode,
                QuizType = f.Object.QuizType,
                Section = f.Object.Section,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<QuizModel>> GetTrueOrFalseQuizzes()
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

            return (await firebase.Child(nameof(QuizModel)).OnceAsync<QuizModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(b => b.Object.QuizType == "trueorfalse").Select(f => new QuizModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                Instructions= f.Object.Instructions,
                QuizCode = f.Object.QuizCode,
                QuizType = f.Object.QuizType,
                Section = f.Object.Section,
                Key = f.Key
            }).ToList();
        }
    }
}
