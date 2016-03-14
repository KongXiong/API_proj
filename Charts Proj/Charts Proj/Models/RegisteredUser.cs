using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Charts_Proj.Models
{
    public class RegisteredUser
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}