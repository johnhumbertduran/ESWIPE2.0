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
    public partial class Q3ModulePage : ContentPage
    {
        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;
        public string Quarter3 = "quarter3";

        public Q3ModulePage()
        {
            InitializeComponent();
        }

        private async void CreateModuleButton(object sender, EventArgs e)
        {
            if (Preferences.ContainsKey("quarter1pass"))
            {
                Preferences.Remove("quarter1pass");
            }

            if (Preferences.ContainsKey("quarter2pass"))
            {
                Preferences.Remove("quarter2pass");
            }

            if (Preferences.ContainsKey("quarter3pass"))
            {
                Preferences.Remove("quarter3pass");
            }

            if (Preferences.ContainsKey("quarter4pass"))
            {
                Preferences.Remove("quarter4pass");
            }

            Preferences.Set("quarter3pass", "quarter3value");
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

            var ModuleData3 = await GetModule(TeacherName);

            if (ModuleData3 != null)
            {
                if (TeacherName == ModuleData3.CreatedBy)
                {
                    if (ModuleData3.Quarter == Quarter3)
                    {
                        Preferences.Set("Quarter3", Quarter3);
                        Preferences.Set("SubjectCode", ModuleData3.SubjectCode);
                    }
                }
            }

            if (Preferences.ContainsKey("Quarter3"))
            {
                Q3ViewModule.IsVisible = true;
                Q3CreateModule.IsVisible = false;
            }
            else
            {
                Q3ViewModule.IsVisible = false;
                Q3CreateModule.IsVisible = true;
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
                return allModule.Where(a => a.CreatedBy == createdby).Where(b => b.Quarter == "quarter3").FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }

        private async void ViewModuleButton(object sender, EventArgs e)
        {
            Preferences.Set("quarter3pass", "quarter3value");
            await Shell.Current.GoToAsync($"//{nameof(ModuleViewPage)}");
        }
    }
}