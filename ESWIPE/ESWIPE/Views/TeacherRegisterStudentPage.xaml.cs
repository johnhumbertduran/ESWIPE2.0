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
            BindingContext = new TeacherRegisterStudentViewModel();
        }
        public TeacherRegisterStudentPage(StudentModel student)
        {
            InitializeComponent();
            BindingContext = new TeacherRegisterStudentViewModel(student);
        }
    }
}