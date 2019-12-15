using System;
using System.Collections.Generic;
using System.Text;

namespace Day14Fuel
{
    public class Reaction
    {
        public Reagent Product { get; set; }

        public List<Reagent> Reagents { get; set; } = new List<Reagent>();
    }
}
