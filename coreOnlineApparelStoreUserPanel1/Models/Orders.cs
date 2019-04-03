using System;
using System.Collections.Generic;

namespace coreOnlineApparelStoreUserPanel1.Models
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderAmount { get; set; }
        public int CustomerId { get; set; }

        public Customers Customer { get; set; }
    }
}
