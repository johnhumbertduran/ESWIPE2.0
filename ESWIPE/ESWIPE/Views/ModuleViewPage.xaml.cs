﻿using ESWIPE.ViewModels;
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
            BindingContext = new ModuleViewViewModel();
        }
    }
}