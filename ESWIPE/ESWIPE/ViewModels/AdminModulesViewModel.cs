using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using ESWIPE.Views;
using Xamarin.Forms;
using ESWIPE.Models;
using MvvmHelpers.Commands;
using System.Windows.Input;
using System.Threading.Tasks;
using ESWIPE.Services.Interfaces;

namespace ESWIPE.ViewModels
{
    class AdminModulesViewModel : ViewModelBase
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
        public AdminModulesViewModel()
        {
            //Title = "Teacher's Data";
            _moduleService = DependencyService.Resolve<IModuleService>();
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


        #endregion
    }
}
