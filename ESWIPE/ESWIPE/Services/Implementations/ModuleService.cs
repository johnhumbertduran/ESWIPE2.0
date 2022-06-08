using System;
using System.Collections.Generic;
using System.Text;

using Firebase.Database;
using Firebase.Database.Query;
using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ESWIPE.Services.Implementations
{
    public class ModuleService : IModuleService
    {
        readonly FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        });

        public async Task<bool> AddorUpdateModule(ModuleModel moduleModel)
        {
            if (!string.IsNullOrWhiteSpace(moduleModel.Key))
            {
                try
                {
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
                var response = await firebase.Child(nameof(ModuleModel)).PostAsync(moduleModel);


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
                return false;
            }
        }

        public async Task<List<ModuleModel>> GetAllModule()
        {
            return (await firebase.Child(nameof(ModuleModel)).OnceAsync<ModuleModel>()).Select(f => new ModuleModel
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
