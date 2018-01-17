using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Customer : BaseEntity {

        
        [Required]
        [DisplayName("Name")]
        [MaxLength(50)]
        public string name { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Surname")]
        public string surname { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Entered mobile format is not valid.")]
        [DisplayName("Phone Number")]
        public string telephoneNumber { get; set; }
        [Required]
        [DisplayName("Address")]
        [MaxLength(100)]
        public string address { get; set; }
    }
}
