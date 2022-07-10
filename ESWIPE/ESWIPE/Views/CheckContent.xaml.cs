using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ESWIPE.Views;


using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;

namespace ESWIPE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckContent : ContentPage
    {
        public CheckContent()
        {
            InitializeComponent();

            
        }

        private async void Cancel_Button(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"///{nameof(TitleContentViewPage)}", false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //string RTE;

            //RTE = myListView.ItemsSource;

            var newHtmlStream = new MemoryStream();
            var writer = new StreamWriter(newHtmlStream, Encoding.UTF8);
            writer.Write(myListView.ItemsSource);
            writer.Flush();
            newHtmlStream.Position = 0;

            WordDocument doc = new WordDocument(newHtmlStream, FormatType.Rtf, XHTMLValidationType.None);
            //writer.Close();
            HTMLExport export = new HTMLExport();
            MemoryStream htmlStream = new MemoryStream();
            // Saving the Word document as RTF
            export.SaveAsXhtml(doc, htmlStream, Encoding.UTF8);
            doc.Close();
            htmlStream.Position = 0;
            // Loading equivalent HTML obtained from DocIO
            //myListView.ItemsSource = IgnoreVoidElementsInHTML(System.Text.Encoding.UTF8.GetString(htmlStream.ToArray()));
            htmlStream.Dispose();


            

        }

        //string IgnoreVoidElementsInHTML(string inputString)
        //{
        //    inputString = inputString.Replace("<meta http-equiv=\"Content-Type\" content=\"application/xhtml+xml; charset=utf-8\">", "");
        //    inputString = inputString.Replace("<br>", "<br/>");
        //    inputString = inputString.Replace("\n", "");
        //    inputString = inputString.Replace("\r", "");
        //    inputString = inputString.Replace("<title></title>", "");
        //    inputString = inputString.Replace("﻿<?xml version=\"1.0\" encoding=\"utf-8\"?><!DOCTYPE html PUBLIC" +
        //        " \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">", "");
        //    return inputString;
        //}
    }
}