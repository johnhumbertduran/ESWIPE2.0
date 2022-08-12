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
    public partial class IdentificationAddAnswerPage : ContentPage
    {
        public IdentificationAddAnswerPage()
        {
            InitializeComponent();
        }
        private void Clear_Clicked(object sender, EventArgs e)
        {
            //QuizCode.Text = "";
            //QuestionCode.Text = "";
            Answer.Text = "";
            Answer.Focus();
        }
    }
}