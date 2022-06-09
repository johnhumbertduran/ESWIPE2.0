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
    class TeacherRegisterStudentViewModel : ViewModelBase
    {
        #region Properties
        private readonly IStudentService _studentService;

        private StudentModel _studentDetail = new StudentModel();

        public StudentModel StudentDetail
        {
            get => _studentDetail;
            set => SetProperty(ref _studentDetail, value);
        }
        #endregion

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

        #region Commands
        public ICommand SaveStudentCommand => new Command(async () =>
        {
            if (IsBusy) { return; }
            IsBusy = true;
            bool res = await _studentService.AddorUpdateStudent(StudentDetail);
            if (res)
            {
                if (!string.IsNullOrWhiteSpace(StudentDetail.Key))
                {
                    await Application.Current.MainPage.DisplayAlert("Update Info", "Records Updated Succesfully!", "OK");
                    //var route = $"//{nameof(AdminTeacherPage)}";
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

        });
        #endregion


    }
}
