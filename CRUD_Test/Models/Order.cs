using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Test.Models
{
    
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Column(TypeName = "date")]
        public DateTime? OrderDate { get; set; }
        [Column(TypeName = "money")]
        public decimal? OrderCost { get; set; }
        [StringLength(1000)]
        public string ItemsDescription { get; set; }
        [StringLength(1000)]
        public string ShippingAddress { get; set; }
    }
}
