using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KyivGazTest.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]*$")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]*$")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}