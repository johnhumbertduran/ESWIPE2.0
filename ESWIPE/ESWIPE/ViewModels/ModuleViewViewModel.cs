using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    class ModuleViewViewModel : ViewModelBase
    {
        //#region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        //private readonly IModuleListService _moduleListService;
        //public ObservableCollection<ModuleListModel> ModuleList { get; set; } = new ObservableCollection<ModuleListModel>();
        //#endregion

        #region Constructor
        public ModuleViewViewModel()
        {
            //Title = "Teacher's Data";
            //_moduleListService = DependencyService.Resolve<IModuleListService>();
            IsRefreshing = true;
            //GetAllModuleList();
        }
        #endregion

        //#region Methods
        //private void GetAllModuleList()
        //{
        //    IsBusy = true;

        //    if (Preferences.ContainsKey("quarter1pass"))
        //    {
                
        //        Task.Run(async () =>
        //        {
        //            var moduleLists = await _moduleListService.GetAllModuleListQ1();

        //            Device.BeginInvokeOnMainThread(() =>
        //            {

        //                ModuleList.Clear();
        //                if (moduleLists?.Count > 0)
        //                {
        //                    foreach (var moduleList in moduleLists)
        //                    {
        //                        ModuleList.Add(moduleList);
        //                    }
        //                }
        //                IsBusy = IsRefreshing = false;
        //            });

        //        });
        //    }


        //    if (Preferences.ContainsKey("quarter2pass"))
        //    {
                
        //        Task.Run(async () =>
        //        {
        //            var moduleLists = await _moduleListService.GetAllModuleListQ2();

        //            Device.BeginInvokeOnMainThread(() =>
        //            {

        //                ModuleList.Clear();
        //                if (moduleLists?.Count > 0)
        //                {
        //                    foreach (var moduleList in moduleLists)
        //                    {
        //                        ModuleList.Add(moduleList);
        //                    }
        //                }
        //                IsBusy = IsRefreshing = false;
        //            });

        //        });
        //    }


        //    if (Preferences.ContainsKey("quarter3pass"))
        //    {
                
        //        Task.Run(async () =>
        //        {
        //            var moduleLists = await _moduleListService.GetAllModuleListQ3();

        //            Device.BeginInvokeOnMainThread(() =>
        //            {

        //                ModuleList.Clear();
        //                if (moduleLists?.Count > 0)
        //                {
        //                    foreach (var moduleList in moduleLists)
        //                    {
        //                        ModuleList.Add(moduleList);
        //                    }
        //                }
        //                IsBusy = IsRefreshing = false;
        //            });

        //        });
        //    }


        //     if (Preferences.ContainsKey("quarter4pass"))
        //    {
                
        //        Task.Run(async () =>
        //        {
        //            var moduleLists = await _moduleListService.GetAllModuleListQ4();

        //            Device.BeginInvokeOnMainThread(() =>
        //            {

        //                ModuleList.Clear();
        //                if (moduleLists?.Count > 0)
        //                {
        //                    foreach (var moduleList in moduleLists)
        //                    {
        //                        ModuleList.Add(moduleList);
        //                    }
        //                }
        //                IsBusy = IsRefreshing = false;
        //            });

        //        });
        //    }

            
        //}
        //#endregion

        //#region Commands

        //public ICommand RefreshCommand => new MvvmHelpers.Commands.Command(() =>
        //{
        //    IsRefreshing = true;
        //    GetAllModuleList();
        //});


        //public ICommand SelectedModuleListCommand => new Xamarin.Forms.Command<ModuleListModel>(async (moduleList) =>
        //{
        //    if (moduleList != null)
        //    {
        //        var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Module", "Delete Module");

        //        if (response == "Update Module")
        //        {
        //            await Application.Current.MainPage.Navigation.PushAsync(new TeacherCreateTitlePage(moduleList));
        //        }
        //        else if (response == "Delete Module")
        //        {
        //            IsBusy = true;
        //            bool deleteResponse = await _moduleListService.DeleteModuleList(moduleList.Key);
        //            if (deleteResponse)
        //            {
        //                GetAllModuleList();
        //            }
        //        }
        //    }
        //});

        //#endregion
    }
}
