using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ESWIPE.ViewModels;
using Xamarin.Forms.Xaml;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Q1ModulePage : ContentPage
    {
        //readonly QuarterViewModel quarterViewModel;
        public Q1ModulePage()
        {
            //quarterViewModel = new QuarterViewModel();
            InitializeComponent();
            //BindingContext = quarterViewModel;
        }

        private async void CreateModuleButton(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(TeacherCreateModulesPage)}");
            //Application.Current.MainPage = new NavigationPage(new TeacherCreateModulesPage());
        }
    }
}