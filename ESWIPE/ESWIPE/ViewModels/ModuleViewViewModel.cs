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
        #region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly IModuleListService _moduleListService;
        public ObservableCollection<ModuleListModel> ModuleList { get; set; } = new ObservableCollection<ModuleListModel>();
        #endregion

        #region Constructor
        public ModuleViewViewModel()
        {
            Title = "Teacher's Data";
            _moduleListService = DependencyService.Resolve<IModuleListService>();
            IsRefreshing = true;
            

            if (Preferences.ContainsKey("quarter1pass"))
            {
                GetAllModuleList1();
                IsBusy = false;
                IsRefreshing = false;
            }
            
            if (Preferences.ContainsKey("quarter2pass"))
            {
                GetAllModuleList2();
                IsBusy = false;
                IsRefreshing = false;
            }
            
            if (Preferences.ContainsKey("quarter3pass"))
            {
                GetAllModuleList3();
                IsBusy = false;
                IsRefreshing = false;
            }
            
            if (Preferences.ContainsKey("quarter4pass"))
            {
                GetAllModuleList4();
                IsBusy = false;
                IsRefreshing = false;
            }

        }
        #endregion

        #region Methods
        private void GetAllModuleList1()
        {
            IsBusy = true;

                Task.Run(async () =>
                {
                    var moduleList1 = await _moduleListService.GetAllModuleListQ1();

                    Device.BeginInvokeOnMainThread(() =>
                    {

                        ModuleList.Clear();
                        if (moduleList1?.Count > 0)
                        {
                            foreach (var moduleList in moduleList1)
                            {
                                ModuleList.Add(moduleList);
                            }
                        }
                        IsBusy = false;
                        IsRefreshing = false;
                    });
                });
        }

        private void GetAllModuleList2()
        {
            IsBusy = true;

                Task.Run(async () =>
                {
                    var moduleList2 = await _moduleListService.GetAllModuleListQ2();

                    Device.BeginInvokeOnMainThread(() =>
                    {

                        ModuleList.Clear();
                        if (moduleList2?.Count > 0)
                        {
                            foreach (var moduleList in moduleList2)
                            {
                                ModuleList.Add(moduleList);
                            }
                        }
                        IsBusy = false;
                        IsRefreshing = false;
                    });
                });
        }

        private void GetAllModuleList3()
        {
            IsBusy = true;

                Task.Run(async () =>
                {
                    var moduleList3 = await _moduleListService.GetAllModuleListQ3();

                    Device.BeginInvokeOnMainThread(() =>
                    {

                        ModuleList.Clear();
                        if (moduleList3?.Count > 0)
                        {
                            foreach (var moduleList in moduleList3)
                            {
                                ModuleList.Add(moduleList);
                            }
                        }
                        IsBusy = false;
                        IsRefreshing = false;
                    });
                  });
        }

        private void GetAllModuleList4()
        {
            IsBusy = true;

                Task.Run(async () =>
                {
                    var moduleLists = await _moduleListService.GetAllModuleListQ4();

                    Device.BeginInvokeOnMainThread(() =>
                    {

                        ModuleList.Clear();
                        if (moduleLists?.Count > 0)
                        {
                            foreach (var moduleList in moduleLists)
                            {
                                ModuleList.Add(moduleList);
                            }
                        }
                        IsBusy = false;
                        IsRefreshing = false;
                    });
                });
        }


        #endregion

        #region Commands

        public ICommand RefreshCommand => new MvvmHelpers.Commands.Command(() =>
        {
            IsRefreshing = true;
            if (Preferences.ContainsKey("quarter1pass"))
            {
                GetAllModuleList1();
            }

            if (Preferences.ContainsKey("quarter2pass"))
            {
                GetAllModuleList2();
            }

            if (Preferences.ContainsKey("quarter3pass"))
            {
                GetAllModuleList3();
            }

            if (Preferences.ContainsKey("quarter4pass"))
            {
                GetAllModuleList4();
            }
        });


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
        //                if (Preferences.ContainsKey("quarter1pass"))
        //                {
        //                    GetAllModuleList1();
        //                }

        //                if (Preferences.ContainsKey("quarter2pass"))
        //                {
        //                    GetAllModuleList2();
        //                }

        //                if (Preferences.ContainsKey("quarter3pass"))
        //                {
        //                    GetAllModuleList3();
        //                }

        //                if (Preferences.ContainsKey("quarter4pass"))
        //                {
        //                    GetAllModuleList4();
        //                }
        //            }
        //        }
        //    }
        //});

        #endregion
    }
}
