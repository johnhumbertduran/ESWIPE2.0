using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.RichTextEditor;

using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Reflection;
using System.IO;
using ESWIPE.Services.Interfaces;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TitleContentViewPage : ContentPage
    {
        public TitleContentViewPage()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Method for generating RTF and load HTML to another RTE. 
        /// </summary>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //"App" is the class of Portable project
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            Stream inputStream = assembly.GetManifestResourceStream(inputFilePath);
            WordDocument document = new WordDocument(inputStream, FormatType.Docx);
            //To-Do some manipulation
            //To-Do some manipulation
            //Saving the Word document
            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);
            stream.Position = 0;
            //Save the stream as a file in the device and invoke it for viewing
            Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Result.docx", "application/msword", stream);
            //Closes the document
            document.Close();
            //Please download the helper files from the below link to save the stream as file and open the file for viewing in Xamarin platform
            //https://help.syncfusion.com/file-formats/docio/create-word-document-in-xamarin#helper-files-for-xamarin
        }
    }
}