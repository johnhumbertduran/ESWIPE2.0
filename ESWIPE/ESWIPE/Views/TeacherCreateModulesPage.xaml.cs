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
                Title = "Quarter 1";
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


        }

        private async void Cancel_Button(object sender, EventArgs e)
        {
            if (Preferences.ContainsKey("quarter1pass"))
            {
                Preferences.Remove("quarter1pass");
                await Shell.Current.GoToAsync($"//{nameof(Q1ModulePage)}");
            }
            
            if (Preferences.ContainsKey("quarter2pass"))
            {
                Preferences.Remove("quarter2pass");
                await Shell.Current.GoToAsync($"//{nameof(Q2ModulePage)}");
            }
            
            if (Preferences.ContainsKey("quarter3pass"))
            {
                Preferences.Remove("quarter3pass");
                await Shell.Current.GoToAsync($"//{nameof(Q3ModulePage)}");
            }
            
            if (Preferences.ContainsKey("quarter4pass"))
            {
                Preferences.Remove("quarter4pass");
                await Shell.Current.GoToAsync($"//{nameof(Q4ModulePage)}");
            }
        }
    }
}