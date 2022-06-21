using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ESWIPE.ViewModels;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Q1ModulePage : ContentPage
    {
        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;
        public string Quarter1;
        public string Quarter2;
        public string Quarter3;
        public string Quarter4;

        readonly QuarterViewModel quarterViewModel;
        public Q1ModulePage()
        {
            quarterViewModel = new QuarterViewModel();
            InitializeComponent();
            BindingContext = quarterViewModel;
        }

        private async void CreateModuleButton(object sender, EventArgs e)
        {
            Preferences.Set("quarter1", "quarter1");
            await Shell.Current.GoToAsync($"//{nameof(TeacherCreateModulesPage)}");
            //Application.Current.MainPage = new NavigationPage(new TeacherCreateModulesPage());
        }

        protected override void OnAppearing()
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
            
            if (Preferences.ContainsKey("Quarter1", ""))
            {
                Quarter1 = Preferences.Get("Quarter1", "Quarter1");
                Q1ViewModule.IsVisible = true;
            }
            else
            {
                Q1CreateModule.IsVisible = true;
            }

            //if (Preferences.ContainsKey("Quarter2", ""))
            //{
            //    Quarter2 = Preferences.Get("Quarter2", "Quarter2");
            //}

            //if (Preferences.ContainsKey("Quarter3", ""))
            //{
            //    Quarter2 = Preferences.Get("Quarter3", "Quarter3");
            //}

            //if (Preferences.ContainsKey("Quarter4", ""))
            //{
            //    Quarter2 = Preferences.Get("Quarter4", "Quarter4");
            //}
        }
    }
}