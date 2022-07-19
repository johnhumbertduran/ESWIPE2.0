using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ESWIPE.Views;


using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using System.Collections;
using Firebase.Database;
using ESWIPE.Models;
using Xamarin.Essentials;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckContent : ContentPage
    {
        //public string TitleContent { set; get; }
        //FirebaseHelper firebaseHelper = new FirebaseHelper();

        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;
        public string Quarters;
        public string SubjectCodePrep;

        public CheckContent()
        {
            InitializeComponent();

            
        }

        private async void Cancel_Button(object sender, EventArgs e)
        {
            if (Preferences.ContainsKey("quarter1pass"))
            {
                Preferences.Remove("quarter1pass");
                await Shell.Current.GoToAsync($"///{nameof(Q1ModulePage)}", false);
            }

            if (Preferences.ContainsKey("quarter2pass"))
            {
                Preferences.Remove("quarter2pass");
                await Shell.Current.GoToAsync($"///{nameof(Q2ModulePage)}", false);
            }

            if (Preferences.ContainsKey("quarter3pass"))
            {
                Preferences.Remove("quarter3pass");
                await Shell.Current.GoToAsync($"///{nameof(Q3ModulePage)}", false);
            }

            if (Preferences.ContainsKey("quarter4pass"))
            {
                Preferences.Remove("quarter4pass");
                await Shell.Current.GoToAsync($"///{nameof(Q4ModulePage)}", false);
            }
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

            if (Preferences.ContainsKey("TeacherName"))
            {
                TeacherName = Preferences.Get("TeacherName", "TeacherNameValue");
            }

            if (Preferences.ContainsKey("Section"))
            {
                Section = Preferences.Get("Section", "SectionValue");
            }

            if (Preferences.ContainsKey("quarter1pass"))
            {
                Quarters = "quarter1";
            }

            if (Preferences.ContainsKey("quarter2pass"))
            {
                Quarters = "quarter2";
            }

            if (Preferences.ContainsKey("quarter3pass"))
            {
                Quarters = "quarter3";
            }

            if (Preferences.ContainsKey("quarter4pass"))
            {
                Quarters = "quarter4";
            }


            var ModuleData = await GetModule(TeacherName,Quarters);

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

            var myContents = await GetAllContents(TeacherName, Quarters, SubjectCodePrep);
            myListView.ItemsSource = myContents;

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

        public static async Task<List<ContentModel>> GetAllContents(string createdby, string quarters, string subjectcode)
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

    }
}