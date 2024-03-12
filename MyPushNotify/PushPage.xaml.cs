using MyPushNotify.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyPushNotify
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PushPage : ContentPage
    {
        INotificationManager notificationManager;
        public PushPage()
        {
            InitializeComponent();
            notificationManager = DependencyService.Get<INotificationManager>();
        }
        void OnSendClick(object sender, EventArgs e)
        {
            notificationManager.SendNotification();
        }
    }
}