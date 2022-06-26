using ESWIPE.Models;
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
    public partial class TeacherCreateTitlePage : ContentPage
    {
        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;

        public TeacherCreateTitlePage()
        {
            InitializeComponent();
            CreateTitleLabel.Text = "Create Title";
            BindingContext = new TeacherCreateTitleViewModel();
        }


        public TeacherCreateTitlePage(ModuleListModel moduleList)
        {
            InitializeComponent();
            CreateTitleLabel.Text = "Update Title";
            BindingContext = new TeacherCreateTitleViewModel(moduleList);
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
            await Shell.Current.GoToAsync($"///{nameof(ModuleViewPage)}", false);
        }
    }
}