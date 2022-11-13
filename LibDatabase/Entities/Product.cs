using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibDatabase.Entities
{
    [Table("tblProducts")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Precision(10, 2)]
        public decimal Price { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
