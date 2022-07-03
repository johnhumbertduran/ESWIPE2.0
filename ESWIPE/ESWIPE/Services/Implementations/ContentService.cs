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
    public class ContentService : IContentService
    {
        readonly FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
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

        public Task<bool> AddorUpdateContent(ContentModel contentModel)
        {
            throw new NotImplementedException();
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

            if (Preferences.ContainsKey("quarter1pass"))
            {
                QuarterSelect = "quarter1";
            }

            if (Preferences.ContainsKey("quarter2pass"))
            {
                QuarterSelect = "quarter2";
            }

            if (Preferences.ContainsKey("quarter3pass"))
            {
                QuarterSelect = "quarter3";
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
                Where(d => d.Object.Title == MTitle).
                Select(f => new ContentModel
                {
                    DateCreated = f.Object.DateCreated,
                    CreatedBy = f.Object.CreatedBy,
                    Quarter = f.Object.Quarter,
                    Title = f.Object.Title,
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
                Where(d => d.Object.Title == MTitle).
                Select(f => new ContentModel
                {
                    DateCreated = f.Object.DateCreated,
                    CreatedBy = f.Object.CreatedBy,
                    Quarter = f.Object.Quarter,
                    Title = f.Object.Title,
                    TitleContent = f.Object.TitleContent,
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

            if (Preferences.ContainsKey("quarter1pass"))
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
                Where(d => d.Object.Title == MTitle).
                Select(f => new ContentModel
                {
                    DateCreated = f.Object.DateCreated,
                    CreatedBy = f.Object.CreatedBy,
                    Quarter = f.Object.Quarter,
                    Title = f.Object.Title,
                    TitleContent = f.Object.TitleContent,
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

            if (Preferences.ContainsKey("quarter1pass"))
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
                Where(d => d.Object.Title == MTitle).
                Select(f => new ContentModel
                {
                    DateCreated = f.Object.DateCreated,
                    CreatedBy = f.Object.CreatedBy,
                    Quarter = f.Object.Quarter,
                    Title = f.Object.Title,
                    TitleContent = f.Object.TitleContent,
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

            if (Preferences.ContainsKey("quarter1pass"))
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
                Where(d => d.Object.Title == MTitle).
                Select(f => new ContentModel
                {
                    DateCreated = f.Object.DateCreated,
                    CreatedBy = f.Object.CreatedBy,
                    Quarter = f.Object.Quarter,
                    Title = f.Object.Title,
                    TitleContent = f.Object.TitleContent,
                    SubjectCode = f.Object.SubjectCode,
                    Key = f.Key
                }).ToList();
        }
    }
}
