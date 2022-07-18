using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using Syncfusion.XForms.RichTextEditor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    class ModuleViewViewModel : ViewModelBase, INotifyPropertyChanged
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



        #endregion

        #region Constructor

        //public string Key;
        //public string UserName;
        //public string TeacherName;
        //public string Section;

        //public PhotoPickerViewModel PickPhoto { get; }
        public ICommand ImageInsertCommand { get; set; }

        public ModuleViewViewModel()
        {
            Title = "Teacher's Data";
            _contentService = DependencyService.Resolve<IContentService>();
            IsRefreshing = true;

            //PickPhoto = new PhotoPickerViewModel();
            ImageInsertCommand = new Command<object>(Load);

            if (Preferences.ContainsKey("quarter1pass"))
            {
                GetAllContent1();
                IsBusy = false;
                IsRefreshing = false;
            }
            
            if (Preferences.ContainsKey("quarter2pass"))
            {
                GetAllContent2();
                IsBusy = false;
                IsRefreshing = false;
            }
            
            if (Preferences.ContainsKey("quarter3pass"))
            {
                GetAllContent3();
                IsBusy = false;
                IsRefreshing = false;
            }
            
            if (Preferences.ContainsKey("quarter4pass"))
            {
                GetAllContent4();
                IsBusy = false;
                IsRefreshing = false;
            }

        }

        public ModuleViewViewModel(ContentModel contentResponse)
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

        /// <summary>
        /// Creates a event args for Image Insert
        /// </summary>
        void Load(object obj)
        {
            ImageInsertedEventArgs imageInsertedEventArgs = (obj as ImageInsertedEventArgs);
            this.GetImage(imageInsertedEventArgs);
        }
        /// <summary>
        /// Gets image stream from picker using dependency service.
        /// </summary>
        /// <param name="imageInsertedEventArgs">Event args to be passed for dependency service</param>
        async void GetImage(ImageInsertedEventArgs imageInsertedEventArgs)
        {
            Stream imageStream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            Syncfusion.XForms.RichTextEditor.ImageSource imageSource = new Syncfusion.XForms.RichTextEditor.ImageSource();
            imageSource.ImageStream = imageStream;
            imageInsertedEventArgs.ImageSourceCollection.Add(imageSource);
        }
        /// <summary>
        /// Property changed event of NotifyPropertyChanged interface
        /// </summary>
        new public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///  Property changed raise method of NotifyPropertyChanged interface
        /// </summary>
        /// <param name="propertyname">Property which has been changed</param>
        public void RaisePropertyChange([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        private void GetAllContent1()
        {
            IsBusy = true;

                Task.Run(async () =>
                {
                    var content1 = await _contentService.GetAllContentQ1();

                    Device.BeginInvokeOnMainThread(() =>
                    {

                        Content.Clear();
                        if (content1?.Count > 0)
                        {
                            foreach (var contents in content1)
                            {
                                Content.Add(contents);
                            }
                        }
                        IsBusy = false;
                        IsRefreshing = false;
                    });
                });
        }

        private void GetAllContent2()
        {
            IsBusy = true;

                Task.Run(async () =>
                {
                    var content2 = await _contentService.GetAllContentQ2();

                    Device.BeginInvokeOnMainThread(() =>
                    {

                        Content.Clear();
                        if (content2?.Count > 0)
                        {
                            foreach (var contents in content2)
                            {
                                Content.Add(contents);
                            }
                        }
                        IsBusy = false;
                        IsRefreshing = false;
                    });
                });
        }

        private void GetAllContent3()
        {
            IsBusy = true;

                Task.Run(async () =>
                {
                    var content3 = await _contentService.GetAllContentQ3();

                    Device.BeginInvokeOnMainThread(() =>
                    {

                        Content.Clear();
                        if (content3?.Count > 0)
                        {
                            foreach (var contents in content3)
                            {
                                Content.Add(contents);
                            }
                        }
                        IsBusy = false;
                        IsRefreshing = false;
                    });
                  });
        }

        private void GetAllContent4()
        {
            IsBusy = true;

                Task.Run(async () =>
                {
                    var content4 = await _contentService.GetAllContentQ4();

                    Device.BeginInvokeOnMainThread(() =>
                    {

                        Content.Clear();
                        if (content4?.Count > 0)
                        {
                            foreach (var contents in content4)
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
            if (Preferences.ContainsKey("quarter1pass"))
            {
                GetAllContent1();
            }

            if (Preferences.ContainsKey("quarter2pass"))
            {
                GetAllContent2();
            }

            if (Preferences.ContainsKey("quarter3pass"))
            {
                GetAllContent3();
            }

            if (Preferences.ContainsKey("quarter4pass"))
            {
                GetAllContent4();
            }
        });


        public ICommand SaveContentCommand => new Xamarin.Forms.Command<ModuleListModel>(async (moduleList) =>
        {
            if (!string.IsNullOrWhiteSpace(ContentDetail.TitleContent))
            {
                if (IsBusy) { return; }
                IsBusy = true;
                bool res = await _contentService.AddorUpdateContent(ContentDetail);
                if (res)
                {
                    if (!string.IsNullOrWhiteSpace(ContentDetail.Key))
                    {
                        await Application.Current.MainPage.DisplayAlert("Update Info", "Content Updated Succesfully!", "OK");
                        await Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        ContentDetail = new ContentModel() { };
                        await Application.Current.MainPage.DisplayAlert("Content Info", "Succesfully added!", "OK");
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
                }
                IsBusy = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Content Info", "Content is empty!", "OK");
            }
        });

        #endregion
    }
}
