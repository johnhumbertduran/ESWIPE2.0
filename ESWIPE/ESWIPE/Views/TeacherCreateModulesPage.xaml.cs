using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ESWIPE.Models;
using ESWIPE.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherCreateModulesPage : ContentPage
    {
        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;

        public TeacherCreateModulesPage()
        {
            InitializeComponent();
            CreateModuleLabel.Text = "Create Module";
            BindingContext = new TeacherCreateModulesViewModel();
        }
        public TeacherCreateModulesPage(ModuleModel module)
        {
            InitializeComponent();
            CreateModuleLabel.Text = "Update Module";
            BindingContext = new TeacherCreateModulesViewModel(module);
        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Send(this, message: "Teacher");

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

            if (Preferences.ContainsKey("quarter1", ""))
            {
                Title = "Quarter 1";
            }

            if (Preferences.ContainsKey("quarter2", ""))
            {
                Title = "Quarter 2";
            }

            if (Preferences.ContainsKey("quarter3", ""))
            {
                Title = "Quarter 3";
            }

            if (Preferences.ContainsKey("quarter4", ""))
            {
                Title = "Quarter 4";
            }


        }

        private async void Cancel_Button(object sender, EventArgs e)
        {
            if (Preferences.ContainsKey("quarter1", ""))
            {
                Preferences.Remove("quarter1", "quarter1");
                await Shell.Current.GoToAsync($"//{nameof(Q1ModulePage)}");
            }
            
            if (Preferences.ContainsKey("quarter2", ""))
            {
                Preferences.Remove("quarter2", "quarter2");
                await Shell.Current.GoToAsync($"//{nameof(Q2ModulePage)}");
            }
            
            if (Preferences.ContainsKey("quarter3", ""))
            {
                Preferences.Remove("quarter3", "quarter3");
                await Shell.Current.GoToAsync($"//{nameof(Q3ModulePage)}");
            }
            
            if (Preferences.ContainsKey("quarter4", ""))
            {
                Preferences.Remove("quarter4", "quarter4");
                await Shell.Current.GoToAsync($"//{nameof(Q4ModulePage)}");
            }
        }
    }
}