using System;
using System.Collections.Generic;

namespace CoreApparelStoreUserPortal.Models
{
    public partial class Admins
    {
        public int AdminId { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string AdminEmail { get; set; }
        public long AdminPhoneNumber { get; set; }
        public string AdminGender { get; set; }
        public string AdminPassword { get; set; }
        public string AdminCountry { get; set; }
        public string AdminState { get; set; }
        public string AdminZipNumber { get; set; }
        public string AdminAddress { get; set; }
    }
}
