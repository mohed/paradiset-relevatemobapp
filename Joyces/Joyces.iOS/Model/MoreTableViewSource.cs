using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Joyces.iOS;

namespace Joyces.iOS.Model
{
    public class MoreTableViewSource : UITableViewSource
    {
        private MoreTableViewController _controller;
        

        List<More> _more;

        public MoreTableViewSource(MoreTableViewController controller, List<More> more)
        {
            // Initialize
            this._controller = controller;
            this._more = more;
        }

        

        public MoreTableViewSource(List<More> more)
        {
            this._more = more;

        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (MoreCell)tableView.DequeueReusableCell("moreCell_id", indexPath);

            var news = _more[indexPath.Row];

            cell.UpdateCell(news);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint rowInSection)
        {
            return _more.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var list = _more[indexPath.Row];
            //_controller.PerformSegue("segueWebsite", _controller);

            _controller.goToUrl(list.companyInfoUrl);
        }
    }
}