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
    class StudentModulesViewModel : ViewModelBase
    {
        //public ObservableRangeCollection<Module> Module { get; set; }
        public AsyncCommand RefreshCommandModule { get; }
        public StudentModulesViewModel()
        {
            Title = "Modules";
            //Module = new ObservableRangeCollection<Module>();
            //RefreshCommandModule = new AsyncCommand(Refresh);
            //_ = Task.Run(async () => await Refresh());
        }

        //async Task Refresh()
        //{
        //    IsBusy = true;

        //    await Task.Delay(2000);

        //    Module.Clear();

        //    var modules = await ModuleService.GetModule();

        //    Module.AddRange(modules);

        //    IsBusy = false;
        //}
    }
}
