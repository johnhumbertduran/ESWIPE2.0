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
    public partial class TeacherCreateQuizPage : ContentPage
    {
        public TeacherCreateQuizPage()
        {
            InitializeComponent();
            CreateQuizLabel.Text = "Create Quiz";
            BindingContext = new TeacherCreateQuizViewModel();
        }
        
        public TeacherCreateQuizPage(QuizModel quiz)
        {
            InitializeComponent();
            CreateQuizLabel.Text = "Update Quiz";
            BindingContext = new TeacherCreateQuizViewModel(quiz);
        }
    }
}