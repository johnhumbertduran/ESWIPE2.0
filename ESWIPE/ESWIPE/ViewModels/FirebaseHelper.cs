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
                    Password = item.Object.Password
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
    }
}
