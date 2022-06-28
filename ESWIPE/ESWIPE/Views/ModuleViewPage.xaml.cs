using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MvvmHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModuleViewPage : ContentPage
    {
        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;

        public bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => ObservableObject.SetProperty(ref _isRefreshing, value);

        }
        readonly ModuleViewViewModel moduleViewViewModel;

        public ModuleViewPage()
        {
            InitializeComponent();
            moduleViewViewModel = new ModuleViewViewModel();
            BindingContext = moduleViewViewModel;
            _moduleListService = DependencyService.Resolve<IModuleListService>();
            GetAllModuleList();
        }

        private void Add_Title(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"//{nameof(TeacherCreateTitlePage)}");
            Navigation.PushAsync(new TeacherCreateTitlePage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Send(this, message: "Teacher");

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

            if (Preferences.ContainsKey("quarter1pass"))
            {
                Title = "Quarter 1";
            }

            if (Preferences.ContainsKey("quarter2pass"))
            {
                Title = "Quarter 2";
            }

            if (Preferences.ContainsKey("quarter3pass"))
            {
                Title = "Quarter 3";
            }

            if (Preferences.ContainsKey("quarter4pass"))
            {
                Title = "Quarter 4";
            }


        }

        private async void Cancel_Button(object sender, EventArgs e)
        {
            if (Preferences.ContainsKey("quarter1pass"))
            {
                Preferences.Remove("quarter1pass");
                await Shell.Current.GoToAsync($"///{nameof(Q1ModulePage)}", false);
            }

            if (Preferences.ContainsKey("quarter2pass"))
            {
                Preferences.Remove("quarter2pass");
                await Shell.Current.GoToAsync($"///{nameof(Q2ModulePage)}", false);
            }

            if (Preferences.ContainsKey("quarter3pass"))
            {
                Preferences.Remove("quarter3pass");
                await Shell.Current.GoToAsync($"///{nameof(Q3ModulePage)}", false);
            }

            if (Preferences.ContainsKey("quarter4pass"))
            {
                Preferences.Remove("quarter4pass");
                await Shell.Current.GoToAsync($"///{nameof(Q4ModulePage)}", false);
            }
        }


        #region Properties
        

        private readonly IModuleListService _moduleListService;
        public ObservableCollection<ModuleListModel> ModuleList { get; set; } = new ObservableCollection<ModuleListModel>();
        #endregion

        #region Constructor
        #endregion

        #region Methods
        private void GetAllModuleList()
        {
            IsBusy = true;

            if (Preferences.ContainsKey("quarter1pass"))
            {

                Task.Run(async () =>
                {
                    var moduleLists = await _moduleListService.GetAllModuleListQ1();

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
                        IsBusy = IsRefreshing = false;
                    });

                });
            }


            if (Preferences.ContainsKey("quarter2pass"))
            {

                Task.Run(async () =>
                {
                    var moduleLists = await _moduleListService.GetAllModuleListQ2();

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
                        IsBusy = IsRefreshing = false;
                    });

                });
            }


            if (Preferences.ContainsKey("quarter3pass"))
            {

                Task.Run(async () =>
                {
                    var moduleLists = await _moduleListService.GetAllModuleListQ3();

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
                        IsBusy = IsRefreshing = false;
                    });

                });
            }


            if(Preferences.ContainsKey("quarter4pass"))
            {

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
                        IsBusy = IsRefreshing = false;
                    });

                });
            }


        }
        #endregion

        #region Commands

        public ICommand RefreshCommand => new MvvmHelpers.Commands.Command(() =>
        {
            IsRefreshing = true;
            GetAllModuleList();
        });


        public ICommand SelectedModuleListCommand => new Xamarin.Forms.Command<ModuleListModel>(async (moduleList) =>
        {
            if (moduleList != null)
            {
                var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Module", "Delete Module");

                if (response == "Update Module")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new TeacherCreateTitlePage(moduleList));
                }
                else if (response == "Delete Module")
                {
                    IsBusy = true;
                    bool deleteResponse = await _moduleListService.DeleteModuleList(moduleList.Key);
                    if (deleteResponse)
                    {
                        GetAllModuleList();
                    }
                }
            }
        });

        #endregion



    }
}