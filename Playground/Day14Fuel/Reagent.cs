using System;
using System.Collections.Generic;
using System.Text;

namespace Day14Fuel
{
    public class Reagent
    {
        public string Name { get; set; }

        public long Quantity { get; set; }

        public Reagent(string name, long quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
