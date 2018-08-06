using Joyces.iOS.Model;
using Foundation;
using System;
using UIKit;
using UserNotifications;


namespace Joyces.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            //if (!UIApplication.SharedApplication.OpenUrl(NSUrl.FromString("joyces://com.informationfactory.abalon")))
            //{
            //    //Use the code below to go to itunes if application not found.
            //    UIApplication.SharedApplication.OpenUrl(NSUrl.FromString("itms://itunes.apple.com/in/app/appname/appid"));
            //}

            // custom stuff here using different properties of the url passed in
            return true;
        }

        //public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        //{
        //    // Override point for customization after application launch.
        //    // If not required for your application you can safely delete this method

        //    //if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
        //    //{
        //    //    var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
        //    //                       UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
        //    //                       new NSSet());

        //    //    UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
        //    //    UIApplication.SharedApplication.RegisterForRemoteNotifications();
        //    //}
        //    //else
        //    //{
        //    //    UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
        //    //    UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
        //    //}

        //    return true;
        //}

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            ProcessNotification(userInfo, false);
        }

        void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
        {
            // Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
            if (null != options && options.ContainsKey(new NSString("aps")))
            {
                //Get the aps dictionary
                NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

                string sActionKey = string.Empty;

                //Extract the alert text
                // NOTE: If you're using the simple alert by just specifying
                // "  aps:{alert:"alert msg here"}  ", this will work fine.
                // But if you're using a complex alert with Localization keys, etc.,
                // your "alert" object from the aps dictionary will be another NSDictionary.
                // Basically the JSON gets dumped right into a NSDictionary,
                // so keep that in mind.
                if (aps.ContainsKey(new NSString("action-loc-key"))) { 
                    sActionKey = (aps[new NSString("action-loc-key")] as NSString).ToString();
                    IosHelper.setNSUserDefaults("actionKey", sActionKey);
                }

                //If this came from the ReceivedRemoteNotification while the app was running,
                // we of course need to manually process things like the sound, badge, and alert.
                //if (!fromFinishedLaunching)
                //{
                //    //Manually show an alert
                //    if (!string.IsNullOrEmpty(sActionKey))
                //    {
                //        UIAlertView avAlert = new UIAlertView("Notification", sActionKey, null, "OK", null);
                //        avAlert.Show();
                //    }
                //}
            }
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        async public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.

            // reset our badge
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;

            //
            var notificationSettings = await UNUserNotificationCenter.Current.GetNotificationSettingsAsync();
            switch (notificationSettings.SoundSetting)
            {
                case UNNotificationSetting.Disabled:
                    Joyces.Helpers.Settings.PushDeviceToken = string.Empty;
                    break;
                case UNNotificationSetting.Enabled:
                    break;
                case UNNotificationSetting.NotSupported:
                    break;
            }
           // Kontrollera så att användaren har en aktiv internetanslutning
            CheckInternetConnection();
        }

        public void CheckInternetConnection()
        {
            if (!Reachability.IsHostReachable("http://google.com"))
            {
                UIAlertView _MessagePopUp = new UIAlertView(Lang.ERROR_HEADLINE, Lang.INTERNET_CONNECTION_REQUIERED, null, Lang.BUTTON_OK, null);
                _MessagePopUp.Show();

                _MessagePopUp.Clicked += (object senders, UIButtonEventArgs es) =>
                {
                    if (es.ButtonIndex == 0)
                        CheckInternetConnection();
                };
            }
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            // Get current device token
            var DeviceToken = deviceToken.Description;
            if (!string.IsNullOrWhiteSpace(DeviceToken))
            {
                DeviceToken = DeviceToken.Trim('<').Trim('>');
            }

            // Get previous device token
            var oldDeviceToken = Joyces.Helpers.Settings.PushDeviceToken;

            // Has the token changed?
            //if (string.IsNullOrEmpty(oldDeviceToken) || !oldDeviceToken.Equals(DeviceToken))
            //    IosHelper.setNSUserDefaults("TokenUpdated", "true");                
            //else
            //    IosHelper.setNSUserDefaults("TokenUpdated", "false");

            // Save new device token
            Joyces.Helpers.Settings.PushDeviceToken = DeviceToken;
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
//#if DEBUG
//            new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, Lang.BUTTON_OK, null).Show();
//#endif
        }
    }
}


