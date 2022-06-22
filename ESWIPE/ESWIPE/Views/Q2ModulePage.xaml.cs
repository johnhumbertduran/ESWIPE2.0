using ESWIPE.Models;
using ESWIPE.ViewModels;
using Firebase.Database;
using System;
using System.Collections.Generic;
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
    public partial class Q2ModulePage : ContentPage
    {
        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;
        public string Quarter2 = "quarter2";

        public Q2ModulePage()
        {
            InitializeComponent();
        }

        private async void CreateModuleButton(object sender, EventArgs e)
        {
            if (Preferences.ContainsKey("quarter1", ""))
            {
                Preferences.Remove("quarter1", "quarter1");
            }

            if (Preferences.ContainsKey("quarter2", ""))
            {
                Preferences.Remove("quarter2", "quarter2");
            }

            if (Preferences.ContainsKey("quarter3", ""))
            {
                Preferences.Remove("quarter3", "quarter3");
            }

            if (Preferences.ContainsKey("quarter4", ""))
            {
                Preferences.Remove("quarter4", "quarter4");
            }

            Preferences.Set("quarter2", "quarter2");
            await Shell.Current.GoToAsync($"//{nameof(TeacherCreateModulesPage)}");
            //Application.Current.MainPage = new NavigationPage(new TeacherCreateModulesPage());
        }

        public static FirebaseClient firebase = new FirebaseClient("https://eswipe-37f7c-default-rtdb.asia-southeast1.firebasedatabase.app/");

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Preferences.ContainsKey("Key", ""))
            {
                Key = Preferences.Get("Key", "Key");
            }

            if (Preferences.ContainsKey("Username", ""))
            {
                UserName = Preferences.Get("Username", "Username");
            }

            if (Preferences.ContainsKey("TeacherName", ""))
            {
                TeacherName = Preferences.Get("TeacherName", "TeacherName");
            }

            if (Preferences.ContainsKey("Section", ""))
            {
                Section = Preferences.Get("Section", "Section");
            }

            var ModuleData2 = await GetModule(TeacherName);

            if (ModuleData2 != null)
            {
                if (TeacherName == ModuleData2.CreatedBy)
                {
                    if (ModuleData2.Quarter == Quarter2)
                    {
                        Preferences.Set("Quarter2", Quarter2);
                    }
                }
            }

            if (Preferences.ContainsKey("Quarter2", ""))
            {
                Quarter2 = Preferences.Get("Quarter2", "Quarter2");
                Q2CreateModule.IsVisible = false;
                Q2ViewModule.IsVisible = true;
            }
            else
            {
                Q2CreateModule.IsVisible = true;
                Q2ViewModule.IsVisible = false;
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
                return allModule.Where(a => a.CreatedBy == createdby).Where(b => b.Quarter == "quarter2").FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }

    }
}