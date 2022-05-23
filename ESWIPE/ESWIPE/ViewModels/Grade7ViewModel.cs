using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers.Commands;
using MvvmHelpers;

namespace ESWIPE.ViewModels
{
    public class Grade7ViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Module1> Module1 { get; set; }
        public Grade7ViewModel()
        {
            Module1 = new ObservableRangeCollection<Module1>();
            //CallServerCommand = new AsyncCommand(CallServer);
            Module1.Add(new Module1 { Heading = "Development Team of the Module", Body = "" });
            Module1.Add(new Module1 { Heading = "Introductory Message", Body = "" });
            Module1.Add(new Module1 { Heading = "What I Need to Know", Body = "" });
        }

        public ICommand CallServerCommand { get; }

        //async Task CallServer()
        //{
        //    //var items = new List<string> { "Development Team of the Module", "Introductory Message", "What I Need to Know" };
        //    //Module1.AddRange(items);
        //}
    }
}
