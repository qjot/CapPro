using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Customer : BaseEntity {

        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Surname")]
        public string surname { get; set; }
        [DisplayName("Phone Number")]
        public string telephoneNumber { get; set; }
        [DisplayName("Address")]
        public string address { get; set; }
    }
}
