using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using System.Threading.Tasks;

namespace ESWIPE.Interfaces
{
    public interface ITestRegister
    {
        Task<bool> AddorUpdateTeacher(TestRegister teacher);
        Task<bool> DeleteTeacher(string key);
        Task<List<TestRegister>> GetAllTeacher();
    }
}
