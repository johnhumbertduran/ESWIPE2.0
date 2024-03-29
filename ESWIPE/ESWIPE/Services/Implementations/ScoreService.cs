﻿using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
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

        public async Task<bool> AddorUpdateScore(ScoreModel scoreModel)
        {
            if (!string.IsNullOrWhiteSpace(scoreModel.Key))
            {
                try
                {
                    await firebase.Child(nameof(ScoreModel)).Child(scoreModel.Key).PutAsync(scoreModel);
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

                scoreModel.DateCreated = date_now.ToString();
                scoreModel.CreatedBy = TeacherName;
                scoreModel.Quarters = Quarters;
                scoreModel.Section = Section;
                scoreModel.QuizType = QuizTypes;
                scoreModel.QuizCode = QuizCode;
                scoreModel.Question = QuizQuestion;

                var response = await firebase.Child(nameof(AnswerModel)).PostAsync(scoreModel);


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

        public async Task<bool> DeleteScore(string key)
        {
            try
            {
                await firebase.Child(nameof(ScoreModel)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error:{ex}");
                return false;
            }
        }

        public async Task<List<ScoreModel>> GetAllScore()
        {
            return (await firebase.Child(nameof(ScoreModel)).OnceAsync<ScoreModel>()).Select(f => new ScoreModel
            {
                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<ScoreModel>> GetScoreForStudent()
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

            return (await firebase.Child(nameof(ScoreModel)).OnceAsync<ScoreModel>()).Where(a => a.Object.CreatedBy == Teacher).Where(b => b.Object.Section == Section).Where(b => b.Object.QuizCode == StudentQuizCode).Where(b => b.Object.Quarters == Quarters).Where(b => b.Object.Question == StudentQuestion).Select(f => new ScoreModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<ScoreModel>> GetEssayScore()
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

            if (Preferences.ContainsKey("essayCode"))
            {
                QuizCode = Preferences.Get("essayCode", "essayCodeValue");
            }

            if (Preferences.ContainsKey("essayQuestion"))
            {
                QuizQuestion = Preferences.Get("essayQuestion", "essayQuestionValue");
            }

            return (await firebase.Child(nameof(ScoreModel)).OnceAsync<ScoreModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(b => b.Object.QuizType == "essay").Where(b => b.Object.QuizCode == QuizCode).Where(b => b.Object.Question == QuizQuestion).Select(f => new ScoreModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<ScoreModel>> GetIdentificationScore()
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

            return (await firebase.Child(nameof(ScoreModel)).OnceAsync<ScoreModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(a => a.Object.Quarters == Quarters).Where(b => b.Object.QuizType == "identification").Where(b => b.Object.QuizCode == QuizCode).Where(b => b.Object.Question == QuizQuestion).Select(f => new ScoreModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<ScoreModel>> GetMultipleChoiceScore()
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

            return (await firebase.Child(nameof(ScoreModel)).OnceAsync<ScoreModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(a => a.Object.Quarters == Quarters).Where(b => b.Object.QuizType == "multiplechoice").Where(b => b.Object.QuizCode == QuizCode).Where(b => b.Object.Question == QuizQuestion).Select(f => new ScoreModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Key = f.Key
            }).ToList();
        }

        public Task<List<ScoreModel>> GetScore()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ScoreModel>> GetSetASetBScore()
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

            return (await firebase.Child(nameof(ScoreModel)).OnceAsync<ScoreModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(a => a.Object.Quarters == Quarters).Where(b => b.Object.QuizType == "setasetb").Where(b => b.Object.QuizCode == QuizCode).Where(b => b.Object.Question == QuizQuestion).Select(f => new ScoreModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<ScoreModel>> GetTrueOrFalseScore()
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

            return (await firebase.Child(nameof(ScoreModel)).OnceAsync<ScoreModel>()).Where(a => a.Object.CreatedBy == TeacherName).Where(a => a.Object.Quarters == Quarters).Where(b => b.Object.QuizType == "trueorfalse").Where(b => b.Object.QuizCode == QuizCode).Where(b => b.Object.Question == QuizQuestion).Select(f => new ScoreModel
            {

                CreatedBy = f.Object.CreatedBy,
                DateCreated = f.Object.DateCreated,
                Quarters = f.Object.Quarters,
                QuizType = f.Object.QuizType,
                QuizCode = f.Object.QuizCode,
                Section = f.Object.Section,
                Question = f.Object.Question,
                Key = f.Key
            }).ToList();
        }
    }
}
