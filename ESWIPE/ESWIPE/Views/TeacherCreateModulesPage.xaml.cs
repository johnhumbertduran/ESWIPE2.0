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

        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Preferences.ContainsKey("Key", ""))
            {
                UserName = Preferences.Get("Key", "Key");
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
        }
    }
}