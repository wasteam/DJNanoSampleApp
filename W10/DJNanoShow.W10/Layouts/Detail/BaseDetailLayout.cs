using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Layouts
{
    public abstract class BaseDetailLayout : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(object), typeof(BaseDetailLayout), new PropertyMetadata(null));

        public static readonly DependencyProperty BodyFontSizeProperty
            = DependencyProperty.Register("BodyFontSize", typeof(int), typeof(BaseDetailLayout), new PropertyMetadata(0));

        public BaseDetailLayout()
        {
            var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
            var isOnMobile = qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"].ToLowerInvariant() == "Mobile".ToLowerInvariant();
            if (isOnMobile)
            {
                DisplayInformation.GetForCurrentView().OrientationChanged += OrientationChanged;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object ViewModel
        {
            get { return GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public int BodyFontSize
        {
            get { return (int)GetValue(BodyFontSizeProperty); }
            set { SetValue(BodyFontSizeProperty, value); }
        }

        public abstract void UpdateFontSize();

        public void OnPropertyChanged(string propertyName)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected List<Control> AllChildren(DependencyObject parent)
        {
            var list = new List<Control>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is Control)
                {
                    list.Add(child as Control);
                }
                list.AddRange(AllChildren(child));
            }

            return list;
        }

        private void OrientationChanged(DisplayInformation sender, object args)
        {
            if (sender.CurrentOrientation == DisplayOrientations.Landscape || sender.CurrentOrientation == DisplayOrientations.LandscapeFlipped)
            {
                ShellViewModel.SetHamburguerButtonProperties(Visibility.Collapsed);
            }
            else
            {
                ShellViewModel.SetHamburguerButtonProperties(Visibility.Visible);
            }
        }
    }
}
