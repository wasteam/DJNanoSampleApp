//---------------------------------------------------------------------------
//
// <copyright file="FavoritesPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>2/24/2016 10:51:56 AM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AppStudio.Uwp;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Pages
{
    public sealed partial class FavoritesPage : Page
    {
        public FavoritesPage()
        {
            this.ViewModel = new FavoritesViewModel();         
            this.InitializeComponent();
            new Microsoft.ApplicationInsights.TelemetryClient().TrackPageView(this.GetType().FullName);
        }
        public FavoritesViewModel ViewModel { get; private set; }
		protected async override void OnNavigatedTo(NavigationEventArgs e)
        {            
            await ViewModel.LoadDataAsync();
			base.OnNavigatedTo(e);
        }
    }    
}
