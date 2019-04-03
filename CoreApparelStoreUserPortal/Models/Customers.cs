using System;
using System.Collections.Generic;

namespace CoreApparelStoreUserPortal.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerUserName { get; set; }
        public string CustomerEmail { get; set; }
        public long CustomerPhoneNumber { get; set; }
        public string CustomerGender { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerState { get; set; }
        public string CustomerZipNumber { get; set; }
        public string CustomerAddress1 { get; set; }
        public string CustomerAddress2 { get; set; }
        public bool SameAddress { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
