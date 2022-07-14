using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using Xamarin.Forms;
using System.Windows.Input;
using ESWIPE.Services.Interfaces;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace ESWIPE.ViewModels
{
    class TitleContentViewViewModel : ViewModelBase
    {
        #region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }
        private readonly IContentService _contentService;

        public ObservableCollection<ContentModel> Content { get; set; } = new ObservableCollection<ContentModel>();

        private ContentModel _contentDetail = new ContentModel();

        public ContentModel ContentDetail
        {
            get => _contentDetail;
            set => SetProperty(ref _contentDetail, value);
        }

        //private string rte;
        //public string Rte
        //{
        //    get { return rte; }
        //    set { SetProperty(ref rte, value); }
        //}

        #endregion

        #region Constructor

        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;

        public TitleContentViewViewModel()
        {
            //Title = "Register Student";

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

            _contentService = DependencyService.Resolve<IContentService>();
        }

        public TitleContentViewViewModel(ContentModel contentResponse)
        {
            //Title = "Update Content";
            _contentService = DependencyService.Resolve<IContentService>();
            ContentDetail = new ContentModel
            {
                DateCreated = contentResponse.DateCreated,
                CreatedBy = contentResponse.CreatedBy,
                Quarter = contentResponse.Quarter,
                //Title = contentResponse.Title,
                TitleContent = contentResponse.TitleContent,
                SubjectCode = contentResponse.SubjectCode,
                Key = contentResponse.Key
            };
        }
        #endregion

        #region Methods
        private void GetAllContent()
        {
            IsBusy = true;

            Task.Run(async () =>
            {
                var content = await _contentService.GetAllContent();

                Device.BeginInvokeOnMainThread(() =>
                {

                    Content.Clear();
                    if (content?.Count > 0)
                    {
                        foreach (var contents in content)
                        {
                            Content.Add(contents);
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
            GetAllContent();
        });

        public ICommand SaveContentCommand => new Command(async () =>
        {
            if (IsBusy) { return; }
            IsBusy = true;
            bool res = await _contentService.AddorUpdateContent(ContentDetail);
            if (res)
            {
                if (!string.IsNullOrWhiteSpace(ContentDetail.Key))
                {
                    await Application.Current.MainPage.DisplayAlert("Update Info", "Content Updated Succesfully!", "OK");

                    //await Shell.Current.GoToAsync("..");

                }
                else
                {
                    //byte[] data = Encoding.ASCII.GetBytes(ContentDetail.TitleContent);
                    //MemoryStream outputStream = new MemoryStream(data, 0, data.Length);
                    //await StoreRTEToTextFormat(outputStream);

                    //Preferences.Set("ContentData", data);
                    //ContentDetail.TitleContent = "";
                    ContentDetail = new ContentModel() { };
                    await Application.Current.MainPage.DisplayAlert("Content Info", "Succesfully added!", "OK");
                }
            }
            IsBusy = false;

        });
        #endregion
    }
}
