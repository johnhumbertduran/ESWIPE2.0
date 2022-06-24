using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ESWIPE.ViewModels;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using ESWIPE.Models;
using System.Diagnostics;
using Firebase.Database;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Q1ModulePage : ContentPage
    {
        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;
        public string Quarter1 = "quarter1";

        public Q1ModulePage()
        {
            InitializeComponent();
        }

        private async void CreateModuleButton(object sender, EventArgs e)
        {
            Preferences.Set("quarter1pass", "quarter1value");
            await Shell.Current.GoToAsync($"//{nameof(TeacherCreateModulesPage)}");
            //Application.Current.MainPage = new NavigationPage(new TeacherCreateModulesPage());
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

            var ModuleData1 = await GetModule(TeacherName);

            if (ModuleData1 != null)
            {
                if (TeacherName == ModuleData1.CreatedBy)
                {
                    if(ModuleData1.Quarter == Quarter1)
                    {
                        Preferences.Set("Quarter1", Quarter1);
                    }
                }
            }

            if (Preferences.ContainsKey("Quarter1"))
            {
                Q1CreateModule.IsVisible = false;
                Q1ViewModule.IsVisible = true;
            }
            else
            {
                Q1CreateModule.IsVisible = true;
                Q1ViewModule.IsVisible = false;
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
        public static async Task<ModuleModel> GetModule(string createdby)
        {
            try
            {
                var allModule = await GetAllModules();
                await firebase.Child("ModuleModel").OnceAsync<ModuleModel>();
                return allModule.Where(a => a.CreatedBy == createdby).Where(b => b.Quarter == "quarter1").FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }

        private void ViewModuleButton(object sender, EventArgs e)
        {
            //Preferences.Set("quarter1pass", "quarter1value");
            //await Shell.Current.GoToAsync("//ModuleViewPage");
            Application.Current.MainPage = new NavigationPage(new ModuleViewPage());
        }
    }
}