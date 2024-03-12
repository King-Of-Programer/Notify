using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyPushNotify.Classes;
using MyPushNotify.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly:Dependency(typeof(NotificationButtonReceiver))]
namespace MyPushNotify.Droid
{
    [BroadcastReceiver(Enabled=true, Exported =true)]
    public class NotificationButtonReceiver : BroadcastReceiver
    {
        public event EventHandler<NotifyEventArgs> NotificationReceived;

        public override void OnReceive(Context context, Intent intent)
        {
            string buttonId = intent.GetStringExtra("button_clicked");
            Toast.MakeText(Android.App.Application.Context, buttonId, ToastLength.Short).Show();
        }
    }
}