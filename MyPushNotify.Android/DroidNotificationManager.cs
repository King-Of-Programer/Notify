using Android.Widget;
using AndroidX.Core.App;
using MyPushNotify.Classes;
using MyPushNotify.Droid;
using MyPushNotify.Interfaces;
using AndroidApp = Android.App.Application;
using System;
using Xamarin.Forms;
using Android.App;
using Android.OS;
using Android.Content;
using Java.Util;
using System.Collections.Generic;

[assembly:Dependency(typeof(DroidNotificationManager))]
namespace MyPushNotify.Droid
{
    public class DroidNotificationManager : INotificationManager
    {
        const string channelId = "default";
        const string channelName = "Default";
        const string channelDescription = "The default channel for notifications.";
        int messageId = 0;

        NotificationManager manager;
        bool channelInitialized = false;
        Dictionary<int, string> events;
        public DroidNotificationManager()
        {
            events = new Dictionary<int, string>
            {
                {Resource.Id.play,"Play" },
                {Resource.Id.next,"Next" },
                {Resource.Id.previous,"Previous" },
                {Resource.Id.favorite,"Favorite" }
            };
        }
        public void SendNotification()
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }
            messageId++;
            RemoteViews remoteViews = new RemoteViews(AndroidApp.Context.PackageName, Resource.Layout.notification);
            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetSmallIcon(Resource.Drawable.mus_pict)
                .SetCustomContentView(remoteViews)
                .SetStyle(new NotificationCompat.DecoratedCustomViewStyle())
                .SetPriority(NotificationCompat.PriorityHigh);

            int reqCode = 0;
            foreach (var ev in events)
            {
                Intent buttonIntent = new Intent(AndroidApp.Context, typeof(NotificationButtonReceiver));
                buttonIntent.PutExtra("button_clicked", ev.Value);
                PendingIntent pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, reqCode, buttonIntent, PendingIntentFlags.UpdateCurrent);
                remoteViews.SetOnClickPendingIntent(ev.Key, pendingIntent);
                reqCode++;
            }

            NotificationManagerCompat notificationManager = NotificationManagerCompat.From(AndroidApp.Context);
            notificationManager.Notify(messageId, builder.Build());
        }
        void CreateNotificationChannel()
        {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }
    }
}