using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers.Commands;

namespace ESWIPE.ViewModels
{
    class TeacherStudentViewModel : ViewModelBase
    {
        #region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly IStudentService _studentService;
        public ObservableCollection<StudentModel> Students { get; set; } = new ObservableCollection<StudentModel>();
        #endregion

        #region Constructor
        public TeacherStudentViewModel()
        {
            //Title = "Teacher's Data";
            _studentService = DependencyService.Resolve<IStudentService>();
            GetAllStudent();
        }
        #endregion

        #region Methods
        private void GetAllStudent()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var studentsList = await _studentService.GetAllStudent();

                Device.BeginInvokeOnMainThread(() =>
                {

                    Students.Clear();
                    if (studentsList?.Count > 0)
                    {
                        foreach (var student in studentsList)
                        {
                            Students.Add(student);
                        }
                    }
                    IsBusy = IsRefreshing = false;
                });

            });
        }
        #endregion

        #region Commands

        public ICommand RefreshCommand => new MvvmHelpers.Commands.Command(() =>
        {
            IsRefreshing = true;
            GetAllStudent();
        });


        public ICommand SelectedStudentCommand => new Xamarin.Forms.Command<StudentModel>(async (student) =>
        {
            if (student != null)
            {
                var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Student", "Delete Student");

                if (response == "Update Student")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new TeacherRegisterStudentPage(student));
                }
                else if (response == "Delete Student")
                {
                    IsBusy = true;
                    bool deleteResponse = await _studentService.DeleteStudent(student.Key);
                    if (deleteResponse)
                    {
                        GetAllStudent();
                    }
                }
            }
        });

        #endregion
    }
}
