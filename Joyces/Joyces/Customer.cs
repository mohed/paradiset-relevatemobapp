using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joyces
{
    public class Customer
    {
        public string accountNumber { get; set; }
        public int? accumulatedPoints { get; set; }
        public object age { get; set; }
        public object birthDate { get; set; }
        public object careOf { get; set; }
        public object city { get; set; }
        public Communicationchoice[] communicationChoices { get; set; }
        public Country country { get; set; }
        public Currency currency { get; set; }
        public string email { get; set; }
        public object extraStreet { get; set; }
        public string firstName { get; set; }
        public string gender { get; set; }
        public object habitStoreNumber { get; set; }
        public object homeStoreNumber { get; set; }
        public object idpId { get; set; }
        public bool? isMainMember { get; set; }
        public bool? isMember { get; set; }
        public object lastBirthday { get; set; }
        public string lastName { get; set; }
        public object middleNames { get; set; }
        public string mobile { get; set; }
        public string name { get; set; }
        public object notes { get; set; }
        public string personalIdNumber { get; set; }
        public object phone { get; set; }
        public int? pointsBalance { get; set; }
        public object pointsNeeded { get; set; }
        public DateTime? registrationDate { get; set; }
        public object registrationStoreNumber { get; set; }
        public object segment { get; set; }
        public object segment2 { get; set; }
        public object sparStatus { get; set; }
        public object state { get; set; }
        public string status { get; set; }
        public object street { get; set; }
        public string type { get; set; }
        public object zipCode { get; set; }
        public string guid { get; set; }

        public class Country
        {
            public string code { get; set; }
            public string name { get; set; }
        }

        public class Currency
        {
            public string code { get; set; }
            public string name { get; set; }
        }

        public class Communicationchoice
        {
            public bool choice { get; set; }
            public Type type { get; set; }
        }

        public class Type
        {
            public string code { get; set; }
            public string name { get; set; }
            public string channel { get; set; }
        }

    }
}
