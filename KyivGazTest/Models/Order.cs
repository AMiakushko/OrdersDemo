using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KyivGazTest.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Number { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int ManagerId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Text)]
        public string Comment { get; set; }
        
        public virtual Manager Manager { get; set; }

    }
}