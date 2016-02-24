//---------------------------------------------------------------------------
//
// <copyright file="BiographyListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>2/24/2016 10:51:56 AM</createdOn>
//
//---------------------------------------------------------------------------

using System.ComponentModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.DataProviders.Html;
using DJNanoShow.Sections;
using DJNanoShow.ViewModels;
using AppStudio.Uwp;

namespace DJNanoShow.Pages
{
    public sealed partial class BiographyListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }

        private DataTransferManager _dataTransferManager;
		public static readonly DependencyProperty HtmlContentProperty =
            DependencyProperty.Register("HtmlContent", typeof(string), typeof(BiographyListPage), new PropertyMetadata(string.Empty));
        public BiographyListPage()
        {
			this.ViewModel = ListViewModel.CreateNew(Singleton<BiographyConfig>.Instance);

            this.InitializeComponent();

            new Microsoft.ApplicationInsights.TelemetryClient().TrackPageView(this.GetType().FullName);
        }

        public string HtmlContent
        {
            get { return (string)GetValue(HtmlContentProperty); }
            set { SetValue(HtmlContentProperty, value); }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();
			if (ViewModel.Items != null && ViewModel.Items.Count > 0)
			{
                HtmlContent = ViewModel.Items[0].Content;
            }
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _dataTransferManager.DataRequested -= OnDataRequested;

            base.OnNavigatedFrom(e);
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            ViewModel.ShareContent(args.Request);
        }
    }
}
