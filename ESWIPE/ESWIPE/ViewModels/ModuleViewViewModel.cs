using ESWIPE.Models;
using ESWIPE.Services.Interfaces;
using ESWIPE.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class ModuleViewViewModel : ViewModelBase
    {

        #region Commands
        public ICommand AddCommand => new Command(async () =>
        {
                    //await Shell.Current.GoToAsync($"//{nameof(TeacherCreateTitlePage)}");
            //Application.Current.MainPage = new NavigationPage(new TeacherCreateTitlePage());
            Application.Current.MainPage.Navigation.PopAsync(new TeacherCreateTitlePage());
        });
        #endregion
    }
}
