using ESWIPE.Models;
using ESWIPE.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ESWIPE.ViewModels
{
    public class QuarterViewModel
    {
        public Command CreateModuleCommand
        {
            get
            {
                return new Command(CreateModule);
            }
        }

        private async void CreateModule()
        {
            MessagingCenter.Send(this, message: "Teacher");
            await Shell.Current.GoToAsync($"//{nameof(TeacherCreateModulesPage)}");
        }

        public string TeacherName;
        public string Quarter1 = "quarter1";
        public string Quarter2 = "quarter2";
        public string Quarter3 = "quarter3";
        public string Quarter4 = "quarter4";

        public QuarterViewModel()
        {
            CheckQuarters();
        }

        

        private async void CheckQuarters()
        {
            if (Preferences.ContainsKey("TeacherName", ""))
            {
                TeacherName = Preferences.Get("TeacherName", "TeacherName");
            }

            var ModuleData = await FirebaseHelper.GetModule(TeacherName);


            if (ModuleData != null)
            {

                var moduleDetails = new ModuleModel()
                {
                    Quarter = ModuleData.Quarter,
                    CreatedBy = ModuleData.CreatedBy
                };

                if (TeacherName == moduleDetails.CreatedBy && moduleDetails.Quarter == Quarter1)
                {
                    Preferences.Set("Quarter1", Quarter1);
                }

                if (TeacherName == moduleDetails.CreatedBy && moduleDetails.Quarter == Quarter2)
                {
                    Preferences.Set("Quarter2", Quarter2);
                }

                if (TeacherName == moduleDetails.CreatedBy && moduleDetails.Quarter == Quarter3)
                {
                    Preferences.Set("Quarter3", Quarter3);
                }

                if (TeacherName == moduleDetails.CreatedBy && moduleDetails.Quarter == Quarter4)
                {
                    Preferences.Set("Quarter4", Quarter4);
                }
            }

        }
    }
}
