using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DJNanoShow
{
    public abstract class PageBase : Page
    {
        private bool HasPortrait = false;
        private DisplayOrientations _currentOrientations;
        public PageBase(bool hasPortrait = false)
        {
            this.HasPortrait = hasPortrait;
            if (HasPortrait)
            {
                SizeChanged += OnSizeWithPortraitChanged;
            }
            else
            {
                SizeChanged += OnSizeChanged;
            }
        }
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 500)
            {
                VisualStateManager.GoToState(this, "SnappedView", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "FullscreenView", true);
            }
        }
        private void OnSizeWithPortraitChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 500)
            {
                VisualStateManager.GoToState(this, "SnappedView", true);
            }
            else if (e.NewSize.Width < e.NewSize.Height)
            {
                VisualStateManager.GoToState(this, "PortraitView", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "FullscreenView", true);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadState(e.Parameter);
            if (HasPortrait)
            {
                _currentOrientations = DisplayInformation.AutoRotationPreferences;
                DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait
                                                            | DisplayOrientations.Landscape
                                                            | DisplayOrientations.LandscapeFlipped
                                                            | DisplayOrientations.PortraitFlipped;

                // Handle orientation changes
                DisplayInformation.GetForCurrentView().OrientationChanged += this.OnOrientationChanged;
                this.TransitionStoryboardState();
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (HasPortrait)
            {
                // Restore previous rotation preferences
                DisplayInformation.AutoRotationPreferences = _currentOrientations;

                // Handle orientation changes
                DisplayInformation.GetForCurrentView().OrientationChanged -= this.OnOrientationChanged;
            }
        }
        private void OnOrientationChanged(DisplayInformation sender, object args)
        {
            this.TransitionStoryboardState();
        }
        private void TransitionStoryboardState()
        {
            string displayOrientation;

            switch (DisplayInformation.GetForCurrentView().CurrentOrientation)
            {
                case DisplayOrientations.Landscape:
                case DisplayOrientations.LandscapeFlipped:
                    displayOrientation = "Landscape";
                    break;
                case DisplayOrientations.Portrait:
                case DisplayOrientations.PortraitFlipped:
                default:
                    displayOrientation = "Portrait";
                    break;
            }

            VisualStateManager.GoToState(this, displayOrientation, false);
        }
        protected abstract void LoadState(object navParameter);
    }
}
