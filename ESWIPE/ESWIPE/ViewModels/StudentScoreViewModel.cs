﻿using System;
using System.Collections.Generic;
using System.Text;

using MvvmHelpers;
using ESWIPE.Models;
using ESWIPE.Services;
using ESWIPE.ViewModels;
using MvvmHelpers.Commands;
using System.Threading.Tasks;

namespace ESWIPE.ViewModels
{
    class StudentScoreViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Student> Student { get; set; }
        public AsyncCommand RefreshCommandStudent { get; }
        public StudentScoreViewModel()
        {
            Title = "Score";
            Student = new ObservableRangeCollection<Student>();
            RefreshCommandStudent = new AsyncCommand(Refresh);
            _ = Task.Run(async () => await Refresh());
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Student.Clear();

            //var students = await StudentService1.GetStudent();

            //Student.AddRange(students);

            IsBusy = false;
        }
    }
}
