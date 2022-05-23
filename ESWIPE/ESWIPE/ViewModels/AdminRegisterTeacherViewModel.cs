using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using ESWIPE.Services.Implementations;
using ESWIPE.Services.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class AdminRegisterTeacherViewModel : BaseViewModel
    {
        #region Properties
        private readonly ITeacher _teacherService;

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
            _teacherService = DependencyService.Resolve<ITeacher>();
        }

        public AdminRegisterTeacherViewModel(TeacherModel teacherResponse)
        {
            _teacherService = DependencyService.Resolve<ITeacher>();
            TeacherDetail = new TeacherModel
            {
                TeacherNumber = teacherResponse.TeacherNumber,
                Name = teacherResponse.Name,
                Section = teacherResponse.Section,
                Username = teacherResponse.Username,
                Password = teacherResponse.Password,
                UserRole = teacherResponse.UserRole,
                Key = teacherResponse.Key
            };
        }
        #endregion

        #region Commands
        public ICommand SaveTeacherCommand => new Command(async () =>
        {
            if (IsBusy) { return; }
            IsBusy = true;
            bool res = await _teacherService.AddorUpdateTeacher(TeacherDetail);
            if (res)
            {
                if (!string.IsNullOrWhiteSpace(TeacherDetail.Key))
                {
                    await Application.Current.MainPage.DisplayAlert("Update Info", "Records Updated Succesfully!", "OK");
                }
                else
                {
                    TeacherDetail = new TeacherModel() { };
                    await Application.Current.MainPage.DisplayAlert("Registration Info", "Succesfully Registered!", "OK");
                }
            }
            IsBusy = false;
            //if (TeacherName == "")
            //{
            //    await Application.Current.MainPage.DisplayAlert("Name empty!", "Please input Name", "OK");
            //}

            //if (TeacherSection == "")
            //{
            //    await Application.Current.MainPage.DisplayAlert("Section empty!", "Please input Section", "OK");
            //}

            //if ((TeacherName != "") && (TeacherSection != ""))
            //{

            //    await TeacherService.AddTeacher(teacherNameText, teacherSectionText, usernameText, passwordText, userRoleText);
            //    TeacherName = "";
            //    TeacherSection = "";
            //    await Application.Current.MainPage.DisplayAlert("Registration Info", "Succesfully Registered!", "OK");
            //    //await Refresh();
            //}

        });
        #endregion

        //async Task Remove(Teacher teacher)
        //{
        //    await TeacherService.RemoveTeacher(teacher.Id);
        //    await Refresh();
        //}

        //async Task Refresh()
        //{
        //    IsBusy = true;

        //    await Task.Delay(2000);

        //    Teacher.Clear();

        //    var teachersLocal = await TeacherService.GetTeacher();
        //    Teacher.AddRange(teachersLocal);

        //    IsBusy = false;
        //}

    }
}
