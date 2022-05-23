using System;
using System.Collections.Generic;
using System.Text;

using MvvmHelpers;
using MvvmHelpers.Commands;
using ESWIPE.Views;
using ESWIPE.Models;
using ESWIPE.Services;
using ESWIPE.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    class TeacherRegisterStudentViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Student> Student { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Student> RemoveCommand { get; }
        public TeacherRegisterStudentViewModel()
        {
            Title = "Register Student";
            Student = new ObservableRangeCollection<Student>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Student>(Remove);
        }

        string emptyNameString = "";
        string emptyYearString = "";
        string emptySectionString = "";

        public string StudentName
        {
            get => emptyNameString;
            set
            {
                if (value == emptyNameString)
                    return;
                emptyNameString = value;
                OnPropertyChanged();
            }
        }

        public string StudentYear
        {
            get => emptyYearString;
            set
            {
                if (value == emptyYearString)
                    return;
                emptyYearString = value;
                OnPropertyChanged();
            }
        }

        public string StudentSection
        {
            get => emptySectionString;
            set
            {
                if (value == emptySectionString)
                    return;
                emptySectionString = value;
                OnPropertyChanged();
            }
        }

        async Task Add()
        {
            var studentNameText = StudentName;
            var studentYearText = StudentYear;
            var studentSectionText = StudentSection;
            var usernameText = StudentName;
            var passwordText = "";
            var userRoleText = "Teacher";

            if (StudentName == "")
            {
                await Application.Current.MainPage.DisplayAlert("Name empty!", "Please input Name", "OK");
            }
            
            if (StudentYear == "")
            {
                await Application.Current.MainPage.DisplayAlert("Year empty!", "Please input Year", "OK");
            }
             
            if (StudentSection == "")
            {
                await Application.Current.MainPage.DisplayAlert("Section empty!", "Please input Section", "OK");
            }
            
            if ((StudentName != "") && (StudentYear != "") && (StudentSection != ""))
            {
                await StudentService.AddStudent(studentNameText, studentYearText, studentSectionText, usernameText, passwordText, userRoleText);

                await Application.Current.MainPage.DisplayAlert("Registration Info", "Succesfully Registered!", "OK");
                StudentName = "";
                StudentYear = "";
                StudentSection = "";
                await Refresh();
            }
        }

        async Task Remove(Student student)
        {
            await StudentService.RemoveStudent(student.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Student.Clear();

            var students = await StudentService.GetStudent();

            Student.AddRange(students);

            IsBusy = false;
        }

    }
}
