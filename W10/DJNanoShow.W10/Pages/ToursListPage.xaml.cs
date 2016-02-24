//---------------------------------------------------------------------------
//
// <copyright file="ToursListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>2/24/2016 10:51:56 AM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.DynamicStorage;
using DJNanoShow.Sections;
using DJNanoShow.ViewModels;
using AppStudio.Uwp;

namespace DJNanoShow.Pages
{
    public sealed partial class ToursListPage : Page
    {
		public GroupedListViewModel ViewModel { get; set; }
        public ToursListPage()
        {
			this.ViewModel = GroupedListViewModel.CreateNew(Singleton<ToursConfig>.Instance);

            this.InitializeComponent();

            new Microsoft.ApplicationInsights.TelemetryClient().TrackPageView(this.GetType().FullName);
        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

    }
}
