using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ESWIPE.Views;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            MessagingCenter.Send<LoginPage>(this,
                (username.Text == "admin") ? "admin" : 
                (username.Text == "teacher") ? "teacher" :
                 (username.Text == "student") ? "student" : "Not found!"
            );

            if (username.Text == "admin")
            {
                //await DisplayAlert("Login Info", "Admin", "ok");
                username.Text = "";
                await Shell.Current.GoToAsync($"//{nameof(AdminTeacherPage)}");
            }
            else if (username.Text == "teacher")
            {
                //await DisplayAlert("Login Info", "Teacher", "ok");
                username.Text = "";
                await Shell.Current.GoToAsync($"//{nameof(TeacherStudentPage)}");
            }
            else if (username.Text == "student")
            {
                //await DisplayAlert("Login Info", "Student", "ok");
                username.Text = "";
                await Shell.Current.GoToAsync($"//{nameof(StudentPage)}");
            }

            
        }
    }
}