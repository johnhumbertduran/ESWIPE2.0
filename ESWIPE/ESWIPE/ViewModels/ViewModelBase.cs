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
        //private bool isGrade7;
        //private bool isGrade8;
        //private bool isGrade9;
        //private bool isGrade10;
        //private bool isGrade11;
        //private bool isGrade12;

        public bool IsAdmin { get => isAdmin; set => SetProperty(ref isAdmin, value); }
        public bool IsTeacher { get => isTeacher; set => SetProperty(ref isTeacher, value); }
        public bool IsStudent { get => isStudent; set => SetProperty(ref isStudent, value); }
        //public bool IsGrade7 { get => isGrade7; set => SetProperty(ref isGrade7, value); }
        //public bool IsGrade8 { get => isGrade8; set => SetProperty(ref isGrade8, value); }
        //public bool IsGrade9 { get => isGrade9; set => SetProperty(ref isGrade9, value); }
        //public bool IsGrade10 { get => isGrade10; set => SetProperty(ref isGrade10, value); }
        //public bool IsGrade11 { get => isGrade11; set => SetProperty(ref isGrade11, value); }
        //public bool IsGrade12 { get => isGrade12; set => SetProperty(ref isGrade12, value); }

        public ViewModelBase()
        {
            //MessagingCenter.Subscribe<LoginPage>(this, "admin", (sender) =>
            //{
            //    IsAdmin = true;
            //    IsTeacher = false;
            //    IsStudent = false;
            //});

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
        }
    }
}
