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
    public class TeacherService : ITeacher
    {
        FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        });

        static int nextTeacherNumber = 20220001;

        //public async Task<TeacherModel> GetOneTeacher()
        //{
        //    return (await firebase.Child(nameof(TeacherModel)).OnceAsync<TeacherModel>()).Select(f => new TeacherModel
        //    {
        //        TeacherNumber = f.Object.TeacherNumber++
        //    }).LastOrDefault();
        //}


        //public async Task<TeacherModel> GetById(int teacherNumber)
        //{
        //    var teacher = await firebase.Child(nameof(TeacherModel) + "/" + teacherNumber).OnceSingleAsync<TeacherModel>());
        //    return teacher++;
        //}

        public async Task<bool> AddorUpdateTeacher(TeacherModel teacherModel)
        {

            

            //return allUsers.Where(a => a.TeacherNumber.ToString() == teacherNumber.ToString()).LastOrDefault();

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

                //await firebase.Child("TeacherModel").OnceAsync<TeacherModel>();

                //var checkNumber = firebase.Child("TeacherModel").OrderByKey().EqualTo("TeacherNumber");
                //var checkNumber = teacherModel.TeacherNumber.ToString

                //var teacher = await firebase.Child(nameof(TeacherModel) + "/" + teacherModel.TeacherNumber).OnceSingleAsync<TeacherModel>();
                //var checkNumber = Convert.ToInt32(teacher) + 1;

                //return (await firebase.Child(nameof(TeacherModel)).OnceAsync<TeacherModel>()).Select(f => new TeacherModel

                //testNumber = firebase.Child("teachernumber").Child(nameof(TeacherModel)).GetType().addOnCompleteListener(new OnCompleteListener<TeacherModel>();
                teacherModel.TeacherNumber = nextTeacherNumber++;
                teacherModel.Username = teacherModel.Name;
                teacherModel.Password = nextTeacherNumber.ToString();
                teacherModel.UserRole = "Teacher";
                nextTeacherNumber++;

                var response = await firebase.Child(nameof(TeacherModel)).PostAsync(teacherModel);

                
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
                

        //public async Task<TeacherModel> GetTeacherNumber(string teacherNumber)
        //{
        //    //const ref = firebase.("TeacherModel");
        //    //const query = ref.orderByChild("time").limitToLast(1);
        //    //query.once("child_added").then((snapshot) => {
        //    //    console.log(snapshot.val().value);
        //    //});
        //    //TeacherModel citiesRef = db.Collection("cities");
        //    //TeacherModel citiesRef = FirebaseObject.
        //    //Query query = citiesRef.OrderByDescending("Name").Limit(3);

        //    try
        //    {
        //        var allUsers = await GetAllTeacher();
        //        await firebase.Child("TeacherModel").OnceAsync<TeacherModel>();

        //        return allUsers.Where(a => a.TeacherNumber.ToString() == teacherNumber.ToString()).LastOrDefault();

        //        //await firebase.Child("TeacherModel").Child(TeacherNumber).GetType;
        //        //return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //return false;
        //    }
        //}

        public async Task<List<TeacherModel>> GetAllTeacher()
        {
            return (await firebase.Child(nameof(TeacherModel)).OnceAsync<TeacherModel>()).Select(f => new TeacherModel
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
