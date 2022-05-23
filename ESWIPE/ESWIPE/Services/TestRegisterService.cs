using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using ESWIPE.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;
using ESWIPE.Models;

namespace ESWIPE.Services
{
    public class TestRegisterService : ITestRegister
    {
        FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        });

        public async Task<bool> AddorUpdateTeacher(TestRegister teacher)
        {
            if (!string.IsNullOrWhiteSpace(teacher.Key))
            {
                try
                {
                    await firebase.Child(nameof(TestRegister)).Child(teacher.Key).PutAsync(teacher);
                    return true;
                    //await Application.Current.MainPage.DisplayAlert("Check Not Null", "return true!", "OK");
                }
                catch (Exception ex)
                {
                    return false;
                    //await Application.Current.MainPage.DisplayAlert("Check Not Null", "return false!", "OK");
                }
            }
            else
            {
                var response = await firebase.Child(nameof(TestRegister)).PostAsync(teacher);
                if (response.Key != null)
                {
                    return true;
                    //await Application.Current.MainPage.DisplayAlert("Check Null", "return true!", "OK");
                }
                else
                {
                    return false;
                    //await Application.Current.MainPage.DisplayAlert("Check Null", "return false!", "OK");
                }
            }
        }

        public async Task<bool> DeleteTeacher(string key)
        {
            try
            {
                await firebase.Child(nameof(TestRegister)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<TestRegister>> GetAllTeacher()
        {
            return (await firebase.Child(nameof(TestRegister)).OnceAsync<TestRegister>()).Select(f => new TestRegister
            {
                TeacherNumber = f.Object.TeacherNumber,
                Name = f.Object.Name,
                Section = f.Object.Section,
                Username = f.Object.Username,
                Password = f.Object.Password,
                UserRole = f.Object.UserRole,
                Key = f.Key
            }).ToList();
        }
    }
}
