using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibDatabase.Entities
{
    [Table("tblBaskets")]
    public class Basket
    {
        public int Count { get; set; }
        [ForeignKey("ProductOf")]
        public int ProductId { get; set; }
        [ForeignKey("UserOf")]
        public int UserId { get; set; }
        public virtual Product ProductOf { get; set; }
        public virtual UserEntity UserOf { get; set; }
    }
}
