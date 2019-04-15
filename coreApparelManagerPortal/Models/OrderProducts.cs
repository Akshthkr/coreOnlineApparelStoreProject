using System;
using System.Collections.Generic;

namespace coreApparelManagerPortal.Models
{
    public partial class OrderProducts
    {
        public int OrderId { get; set; }
        public int Productid { get; set; }
        public int Quantity { get; set; }

        public Orders Order { get; set; }
        public Products Product { get; set; }
    }
}
