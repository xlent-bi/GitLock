using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XlentLock
{
    public class App : Application
    {
        private Color xlentGray;
        private Color xlentOrange;
        public App()
        {
            xlentGray = new Color(255, 170, 25);
            xlentOrange = new Color(100, 101, 105);
            // The root page of your application
            var lockpage = new LockPage();
            MainPage = new NavigationPage(lockpage);
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
