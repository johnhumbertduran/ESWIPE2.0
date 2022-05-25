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
    public class StudentService : IStudentService
    {
        FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        });

        static int nextStudentNumber = 20220000;
        public async Task<bool> AddorUpdateStudent(StudentModel studentModel)
        {
            if (!string.IsNullOrWhiteSpace(studentModel.Key))
            {
                try
                {
                    await firebase.Child(nameof(StudentModel)).Child(studentModel.Key).PutAsync(studentModel);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                nextStudentNumber++;
                studentModel.StudentNumber = nextStudentNumber;
                studentModel.Username = studentModel.StudentName;
                studentModel.Password = studentModel.StudentNumber.ToString();
                studentModel.UserRole = "Student";

                var response = await firebase.Child(nameof(StudentModel)).PostAsync(studentModel);


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

        public async Task<bool> DeleteStudent(string key)
        {
            try
            {
                await firebase.Child(nameof(StudentModel)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<StudentModel>> GetAllStudent()
        {
            return (await firebase.Child(nameof(StudentModel)).OnceAsync<StudentModel>()).Select(f => new StudentModel
            {
                StudentNumber = f.Object.StudentNumber,
                StudentName = f.Object.StudentName,
                Year = f.Object.Year,
                Section = f.Object.Section,
                Username = f.Object.Username,
                Password = f.Object.Password,
                UserRole = f.Object.UserRole,
                Key = f.Key
            }).ToList();
        }
    }
}
