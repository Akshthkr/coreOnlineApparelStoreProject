using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coreOnlineApparelStoreAdminPortal.Models
{
    public class Feedback
    {
        [Column(Order = 0), Key, ForeignKey("Product")]
        public int ProductId { get; set; }
        [Column(Order = 1), Key, ForeignKey("Order")]
        public int OrderId { get; set; }
        public string message { get; set; }
        public Product Products { get; set; }
        public Order Order { get; set; }
    }
}
