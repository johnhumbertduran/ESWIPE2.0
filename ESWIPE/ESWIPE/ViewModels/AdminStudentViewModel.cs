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
    public class AdminStudentViewModel : ViewModelBase
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
        public AdminStudentViewModel()
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
                        foreach (var teacher in studentsList)
                        {
                            Students.Add(teacher);
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


        #endregion
    }
}
