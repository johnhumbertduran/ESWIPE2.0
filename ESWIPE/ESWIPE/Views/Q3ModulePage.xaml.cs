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
        public string SubjectCodePrep;

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

            if (Preferences.ContainsKey("SubjectCode"))
            {
                SubjectCodePrep = Preferences.Get("SubjectCode", "SubjectCodeValue");
            }

            if (Preferences.ContainsKey("Quarter3"))
            {
                var ContentData3 = await GetContent(TeacherName, SubjectCodePrep);

                if (ContentData3 != null)
                {
                    if (TeacherName == ContentData3.CreatedBy)
                    {
                        if (SubjectCodePrep == ModuleData3.SubjectCode)
                        {
                            Q3ViewModuleContent.IsVisible = true;
                            Q3CreateModule.IsVisible = false;
                            Q3ViewModule.IsVisible = false;
                        }
                    }
                }
                else
                {
                    Q3ViewModuleContent.IsVisible = false;
                    Q3CreateModule.IsVisible = false;
                    Q3ViewModule.IsVisible = true;
                }
            }
            else
            {
                Q3ViewModuleContent.IsVisible = false;
                Q3CreateModule.IsVisible = true;
                Q3ViewModule.IsVisible = false;
            }



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
        public static async Task<ContentModel> GetContent(string createdby, string subjectcode)
        {
            try
            {
                var allContent = await GetAllContents();
                await firebase.Child("ContentModel").OnceAsync<ContentModel>();
                return allContent.Where(a => a.CreatedBy == createdby).Where(b => b.Quarter == "quarter3").Where(b => b.SubjectCode == subjectcode).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }

        private async void CreateModuleContentButton(object sender, EventArgs e)
        {
            Preferences.Set("quarter3pass", "quarter3value");
            await Shell.Current.GoToAsync($"//{nameof(ModuleViewPage)}");
        }
        private async void ViewModuleContentButton(object sender, EventArgs e)
        {
            Preferences.Set("quarter3pass", "quarter3value");
            await Shell.Current.GoToAsync($"//{nameof(CheckContent)}");
        }
    }
}