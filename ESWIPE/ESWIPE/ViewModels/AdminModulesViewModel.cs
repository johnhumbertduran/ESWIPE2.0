using System;
using System.Collections.Generic;
using System.Text;

using MvvmHelpers;
using ESWIPE.Models;
using ESWIPE.Services;
using ESWIPE.ViewModels;
using MvvmHelpers.Commands;
using System.Threading.Tasks;

namespace ESWIPE.ViewModels
{
    class AdminModulesViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Module> Module { get; set; }
        public AsyncCommand RefreshCommandModule { get; }
        public AdminModulesViewModel()
        {
            Title = "Modules's Data";
            Module = new ObservableRangeCollection<Module>();
        }
    }
}
