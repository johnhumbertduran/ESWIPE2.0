using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers.Commands;

namespace ESWIPE.ViewModels
{
    class TeacherModulesViewModel : ViewModelBase
    {
        #region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly IModuleService _moduleService;
        public ObservableCollection<ModuleModel> Modules { get; set; } = new ObservableCollection<ModuleModel>();
        #endregion

        #region Constructor
        public TeacherModulesViewModel()
        {
            //Title = "Teacher's Data";
            _moduleService = DependencyService.Resolve<IModuleService>();
            IsRefreshing = true;
            GetAllModule();
        }
        #endregion

        #region Methods
        private void GetAllModule()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var modulesList = await _moduleService.GetAllModule();

                Device.BeginInvokeOnMainThread(() =>
                {

                    Modules.Clear();
                    if (modulesList?.Count > 0)
                    {
                        foreach (var module in modulesList)
                        {
                            Modules.Add(module);
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
            GetAllModule();
        });


        public ICommand SelectedModuleCommand => new Xamarin.Forms.Command<ModuleModel>(async (module) =>
        {
            if (module != null)
            {
                var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Module", "Delete Module");

                if (response == "Update Module")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new TeacherCreateModulesPage(module));
                }
                else if (response == "Delete Module")
                {
                    IsBusy = true;
                    bool deleteResponse = await _moduleService.DeleteModule(module.Key);
                    if (deleteResponse)
                    {
                        GetAllModule();
                    }
                }
            }
        });

        #endregion
    }
}
