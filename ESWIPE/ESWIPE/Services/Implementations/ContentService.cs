using System;
using System.Collections.Generic;
using System.Text;

using Firebase.Database;
using Firebase.Database.Query;
using ESWIPE.Models;
using ESWIPE.Views;
using ESWIPE.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.IO;
using Syncfusion.DocIO.DLS;
using System.Diagnostics;

namespace ESWIPE.Services.Implementations
{
    public class ContentService : IContentService
    {
        readonly FirebaseClient firebase = new FirebaseClient(Models.Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Models.Settings.FireBaseSecret)
        });

        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;
        public string Q1;
        public string Q2;
        public string Q3;
        public string Q4;
        public string QuarterSelect;
        public string SCode;
        public string MTitle;

        public string RTE;




        public async Task<bool> AddorUpdateContent(ContentModel contentModel)
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

            if (!string.IsNullOrWhiteSpace(contentModel.Key))
            {
                try
                {
                    if (Preferences.ContainsKey("quarter1pass"))
                    {
                        Q1 = "quarter1";
                        contentModel.Quarter = Q1;
                    }

                    if (Preferences.ContainsKey("quarter2pass"))
                    {
                        Q2 = "quarter2";
                        contentModel.Quarter = Q2;
                    }

                    if (Preferences.ContainsKey("quarter3pass"))
                    {
                        Q3 = "quarter3";
                        contentModel.Quarter = Q3;
                    }

                    if (Preferences.ContainsKey("quarter4pass"))
                    {
                        Q4 = "quarter4";
                        contentModel.Quarter = Q4;
                    }

                    await firebase.Child(nameof(ContentModel)).Child(contentModel.Key).PutAsync(contentModel);
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
                var delay = TimeSpan.FromMinutes(480);
                var date_now = DateTime.UtcNow + delay;

                contentModel.DateCreated = date_now.ToString();
                contentModel.CreatedBy = TeacherName;

                if (Preferences.ContainsKey("quarter1pass"))
                {
                    Q1 = "quarter1";
                    contentModel.Quarter = Q1;
                }

                if (Preferences.ContainsKey("quarter2pass"))
                {
                    Q2 = "quarter2";
                    contentModel.Quarter = Q2;
                }

                if (Preferences.ContainsKey("quarter3pass"))
                {
                    Q3 = "quarter3";
                    contentModel.Quarter = Q3;
                }

                if (Preferences.ContainsKey("quarter4pass"))
                {
                    Q4 = "quarter4";
                    contentModel.Quarter = Q4;
                }

                if (Preferences.ContainsKey("SubjectCode"))
                {
                    var SubjectCodePrep = Preferences.Get("SubjectCode", "SubjectCodeValue");
                    contentModel.SubjectCode = SubjectCodePrep;
                }

                var response = await firebase.Child(nameof(ContentModel)).PostAsync(contentModel);

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

        public async Task<bool> DeleteContent(string key)
        {
            try
            {
                await firebase.Child(nameof(ContentModel)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error:{ex}");
                return false;
            }
        }

        public async Task<List<ContentModel>> GetAllContent()
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


            return (await firebase.Child(nameof(ContentModel)).OnceAsync<ContentModel>()).
                Select(f => new ContentModel
                {
                    DateCreated = f.Object.DateCreated,
                    CreatedBy = f.Object.CreatedBy,
                    Quarter = f.Object.Quarter,
                    //Title = f.Object.Title,
                    TitleContent = f.Object.TitleContent,
                    SubjectCode = f.Object.SubjectCode,
                    Key = f.Key
                }).ToList();

        }

        public async Task<List<ContentModel>> GetAllContentQ1()
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
                QuarterSelect = "quarter1";
            }

            if (Preferences.ContainsKey("SubjectCode"))
            {
                var SubjectCodePrep = Preferences.Get("SubjectCode", "SubjectCodeValue");
                SCode = SubjectCodePrep;
            }

            return (await firebase.Child(nameof(ContentModel)).OnceAsync<ContentModel>()).
                Where(a => a.Object.CreatedBy == TeacherName).
                Where(b => b.Object.Quarter == QuarterSelect).
                Where(c => c.Object.SubjectCode == SCode).
                Select(f => new ContentModel
                {
                    DateCreated = f.Object.DateCreated,
                    CreatedBy = f.Object.CreatedBy,
                    Quarter = f.Object.Quarter,
                    //Title = f.Object.Title,
                    SubjectCode = f.Object.SubjectCode,
                    Key = f.Key
                }).ToList();
        }

        public async Task<List<ContentModel>> GetAllContentQ2()
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

            if (Preferences.ContainsKey("quarter2pass"))
            {
                QuarterSelect = "quarter2";
            }

            if (Preferences.ContainsKey("SubjectCode"))
            {
                var SubjectCodePrep = Preferences.Get("SubjectCode", "SubjectCodeValue");
                SCode = SubjectCodePrep;
            }

            return (await firebase.Child(nameof(ContentModel)).OnceAsync<ContentModel>()).
                Where(a => a.Object.CreatedBy == TeacherName).
                Where(b => b.Object.Quarter == QuarterSelect).
                Where(c => c.Object.SubjectCode == SCode).
                Select(f => new ContentModel
                {
                    DateCreated = f.Object.DateCreated,
                    CreatedBy = f.Object.CreatedBy,
                    Quarter = f.Object.Quarter,
                    //Title = f.Object.Title,
                    SubjectCode = f.Object.SubjectCode,
                    Key = f.Key
                }).ToList();
        }

        public async Task<List<ContentModel>> GetAllContentQ3()
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

            if (Preferences.ContainsKey("quarter3pass"))
            {
                QuarterSelect = "quarter3";
            }

            if (Preferences.ContainsKey("SubjectCode"))
            {
                var SubjectCodePrep = Preferences.Get("SubjectCode", "SubjectCodeValue");
                SCode = SubjectCodePrep;
            }

            return (await firebase.Child(nameof(ContentModel)).OnceAsync<ContentModel>()).
                Where(a => a.Object.CreatedBy == TeacherName).
                Where(b => b.Object.Quarter == QuarterSelect).
                Where(c => c.Object.SubjectCode == SCode).
                Select(f => new ContentModel
                {
                    DateCreated = f.Object.DateCreated,
                    CreatedBy = f.Object.CreatedBy,
                    Quarter = f.Object.Quarter,
                    //Title = f.Object.Title,
                    SubjectCode = f.Object.SubjectCode,
                    Key = f.Key
                }).ToList();
        }

        public async Task<List<ContentModel>> GetAllContentQ4()
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

            if (Preferences.ContainsKey("quarter4pass"))
            {
                QuarterSelect = "quarter4";
            }

            if (Preferences.ContainsKey("SubjectCode"))
            {
                var SubjectCodePrep = Preferences.Get("SubjectCode", "SubjectCodeValue");
                SCode = SubjectCodePrep;
            }

            return (await firebase.Child(nameof(ContentModel)).OnceAsync<ContentModel>()).
                Where(a => a.Object.CreatedBy == TeacherName).
                Where(b => b.Object.Quarter == QuarterSelect).
                Where(c => c.Object.SubjectCode == SCode).
                Select(f => new ContentModel
                {
                    DateCreated = f.Object.DateCreated,
                    CreatedBy = f.Object.CreatedBy,
                    Quarter = f.Object.Quarter,
                    //Title = f.Object.Title,
                    SubjectCode = f.Object.SubjectCode,
                    Key = f.Key
                }).ToList();
        }
    }
}
