using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ESWIPE.ViewModels;
using ESWIPE.Models;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestRegisterPage : ContentPage
    {
        public TestRegisterPage()
        {
            InitializeComponent();
            this.BindingContext = new TestRegisterViewModel();
        }
        public TestRegisterPage(TestRegister testRegister)
        {
            InitializeComponent();
            this.BindingContext = new TestRegisterViewModel(testRegister);
        }
    }
}