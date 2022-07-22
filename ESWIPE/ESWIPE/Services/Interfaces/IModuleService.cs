using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using System.Threading.Tasks;

namespace ESWIPE.Services.Interfaces
{
    public interface IModuleService
    {
        Task<bool> AddorUpdateModule(ModuleModel moduleModel);
        Task<bool> DeleteModule(string key);
        Task<List<ModuleModel>> GetAllModule();
        Task<List<ModuleModel>> GetAllModuleForAdmin();
    }
}
