using Joyces.iOS.Model;
using Joyces;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using Joyces.iOS;
using Joyces.iOS.Helpers;
using FFImageLoading;

namespace Joyces.iOS
{
    public partial class NewsfeedTableViewController : UITableViewController
    {
        List<Joyces.News> newsList = Joyces.Platform.AppContext.Instance.Platform.NewsList;

        LoadingOverlay loadPop;
        bool bListLoaded = false;
        private UIRefreshControl refreshControl;
        UIAlertView _message;

        override public void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationItem.RightBarButtonItem.TintColor = IosHelper.FromHexString(GeneralSettings.TabBarTint);
            setCurrentClientTheme();
        }
        private void setCurrentClientTheme()
        {
            this.Title = Lang.NEWS_HEADER;
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
                try
                {
                    this.View.BackgroundColor = ColorExtensions.ToUIColor(GeneralSettings.BackgroundColor); // Color.FromHex("#00FF00").ToUIColor();
                }
                catch (Exception eee)
                {

                }
                //HEADER BACKGROUND COLOR
                // this.NavigationController.NavigationBar.BarTintColor = IosHelper.FromHexString(GeneralSettings.BackgroundColor);// UIColor.Yellow;
            }
            catch (Exception e)
            {

            }
        }
        public NewsfeedTableViewController(IntPtr handle) : base(handle)
        {
            var bounds = UIScreen.MainScreen.Bounds;
            loadPop = new LoadingOverlay(bounds);
            View.Add(loadPop);



            if (newsList != null)
                getNewsfeed();
            else
            {
                //Fuling, egenligen är detta fel! Felsök varför den kommer hit!
                //dvs denna ska egenligen inte vara bortkommenterad

                //_message = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, null, Lang.BUTTON_OK, null);
                //_message.Show();

                fetchData();
            };

            loadPop.Hide();
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (newsList != null)
                getNewsfeed();


            // Create the UIRefreshControl
            refreshControl = new UIRefreshControl();

            // Handle the pullDownToRefresh event
            refreshControl.ValueChanged += refreshTable;

            // Om man vill lägga till "Pull to Refresh"-texten
            //refreshControl.AttributedTitle = new NSAttributedString("Pull to Refresh", font: UIFont.FromName("System", 17.0f), foregroundColor: UIColor.Black);

            // Add the UIRefreshControl to the TableView
            TableView.AddSubview(refreshControl);
        }

        private async void getNewsfeed()
        {

            if (newsList != null)
                loadTable(newsList);
        }

        public void loadTable(List<Joyces.News> lNewsList)
        {
            newsTableView.Source = new NewsfeedTableViewSource(lNewsList);

            newsTableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            newsTableView.SeparatorInset = UIEdgeInsets.Zero;
            newsTableView.RowHeight = 300f;
            newsTableView.EstimatedRowHeight = 300f;
            newsTableView.ReloadData();
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

                var resp = await RestAPI.GetNews(sCustomerId, sUserToken);

                if (resp == null)
                {
                    _message = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.SERVICE_NOT_AVAILABLE, null, Lang.BUTTON_OK, null);
                    _message.Show();
                }
                else if (resp != null && resp is List<Joyces.News>)
                {
                    Joyces.Platform.AppContext.Instance.Platform.NewsList = (List<Joyces.News>)resp;

                    if (Joyces.Platform.AppContext.Instance.Platform.NewsList == null)
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
                        newsTableView.Source = new NewsfeedTableViewSource(Joyces.Platform.AppContext.Instance.Platform.NewsList);

                        newsTableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
                        newsTableView.SeparatorInset = UIEdgeInsets.Zero;
                        newsTableView.RowHeight = 300f;
                        newsTableView.EstimatedRowHeight = 300f;
                        newsTableView.ReloadData();
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
                Joyces.Helpers.Settings.AccessToken = string.Empty;
                Joyces.Helpers.Settings.UserEmail = string.Empty;

                var mainController = Storyboard.InstantiateViewController("loginNavigationController");
                UIApplication.SharedApplication.KeyWindow.RootViewController = mainController;

                _message = new UIAlertView(Lang.MESSAGE_HEADLINE, Lang.UNEXPECTED_ERROR, null, Lang.BUTTON_OK, null);
                _message.Show();
            }
        }
    }
}