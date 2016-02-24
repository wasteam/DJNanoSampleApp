//---------------------------------------------------------------------------
//
// <copyright file="AboutPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>2/24/2016 10:51:56 AM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Pages
{
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            AboutThisAppModel = new AboutThisAppViewModel();

            this.InitializeComponent();
            new Microsoft.ApplicationInsights.TelemetryClient().TrackPageView(this.GetType().FullName);
        }

        public AboutThisAppViewModel AboutThisAppModel { get; private set; }
    }
}
