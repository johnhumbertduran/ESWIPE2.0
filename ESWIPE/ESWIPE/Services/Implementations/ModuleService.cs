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

            if (Preferences.ContainsKey("Key", ""))
            {
                Key = Preferences.Get("Key", "Key");
            }

            if (Preferences.ContainsKey("Username", ""))
            {
                UserName = Preferences.Get("Username", "Username");
            }

            if (Preferences.ContainsKey("TeacherName", ""))
            {
                TeacherName = Preferences.Get("TeacherName", "TeacherName");
            }

            if (Preferences.ContainsKey("Section", ""))
            {
                Section = Preferences.Get("Section", "Section");
            }

            if (!string.IsNullOrWhiteSpace(moduleModel.Key))
            {
                try
                {
                    if (Preferences.ContainsKey("quarter1", ""))
                    {
                        Q1 = Preferences.Get("quarter1", "quarter1");
                        moduleModel.Quarter = Q1;
                    }
                    
                    if (Preferences.ContainsKey("quarter2", ""))
                    {
                        Q2 = Preferences.Get("quarter2", "quarter2");
                        moduleModel.Quarter = Q2;
                    }

                    if (Preferences.ContainsKey("quarter3", ""))
                    {
                        Q3 = Preferences.Get("quarter3", "quarter3");
                        moduleModel.Quarter = Q3;
                    }

                    if (Preferences.ContainsKey("quarter4", ""))
                    {
                        Q4 = Preferences.Get("quarter4", "quarter4");
                        moduleModel.Quarter = Q4;
                    }

                    await firebase.Child(nameof(ModuleModel)).Child(moduleModel.Key).PutAsync(moduleModel);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                var delay = TimeSpan.FromMinutes(480);
                var date_now = DateTime.UtcNow + delay;

                moduleModel.DateCreated = date_now.ToString();
                moduleModel.CreatedBy = TeacherName;

                if (Preferences.ContainsKey("quarter1", ""))
                {
                    Q1 = Preferences.Get("quarter1", "quarter1");
                    moduleModel.Quarter = Q1;
                    //moduleModel.ActiveQuarter = "Q1";
                }
                
                if (Preferences.ContainsKey("quarter2", ""))
                {
                    Q2 = Preferences.Get("quarter2", "quarter2");
                    moduleModel.Quarter = Q2;
                    //moduleModel.ActiveQuarter = "Q2";
                }
                
                if (Preferences.ContainsKey("quarter3", ""))
                {
                    Q3 = Preferences.Get("quarter3", "quarter3");
                    moduleModel.Quarter = Q3;
                    //moduleModel.ActiveQuarter = "Q3";
                }
                
                if (Preferences.ContainsKey("quarter4", ""))
                {
                    Q4 = Preferences.Get("quarter4", "quarter4");
                    moduleModel.Quarter = Q4;
                    //moduleModel.ActiveQuarter = "Q4";
                }

                var response = await firebase.Child(nameof(ModuleModel)).PostAsync(moduleModel);
                //moduleModel.Key = response.Key;
                //await firebase.Child(nameof(ModuleModel)).Child(moduleModel.Key).PutAsync(moduleModel);

                if (response.Key != null)
                {
                    if (Preferences.ContainsKey("quarter1", ""))
                    {
                        Preferences.Remove("quarter1", "quarter1");
                    }

                    if (Preferences.ContainsKey("quarter2", ""))
                    {
                        Preferences.Remove("quarter2", "quarter2");
                    }

                    if (Preferences.ContainsKey("quarter3", ""))
                    {
                        Preferences.Remove("quarter3", "quarter3");
                    }

                    if (Preferences.ContainsKey("quarter4", ""))
                    {
                        Preferences.Remove("quarter4", "quarter4");
                    }

                    return true;
                }
                else
                {
                    if (Preferences.ContainsKey("quarter1", ""))
                    {
                        Preferences.Remove("quarter1", "quarter1");
                    }

                    if (Preferences.ContainsKey("quarter2", ""))
                    {
                        Preferences.Remove("quarter2", "quarter2");
                    }

                    if (Preferences.ContainsKey("quarter3", ""))
                    {
                        Preferences.Remove("quarter3", "quarter3");
                    }

                    if (Preferences.ContainsKey("quarter4", ""))
                    {
                        Preferences.Remove("quarter4", "quarter4");
                    }

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
                return false;
            }
        }

        public async Task<List<ModuleModel>> GetAllModule()
        {
            if (Preferences.ContainsKey("Key", ""))
            {
                Key = Preferences.Get("Key", "Key");
            }

            if (Preferences.ContainsKey("Username", ""))
            {
                UserName = Preferences.Get("Username", "Username");
            }

            if (Preferences.ContainsKey("TeacherName", ""))
            {
                TeacherName = Preferences.Get("TeacherName", "TeacherName");
            }

            if (Preferences.ContainsKey("Section", ""))
            {
                Section = Preferences.Get("Section", "Section");
            }

            return (await firebase.Child(nameof(ModuleModel)).OnceAsync<ModuleModel>()).Where(a => a.Object.CreatedBy == TeacherName).Select(f => new ModuleModel
            {
                DateCreated = f.Object.DateCreated,
                CreatedBy = f.Object.CreatedBy,
                SubjectCode = f.Object.SubjectCode,
                SubjectQuizCode = f.Object.SubjectQuizCode,
                SubjectQuizQty = f.Object.SubjectQuizQty,
                Key = f.Key
            }).ToList();
        }
    }
}
