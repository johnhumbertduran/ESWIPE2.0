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
    public partial class TeacherStudentPage : ContentPage
    {
        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;

        public TeacherStudentPage()
        {
            InitializeComponent();
            //BindingContext = new TeacherStudentViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Preferences.ContainsKey("Username", ""))
            {
                UserName = Preferences.Get("Username", "Username");
            }
            else
            {
                Preferences.Set("Username", "Username");
                UserName = Preferences.Get("Username", "Username");
            }

            if (Preferences.ContainsKey("TeacherName", ""))
            {
                TeacherName = Preferences.Get("TeacherName", "TeacherName");
            }
            else
            {
                Preferences.Set("TeacherName", "TeacherName");
                TeacherName = Preferences.Get("TeacherName", "TeacherName");
            }

            if (Preferences.ContainsKey("Section", ""))
            {
                Section = Preferences.Get("Section", "Section");
            }
            else
            {
                Preferences.Set("Section", "Section");
                Section = Preferences.Get("Section", "Section");
            }
            Title = TeacherName;

        }
    }
}