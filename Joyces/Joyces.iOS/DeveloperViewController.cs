using Foundation;
using Joyces.iOS.Model;
using System;
using System.Threading.Tasks;
using UIKit;

namespace Joyces.iOS
{
    public partial class DeveloperViewController : UIViewController
    {
        LoadingOverlay loadPop;
        

        public DeveloperViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            //Laddas in varje gång vyn laddas!
            var bounds = UIScreen.MainScreen.Bounds;
            loadPop = new LoadingOverlay(bounds);
            View.Add(loadPop);

            var DeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");
            txtDeviceToken.Text = DeviceToken;

            txtBundleIdentifier.Text = NSBundle.MainBundle.InfoDictionary["CFBundleIdentifier"].ToString();

            //Testar ifall WSO2 är up-and-running
            CheckWSO2ServerStatus();

            //Testar ifall REST är up-and-running
            CheckRESTServerStatus();

            loadPop.Hide();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        async private void CheckWSO2ServerStatus()
        {
            var appTokenModel = await RestAPI.GetApplicationToken();

            if (appTokenModel != null)
            {
                TextViewServerStatus.Text = "Online";
                TextViewServerStatus.TextColor = new UIColor(red: 0.14f, green: 0.62f, blue: 0.07f, alpha: 1.0f);
            }
            else
            {
                TextViewServerStatus.Text = "Offline";
                TextViewServerStatus.TextColor = new UIColor(red: 0.62f, green: 0.07f, blue: 0.07f, alpha: 1.0f);
            }
        }

        async private void CheckRESTServerStatus()
        {
            var tokenModel = await RestAPI.GetUserToken(GeneralSettings.TestUserUsername, GeneralSettings.TestUserPassword);

            var getCustomer = await RestAPI.GetCustomer(GeneralSettings.TestUserUsername, ((TokenModel)tokenModel).access_token);

            if (getCustomer != null && getCustomer is Customer)
            {
                TextViewRESTStatus.Text = "Online";
                TextViewRESTStatus.TextColor = new UIColor(red: 0.14f, green: 0.62f, blue: 0.07f, alpha: 1.0f);
            }
            else if (getCustomer != null && tokenModel is AbalonErrors)
            {
                TextViewRESTStatus.Text = "Offline: " + ((AbalonErrors)tokenModel).message;
                TextViewRESTStatus.TextColor = new UIColor(red: 0.62f, green: 0.07f, blue: 0.07f, alpha: 1.0f);
            }
            else
            {
                TextViewRESTStatus.Text = "Offline";
                TextViewRESTStatus.TextColor = new UIColor(red: 0.62f, green: 0.07f, blue: 0.07f, alpha: 1.0f);
            }
        }
    }
}