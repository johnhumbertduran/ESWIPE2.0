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
    public class Grade7Module1Heading1ViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Module1> Module1 { get; set; }

        public Grade7Module1Heading1ViewModel()
        {
            Module1 = new ObservableRangeCollection<Module1>();
            //CallServerCommand = new AsyncCommand(CallServer);
        }

       
    }
}
