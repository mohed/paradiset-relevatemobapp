using Joyces;
using Joyces.iOS.Model;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using Joyces.Repository;
using Joyces.iOS.Model;
using Joyces.iOS.Helpers;
using FFImageLoading;

namespace Joyces.iOS
{
    public partial class MoreTableViewController : UITableViewController
    {
        List<More> moreList = Joyces.Platform.AppContext.Instance.Platform.MoreList;

        LoadingOverlay loadPop;

        public MoreTableViewController(IntPtr handle) : base(handle)
        {
            var bounds = UIScreen.MainScreen.Bounds;
            loadPop = new LoadingOverlay(bounds);
            View.Add(loadPop);
            try
            {
                this.View.BackgroundColor = ColorExtensions.ToUIColor(GeneralSettings.BackgroundColor); // Color.FromHex("#00FF00").ToUIColor();
            }
            catch (Exception eee)
            {

            }
            if (moreList != null)
                getMore();
            else
            {
                LoadMore();
                getMore();
                //UIAlertView _error = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, null, Lang.BUTTON_OK, null);
                //_error.Show();
            }

            loadPop.Hide();
        }

        async private void LoadMore()
        {
            string sAccessToken = Joyces.Helpers.Settings.AccessToken;
            moreList = await RestAPI.GetMore(sAccessToken);
        }

        private void setCurrentClientTheme()
        {
            this.Title = Lang.MORE_HEADER;
            try
            {
                setTitleTheme();
            }
            catch (Exception e)
            {

            }
        }
        private void setTitleTheme()
        {
            try
            {

                //TITLE FONT
                UIStringAttributes ats = new UIStringAttributes();
                ats.Font = ThemeHelperIOS.GetThemeFont(17);
                // ats.ForegroundColor = UIColor.Purple;
                this.NavigationController.NavigationBar.TitleTextAttributes = ats;
                //BACK BUTTON COLOR
                //this.NavigationController.NavigationBar.TintColor = IosHelper.FromHexString(GeneralSettings.ButtonTextColor);// UIColor.Magenta;

                //HEADER BACKGROUND COLOR
                // this.NavigationController.NavigationBar.BarTintColor = IosHelper.FromHexString(GeneralSettings.BackgroundColor);// UIColor.Yellow;
            }
            catch (Exception e)
            {

            }
        }

        override public void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationItem.RightBarButtonItem.TintColor = IosHelper.FromHexString(GeneralSettings.TabBarTint);
            setCurrentClientTheme();
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (moreList != null)
                getMore();
        }

        private async void getMore()
        {

            if (moreList != null)
                loadTable(moreList);
        }

        public void loadTable(List<More> lMoreList)
        {
            moreTableView.Source = new MoreTableViewSource(this, lMoreList);

            moreTableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            moreTableView.SeparatorInset = UIEdgeInsets.Zero;
            moreTableView.RowHeight = 140f;
            moreTableView.EstimatedRowHeight = 140f;
            moreTableView.ReloadData();
        }

        public override bool ShouldPerformSegue(String segueIdentifier, NSObject sender)
        {
            if (segueIdentifier == "segueAccount")
                return true;
            else
                return false;
        }

        public void goToUrl(string sUrl)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl(sUrl));
        }
    }
}