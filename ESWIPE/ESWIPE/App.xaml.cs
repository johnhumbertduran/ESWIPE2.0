using ESWIPE.Views;
using MonkeyCache.FileStore;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ESWIPE.Services;
using ESWIPE.Interfaces;
using Xamarin.Essentials;
using ESWIPE.Services.Implementations;
using ESWIPE.Services.Interfaces;

namespace ESWIPE
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            //MainPage = new LoginPage();
            //Barrel.ApplicationId = AppInfo.PackageName;

            MainPage = new AppShell();
            //MainPage = new TemporaryIndexPage();
            //DependencyService.Register<ITeacher, TeacherService>();
            //DependencyService.Register<IEmployeeService, EmployeeService>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
