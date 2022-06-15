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

namespace ESWIPE.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        public LoginViewModel()
        {
        }

        

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
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
                        MessagingCenter.Send(this, message: "Admin");

                    Username = "";
                    Password = "";
                        //var userDetails = new AdminInfo()
                        //{
                        //    Username = StudentUser.Username,
                        //    Password = StudentUser.Password
                        //};

                        //if (Preferences.ContainsKey(nameof(App.UserDetails)))
                        //{
                        //    Preferences.Remove(nameof(App.UserDetails));
                        //}

                        //string UserDetailStr = JsonConvert.SerializeObject(userDetails);
                        await Shell.Current.GoToAsync($"//{nameof(AdminTeacherPage)}");
                }
                else
                {

                    var StudentUser = await FirebaseHelper.GetStudent(Username);



                    if (StudentUser != null)
                    {

                        if (StudentUser.UserRole == "Student")
                        {
                            MessagingCenter.Send(this, message: "Student"
                            );
                        }

                        if (Username == StudentUser.Username && Password == StudentUser.Password)
                        {

                            var userDetails = new StudentModel()
                            {
                                Username = StudentUser.Username,
                                Password = StudentUser.Password,
                                StudentName = StudentUser.StudentName,
                                Year = StudentUser.Year,
                                Section = StudentUser.Section,
                                SubjectsCode = StudentUser.SubjectsCode,
                                QuizCode = StudentUser.QuizCode,
                                QuizScore = StudentUser.QuizScore
                            };

                            if (Preferences.ContainsKey(nameof(App.UserDetails)))
                            {
                                Preferences.Remove(nameof(App.UserDetails));
                            }

                            string UserDetailStr = JsonConvert.SerializeObject(userDetails);
                            Password = "";
                            Username = "";

                            //Preferences.Set(nameof(App.UserDetails), UserDetailStr);
                            //App.UserDetails = userDetails;

                            //await App.Current.MainPage.DisplayAlert("Login Success", "Student", "Ok");

                            //AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
                            await Shell.Current.GoToAsync($"//{nameof(StudentPage)}?Creds={UserDetailStr}");
                            //await App.Current.MainPage.Navigation.PushAsync(new StudentPage(Username));

                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                        }
                    }
                    else
                    {
                        ////await App.Current.MainPage.DisplayAlert("Login Failed", "Not Found", "Ok");
                        var TeacherUser = await FirebaseHelper.GetTeacher(Username);



                        if (TeacherUser != null)
                        {
                            if (TeacherUser.UserRole == "Teacher")
                            {
                                MessagingCenter.Send(this, "Teacher"
                                );
                            }
                            if (Username == TeacherUser.Username && Password == TeacherUser.Password)
                            {

                                var UserDetails = new TeacherModel()
                                {
                                    Username = TeacherUser.Username,
                                    Password = TeacherUser.Password,
                                    Name = TeacherUser.Name,
                                    Section = TeacherUser.Section
                                };

                                if (Preferences.ContainsKey(nameof(App.UserDetails)))
                                {
                                    Preferences.Remove(nameof(App.UserDetails));
                                }

                                string UserDetailStr = JsonConvert.SerializeObject(UserDetails);
                                Preferences.Set(nameof(App.UserDetails), UserDetailStr);
                                App.UserDetails = UserDetails;

                                Password = "";
                                Username = "";

                                //await App.Current.MainPage.DisplayAlert("Login Success", "Teacher", "Ok");
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

                    //else
                    //{


                    //if (StudentUser.UserRole == "Student")
                    //{
                    //    await App.Current.MainPage.DisplayAlert("Login Success", "Student", "Ok");
                    //}
                    //}



                    //if (AdminUser.UserRole == "Admin")
                    //{
                    //    await App.Current.MainPage.DisplayAlert("Login Success", "", "Ok");

                    //    //firebase return null valuse if user data not found in database
                    //    if (user != null)
                    //    {
                    //        if (Username == user.Username && Password == user.Password)
                    //        {
                    //            //await App.Current.MainPage.DisplayAlert("Login Success", "", "Ok");
                    //            //await Shell.Current.GoToAsync($"//{nameof(TeacherStudentPage)}");
                    //            //Navigate to Wellcom page after successfuly login
                    //            //pass user email to welcom page
                    //            //await App.Current.MainPage.Navigation.PushAsync(new WelcomPage(Email));
                    //        }
                    //        else
                    //        {
                    //            await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");
                    //    }

                    //}
                    //else 


                    //if (StudentUser.UserRole == "Student")
                    //{
                    //    //firebase return null valuse if user data not found in database
                    //    if (StudentUser != null)
                    //    {
                    //        if (Username == StudentUser.Username && Password == StudentUser.Password)
                    //        {
                    //            await App.Current.MainPage.DisplayAlert("Login Success", "This is Student", "Ok");
                    //            //await Shell.Current.GoToAsync($"//{nameof(TeacherStudentPage)}");
                    //            //Navigate to Wellcom page after successfuly login
                    //            //pass user email to welcom page
                    //            //await App.Current.MainPage.Navigation.PushAsync(new WelcomPage(Email));
                    //        }
                    //        else
                    //        {
                    //            await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");
                    //    }

                    //}


                    //if (TeacherUser.UserRole == "Teacher")
                    //{
                    //    //firebase return null valuse if user data not found in database
                    //    if (TeacherUser != null)
                    //    {
                    //        if (Username == TeacherUser.Username && Password == TeacherUser.Password)
                    //        {
                    //            await App.Current.MainPage.DisplayAlert("Login Success", "This is Teacher", "Ok");
                    //            //await Shell.Current.GoToAsync($"//{nameof(TeacherStudentPage)}");
                    //            //Navigate to Wellcom page after successfuly login
                    //            //pass user email to welcom page
                    //            //await App.Current.MainPage.Navigation.PushAsync(new WelcomPage(Email));
                    //        }
                    //        else
                    //        {
                    //            await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");
                    //    }

                    //}


                }
            }
        }
    }
}
