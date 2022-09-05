 using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ESWIPE.Services.Implementations
{
    public class AnswerService : IAnswerService
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
        public async Task<bool> AddorUpdateAnswer(AnswerModel answerModel)
        {
            if (!string.IsNullOrWhiteSpace(answerModel.Key))
            {
                try
                {
                    await firebase.Child(nameof(AnswerModel)).Child(answerModel.Key).PutAsync(answerModel);
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error:{ex}");
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
                
                if (Preferences.ContainsKey("essayQuestion"))
                {
                    QuizQuestion = Preferences.Get("essayQuestion", "essayQuestionValue");
                }

                if (Preferences.ContainsKey("identificationQuestion"))
                {
                    QuizQuestion = Preferences.Get("identificationQuestion", "identificationQuestionValue");
                }

                if (Preferences.ContainsKey("multipleChoiceQuestion"))
                {
                    QuizQuestion = Preferences.Get("multipleChoiceQuestion", "multipleChoiceQuestionValue");
                }

                if (Preferences.ContainsKey("setASetBQuestion"))
                {
                    QuizQuestion = Preferences.Get("setASetBQuestion", "setASetBQuestionValue");
                }
                
                if (Preferences.ContainsKey("trueOrFalseQuestion"))
                {
                    QuizQuestion = Preferences.Get("trueOrFalseQuestion", "trueOrFalseQuestionValue");
                }

                //if (Preferences.ContainsKey("setASetBCode"))
                //{
                //    QuizCode = Preferences.Get("setASetBCode", "setASetBCodeValue");

                //    if (Preferences.ContainsKey("setACode"))
                //    {
                //        Sets = "SetA";
                //    }

                //    if (Preferences.ContainsKey("setBCode"))
                //    {
                //        Sets = "SetB";
                //    }

                //    answerModel.Set = Sets;

                //}





                var delay = TimeSpan.FromMinutes(480);
                var date_now = DateTime.UtcNow + delay;

                answerModel.DateCreated = date_now.ToString();
                answerModel.CreatedBy = TeacherName;
                answerModel.Quarters = Quarters;
                answerModel.Section = Section;
                answerModel.QuizType = QuizTypes;
                answerModel.QuizCode = QuizCode;
                answerModel.Question= QuizQuestion;

                var response = await firebase.Child(nameof(AnswerModel)).PostAsync(answerModel);
                

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
        
        // Not Correct Answer for Multiple Choice
        public async Task<bool> AddorUpdateNotCorrectAnswer(AnswerModel answerModel)
        {
            if (!string.IsNullOrWhiteSpace(answerModel.Key))
            {
                try
                {
                    await firebase.Child(nameof(AnswerModel)).Child(answerModel.Key).PutAsync(answerModel);
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error:{ex}");
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
                
                if (Preferences.ContainsKey("essayQuestion"))
                {
                    QuizQuestion = Preferences.Get("essayQuestion", "essayQuestionValue");
                }

                if (Preferences.ContainsKey("identificationQuestion"))
                {
                    QuizQuestion = Preferences.Get("identificationQuestion", "identificationQuestionValue");
                }

                if (Preferences.ContainsKey("multipleChoiceQuestion"))
                {
                    QuizQuestion = Preferences.Get("multipleChoiceQuestion", "multipleChoiceQuestionValue");
                }

                if (Preferences.ContainsKey("setASetBQuestion"))
                {
                    QuizQuestion = Preferences.Get("setASetBQuestion", "setASetBQuestionValue");
                }
                
                if (Preferences.ContainsKey("trueOrFalseQuestion"))
                {
                    QuizQuestion = Preferences.Get("trueOrFalseQuestion", "trueOrFalseQuestionValue");
                }

                var delay = TimeSpan.FromMinutes(480);
                var date_now = DateTime.UtcNow + delay;

                answerModel.DateCreated = date_now.ToString();
                answerModel.CreatedBy = TeacherName;
                answerModel.Quarters = Quarters;
                answerModel.Section = Section;
                answerModel.QuizType = QuizTypes;
                answerModel.QuizCode = QuizCode;
                answerModel.Question= QuizQuestion;

                var response = await firebase.Child(nameof(AnswerModel)).PostAsync(answerModel);
                

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

        public async Task<bool> DeleteAnswer(string key)
        {
            try
            {
                await firebase.Child(nameof(AnswerModel)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error:{ex}");
                return false;
            }
        }

        public async Task<List<AnswerModel>> GetAllAnswer()
        {
            return (await firebase.Child(nameof(AnswerModel)).OnceAsync<AnswerModel>()).Select(f => new AnswerModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Answer = f.Object.Answer,
                CorrectAnswer = f.Object.CorrectAnswer,
                Key = f.Key
            }).ToList();
        }
        
        public async Task<List<AnswerModel>> GetAnswerForStudent()
        {
            if (Preferences.ContainsKey("Key"))
            {
                Key = Preferences.Get("Key", "KeyValue");
            }

            if (Preferences.ContainsKey("Username"))
            {
                UserName = Preferences.Get("Username", "UsernameValue");
            }

            if (Preferences.ContainsKey("StudentName"))
            {
                StudentName = Preferences.Get("StudentName", "StudentNameValue");
            }

            if (Preferences.ContainsKey("MyTeacher"))
            {
                Teacher = Preferences.Get("MyTeacher", "TeacherValue");
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

            if (Preferences.ContainsKey("StudentQuizCode"))
            {
                StudentQuizCode = Preferences.Get("StudentQuizCode", "StudentQuizCodeValue");
            }
            
            if (Preferences.ContainsKey("StudentQuestion"))
            {
                StudentQuestion = Preferences.Get("StudentQuestion", "StudentQuestionValue");
            }

            return (await firebase.Child(nameof(AnswerModel)).OnceAsync<AnswerModel>()).Where(a => a.Object.CreatedBy == Teacher).Where(b => b.Object.Section == Section).Where(b => b.Object.QuizCode == StudentQuizCode).Where(b => b.Object.Quarters == Quarters).Where(b => b.Object.Question == StudentQuestion).Select(f => new AnswerModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Answer = f.Object.Answer,
                CorrectAnswer = f.Object.CorrectAnswer,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<AnswerModel>> GetEssayAnswer()
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

            if (Preferences.ContainsKey("essayQuestion"))
            {
                QuizQuestion = Preferences.Get("essayQuestion", "essayQuestionValue");
            }

            return (await firebase.Child(nameof(AnswerModel)).OnceAsync<AnswerModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(b => b.Object.QuizType == "essay").Where(b => b.Object.QuizCode == QuizCode).Where(b => b.Object.Question == QuizQuestion).Select(f => new AnswerModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Answer = f.Object.Answer,
                CorrectAnswer = f.Object.CorrectAnswer,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<AnswerModel>> GetIdentificationAnswer()
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

            if (Preferences.ContainsKey("identificationCode"))
            {
                QuizCode = Preferences.Get("identificationCode", "identificationCodeValue");
            }

            if (Preferences.ContainsKey("identificationQuestion"))
            {
                QuizQuestion = Preferences.Get("identificationQuestion", "identificationQuestionValue");
            }

            return (await firebase.Child(nameof(AnswerModel)).OnceAsync<AnswerModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(a => a.Object.Quarters == Quarters).Where(b => b.Object.QuizType == "identification").Where(b => b.Object.QuizCode == QuizCode).Where(b => b.Object.Question == QuizQuestion).Select(f => new AnswerModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Answer = f.Object.Answer,
                CorrectAnswer = f.Object.CorrectAnswer,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<AnswerModel>> GetMultipleChoiceAnswer()
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

            if (Preferences.ContainsKey("multipleChoiceCode"))
            {
                QuizCode = Preferences.Get("multipleChoiceCode", "multipleChoiceCodeValue");
            }

            if (Preferences.ContainsKey("multipleChoiceQuestion"))
            {
                QuizQuestion = Preferences.Get("multipleChoiceQuestion", "multipleChoiceQuestionValue");
            }

            return (await firebase.Child(nameof(AnswerModel)).OnceAsync<AnswerModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(a => a.Object.Quarters == Quarters).Where(b => b.Object.QuizType == "multiplechoice").Where(b => b.Object.QuizCode == QuizCode).Where(b => b.Object.Question == QuizQuestion).Where(b => b.Object.Answer != null).Select(f => new AnswerModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Answer = f.Object.Answer,
                CorrectAnswer = f.Object.CorrectAnswer,
                Key = f.Key
            }).ToList();
        }
        
        public async Task<List<AnswerModel>> GetMultipleChoiceCorrectAnswer()
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

            if (Preferences.ContainsKey("multipleChoiceCode"))
            {
                QuizCode = Preferences.Get("multipleChoiceCode", "multipleChoiceCodeValue");
            }

            if (Preferences.ContainsKey("multipleChoiceQuestion"))
            {
                QuizQuestion = Preferences.Get("multipleChoiceQuestion", "multipleChoiceQuestionValue");
            }

            return (await firebase.Child(nameof(AnswerModel)).OnceAsync<AnswerModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(a => a.Object.Quarters == Quarters).Where(b => b.Object.QuizType == "multiplechoice").Where(b => b.Object.QuizCode == QuizCode).Where(b => b.Object.Question == QuizQuestion).Where(b => b.Object.CorrectAnswer != null).Select(f => new AnswerModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Answer = f.Object.Answer,
                CorrectAnswer = f.Object.CorrectAnswer,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<AnswerModel>> GetSetASetBAnswer()
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

            if (Preferences.ContainsKey("setASetBCode"))
            {
                QuizCode = Preferences.Get("setASetBCode", "setASetBCodeValue");
            }

            if (Preferences.ContainsKey("setASetBQuestion"))
            {
                QuizQuestion = Preferences.Get("setASetBQuestion", "setASetBQuestionValue");
            }

            return (await firebase.Child(nameof(AnswerModel)).OnceAsync<AnswerModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(a => a.Object.Quarters == Quarters).Where(b => b.Object.QuizType == "setasetb").Where(b => b.Object.QuizCode == QuizCode).Where(b => b.Object.Question == QuizQuestion).Select(f => new AnswerModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Answer = f.Object.Answer,
                CorrectAnswer = f.Object.CorrectAnswer,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<AnswerModel>> GetTrueOrFalseAnswer()
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

            if (Preferences.ContainsKey("trueOrFalseCode"))
            {
                QuizCode = Preferences.Get("trueOrFalseCode", "trueOrFalseCodeValue");
            }

            if (Preferences.ContainsKey("trueOrFalseQuestion"))
            {
                QuizQuestion = Preferences.Get("trueOrFalseQuestion", "trueOrFalseQuestionValue");
            }

            return (await firebase.Child(nameof(AnswerModel)).OnceAsync<AnswerModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(a => a.Object.Quarters == Quarters).Where(b => b.Object.QuizType == "trueorfalse").Where(b => b.Object.QuizCode == QuizCode).Where(b => b.Object.Question == QuizQuestion).Select(f => new AnswerModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Answer = f.Object.Answer,
                CorrectAnswer = f.Object.CorrectAnswer,
                Key = f.Key
            }).ToList();
        }
    }
}
