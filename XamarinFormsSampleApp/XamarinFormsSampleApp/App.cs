using Xamarin.Forms;

namespace XamarinFormsSampleApp
{
    public class App : Application
    {
        public static int ScreenWidth;

        public App()
        {
            // The root page of your application
            //TODO Step5 Code insert here
            MainPage = new MyListViewPage("全家店家列表");
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}