using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ESWIPE.Services.Implementations
{
    public class QuestionService : IQuestionService
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
        public string QuizCode;

        public async Task<bool> AddorUpdateQuestion(QuestionModel questionModel)
        {


            if (!string.IsNullOrWhiteSpace(questionModel.Key))
            {
                try
                {
                    await firebase.Child(nameof(QuestionModel)).Child(questionModel.Key).PutAsync(questionModel);
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
                
                if (Preferences.ContainsKey("essayCode"))
                {
                    QuizCode = Preferences.Get("essayCode", "essayCodeValue");
                }

                if (Preferences.ContainsKey("identificationCode"))
                {
                    QuizCode = Preferences.Get("identificationCode", "identificationCodeValue");
                }
                
                if (Preferences.ContainsKey("multipleChoiceCode"))
                {
                    QuizCode = Preferences.Get("multipleChoiceCode", "multipleChoiceCodeValue");
                }

                if (Preferences.ContainsKey("setASetBCode"))
                {
                    QuizCode = Preferences.Get("setASetBCode", "setASetBCodeValue");
                }
                
                if (Preferences.ContainsKey("trueOrFalseCode"))
                {
                    QuizCode = Preferences.Get("trueOrFalseCode", "trueOrFalseCodeValue");
                }

                var delay = TimeSpan.FromMinutes(480);
                var date_now = DateTime.UtcNow + delay;

                questionModel.DateCreated = date_now.ToString();
                questionModel.CreatedBy = TeacherName;
                questionModel.Quarters = Quarters;
                questionModel.Section = Section;
                questionModel.QuizType = QuizTypes;
                questionModel.QuizCode = QuizCode;
                var response = await firebase.Child(nameof(QuestionModel)).PostAsync(questionModel);
                //quizModel.Key = response.Key;
                //await firebase.Child(nameof(QuizModel)).Child(quizModel.Key).PutAsync(quizModel);

                //if (Preferences.ContainsKey("essayCode"))
                //{
                //    Preferences.Remove("essayCode");
                //}

                //if (Preferences.ContainsKey("identificationCode"))
                //{
                //    Preferences.Remove("identificationCode");
                //}
                
                //if (Preferences.ContainsKey("multipleChoiceCode"))
                //{
                //    Preferences.Remove("multipleChoiceCode");
                //}
                
                //if (Preferences.ContainsKey("setASetBCode"))
                //{
                //    Preferences.Remove("setASetBCode");
                //}
                
                //if (Preferences.ContainsKey("trueOrFalseCode"))
                //{
                //    Preferences.Remove("trueOrFalseCode");
                //}

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

        public async Task<bool> DeleteQuestion(string key)
        {
            try
            {
                await firebase.Child(nameof(QuestionModel)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<QuestionModel>> GetAllQuestion()
        {
            return (await firebase.Child(nameof(QuestionModel)).OnceAsync<QuestionModel>()).Select(f => new QuestionModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                Question = f.Object.Question,
                QuizCode = f.Object.QuizCode,
                QuizType = f.Object.QuizType,
                Section = f.Object.Section,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<QuestionModel>> GetEssayQuestions()
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

            if (Preferences.ContainsKey("essayCode"))
            {
                QuizCode = Preferences.Get("essayCode", "essayCodeValue");
            }

            return (await firebase.Child(nameof(QuestionModel)).OnceAsync<QuestionModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(b => b.Object.QuizType == "essay").Where(b => b.Object.QuizCode == QuizCode).Select(f => new QuestionModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                Question = f.Object.Question,
                QuizCode = f.Object.QuizCode,
                QuizType = f.Object.QuizType,
                Section = f.Object.Section,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<QuestionModel>> GetIdentificationQuestions()
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

            if (Preferences.ContainsKey("identificationCode"))
            {
                QuizCode = Preferences.Get("identificationCode", "identificationCodeValue");
            }

            return (await firebase.Child(nameof(QuestionModel)).OnceAsync<QuestionModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(b => b.Object.QuizType == "identification").Where(b => b.Object.QuizCode == QuizCode).Select(f => new QuestionModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                Question = f.Object.Question,
                QuizCode = f.Object.QuizCode,
                QuizType = f.Object.QuizType,
                Section = f.Object.Section,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<QuestionModel>> GetMultipleChoiceQuestions()
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

            if (Preferences.ContainsKey("multipleChoiceCode"))
            {
                QuizCode = Preferences.Get("multipleChoiceCode", "multipleChoiceCodeValue");
            }

            return (await firebase.Child(nameof(QuestionModel)).OnceAsync<QuestionModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(b => b.Object.QuizType == "multiplechoice").Where(b => b.Object.QuizCode == QuizCode).Select(f => new QuestionModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                Question = f.Object.Question,
                QuizCode = f.Object.QuizCode,
                QuizType = f.Object.QuizType,
                Section = f.Object.Section,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<QuestionModel>> GetSetASetBQuestions()
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

            if (Preferences.ContainsKey("setASetBCode"))
            {
                QuizCode = Preferences.Get("setASetBCode", "setASetBCodeValue");
            }

            return (await firebase.Child(nameof(QuestionModel)).OnceAsync<QuestionModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(b => b.Object.QuizType == "setasetb").Where(b => b.Object.QuizCode == QuizCode).Select(f => new QuestionModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                Question = f.Object.Question,
                QuizCode = f.Object.QuizCode,
                QuizType = f.Object.QuizType,
                Section = f.Object.Section,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<QuestionModel>> GetTrueOrFalseQuestions()
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

            if (Preferences.ContainsKey("trueOrFalseCode"))
            {
                QuizCode = Preferences.Get("trueOrFalseCode", "trueOrFalseCodeValue");
            }

            return (await firebase.Child(nameof(QuestionModel)).OnceAsync<QuestionModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(b => b.Object.QuizType == "trueorfalse").Where(b => b.Object.QuizCode == QuizCode).Select(f => new QuestionModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                Question = f.Object.Question,
                QuizCode = f.Object.QuizCode,
                QuizType = f.Object.QuizType,
                Section = f.Object.Section,
                Key = f.Key
            }).ToList();
        }
    }
}
