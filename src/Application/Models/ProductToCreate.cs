using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductToCreate
    {
        public string? Name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }

    }
}
