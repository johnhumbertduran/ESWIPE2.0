﻿using ESWIPE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherStudentPage : ContentPage
    {
        

        readonly TeacherStudentViewModel teacherStudentViewModel;
        public TeacherStudentPage()
        {
            InitializeComponent();
            teacherStudentViewModel = new TeacherStudentViewModel();
            BindingContext = teacherStudentViewModel;
        }
        public string Key;
        public string UserName;
        public string TeacherName;
        public string Section;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Preferences.ContainsKey("Key"))
            {
                Key = Preferences.Get("Key", "KeyValue");
            }

            if (Preferences.ContainsKey("Username"))
            {
                UserName = Preferences.Get("Username", "UsernameValue");
            }

            if (Preferences.ContainsKey("TeacherName"))
            {
                TeacherName = Preferences.Get("TeacherName", "TeacherNameValue");
            }

            if (Preferences.ContainsKey("Section"))
            {
                Section = Preferences.Get("Section", "SectionValue");
            }
            Title = TeacherName;
            BindingContext = new TeacherStudentViewModel();
        }
    }
}