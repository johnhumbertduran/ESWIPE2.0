﻿using ESWIPE.ViewModels;
using ESWIPE.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ESWIPE
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(Grade7Module1Heading1Page), typeof(Grade7Module1Heading1Page));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async void Admin_FlyoutItem_Appearing(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(AdminTeacherPage)}");
        }

        private async void Teacher_FlyoutItem_Appearing(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(TeacherStudentPage)}");
        }

        private void FlyoutItem_Appearing(object sender, EventArgs e)
        {

        }
    }
}
