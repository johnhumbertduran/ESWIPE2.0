using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using ESWIPE.Models;

namespace ESWIPE.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> AddOrUpdateEmployee(EmployeeModel employeeModel);
        Task<bool> DeleteEmployee(string key);
        Task<List<EmployeeModel>> GetAllEmployee();
    }
}
