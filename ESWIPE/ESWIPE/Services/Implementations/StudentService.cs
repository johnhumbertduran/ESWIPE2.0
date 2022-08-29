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
using System.Diagnostics;

namespace ESWIPE.Services.Implementations
{
    public class StudentService : IStudentService
    {
        readonly FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        });

        public string Key;
        public string UserName;
        public string Teacher;
        public string Section;

        //static int nextStudentNumber = 20220000;
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
                    Debug.WriteLine($"Error:{ex}");
                    return false;
                }
            }
            else
            {

                if (Preferences.ContainsKey("TeacherName"))
                {
                    Teacher = Preferences.Get("TeacherName", "TeacherNameValue");
                }
                
                if (Preferences.ContainsKey("Section"))
                {
                    Section = Preferences.Get("Section", "SectionValue");
                }

                studentModel.Teacher = Teacher;
                studentModel.Section = Section;
                studentModel.UserRole = "Student";

                var response = await firebase.Child(nameof(StudentModel)).PostAsync(studentModel);
                //studentModel.Key = response.Key;
                //await firebase.Child(nameof(StudentModel)).Child(studentModel.Key).PutAsync(studentModel);

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
                Debug.WriteLine($"Error:{ex}");
                return false;
            }
        }

        public async Task<List<StudentModel>> GetAllStudent()
        {
            if (Preferences.ContainsKey("Key"))
            {
                Key = Preferences.Get("Key", "KeyValue");
            }

            if (Preferences.ContainsKey("Username"))
            {
                UserName = Preferences.Get("Username", "UsernameValue");
            }

            if (Preferences.ContainsKey("TeacherName"))
            {
                Teacher = Preferences.Get("TeacherName", "TeacherNameValue");
            }

            if (Preferences.ContainsKey("Section"))
            {
                Section = Preferences.Get("Section", "SectionValue");
            }

            return (await firebase.Child(nameof(StudentModel)).OnceAsync<StudentModel>()).Where(a => a.Object.Section == Section).Select(f => new StudentModel
            {
                //StudentNumber = f.Object.StudentNumber,
                StudentName = f.Object.StudentName,
                Year = f.Object.Year,
                Section = f.Object.Section,
                Username = f.Object.Username,
                Password = f.Object.Password,
                UserRole = f.Object.UserRole,
                Key = f.Key
            }).ToList();
        }

        public async Task<List<StudentModel>> GetAllStudentForAdmin()
        {
            return (await firebase.Child(nameof(StudentModel)).OnceAsync<StudentModel>()).Select(f => new StudentModel
            {
                //StudentNumber = f.Object.StudentNumber,
                StudentName = f.Object.StudentName,
                Teacher = f.Object.Teacher,
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
