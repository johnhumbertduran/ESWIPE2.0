using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using ESWIPE.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers.Commands;
using MvvmHelpers;

namespace ESWIPE.ViewModels
{
    public class Grade7Module1ViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Module1> Module1 { get; set; }

        public AsyncCommand<Module1> SelectedCommand { get; }
        public Grade7Module1ViewModel()
        {
            Module1 = new ObservableRangeCollection<Module1>();
            //CallServerCommand = new AsyncCommand(CallServer);

            Module1.Add(new Module1 { Id = 1, Heading = "Development Team of the Module", Body = "dsds" });
            Module1.Add(new Module1 { Id = 2, Heading = "Introductory Message", Body = "dsds" });
            Module1.Add(new Module1 { Id = 3, Heading = "What I Need to Know", Body = "dsdsdsd" });

            SelectedCommand = new AsyncCommand<Module1>(Selected);
        }

        //Module1 previouslySelected;
        //Module1 selectedHeading;

        //public Module1 SelectedHeading
        //{
        //    get => selectedHeading;
        //    set => SetProperty(ref selectedHeading, value);
        //    //set
        //    //    {
        //    //    if(value != null)
        //    //    {
        //    //        //Application.Current.MainPage.DisplayAlert("Selected", value.Heading, "Ok");
        //    //        var route = $"{nameof(Grade7Module1Heading1Page)}";
        //    //        Shell.Current.GoToAsync(route);
        //    //        previouslySelected = value;
        //    //        value = null;
        //    //    }
        //    //    selectedHeading = value;
        //    //    OnPropertyChanged();
        //    //}
        //}

        async Task Selected(Module1 heading)
        {
            //var heading = args as Module1;
            if (heading == null)
                return;
            //SelectedHeading = null;

            //var route = $"{nameof(Grade7Module1Heading1Page)}";
            await Shell.Current.GoToAsync($"{nameof(Grade7Module1Heading1Page)}?HeadingId={heading.Body}");
            //await Application.Current.MainPage.DisplayAlert("Selected", "Hey", "Ok");
        }

        string data = "";
        public string Data
        {
            get => data;
            set
            {
                data = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
            }
        }

        //public ICommand CallServerCommand { get; }

        //async Task CallServer()
        //{
        //    //var items = new List<string> { "Development Team of the Module", "Introductory Message", "What I Need to Know" };
        //    //Module1.AddRange(items);
        //}
    }
}
