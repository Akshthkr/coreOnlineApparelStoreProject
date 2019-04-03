﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreOnlineApparelStoreAdminPortal.Models
{
    public class Customer
    {
       
            public int CustomerId { get; set; }
            public string CustomerFirstName { get; set; }
            public string CustomerLastName { get; set; }

            public string CustomerUserName { get; set; }
            public string CustomerEmail { get; set; }
            public long CustomerPhoneNumber { get; set; }
            public String CustomerGender { get; set; }
            //public string CustomerPassword { get; set; }
            public string CustomerCountry { get; set; }

            public string CustomerState { get; set; }
            public string CustomerZipNumber { get; set; }
            public string CustomerAddress1 { get; set; }
            public string CustomerAddress2 { get; set; }
        public bool SameAddress { get; set; }
        }

    
}