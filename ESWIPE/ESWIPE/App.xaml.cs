using ESWIPE.Views;
using MonkeyCache.FileStore;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ESWIPE.Services;
using Xamarin.Essentials;
using ESWIPE.Services.Implementations;
using ESWIPE.Services.Interfaces;

namespace ESWIPE
{
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjY1MDg2QDMyMzAyZTMyMmUzMGppMmljVlY1cEVwdEtXRGtNSkM2M0dFVlhJOGVMTVhISllON09RWVcvNDg9");

            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            //MainPage = new LoginPage();
            //Barrel.ApplicationId = AppInfo.PackageName;

            MainPage = new AppShell();

            DependencyService.Register<IModuleListService, ModuleListService>();
            DependencyService.Register<ITeacherService, TeacherService>();
            DependencyService.Register<IStudentService, StudentService>();
            DependencyService.Register<IContentService, ContentService>();
            DependencyService.Register<IModuleService, ModuleService>();
            DependencyService.Register<IAnswerService, AnswerService>();
            DependencyService.Register<IQuizService, QuizService>();
            //MainPage = new TemporaryIndexPage();
            //DependencyService.Register<IEmployeeService, EmployeeService>();

            //if (!string.IsNullOrEmpty(Preferences.Get("MyFirebaseRefreshToken", "")))
            //{
            //}
            //else
            //{
            //    //Shell.Current.GoToAsync($"//{nameof(StudentPage)}");
            //    MainPage = new AppShell();
            //}

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
