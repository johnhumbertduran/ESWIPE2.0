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
    public partial class TeacherRegisterStudentPage : ContentPage
    {

        public TeacherRegisterStudentPage()
        {
            InitializeComponent();
            RegisterStudentLabel.Text = "Register Student";
            BindingContext = new TeacherRegisterStudentViewModel();
        }
        public TeacherRegisterStudentPage(StudentModel student)
        {
            InitializeComponent();
            RegisterStudentLabel.Text = "Update Student";
            BindingContext = new TeacherRegisterStudentViewModel(student);
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            StudentName.Text = "";
            Year.Text = "";
            //Section.Text = "";
            Username.Text = "";
            Password.Text = "";
            StudentName.Focus();
        }
    }
}