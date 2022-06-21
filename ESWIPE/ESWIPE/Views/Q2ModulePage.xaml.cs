using ESWIPE.ViewModels;
using System;
using System.Collections.Generic;
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
        public string Quarter2;

        readonly QuarterViewModel quarterViewModel;
        public Q2ModulePage()
        {
            quarterViewModel = new QuarterViewModel();
            InitializeComponent();
            BindingContext = quarterViewModel;
        }

        private async void CreateModuleButton(object sender, EventArgs e)
        {
            Preferences.Set("quarter2", "quarter2");
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

            if (Preferences.ContainsKey("Quarter2", ""))
            {
                Quarter2 = Preferences.Get("Quarter2", "Quarter2");
                Q2ViewModule.IsVisible = true;
            }
            else
            {
                Q2CreateModule.IsVisible = true;
            }

        }
    }
}