using Joyces;
using Joyces.iOS.Model;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using Joyces.iOS.Helpers;
using FFImageLoading;

namespace Joyces.iOS
{
    public partial class OfferTableViewController : UITableViewController
    {
        List<Offer> offerList = Joyces.Platform.AppContext.Instance.Platform.OfferList;
        bool bListLoaded = false;
        LoadingOverlay loadPop;
        private UIRefreshControl refreshControl;
        UIAlertView _message;

        public OfferTableViewController(IntPtr handle) : base(handle)
        {
            var bounds = UIScreen.MainScreen.Bounds;
            loadPop = new LoadingOverlay(bounds);
            View.Add(loadPop);

            if (offerList != null)
                getOffer();
            else
            {
                //Fuling, egenligen är detta fel! Felsök varför den kommer hit!
                //dvs denna ska egenligen inte vara bortkommenterad

                //_message = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, null, Lang.BUTTON_OK, null);
                //_message.Show();
                fetchData();
            }

            loadPop.Hide();

        }

        override public void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationItem.RightBarButtonItem.TintColor = IosHelper.FromHexString(GeneralSettings.TabBarTint);
            setCurrentClientTheme();
        }
        private void setCurrentClientTheme()
        {
            this.Title = Lang.OFFER_HEADER;
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

                //HEADER BACKGROUND COLOR
                // this.NavigationController.NavigationBar.BarTintColor = IosHelper.FromHexString(GeneralSettings.BackgroundColor);// UIColor.Yellow;
            }
            catch (Exception e)
            {

            }
        }
        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Create the UIRefreshControl
            refreshControl = new UIRefreshControl();

            // Handle the pullDownToRefresh event
            refreshControl.ValueChanged += refreshTable;

            // Add the UIRefreshControl to the TableView
            TableView.AddSubview(refreshControl);
            try
            {
                this.View.BackgroundColor = ColorExtensions.ToUIColor(GeneralSettings.BackgroundColor); // Color.FromHex("#00FF00").ToUIColor();
            }
            catch (Exception eee)
            {

            }
            if (offerList != null)
                getOffer();
        }

        private async void getOffer()
        {
            if (offerList != null)
                loadTable(offerList);
        }

        public void loadTable(List<Offer> lOfferList)
        {
            offerTableView.Source = new OfferTableViewSource(lOfferList);

            offerTableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            offerTableView.SeparatorInset = UIEdgeInsets.Zero;
            offerTableView.RowHeight = 120f;
            offerTableView.EstimatedRowHeight = 120f;
            offerTableView.ReloadData();
        }

        private void refreshTable(object sender, EventArgs e)
        {
            fetchData();
            refreshControl.EndRefreshing();
            TableView.ReloadData();
        }

        async private void fetchData()
        {
            try
            {
                string sCustomerId = Joyces.Platform.AppContext.Instance.Platform.CustomerId;
                string sUserToken = Joyces.Helpers.Settings.AccessToken;

                var resp = await RestAPI.GetOffer(sCustomerId, sUserToken);

                if (resp == null)
                {
                    _message = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, null, Lang.BUTTON_OK, null);
                    _message.Show();
                }
                else if (resp != null && resp is List<Offer>)
                {
                    Joyces.Platform.AppContext.Instance.Platform.OfferList = (List<Offer>)resp;

                    //Troligen har giltighetstiden för Token gått ut.
                    if (Joyces.Platform.AppContext.Instance.Platform.OfferList == null)
                    {
                        Joyces.Helpers.Settings.AccessToken = string.Empty;
                        Joyces.Helpers.Settings.UserEmail = string.Empty;

                        var mainController = Storyboard.InstantiateViewController("loginNavigationController");
                        UIApplication.SharedApplication.KeyWindow.RootViewController = mainController;

                        _message = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.USER_HAS_LOGGED_OUT, null, Lang.BUTTON_OK, null);
                        _message.Show();
                    }
                    else
                    {
                        offerTableView.Source = new OfferTableViewSource(Joyces.Platform.AppContext.Instance.Platform.OfferList);

                        offerTableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
                        offerTableView.SeparatorInset = UIEdgeInsets.Zero;
                        offerTableView.RowHeight = 120f;
                        offerTableView.EstimatedRowHeight = 120f;
                        offerTableView.ReloadData();
                    }
                }
                else if (resp != null && resp is AbalonErrors)
                {
                    var localError = (AbalonErrors)resp;

                    if (localError.error.Contains("invalid_token") || localError.error.Contains("invalid_grant"))
                    {
                        Joyces.Helpers.Settings.AccessToken = string.Empty;
                        Joyces.Helpers.Settings.UserEmail = string.Empty;

                        var mainController = Storyboard.InstantiateViewController("loginNavigationController");
                        UIApplication.SharedApplication.KeyWindow.RootViewController = mainController;

                        _message = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.USER_HAS_LOGGED_OUT, null, Lang.BUTTON_OK, null);
                        _message.Show();

                    }
                    else
                    {
                        UIAlertView _error = new UIAlertView(Lang.MESSAGE_HEADLINE, localError.message, null, Lang.BUTTON_OK, null);
                        _error.Show();
                    }
                }
                else
                {
                    //_message = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.UNEXPECTED_ERROR + " #1", null, Lang.BUTTON_OK, null);
                    //_message.Show();
                }
            }
            catch (Exception ex)
            {
                var mainController = Storyboard.InstantiateViewController("loginNavigationController");
                UIApplication.SharedApplication.KeyWindow.RootViewController = mainController;

                _message = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.UNEXPECTED_ERROR + " #2", null, Lang.BUTTON_OK, null);
                _message.Show();
            }
        }
    }
}