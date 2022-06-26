using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    class TeacherCreateTitleViewModel : ViewModelBase
    {
        #region Properties
        private readonly IModuleListService _moduleListService;

        private ModuleListModel _moduleListDetail = new ModuleListModel();

        public ModuleListModel ModuleListDetail
        {
            get => _moduleListDetail;
            set => SetProperty(ref _moduleListDetail, value);
        }
        #endregion

        #region Constructor

        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;

        public TeacherCreateTitleViewModel()
        {
            //Title = "Register Teacher";

            if (Preferences.ContainsKey("Key"))
            {
                Key = Preferences.Get("Key", "KeyValue");
            }

            if (Preferences.ContainsKey("Username"))
            {
                UserName = Preferences.Get("Username", "UsernameValue");
            }

            if (Preferences.ContainsKey("TeacherName"))
            {
                TeacherName = Preferences.Get("TeacherName", "TeacherNameValue");
            }

            if (Preferences.ContainsKey("Section"))
            {
                Section = Preferences.Get("Section", "SectionValue");
            }

            _moduleListService = DependencyService.Resolve<IModuleListService>();
        }

        public TeacherCreateTitleViewModel(ModuleListModel moduleListResponse)
        {
            _moduleListService = DependencyService.Resolve<IModuleListService>();
            ModuleListDetail = new ModuleListModel
            {
                Title = moduleListResponse.Title,
                Key = moduleListResponse.Key
            };
        }
        #endregion

        #region Commands
        public ICommand SaveModuleListCommand => new Command(async () =>
        {
            if (IsBusy) { return; }
            IsBusy = true;
            bool res = await _moduleListService.AddorUpdateModuleList(ModuleListDetail);
            if (res)
            {
                if (!string.IsNullOrWhiteSpace(ModuleListDetail.Key))
                {
                    await Application.Current.MainPage.DisplayAlert("Update Info", "Records Updated Succesfully!", "OK");
                    await Shell.Current.GoToAsync("..");
                    //await Application.Current.MainPage.Navigation.PushAsync(new AdminTeacherPage());
                }
                else
                {
                    ModuleListDetail = new ModuleListModel() { };
                    await Application.Current.MainPage.DisplayAlert("Title", "Succesfully Added Title!", "OK");
                    await Shell.Current.GoToAsync($"///{nameof(ModuleViewPage)}", false);
                }
            }
            IsBusy = false;

        });
        #endregion
    }
}
