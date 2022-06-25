using ESWIPE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModuleViewPage : ContentPage
    {
        public ModuleViewPage()
        {
            InitializeComponent();
        }

        private void Add_Title(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"//{nameof(TeacherCreateTitlePage)}");
            Navigation.PushAsync(new TeacherCreateTitlePage());
        }
    }
}