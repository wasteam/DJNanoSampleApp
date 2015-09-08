using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

namespace DJNanoShow.Services
{
    public static class FullScreenService
    {
        public static event EventHandler<bool> FullScreenModeChanged;
        public static void ChangeFullScreenMode()
        {
            if (IsFullScreenMode)
            {
                ApplicationView.GetForCurrentView().ExitFullScreenMode();
            }
            else
            {
                ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
            }
            IsFullScreenMode = !IsFullScreenMode;
            NotifyFullScreenModeChangedEvent();            
        }
        private static bool _isFullScreenMode = false;
        public static bool IsFullScreenMode
        {
            get
            {
                return _isFullScreenMode;
            }
            set
            {
                _isFullScreenMode = value;
            }
        }

        private static void NotifyFullScreenModeChangedEvent()
        {
            if (FullScreenModeChanged != null)
            {
                FullScreenModeChanged(null, IsFullScreenMode);
            }
        }
    }
}
