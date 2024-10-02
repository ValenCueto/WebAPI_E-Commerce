﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; } 
        public List<CartDetail>? Details { get; set; }
        public float TotalPrice { get; set; }
    }
}
