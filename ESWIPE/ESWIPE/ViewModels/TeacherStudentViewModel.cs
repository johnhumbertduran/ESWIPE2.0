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
using Xamarin.Essentials;

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

        private string key;
        public string Key
        {
            get { return key; }
            set { SetProperty(ref key, value); }
        }

        private string username;
        public string UserName
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string teachername;
        public string TeacherName
        {
            get { return teachername; }
            set { SetProperty(ref teachername, value); }
        }

        private string section;
        public string Section
        {
            get { return section; }
            set { SetProperty(ref section, value); }
        }

        #region Constructor
        public TeacherStudentViewModel()
        {
            
            _studentService = DependencyService.Resolve<IStudentService>();
            GetAllStudent();

            //if (Preferences.ContainsKey("Key", ""))
            //{
            //    Key = Preferences.Get("Key", "");
            //}

            if (Preferences.ContainsKey("Username", ""))
            {
                UserName = Preferences.Get("Username", "");
            }

            if (Preferences.ContainsKey("TeacherName", ""))
            {
                TeacherName = Preferences.Get("TeacherName", "");
            }

            if (Preferences.ContainsKey("Section", ""))
            {
                Section = Preferences.Get("Section", "");
            }

            Title = TeacherName;

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
