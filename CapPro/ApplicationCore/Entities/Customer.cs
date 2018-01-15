using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Customer : BaseEntity {

        public string name { get; set; }
        public string surname { get; set; }
        public string telephoneNumber { get; set; }
        public string address { get; set; }
    }
}
