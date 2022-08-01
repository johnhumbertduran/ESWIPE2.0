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
        public string SubjectCodePrep;

        public Q1ModulePage()
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
                        Preferences.Set("SubjectCode", ModuleData1.SubjectCode);
                    }
                }
            }


            if (Preferences.ContainsKey("SubjectCode"))
            {
                SubjectCodePrep = Preferences.Get("SubjectCode", "SubjectCodeValue");
            }

            if (Preferences.ContainsKey("Quarter1"))
            {
                var ContentData = await GetContent(TeacherName, SubjectCodePrep);

                if (ContentData != null)
                {
                    if (TeacherName == ContentData.CreatedBy)
                    {
                        if (SubjectCodePrep == ContentData.SubjectCode)
                        {
                            Q1ViewModuleContent.IsVisible = true;
                            Q1CreateQuiz.IsVisible = true;
                            Q1CreateModule.IsVisible = false;
                            Q1ViewModule.IsVisible = false;
                        }
                    }
                }
                else
                {
                    Q1ViewModuleContent.IsVisible = false;
                    Q1CreateModule.IsVisible = false;
                    Q1ViewModule.IsVisible = true;
                }
            }
            else
            {
                Q1ViewModuleContent.IsVisible = false;
                Q1CreateModule.IsVisible = true;
                Q1ViewModule.IsVisible = false;
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
                    //SubjectQuizCode = item.Object.SubjectQuizCode,
                    //SubjectQuizQty = item.Object.SubjectQuizQty,
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
        public static async Task<ContentModel> GetContent(string createdby, string subjectcode)
        {
            try
            {
                var allContent= await GetAllContents();
                await firebase.Child("ContentModel").OnceAsync<ContentModel>();
                return allContent.Where(a => a.CreatedBy == createdby).Where(b => b.Quarter == "quarter1").Where(b => b.SubjectCode == subjectcode).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }

        private async void CreateModuleContentButton(object sender, EventArgs e)
        {
            Preferences.Set("quarter1pass", "quarter1value");
            await Shell.Current.GoToAsync($"//{nameof(ModuleViewPage)}");
            //Application.Current.MainPage = new NavigationPage(new ModuleViewPage());
        }
        private async void ViewModuleContentButton(object sender, EventArgs e)
        {
            Preferences.Set("quarter1pass", "quarter1value");
            await Shell.Current.GoToAsync($"//{nameof(CheckContent)}");
            //Application.Current.MainPage = new NavigationPage(new ModuleViewPage());
        }

        private async void Q1CreateQuiz_Clicked(object sender, EventArgs e)
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

            Preferences.Set("quarter1pass", "quarter1value");
            await Shell.Current.GoToAsync("//QuizTypePage", true);
        }
    }
}