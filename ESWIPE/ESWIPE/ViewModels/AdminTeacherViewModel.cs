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
    public class AdminTeacherViewModel : BaseViewModel
    {
        #region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly ITeacher _teacherService;
        public ObservableCollection<TeacherModel> Teachers { get; set; } = new ObservableCollection<TeacherModel>();
        //public AsyncCommand<Object> SelectedCommand { get; }
        #endregion

        #region Constructor
        public AdminTeacherViewModel()
        {
            //Title = "Teacher's Data";
            //Teachers = new ObservableCollection<TeacherModel>();
            //SelectedCommand = new AsyncCommand<Object>(Selected);
            _teacherService = DependencyService.Resolve<ITeacher>();
            GetAllTeacher();
        }
        #endregion

        #region Methods
        private void GetAllTeacher()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var teachersList = await _teacherService.GetAllTeacher();

                Device.BeginInvokeOnMainThread(() =>
                {

                    Teachers.Clear();
                    if (teachersList?.Count > 0)
                    {
                        foreach (var employee in teachersList)
                        {
                            Teachers.Add(employee);
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
            GetAllTeacher();
        });

        TeacherModel previouslySelected;
        TeacherModel selectedTeacher;

        public TeacherModel SelectedTeacher
        {
            get => selectedTeacher;
            set /*=> SetProperty(ref selectedTeacher, value);*/
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", "Test", "OK");
                    previouslySelected = value;
                    value = null;
                }

                selectedTeacher = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectedTeacherCommand => new Xamarin.Forms.Command<TeacherModel>(async (teacher) =>
        {
            if (teacher != null)
            {
                var response = await Application.Current.MainPage.DisplayActionSheet("Options!", "Cancel", null, "Update Teacher", "Delete Teacher");

                if (response == "Update Teacher")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new AdminRegisterTeacherPage(teacher));
                }
                else if (response == "Delete Teacher")
                {
                    IsBusy = true;
                    bool deleteResponse = await _teacherService.DeleteTeacher(teacher.Key);
                    if (deleteResponse)
                    {
                        GetAllTeacher();
                    }
                }
            }
        });

        //async Task Selected(Object args)
        //{
        //    var teacher = args as TeacherModel;
        //    if (teacher == null)
        //        return;
        //    SelectedTeacher = null;
        //    await Application.Current.MainPage.DisplayAlert("Selected", teacher.Name, "OK");
        //}

        #endregion
    }
}
