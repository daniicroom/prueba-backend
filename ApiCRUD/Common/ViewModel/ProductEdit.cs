﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel
{
    public class ProductEdit
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
