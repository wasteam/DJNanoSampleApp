//---------------------------------------------------------------------------
//
// <copyright file="GalleryListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>12/23/2015 11:24:07 AM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AppStudio.DataProviders.Instagram;
using DJNanoShow.Sections;
using DJNanoShow.ViewModels;
using AppStudio.Uwp;

namespace DJNanoShow.Pages
{
    public sealed partial class GalleryListPage : Page
    {
        public GalleryListPage()
        {
            this.ViewModel = ListViewModel.CreateNew(Singleton<GalleryConfig>.Instance);

            this.InitializeComponent();
            new Microsoft.ApplicationInsights.TelemetryClient().TrackPageView(this.GetType().FullName);
        }

        public ListViewModel ViewModel { get; set; }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();

            base.OnNavigatedTo(e);
        }

    }
}
