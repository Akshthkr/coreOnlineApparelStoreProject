using System;
using System.Collections.Generic;

namespace CoreApparelStoreUserPortal.Models
{
    public partial class Orders
    {
        public Orders()
        {
            Feedbacks = new HashSet<Feedbacks>();
            OrderProducts = new HashSet<OrderProducts>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderAmount { get; set; }
        public int CustomerId { get; set; }

        public Customers Customer { get; set; }
        public ICollection<Feedbacks> Feedbacks { get; set; }
        public ICollection<OrderProducts> OrderProducts { get; set; }
    }
}
