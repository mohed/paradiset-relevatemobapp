using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Joyces.iOS.Model;
using Joyces.Repository;
using Joyces;

namespace Joyces.iOS.Model
{
    public class OfferTableViewSource : UITableViewSource
    {
        List<Offer> offers;

        public OfferTableViewSource(List<Offer> offerList)
        {
            this.offers = offerList;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (OfferCell)tableView.DequeueReusableCell("offerCell_id", indexPath);

            var news = offers[indexPath.Row];

            cell.UpdateCell(news);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint rowInSection)
        {
            return offers.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var list = offers[indexPath.Row];

            if (!string.IsNullOrEmpty(list.imageUrl))
                IosHelper.setNSUserDefaults("offerImageURL", list.imageUrl);
            else
                IosHelper.setNSUserDefaults("offerImageURL", "");

            if (!string.IsNullOrEmpty(list.name))
                IosHelper.setNSUserDefaults("offerName", list.name);
            else
                IosHelper.setNSUserDefaults("offerName", "");

            if (!string.IsNullOrEmpty(list.note))
                IosHelper.setNSUserDefaults("offerNote", list.note);
            else
                IosHelper.setNSUserDefaults("offerNote", "");

            if (!string.IsNullOrEmpty(list.dutyText))
                IosHelper.setNSUserDefaults("offerDutyText", list.dutyText);
            else
                IosHelper.setNSUserDefaults("offerDutyText", "");

            if (!string.IsNullOrEmpty(list.code))
                IosHelper.setNSUserDefaults("offerCode", list.code);
            else
                IosHelper.setNSUserDefaults("offerCode", "");

            IosHelper.setNSUserDefaults("offerValue", ObjectRepository.parseOfferValue(list));
            IosHelper.setNSUserDefaults("offerValidityDate", ObjectRepository.ParseDateTimeToCulture(list.validityDate));
        }
    }
}