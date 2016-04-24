using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;

namespace XamarinFormsSampleApp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            var size = new Size(bounds.Width * scaleFactor, bounds.Height * scaleFactor);
            XamarinFormsSampleApp.App.ScreenWidth = (int)size.Width;

            LoadApplication(new XamarinFormsSampleApp.App());
        }
    }
}