using ESWIPE.Services.Interfaces;
using Syncfusion.XForms.RichTextEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    //internal class PhotoPickerViewModel
    //{
    //}

    /// <summary>
    /// Represents a view model class of the application
    /// </summary>
    public class PhotoPickerViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Insert image command property
        /// </summary>
        public ICommand ImageInsertCommand { get; set; }

        public PhotoPickerViewModel()
        {
            ImageInsertCommand = new Command<object>(Load);
            return;
        }
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
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///  Property changed raise method of NotifyPropertyChanged interface
        /// </summary>
        /// <param name="propertyname">Property which has been changed</param>
        public void RaisePropertyChange([CallerMemberName] string propertyname = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }

}
