using ESWIPE.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class Q3StudentPage : ContentPage
    {
        public string Key;
        public string UserName;
        public string StudentName;
        public string Year;
        public string Section;
        public string Quarters;
        public string SubjectCodePrep;
        public string TeacherName;
        public Q3StudentPage()
        {
            InitializeComponent();
        }
        public static FirebaseClient firebase = new FirebaseClient("https://eswipe-37f7c-default-rtdb.asia-southeast1.firebasedatabase.app/");

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Preferences.ContainsKey("Key"))
            {
                Key = Preferences.Get("Key", "KeyValue");
            }

            if (Preferences.ContainsKey("Username"))
            {
                UserName = Preferences.Get("Username", "UsernameValue");
            }

            if (Preferences.ContainsKey("StudentName"))
            {
                StudentName = Preferences.Get("StudentName", "StudentNameValue");
            }

            if (Preferences.ContainsKey("Year"))
            {
                Year = Preferences.Get("Year", "YearValue");
            }

            if (Preferences.ContainsKey("Section"))
            {
                Section = Preferences.Get("Section", "SectionValue");
            }

            var TeacherData = await GetTeacher(Section);

            TeacherName = TeacherData.Name;

            Quarters = "quarter3";


            var ModuleData = await GetModule(TeacherName, Quarters);

            if (ModuleData != null)
            {
                if (TeacherName == ModuleData.CreatedBy)
                {
                    if (ModuleData.Quarter == Quarters)
                    {
                        Preferences.Set("SubjectCode", ModuleData.SubjectCode);
                    }
                }
            }

            if (Preferences.ContainsKey("SubjectCode"))
            {
                SubjectCodePrep = Preferences.Get("SubjectCode", "SubjectCodeValue");
            }

            var CheckContents = await GetContent(TeacherName, Quarters, SubjectCodePrep);
            var myContents = await PasteContents(TeacherName, Quarters, SubjectCodePrep);
                myListView.ItemsSource = myContents;

            if (CheckContents != null)
            {
                if (CheckContents.TitleContent != null)
                {
                    myListView.IsVisible = true;
                    Q3Label.IsVisible = false;
                }
            }
            else
            {
                myListView.IsVisible = false;
                Q3Label.IsVisible = true;
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


        //Read All Contents
        public static async Task<List<ContentModel>> GetAllContents()
        {
            try
            {
                var contentlist = (await firebase
                .Child("ContentModel")
                .OnceAsync<ContentModel>()).Select(item =>
                new ContentModel
                {
                    Key = item.Object.Key,
                    DateCreated = item.Object.DateCreated,
                    CreatedBy = item.Object.CreatedBy,
                    Quarter = item.Object.Quarter,
                    SubjectCode = item.Object.SubjectCode,
                    TitleContent = item.Object.TitleContent
                    //SubjectQuizCode = item.Object.SubjectQuizCode,
                    //SubjectQuizQty = item.Object.SubjectQuizQty,
                }).ToList();
                return contentlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read Contents
        public static async Task<ContentModel> GetContent(string createdby, string quarters, string subjectcode)
        {
            try
            {
                var allContent = await GetAllContents();
                await firebase.Child("ContentModel").OnceAsync<ContentModel>();
                return allContent.Where(a => a.CreatedBy == createdby).Where(b => b.Quarter == quarters).Where(b => b.SubjectCode == subjectcode).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Paste All Contents
        public static async Task<List<ContentModel>> PasteContents(string createdby, string quarters, string subjectcode)
        {
            try
            {
                var contentlist = (await firebase
                .Child(nameof(ContentModel))
                .OnceAsync<ContentModel>()).Where(a => a.Object.CreatedBy == createdby).Where(b => b.Object.Quarter == quarters).Where(b => b.Object.SubjectCode == subjectcode).Select(item =>
                new ContentModel
                {
                    Key = item.Object.Key,
                    DateCreated = item.Object.DateCreated,
                    CreatedBy = item.Object.CreatedBy,
                    Quarter = item.Object.Quarter,
                    SubjectCode = item.Object.SubjectCode,
                    TitleContent = item.Object.TitleContent
                }).ToList();
                return contentlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read All Teachers
        public static async Task<List<TeacherModel>> GetAllTeachers()
        {
            try
            {
                var teacherlist = (await firebase
                .Child("TeacherModel")
                .OnceAsync<TeacherModel>()).Select(item =>
                new TeacherModel
                {
                    Key = item.Object.Key,
                    Name = item.Object.Name,
                    Section = item.Object.Section
                }).ToList();
                return teacherlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read Teacher
        public static async Task<TeacherModel> GetTeacher(string section)
        {
            try
            {
                var allTeacher = await GetAllTeachers();
                await firebase.Child("TeacherModel").OnceAsync<TeacherModel>();
                return allTeacher.Where(a => a.Section == section).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }
    }
}