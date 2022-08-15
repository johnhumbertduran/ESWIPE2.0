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
using System.Diagnostics;

namespace ESWIPE.Services.Implementations
{
    public class ModuleListService : IModuleListService
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

        public async Task<bool> AddorUpdateModuleList(ModuleListModel moduleListModel)
        {

            if (!string.IsNullOrWhiteSpace(moduleListModel.Key))
            {
                try
                {

                    await firebase.Child(nameof(ModuleListModel)).Child(moduleListModel.Key).PutAsync(moduleListModel);
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

                moduleListModel.DateCreated = date_now.ToString();
                moduleListModel.CreatedBy = TeacherName;

                if (Preferences.ContainsKey("quarter1pass"))
                {
                    Q1 = "quarter1";
                    moduleListModel.Quarter = Q1;
                }

                if (Preferences.ContainsKey("quarter2pass"))
                {
                    Q2 = "quarter2";
                    moduleListModel.Quarter = Q2;
                }

                if (Preferences.ContainsKey("quarter3pass"))
                {
                    Q3 = "quarter3";
                    moduleListModel.Quarter = Q3;
                }

                if (Preferences.ContainsKey("quarter4pass"))
                {
                    Q4 = "quarter4";
                    moduleListModel.Quarter = Q4;
                }
                
                if (Preferences.ContainsKey("SubjectCode"))
                {
                    var SubjectCodePrep = Preferences.Get("SubjectCode", "SubjectCodeValue");
                    moduleListModel.SubjectCode = SubjectCodePrep;
                }

                var response = await firebase.Child(nameof(ModuleListModel)).PostAsync(moduleListModel);
                //moduleModel.Key = response.Key;
                //await firebase.Child(nameof(ModuleModel)).Child(moduleModel.Key).PutAsync(moduleModel);

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

        public async Task<bool> DeleteModuleList(string key)
        {
            try
            {
                await firebase.Child(nameof(ModuleListModel)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error:{ex}");
                return false;
            }
        }

        public async Task<List<ModuleListModel>> GetAllModuleList()
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

            return (await firebase.Child(nameof(ModuleListModel)).OnceAsync<ModuleListModel>()).
                Where(a => a.Object.CreatedBy == TeacherName).
                Where(b => b.Object.Quarter == QuarterSelect).
                Where(c => c.Object.SubjectCode == SCode).
                Select(f => new ModuleListModel
            {
                DateCreated = f.Object.DateCreated,
                CreatedBy = f.Object.CreatedBy,
                Quarter = f.Object.Quarter,
                Title = f.Object.Title,
                SubjectCode = f.Object.SubjectCode,
                Key = f.Key
            }).ToList();
        }


        public async Task<List<ModuleListModel>> GetAllModuleListQ1()
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

            return (await firebase.Child(nameof(ModuleListModel)).OnceAsync<ModuleListModel>()).
                Where(a => a.Object.CreatedBy == TeacherName).
                Where(b => b.Object.Quarter == QuarterSelect).
                Where(c => c.Object.SubjectCode == SCode).
                Select(f => new ModuleListModel
            {
                DateCreated = f.Object.DateCreated,
                CreatedBy = f.Object.CreatedBy,
                Quarter = f.Object.Quarter,
                Title = f.Object.Title,
                SubjectCode = f.Object.SubjectCode,
                Key = f.Key
            }).ToList();
        }

        

        public async Task<List<ModuleListModel>> GetAllModuleListQ2()
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

            return (await firebase.Child(nameof(ModuleListModel)).OnceAsync<ModuleListModel>()).
                Where(a => a.Object.CreatedBy == TeacherName).
                Where(b => b.Object.Quarter == QuarterSelect).
                Where(c => c.Object.SubjectCode == SCode).
                Select(f => new ModuleListModel
            {
                DateCreated = f.Object.DateCreated,
                CreatedBy = f.Object.CreatedBy,
                Quarter = f.Object.Quarter,
                Title = f.Object.Title,
                SubjectCode = f.Object.SubjectCode,
                Key = f.Key
            }).ToList();
        }

        

        public async Task<List<ModuleListModel>> GetAllModuleListQ3()
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

            return (await firebase.Child(nameof(ModuleListModel)).OnceAsync<ModuleListModel>()).
                Where(a => a.Object.CreatedBy == TeacherName).
                Where(b => b.Object.Quarter == QuarterSelect).
                Where(c => c.Object.SubjectCode == SCode).
                Select(f => new ModuleListModel
            {
                DateCreated = f.Object.DateCreated,
                CreatedBy = f.Object.CreatedBy,
                Quarter = f.Object.Quarter,
                Title = f.Object.Title,
                SubjectCode = f.Object.SubjectCode,
                Key = f.Key
            }).ToList();
        }

        

        public async Task<List<ModuleListModel>> GetAllModuleListQ4()
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

            return (await firebase.Child(nameof(ModuleListModel)).OnceAsync<ModuleListModel>()).
                Where(a => a.Object.CreatedBy == TeacherName).
                Where(b => b.Object.Quarter == QuarterSelect).
                Where(c => c.Object.SubjectCode == SCode).
                Select(f => new ModuleListModel
            {
                DateCreated = f.Object.DateCreated,
                CreatedBy = f.Object.CreatedBy,
                Quarter = f.Object.Quarter,
                Title = f.Object.Title,
                SubjectCode = f.Object.SubjectCode,
                Key = f.Key
            }).ToList();
        }


    }
}
