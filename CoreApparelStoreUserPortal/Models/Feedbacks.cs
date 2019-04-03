using System;
using System.Collections.Generic;

namespace CoreApparelStoreUserPortal.Models
{
    public partial class Feedbacks
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public string Message { get; set; }

        public Orders Order { get; set; }
        public Products Product { get; set; }
    }
}
