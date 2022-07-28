using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ESWIPE.Models;
using ESWIPE.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminRegisterTeacherPage : ContentPage
    {
        public AdminRegisterTeacherPage()
        {
            InitializeComponent();
            RegisterTeacherLabel.Text = "Register Teacher";
            TeacherName.Text = "";
            TeacherSection.Text = "";
            TeacherUsername.Text = "";
            TeacherPassword.Text = "";
            BindingContext = new AdminRegisterTeacherViewModel();
        }
        public AdminRegisterTeacherPage(TeacherModel teacher)
        {
            InitializeComponent();
            RegisterTeacherLabel.Text = "Update Teacher";
            BindingContext = new AdminRegisterTeacherViewModel(teacher);
        }
        private void Clear_Clicked(object sender, EventArgs e)
        {
            TeacherName.Text = "";
            TeacherSection.Text = "";
            TeacherUsername.Text = "";
            TeacherPassword.Text = "";
            TeacherName.Focus();
        }
    }
}