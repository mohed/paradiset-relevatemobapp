using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Joyces.iOS;
using Joyces.iOS.Model;

namespace Joyces.iOS.Model
{
    public class NewsfeedTableViewSource : UITableViewSource
    {
        List<Joyces.News> newsfeed;
        

        public NewsfeedTableViewSource(List<Joyces.News> newsfeed)
        {
            this.newsfeed = newsfeed;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (NewsfeedCell)tableView.DequeueReusableCell("newsfeedCell_id", indexPath);

            var news = newsfeed[indexPath.Row];

            cell.UpdateCell(news);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint rowInSection)
        {
            return newsfeed.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var list = newsfeed[indexPath.Row];

            if (!string.IsNullOrEmpty(list.imageUrl))
                IosHelper.setNSUserDefaults("newsImageURL", list.imageUrl);
            else
                IosHelper.setNSUserDefaults("newsImageURL", "");

            if (!string.IsNullOrEmpty(list.name))
                IosHelper.setNSUserDefaults("newsName", list.name);
            else
                IosHelper.setNSUserDefaults("newsName", "");

            if (!string.IsNullOrEmpty(list.note))
                IosHelper.setNSUserDefaults("newsNote", list.note);
            else
                IosHelper.setNSUserDefaults("newsNote", "");
        }
    }
}