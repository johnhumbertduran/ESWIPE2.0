using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Reflection;
using System.IO;
using Firebase.Database;
using Firebase.Database.Query;

namespace ESWIPE.ViewModels
{
    class CheckContentViewModel : ViewModelBase
    {
        #region Properties
        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly IContentService _contentService;
        public ObservableCollection<ContentModel> Contents { get; set; } = new ObservableCollection<ContentModel>();
        #endregion




        #region Constructor
        public CheckContentViewModel()
        {

            _contentService = DependencyService.Resolve<IContentService>();
            GetAllContent();


            //Title = TeacherName;

        }

        #endregion


        #region Methods
        private void GetAllContent()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var contentsList = await _contentService.GetAllContent();

                

                Device.BeginInvokeOnMainThread(() =>
                {
                    Contents.Clear();

                    if (contentsList?.Count > 0)
                    {

                        foreach (var content in contentsList)
                        {
                            Contents.Add(content);
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
            GetAllContent();
        });


        public ICommand SelectedContentCommand => new Xamarin.Forms.Command<ContentModel>(async (content) =>
        {
            if (content != null)
            {
                var response = await Application.Current.MainPage.DisplayActionSheet("I would like to", "Cancel", null, "Update Student", "Delete Student");

                if (response == "Update Student")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new TitleContentViewPage(content));
                }
                else if (response == "Delete Student")
                {
                    IsBusy = true;
                    bool deleteResponse = await _contentService.DeleteContent(content.Key);
                    if (deleteResponse)
                    {
                        GetAllContent();
                    }
                }
            }
        });

        #endregion
    }
}
