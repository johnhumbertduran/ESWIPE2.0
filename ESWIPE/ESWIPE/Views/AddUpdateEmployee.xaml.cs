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
    public partial class AddUpdateEmployee : ContentPage
    {
        public AddUpdateEmployee()
        {
            InitializeComponent();
            BindingContext = new AddUpdateEmployeePageViewModel();
        }

        public AddUpdateEmployee(EmployeeModel employee)
        {
            InitializeComponent();
            BindingContext = new AddUpdateEmployeePageViewModel(employee);
        }
    }
}