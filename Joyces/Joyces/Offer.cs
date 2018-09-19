using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joyces
{
    public class Offer
    {
        public string activator { get; set; }
        public int? allowedNumberOfRedemptions { get; set; }
        public string availableFromTime { get; set; }
        public string availableToTime { get; set; }
        public string code { get; set; }
        public string combinable { get; set; }
        public Currency currency { get; set; }
        public string dutyText { get; set; }
        public DateTime? effectiveDate { get; set; }
        public string imageUrl { get; set; }
        public string internalDescription { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        public bool? preselected { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string unit { get; set; }
        public List<string> validForProductCodes { get; set; }
        public DateTime? validityDate { get; set; }
        public decimal? value { get; set; }
    }

    public class Currency
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class PunchCard : Offer
    {
        public int numberOfPunchesTotal { get; set; }
        public int currentPunch { get; set; }
        public decimal lastPunchValue { get; set; }
    }
}