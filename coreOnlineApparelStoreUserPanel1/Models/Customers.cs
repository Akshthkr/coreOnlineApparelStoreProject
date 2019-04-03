using System;
using System.Collections.Generic;

namespace coreOnlineApparelStoreUserPanel1.Models
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
        public string CustomerAddress { get; set; }
        public string CustomerState { get; set; }
        public string CustomerCountry { get; set; }
        public long CustomerZipCode { get; set; }
        public bool SameAddress { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
