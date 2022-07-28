using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using ESWIPE.Views;
using ESWIPE.Services.Implementations;
using ESWIPE.Services.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class AdminRegisterTeacherViewModel : BaseViewModel
    {
        #region Properties
        private readonly ITeacherService _teacherService;

        private TeacherModel _teacherDetail = new TeacherModel();

        public TeacherModel TeacherDetail
        {
            get => _teacherDetail;
            set => SetProperty(ref _teacherDetail, value);
        }
        #endregion

        #region Constructor
        public AdminRegisterTeacherViewModel()
        {
            //Title = "Register Teacher";
            _teacherService = DependencyService.Resolve<ITeacherService>();
        }

        public AdminRegisterTeacherViewModel(TeacherModel teacherResponse)
        {
            _teacherService = DependencyService.Resolve<ITeacherService>();
            TeacherDetail = new TeacherModel
            {
                //TeacherNumber = teacherResponse.TeacherNumber,
                Name = teacherResponse.Name,
                Section = teacherResponse.Section,
                Key = teacherResponse.Key,
                Username = teacherResponse.Username,
                Password = teacherResponse.Password,
                UserRole = teacherResponse.UserRole
            };
        }
        #endregion

        #region Commands

        public Command SaveTeacherCommand
        {
            get
            {
                return new Command(SaveTeacher);
            }
        }

        private async void SaveTeacher()
        {
            if (string.IsNullOrEmpty(_teacherDetail.Name))
            {
                await App.Current.MainPage.DisplayAlert("Empty Name Value", "Please enter Name", "OK");
            }
            else if (string.IsNullOrEmpty(_teacherDetail.Section))
            {
                await App.Current.MainPage.DisplayAlert("Empty Section Value", "Please enter Section", "OK");
            }
            else if (string.IsNullOrEmpty(_teacherDetail.Username))
            {
                await App.Current.MainPage.DisplayAlert("Empty Username Value", "Please enter Username", "OK");
            }
            else if (string.IsNullOrEmpty(_teacherDetail.Password))
            {
                await App.Current.MainPage.DisplayAlert("Empty Password Value", "Please enter Password", "OK");
            }
            else
            {
                if (IsBusy) return;
                IsBusy = true;
                bool res = await _teacherService.AddorUpdateTeacher(TeacherDetail);
                if (res)
                {
                    if (!string.IsNullOrWhiteSpace(TeacherDetail.Key))
                    {
                        await Application.Current.MainPage.DisplayAlert("Update Info", "Records Updated Succesfully!", "OK");

                        await Shell.Current.GoToAsync($"//{nameof(AdminTeacherPage)}");
                    }
                    else
                    {
                        TeacherDetail = new TeacherModel() { };
                        await Application.Current.MainPage.DisplayAlert("Registration Info", "Succesfully Registered!", "OK");
                    }
                }
                IsBusy = false;
            }
        }
        //public ICommand SaveTeacherCommand => new Command(async () =>
        //{
        //    if (IsBusy) return;
        //    IsBusy = true;
        //    bool res = await _teacherService.AddorUpdateTeacher(TeacherDetail);
        //    if (res)
        //    {
        //        if (!string.IsNullOrWhiteSpace(TeacherDetail.Key))
        //        {
        //            await Application.Current.MainPage.DisplayAlert("Update Info", "Records Updated Succesfully!", "OK");

        //            await Shell.Current.GoToAsync($"//{nameof(AdminTeacherPage)}");
        //        }
        //        else
        //        {
        //            TeacherDetail = new TeacherModel() { };
        //            await Application.Current.MainPage.DisplayAlert("Registration Info", "Succesfully Registered!", "OK");
        //        }
        //    }
        //    IsBusy = false;

        //});
        #endregion

    }
}
