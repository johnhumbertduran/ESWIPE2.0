using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ESWIPE.Models;
using ESWIPE.Services;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class TestRegisterViewModel : ViewModelBase
    {
        #region Properties
        private readonly TestRegisterService _testRegisterService;

        private TestRegister _teacherDetail = new TestRegister();

        public TestRegister TeacherDetail
        {
            get => _teacherDetail;
            set => SetProperty(ref _teacherDetail, value);
        }
        #endregion

        #region Constructor
        public TestRegisterViewModel()
        {
            _testRegisterService = DependencyService.Resolve<TestRegisterService>();
        }
        
        public TestRegisterViewModel(TestRegister teacherResponse)
        {
            _testRegisterService = DependencyService.Resolve<TestRegisterService>();
            TeacherDetail = new TestRegister
            {
                Key = teacherResponse.Key,
                TeacherNumber = teacherResponse.TeacherNumber,
                Name = teacherResponse.Name,
                Section = teacherResponse.Section,
                Username = teacherResponse.Username,
                Password = teacherResponse.Password,
                UserRole = teacherResponse.UserRole
            };
        }
        #endregion

        #region Commands
        public ICommand SaveTeacherCommand => new Command(async () =>
        {
            if (IsBusy) return;
            IsBusy = true;
            bool res = await _testRegisterService.AddorUpdateTeacher(TeacherDetail);
            if (res)
            {
                if (!string.IsNullOrWhiteSpace(TeacherDetail.Key))
                {
                    await Application.Current.MainPage.DisplayAlert("Update Info", "Records Updated Succesfully!", "OK");
                }
                else
                {
                    TeacherDetail = new TestRegister() { };
                    await Application.Current.MainPage.DisplayAlert("Registration Info", "Succesfully Registered!", "OK");
                }
            }
            IsBusy = false;
        });
        #endregion
    }
}
