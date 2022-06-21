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
    public class TeacherService : ITeacherService
    {
        FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        });

        //static int nextTeacherNumber = 20220000;

        public async Task<bool> AddorUpdateTeacher(TeacherModel teacherModel)
        {


            if (!string.IsNullOrWhiteSpace(teacherModel.Key))
            {
                try
                {
                    await firebase.Child(nameof(TeacherModel)).Child(teacherModel.Key).PutAsync(teacherModel);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                //nextTeacherNumber++;
                //teacherModel.TeacherNumber = nextTeacherNumber;
                //teacherModel.Username = teacherModel.Name;
                //teacherModel.Password = teacherModel.TeacherNumber.ToString();
                teacherModel.UserRole = "Teacher";

                var response = await firebase.Child(nameof(TeacherModel)).PostAsync(teacherModel);
                //teacherModel.Key = response.Key;
                //await firebase.Child(nameof(TeacherModel)).Child(teacherModel.Key).PutAsync(teacherModel);

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

        public async Task<bool> DeleteTeacher(string key)
        {
            try
            {
                await firebase.Child(nameof(TeacherModel)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
                
        public async Task<List<TeacherModel>> GetAllTeacher()
        {
            return (await firebase.Child(nameof(TeacherModel)).OnceAsync<TeacherModel>()).Select(f => new TeacherModel
            {
                //TeacherNumber = f.Object.TeacherNumber,
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
