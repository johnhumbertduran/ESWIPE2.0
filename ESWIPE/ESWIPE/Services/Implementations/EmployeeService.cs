using System;
using System.Collections.Generic;
using System.Text;

using Firebase.Database;
using Firebase.Database.Query;
using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ESWIPE.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        readonly FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        });

        public async Task<bool> AddOrUpdateEmployee(EmployeeModel employeeModel)
        {
            if (!string.IsNullOrWhiteSpace(employeeModel.Key))
            {
                try
                {
                    await firebase.Child(nameof(EmployeeModel)).Child(employeeModel.Key).PutAsync(employeeModel);
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
                var response = await firebase.Child(nameof(EmployeeModel)).PostAsync(employeeModel);
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

        public async Task<bool> DeleteEmployee(string key)
        {
            try
            {
                await firebase.Child(nameof(EmployeeModel)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error:{ex}");
                return false;
            }
        }

        public async Task<List<EmployeeModel>> GetAllEmployee()
        {
            return (await firebase.Child(nameof(EmployeeModel)).OnceAsync<EmployeeModel>()).Select(f => new EmployeeModel
            {
                Email = f.Object.Email,
                FirstName = f.Object.FirstName,
                Gender = f.Object.Gender,
                LastName = f.Object.LastName,
                MobileNumber = f.Object.MobileNumber,
                Position = f.Object.Position,
                Key = f.Key
            }).ToList();

        }
    }
}
