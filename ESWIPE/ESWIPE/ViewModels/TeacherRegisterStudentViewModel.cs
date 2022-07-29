using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using ESWIPE.Services.Implementations;
using ESWIPE.Services.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;
using ESWIPE.Views;

namespace ESWIPE.ViewModels
{
    public class TeacherRegisterStudentViewModel : ViewModelBase
    {

        #region Constructor
        public TeacherRegisterStudentViewModel()
        {
            Title = "Register Student";
            _studentService = DependencyService.Resolve<IStudentService>();
        }

        public TeacherRegisterStudentViewModel(StudentModel studentResponse)
        {
            Title = "Update Student";
            _studentService = DependencyService.Resolve<IStudentService>();
            StudentDetail = new StudentModel
            {
                //StudentNumber = studentResponse.StudentNumber,
                StudentName = studentResponse.StudentName,
                Year = studentResponse.Year,
                Section = studentResponse.Section,
                Username = studentResponse.Username,
                Password = studentResponse.Password,
                UserRole = studentResponse.UserRole,
                SubjectsCode = studentResponse.SubjectsCode,
                QuizCode = studentResponse.QuizCode,
                QuizScore = studentResponse.QuizScore,
                Key = studentResponse.Key
            };
        }
        #endregion


        #region Properties
        private readonly IStudentService _studentService;

        private StudentModel _studentDetail = new StudentModel();

        public StudentModel StudentDetail
        {
            get => _studentDetail;
            set => SetProperty(ref _studentDetail, value);
        }

        #endregion



        #region Commands

        public Command SaveStudentCommand
        {
            get
            {
                return new Command(SaveStudent);
            }
        }

        private async void SaveStudent()
        {
            if (string.IsNullOrEmpty(_studentDetail.StudentName))
            {
                await App.Current.MainPage.DisplayAlert("Empty Name Value", "Please enter Name", "OK");
            }
            else if (string.IsNullOrEmpty(_studentDetail.Year))
            {
                await App.Current.MainPage.DisplayAlert("Empty Year Value", "Please enter Year", "OK");
            }
            else if (string.IsNullOrEmpty(_studentDetail.Section))
            {
                await App.Current.MainPage.DisplayAlert("Empty Section Value", "Please enter Section", "OK");
            }
            else if (string.IsNullOrEmpty(_studentDetail.Username))
            {
                await App.Current.MainPage.DisplayAlert("Empty Username Value", "Please enter Username", "OK");
            }
            else if (string.IsNullOrEmpty(_studentDetail.Password))
            {
                await App.Current.MainPage.DisplayAlert("Empty Password Value", "Please enter Password", "OK");
            }
            else
            {
                if (IsBusy) { return; }
                IsBusy = true;
                bool res = await _studentService.AddorUpdateStudent(StudentDetail);
                if (res)
                {
                    if (!string.IsNullOrWhiteSpace(StudentDetail.Key))
                    {
                        await Application.Current.MainPage.DisplayAlert("Update Info", "Records Updated Succesfully!", "OK");

                        await Shell.Current.GoToAsync("..");
                        //await Application.Current.MainPage.Navigation.PushAsync(new AdminTeacherPage());
                    }
                    else
                    {
                        StudentDetail = new StudentModel() { };
                        await Application.Current.MainPage.DisplayAlert("Registration Info", "Succesfully Registered!", "OK");
                    }
                }
                IsBusy = false;
            }
        }
        //public ICommand SaveStudentCommand => new Command(async () =>
        //{
        //    if (string.IsNullOrEmpty(StudentName))
        //    {
        //        await App.Current.MainPage.DisplayAlert("Empty Name Value", "Please enter Name", "OK");
        //    }
        //    else if (string.IsNullOrEmpty(Year))
        //    {
        //        await App.Current.MainPage.DisplayAlert("Empty Year Value", "Please enter Year", "OK");
        //    }
        //    else if (string.IsNullOrEmpty(Section))
        //    {
        //        await App.Current.MainPage.DisplayAlert("Empty Section Value", "Please enter Section", "OK");
        //    }
        //    else if (string.IsNullOrEmpty(Username))
        //    {
        //        await App.Current.MainPage.DisplayAlert("Empty Username Value", "Please enter Username", "OK");
        //    }
        //    else if (string.IsNullOrEmpty(Password))
        //    {
        //        await App.Current.MainPage.DisplayAlert("Empty Password Value", "Please enter Password", "OK");
        //    }
        //    else
        //    {
        //        if (IsBusy) { return; }
        //        IsBusy = true;
        //        bool res = await _studentService.AddorUpdateStudent(StudentDetail);
        //        if (res)
        //        {
        //            if (!string.IsNullOrWhiteSpace(StudentDetail.Key))
        //            {
        //                await Application.Current.MainPage.DisplayAlert("Update Info", "Records Updated Succesfully!", "OK");

        //                await Shell.Current.GoToAsync("..");
        //                //await Application.Current.MainPage.Navigation.PushAsync(new AdminTeacherPage());
        //            }
        //            else
        //            {
        //                StudentDetail = new StudentModel() { };
        //                await Application.Current.MainPage.DisplayAlert("Registration Info", "Succesfully Registered!", "OK");
        //            }
        //        }
        //        IsBusy = false;
        //    }

        //});
        #endregion
    }
}
