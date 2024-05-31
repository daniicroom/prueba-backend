using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(63,4)")]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
