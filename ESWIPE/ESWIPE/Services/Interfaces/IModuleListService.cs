using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using System.Threading.Tasks;

namespace ESWIPE.Services.Interfaces
{
    public interface IModuleListService
    {
        Task<bool> AddorUpdateModuleList(ModuleListModel moduleListModel);
        Task<bool> DeleteModuleList(string key);
        Task<List<ModuleListModel>> GetAllModuleList();
    }
}
