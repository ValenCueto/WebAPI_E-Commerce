using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public float TotalPrice { get; set; }
        public User Client { get; set; }
    }
}
