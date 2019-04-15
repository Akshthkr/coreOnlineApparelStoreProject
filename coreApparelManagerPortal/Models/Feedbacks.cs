using System;
using System.Collections.Generic;

namespace coreApparelManagerPortal.Models
{
    public partial class Feedbacks
    {
        public int CustomerId { get; set; }
        public string Message { get; set; }
        public int FeedbackId { get; set; }

        public Customers Customer { get; set; }
    }
}
