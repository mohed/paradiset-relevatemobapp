using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Gcm;
using Android.Util;
using Android.Media;
using Android.Graphics;

namespace Joyces.Droid.Model
{
    [Service(Exported = false), IntentFilter(new[] { "com.google.android.c2dm.intent.RECEIVE" })]
    class MyGcmListenerService : GcmListenerService
    {
        public override void OnMessageReceived(string from, Bundle data)
        {
            var message = data.GetString("message");
            var title = data.GetString("title");

            Log.Debug("MyGcmListenerService", "From:    " + from);
            Log.Debug("MyGcmListenerService", "Message: " + message);

            SendNotification(message, title);
        }

        void SendNotification(string message, string title)
        {
            try
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);
               // Bitmap bMap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.Icon);
                var notificationBuilder = new Notification.Builder(this)
                    .SetContentTitle(title)
                    .SetContentText(message)
                    .SetPriority((int)NotificationPriority.Max)
                    .SetVisibility(NotificationVisibility.Public)
                    .SetStyle(new Android.App.Notification.BigTextStyle().BigText(message))
                    .SetVibrate(new long[] { 100, 1000, 100 })
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                    .SetLights(Android.Resource.Color.HoloOrangeDark, 1, 1)
                    .SetAutoCancel(true)
                    .SetSmallIcon(Resource.Drawable.IconNotification)
                   .SetContentIntent(pendingIntent);
                

                // .SetLargeIcon(bMap)
                //.SetSmallIcon(Resource.Drawable.Icon)


                var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                
                notificationManager.Notify(0, notificationBuilder.Build());
            }catch(Exception eee)
            {

            }
            //.SetSmallIcon(Resource.Drawable.ic_launcher)
        }
    }
}