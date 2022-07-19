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
    public partial class Q4ModulePage : ContentPage
    {
        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;
        public string Quarter4 = "quarter4";
        public string SubjectCodePrep;

        public Q4ModulePage()
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

            Preferences.Set("quarter4pass", "quarter4value");
            await Shell.Current.GoToAsync($"//{nameof(TeacherCreateModulesPage)}");
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

            var ModuleData4 = await GetModule(TeacherName);

            if (ModuleData4 != null)
            {
                if (TeacherName == ModuleData4.CreatedBy)
                {
                    if (ModuleData4.Quarter == Quarter4)
                    {
                        Preferences.Set("Quarter4", Quarter4);
                        Preferences.Set("SubjectCode", ModuleData4.SubjectCode);
                    }
                }
            }

            if (Preferences.ContainsKey("SubjectCode"))
            {
                SubjectCodePrep = Preferences.Get("SubjectCode", "SubjectCodeValue");
            }

            if (Preferences.ContainsKey("Quarter4"))
            {
                var ContentData4 = await GetContent(TeacherName, SubjectCodePrep);

                if (ContentData4 != null)
                {
                    if (TeacherName == ContentData4.CreatedBy)
                    {
                        if (SubjectCodePrep == ModuleData4.SubjectCode)
                        {
                            Q4ViewModuleContent.IsVisible = true;
                            Q4CreateModule.IsVisible = false;
                            Q4ViewModule.IsVisible = false;
                        }
                    }
                }
                else
                {
                    Q4ViewModuleContent.IsVisible = false;
                    Q4CreateModule.IsVisible = false;
                    Q4ViewModule.IsVisible = true;
                }
            }
            else
            {
                Q4ViewModuleContent.IsVisible = false;
                Q4CreateModule.IsVisible = true;
                Q4ViewModule.IsVisible = false;
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
                return allModule.Where(a => a.CreatedBy == createdby).Where(b => b.Quarter == "quarter4").FirstOrDefault();
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
                return allContent.Where(a => a.CreatedBy == createdby).Where(b => b.Quarter == "quarter4").Where(b => b.SubjectCode == subjectcode).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }

        private async void CreateModuleContentButton(object sender, EventArgs e)
        {
            Preferences.Set("quarter4pass", "quarter4value");
            await Shell.Current.GoToAsync($"//{nameof(ModuleViewPage)}");
        }
        private async void ViewModuleContentButton(object sender, EventArgs e)
        {
            Preferences.Set("quarter4pass", "quarter4value");
            await Shell.Current.GoToAsync($"//{nameof(CheckContent)}");
        }
    }
}