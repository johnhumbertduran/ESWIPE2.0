using System;
using System.Collections.Generic;
using System.Text;


using ESWIPE.Models;
using System.Diagnostics;
using System.Linq;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace ESWIPE.ViewModels
{
    public class FirebaseHelper
    {
        //public static FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        //{
        //    AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        //});
        public static FirebaseClient firebase = new FirebaseClient("https://eswipe-37f7c-default-rtdb.asia-southeast1.firebasedatabase.app/");

        //Read All
        public static async Task<List<TeacherModel>> GetAllTeachers()
        {
            try
            {
                var teacherlist = (await firebase
                .Child("TeacherModel")
                .OnceAsync<TeacherModel>()).Select(item =>
                new TeacherModel
                {
                    Username = item.Object.Username,
                    Password = item.Object.Password,
                    Name = item.Object.Name,
                    Section = item.Object.Section,
                    UserRole = item.Object.UserRole
                }).ToList();
                return teacherlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read 
        public static async Task<TeacherModel> GetTeacher(string username)
        {
            try
            {
                var allTeachers = await GetAllTeachers();
                await firebase.Child("TeacherModel").OnceAsync<TeacherModel>();
                return allTeachers.Where(a => a.Username == username).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }







        //Read All Student
        public static async Task<List<StudentModel>> GetAllStudents()
        {
            try
            {
                var studentlist = (await firebase
                .Child("StudentModel")
                .OnceAsync<StudentModel>()).Select(item =>
                new StudentModel
                {
                    Username = item.Object.Username,
                    Password = item.Object.Password,
                    StudentName = item.Object.StudentName,
                    Year = item.Object.Year,
                    Section = item.Object.Section,
                    SubjectsCode = item.Object.SubjectsCode,
                    QuizCode = item.Object.QuizCode,
                    QuizScore = item.Object.QuizScore,
                    UserRole = item.Object.UserRole
                }).ToList();
                return studentlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read 
        public static async Task<StudentModel> GetStudent(string username)
        {
            try
            {
                var allStudents = await GetAllStudents();
                await firebase.Child("StudentModel").OnceAsync<StudentModel>();
                return allStudents.Where(a => a.Username == username).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }







        //Read All Modules
        public static async Task<List<ModuleModel>> GetAllModules()
        {
            try
            {
                var modulelist = (await firebase
                .Child("ModuleModel")
                .OnceAsync<ModuleModel>()).Select(item =>
                new ModuleModel
                {
                    Key = item.Object.Key,
                    DateCreated = item.Object.DateCreated,
                    CreatedBy = item.Object.CreatedBy,
                    Quarter = item.Object.Quarter,
                    SubjectCode = item.Object.SubjectCode,
                    SubjectQuizCode = item.Object.SubjectQuizCode,
                    SubjectQuizQty = item.Object.SubjectQuizQty,
                }).ToList();
                return modulelist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read 
        public static async Task<ModuleModel> GetModule(string createdby, string quarters)
        {
            try
            {
                var allModule = await GetAllModules();
                await firebase.Child("ModuleModel").OnceAsync<ModuleModel>();
                return allModule.Where(a => a.CreatedBy == createdby).Where(b => b.Quarter == quarters).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }
    }
}
