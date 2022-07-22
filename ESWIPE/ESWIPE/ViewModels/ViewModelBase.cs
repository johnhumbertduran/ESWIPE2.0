using System;
using System.Collections.Generic;
using System.Text;
using ESWIPE.Views;
using MvvmHelpers;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class ViewModelBase : MvvmHelpers.BaseViewModel
    {

        //#region Properties
        //private bool _isBusy;
        //public bool IsBusy
        //{
        //    get => _isBusy;
        //    set => SetProperty(ref _isBusy, value);
        //}
        //#endregion

        private bool isAdmin;
        private bool isTeacher;
        private bool isStudent;

        public bool IsAdmin { get => isAdmin; set => SetProperty(ref isAdmin, value); }
        public bool IsTeacher { get => isTeacher; set => SetProperty(ref isTeacher, value); }
        public bool IsStudent { get => isStudent; set => SetProperty(ref isStudent, value); }

        public ViewModelBase()
        {
            MessagingCenter.Subscribe<LoginViewModel>(this, "Admin", (sender) =>
            {
                IsAdmin = true;
                IsTeacher = false;
                IsStudent = false;
            });

            MessagingCenter.Subscribe<LoginViewModel>(this, "Teacher", (sender) =>
            {
                IsAdmin = false;
                IsTeacher = true;
                IsStudent = false;
            });

            MessagingCenter.Subscribe<LoginViewModel>(this, "Student", (sender) =>
            {
                IsAdmin = false;
                IsTeacher = false;
                IsStudent = true;
            });

            MessagingCenter.Subscribe<SplashPage>(this, "Teacher", (sender) =>
            {
                IsAdmin = false;
                IsTeacher = true;
                IsStudent = false;
            });
            
            MessagingCenter.Subscribe<TeacherCreateModulesPage>(this, "Teacher", (sender) =>
            {
                IsAdmin = false;
                IsTeacher = true;
                IsStudent = false;
            });

            MessagingCenter.Subscribe<QuarterViewModel>(this, "Teacher", (sender) =>
            {
                IsAdmin = false;
                IsTeacher = true;
                IsStudent = false;
            });

            MessagingCenter.Subscribe<SplashPage>(this, "Student", (sender) =>
            {
                IsAdmin = false;
                IsTeacher = false;
                IsStudent = true;
            });
            //For retaining the user who removed the app from active apps
            MessagingCenter.Subscribe<SplashPage>(this, "Admin", (sender) =>
            {
                IsAdmin = true;
                IsTeacher = false;
                IsStudent = false;
            });
        }
    }
}
