using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using ESWIPE.Services.Implementations;
using ESWIPE.Services.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;
using ESWIPE.Views;
using ESWIPE.ViewModels;
using System.ComponentModel;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ESWIPE.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {


        //public event PropertyChangedEventHandler PropertyChanged;
        public LoginViewModel()
        {
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }


        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
        


        public Command LoginCommand
        {
            get
            {
                return new Command(Login);
            }
        }

        private async void Login()
        {
            //null or empty field validation, check weather email and password is null or empty

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            }
            else
            {
                //call GetUser function which we define in Firebase helper class
                //var AdminUser = await FirebaseHelper.GetTeacher(Username);

                if (Username == "adswipen2206" && Password == "pasadipe0622")
                {
                    string AUName = "AdminUserName";
                    string AName = "AdminLoggedIn";
                        MessagingCenter.Send(this, message: "Admin");
                        Preferences.Set("Username", AUName);
                        Preferences.Set("AdminName", AName);
                        Username = "";
                        Password = "";

                        await Shell.Current.GoToAsync($"//{nameof(AdminTeacherPage)}");
                }
                else
                {

                    var StudentUser = await FirebaseHelper.GetStudent(Username);



                    if (StudentUser != null)
                    {

                        if (StudentUser.UserRole == "Student")
                        {
                            MessagingCenter.Send(this, message: "Student");
                        }

                        if (Username == StudentUser.Username && Password == StudentUser.Password)
                        {
                            var userDetails = new StudentModel()
                            {
                                Key = StudentUser.Key,
                                Username = StudentUser.Username,
                                StudentName = StudentUser.StudentName,
                                Year = StudentUser.Year,
                                Section = StudentUser.Section,
                                SubjectsCode = StudentUser.SubjectsCode,
                                QuizCode = StudentUser.QuizCode,
                                QuizScore = StudentUser.QuizScore
                            };

                            Preferences.Set("Key", userDetails.Key);
                            Preferences.Set("Username", userDetails.Username);
                            Preferences.Set("StudentName", userDetails.StudentName);
                            Preferences.Set("Teacher", userDetails.TeacherName);
                            Preferences.Set("Year", userDetails.Year);
                            Preferences.Set("Section", userDetails.Section);

                            Password = "";
                            Username = "";

                            await Shell.Current.GoToAsync($"//{nameof(StudentPage)}");


                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                        }
                    }
                    else
                    {
                        var TeacherUser = await FirebaseHelper.GetTeacher(Username);
                        //var ModuleData = await FirebaseHelper.GetModule(TeacherUser.Name);



                        if (TeacherUser != null)
                        {
                            if (TeacherUser.UserRole == "Teacher")
                            {
                                MessagingCenter.Send(this, "Teacher");
                            }
                            if (Username == TeacherUser.Username && Password == TeacherUser.Password)
                            {
                                var userDetails = new TeacherModel()
                                {
                                    Key = TeacherUser.Key,
                                    Username = TeacherUser.Username,
                                    Name = TeacherUser.Name,
                                    Section = TeacherUser.Section
                                };

                                Preferences.Set("Key", userDetails.Key);
                                Preferences.Set("Username", userDetails.Username);
                                Preferences.Set("TeacherName", userDetails.Name);
                                Preferences.Set("Section", userDetails.Section);

                                Password = "";
                                Username = "";

                                await Shell.Current.GoToAsync($"//{nameof(TeacherStudentPage)}");

                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                            Password = "";
                            Username = "";
                        }
                    }


                }
            }
        }
    }
}
