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
    public class ModuleService : IModuleService
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

        public async Task<bool> AddorUpdateModule(ModuleModel moduleModel)
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

            if (!string.IsNullOrWhiteSpace(moduleModel.Key))
            {
                try
                {
                    if (Preferences.ContainsKey("quarter1pass"))
                    {
                        Q1 = "quarter1";
                        moduleModel.Quarter = Q1;
                    }
                    
                    if (Preferences.ContainsKey("quarter2pass"))
                    {
                        Q2 = "quarter2";
                        moduleModel.Quarter = Q2;
                    }

                    if (Preferences.ContainsKey("quarter3pass"))
                    {
                        Q3 = "quarter3";
                        moduleModel.Quarter = Q3;
                    }

                    if (Preferences.ContainsKey("quarter4pass"))
                    {
                        Q4 = "quarter4";
                        moduleModel.Quarter = Q4;
                    }

                    await firebase.Child(nameof(ModuleModel)).Child(moduleModel.Key).PutAsync(moduleModel);
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

                moduleModel.DateCreated = date_now.ToString();
                moduleModel.CreatedBy = TeacherName;

                if (Preferences.ContainsKey("quarter1pass"))
                {
                    Q1 = "quarter1";
                    moduleModel.Quarter = Q1;
                }
                
                if (Preferences.ContainsKey("quarter2pass"))
                {
                    Q2 = "quarter2";
                    moduleModel.Quarter = Q2;
                }
                
                if (Preferences.ContainsKey("quarter3pass"))
                {
                    Q3 = "quarter3";
                    moduleModel.Quarter = Q3;
                }
                
                if (Preferences.ContainsKey("quarter4pass"))
                {
                    Q4 = "quarter4";
                    moduleModel.Quarter = Q4;
                }

                var response = await firebase.Child(nameof(ModuleModel)).PostAsync(moduleModel);
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

        public async Task<bool> DeleteModule(string key)
        {
            try
            {
                await firebase.Child(nameof(ModuleModel)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error:{ex}");
                return false;
            }
        }

        public async Task<List<ModuleModel>> GetAllModule()
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

            return (await firebase.Child(nameof(ModuleModel)).OnceAsync<ModuleModel>()).Where(a => a.Object.CreatedBy == TeacherName).Select(f => new ModuleModel
            {
                DateCreated = f.Object.DateCreated,
                CreatedBy = f.Object.CreatedBy,
                SubjectCode = f.Object.SubjectCode,
                //SubjectQuizCode = f.Object.SubjectQuizCode,
                //SubjectQuizQty = f.Object.SubjectQuizQty,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<ModuleModel>> GetAllModuleForAdmin()
        {
            return (await firebase.Child(nameof(ModuleModel)).OnceAsync<ModuleModel>()).Select(f => new ModuleModel
            {
                DateCreated = f.Object.DateCreated,
                CreatedBy = f.Object.CreatedBy,
                SubjectCode = f.Object.SubjectCode,
                //SubjectQuizCode = f.Object.SubjectQuizCode,
                //SubjectQuizQty = f.Object.SubjectQuizQty,
                Key = f.Key
            }).ToList();
        }
    }
}
