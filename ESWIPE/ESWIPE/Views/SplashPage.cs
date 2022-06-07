using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ESWIPE.Views
{
    public class SplashPage : ContentPage
    {

        Image splashImage;

        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
            


            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "es_logo.png",
                WidthRequest = 350,
                HeightRequest = 350
            };
            AbsoluteLayout.SetLayoutFlags(splashImage,
               AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
             new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

            //this.BackgroundColor = Color.FromHex("#429de3");
            this.BackgroundColor = Color.FromHex("#ffffff");
            this.Content = sub;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.ScaleTo(1, 2000); //Time-consuming processes such as initialization
            await splashImage.ScaleTo(0.7, 2000, Easing.Linear);
            await splashImage.ScaleTo(150, 2000, Easing.Linear);
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            //Application.Current.MainPage = new NavigationPage(new TemporaryIndexPage());    //After loading  MainPage it gets Navigated to our new Page
        }
    }
}
