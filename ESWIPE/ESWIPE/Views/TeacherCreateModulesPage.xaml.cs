using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ESWIPE.Models;
using ESWIPE.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherCreateModulesPage : ContentPage
    {

        public TeacherCreateModulesPage()
        {
            InitializeComponent();
            CreateModuleLabel.Text = "Create Module";
            BindingContext = new TeacherCreateModulesViewModel();
        }
        public TeacherCreateModulesPage(ModuleModel module)
        {
            InitializeComponent();
            CreateModuleLabel.Text = "Update Module";
            BindingContext = new TeacherCreateModulesViewModel(module);
        }
    }
}