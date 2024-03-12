using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyPushNotify
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new PushPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
