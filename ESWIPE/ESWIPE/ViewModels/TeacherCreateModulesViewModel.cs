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
    class TeacherCreateModulesViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Module> Module { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Module> RemoveCommand { get; }
        public TeacherCreateModulesViewModel()
        {
            Title = "Create Quiz";
            Module = new ObservableRangeCollection<Module>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Module>(Remove);
        }

        string emptyCreatedByString = "";
        string emptySubjectCodeString = "";
        string emptySubjectQuizCodeString = "";
        string emptySubjectQuizQtyString = "";

        public string CreatedBy
        {
            get => emptyCreatedByString;
            set
            {
                if (value == emptyCreatedByString)
                    return;
                emptyCreatedByString = value;
                OnPropertyChanged();
            }
        }

        public string SubjectCode
        {
            get => emptySubjectCodeString;
            set
            {
                if (value == emptySubjectCodeString)
                    return;
                emptySubjectCodeString = value;
                OnPropertyChanged();
            }
        }

        public string SubjectQuizCode
        {
            get => emptySubjectQuizCodeString;
            set
            {
                if (value == emptySubjectQuizCodeString)
                    return;
                emptySubjectQuizCodeString = value;
                OnPropertyChanged();
            }
        }

        public string SubjectQuizQty
        {
            get => emptySubjectQuizQtyString;
            set
            {
                if (value == emptySubjectQuizQtyString)
                    return;
                emptySubjectQuizQtyString = value;
                OnPropertyChanged();
            }
        }

        async Task Add()
        {
            var createdByText = CreatedBy;
            var subjectCodeText = SubjectCode;
            var subjectQuizCodeText = SubjectQuizCode;
            var subjectQuizQtyText = SubjectQuizQty;

            if (SubjectCode == "")
            {
                await Application.Current.MainPage.DisplayAlert("Subject Code empty!", "Please input Subject Code", "OK");
            }

            if (SubjectQuizCode == "")
            {
                await Application.Current.MainPage.DisplayAlert("Subject Quiz Code empty!", "Please input Subject Quiz Code", "OK");
            }

            if (SubjectQuizQty == "")
            {
                await Application.Current.MainPage.DisplayAlert("Subject Quiz Qty empty!", "Please input Subject Quiz Qty", "OK");
            }

            if ((SubjectCode != "") && (SubjectQuizCode != "") && (SubjectQuizQty != ""))
            {
                await ModuleService.AddModule(createdByText, subjectCodeText, subjectQuizCodeText, subjectQuizQtyText);

                await Application.Current.MainPage.DisplayAlert("Create Module Info", "Succesfully Created Module!", "OK");
                SubjectCode = "";
                SubjectQuizCode = "";
                SubjectQuizQty = "";
                await Refresh();
            }
        }

        async Task Remove(Module module)
        {
            await ModuleService.RemoveModule(module.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Module.Clear();

            var modules = await ModuleService.GetModule();

            Module.AddRange(modules);

            IsBusy = false;
        }
    }
}
