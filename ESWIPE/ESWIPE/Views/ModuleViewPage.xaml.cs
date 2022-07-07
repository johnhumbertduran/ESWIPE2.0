using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MvvmHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Firebase.Database;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModuleViewPage : ContentPage
    {
        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;
        public string Quarters;


        public ModuleViewPage()
        {
            InitializeComponent();
        }

        private void Add_Title(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"//{nameof(TeacherCreateTitlePage)}");
            Navigation.PushAsync(new TeacherCreateTitlePage());
        }

        public static FirebaseClient firebase = new FirebaseClient("https://eswipe-37f7c-default-rtdb.asia-southeast1.firebasedatabase.app/");

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Send(this, message: "Teacher");

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

            var ModuleData1 = await GetTitle(TeacherName, Quarters);

            if (Preferences.ContainsKey("quarter1pass"))
            {
                Title = "Quarter 1";

                if (ModuleData1 != null)
                {
                    if (TeacherName == ModuleData1.CreatedBy)
                    {
                        if (ModuleData1.Quarter == Quarters)
                        {
                            Preferences.Set("SubjectCode", ModuleData1.SubjectCode);
                        }
                    }
                }
            }

            if (Preferences.ContainsKey("quarter2pass"))
            {
                Title = "Quarter 2";
            }

            if (Preferences.ContainsKey("quarter3pass"))
            {
                Title = "Quarter 3";
            }

            if (Preferences.ContainsKey("quarter4pass"))
            {
                Title = "Quarter 4";
            }

            BindingContext = new ModuleViewViewModel();
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


        //Read All Title
        public static async Task<List<ModuleListModel>> GetAllTitles()
        {
            try
            {
                var modulelist = (await firebase
                .Child("ModuleListModel")
                .OnceAsync<ModuleListModel>()).Select(item =>
                new ModuleListModel
                {
                    Key = item.Object.Key,
                    DateCreated = item.Object.DateCreated,
                    CreatedBy = item.Object.CreatedBy,
                    Quarter = item.Object.Quarter,
                    SubjectCode = item.Object.SubjectCode,
                    Title = item.Object.Title,
                }).ToList();
                return modulelist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read title
        public static async Task<ModuleListModel> GetTitle(string createdby, string quarter)
        {
            try
            {
                var allTitle = await GetAllTitles();
                await firebase.Child("ModuleListModel").OnceAsync<ModuleListModel>();
                return allTitle.Where(a => a.CreatedBy == createdby).Where(a => a.Quarter == quarter).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }

    }
}