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
using Android.Util;
using Android.Gms.Gcm;
using Android.Gms.Iid;

namespace Joyces.Droid.Model
{
    [Service(Exported = false)]
    public class RegistrationIntentService : IntentService
    {
        static object locker = new object();

        public RegistrationIntentService() : base("RegistrationIntentService") { }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                Log.Info("RegistrationIntentService", "Calling InstanceID.GetToken");
                lock (locker)
                {

                    var instanceID = InstanceID.GetInstance(this);

                    //instanceID.DeleteInstanceID();
                    //instanceID = InstanceID.GetInstance(this);

                    string sSenderId = Joyces.Platform.AppContext.Instance.Platform.SenderId;

                    var token = instanceID.GetToken(sSenderId, GoogleCloudMessaging.InstanceIdScope, null);

                    Log.Info("RegistrationIntentService", "GCM Registration Token: " + token);
                    //SendRegistrationToAppServer(token);

                    Joyces.Helpers.Settings.PushDeviceToken = token;

                    //TODO::kolla varför den crashar här?!
                    Subscribe(token);
                }
            }
            catch (Exception e)
            {
                Log.Debug("RegistrationIntentService", "Failed to get a registration token");
                return;
            }
        }

        void Subscribe(string token)
        {
            try
            {
                var pubSub = GcmPubSub.GetInstance(this);
                pubSub.Subscribe(token, "/topics/global", null);
            }
            catch (Exception ex)
            {

            }
        }
    }
}