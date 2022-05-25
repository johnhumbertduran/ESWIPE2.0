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
    class TeacherCreateModulesViewModel : ViewModelBase
    {
        #region Properties
        private readonly IModuleService _moduleService;

        private ModuleModel _moduleDetail = new ModuleModel();

        public ModuleModel ModuleDetail
        {
            get => _moduleDetail;
            set => SetProperty(ref _moduleDetail, value);
        }
        #endregion

        #region Constructor
        public TeacherCreateModulesViewModel()
        {
            //Title = "Register Teacher";
            _moduleService = DependencyService.Resolve<IModuleService>();
        }

        public TeacherCreateModulesViewModel(ModuleModel moduleResponse)
        {
            _moduleService = DependencyService.Resolve<IModuleService>();
            ModuleDetail = new ModuleModel
            {
                DateCreated = moduleResponse.DateCreated,
                CreatedBy = moduleResponse.CreatedBy,
                SubjectCode = moduleResponse.SubjectCode,
                SubjectQuizCode = moduleResponse.SubjectQuizCode,
                SubjectQuizQty = moduleResponse.SubjectQuizQty,
                Key = moduleResponse.Key
            };
        }
        #endregion

        #region Commands
        public ICommand SaveModuleCommand => new Command(async () =>
        {
            if (IsBusy) { return; }
            IsBusy = true;
            bool res = await _moduleService.AddorUpdateModule(ModuleDetail);
            if (res)
            {
                if (!string.IsNullOrWhiteSpace(ModuleDetail.Key))
                {
                    await Application.Current.MainPage.DisplayAlert("Update Info", "Records Updated Succesfully!", "OK");
                    //var route = $"//{nameof(AdminTeacherPage)}";
                    await Shell.Current.GoToAsync($"//{nameof(TeacherModulesPage)}");
                    //await Application.Current.MainPage.Navigation.PushAsync(new AdminTeacherPage());
                }
                else
                {
                    ModuleDetail = new ModuleModel() { };
                    await Application.Current.MainPage.DisplayAlert("Registration Info", "Succesfully Registered!", "OK");
                }
            }
            IsBusy = false;

        });
        #endregion

    }
}
