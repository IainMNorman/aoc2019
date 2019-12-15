using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day14Fuel
{
    public class Refinery
    {
        public List<Reaction> Reactions { get; set; } = new List<Reaction>();
        public List<Reagent> Inventory { get; set; } = new List<Reagent>();
        public long OreUsed { get; set; }

        public Refinery(string path)
        {            
            Init(path);
        }

        private void Init(string path)
        {
            OreUsed = 0;

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var reaction = new Reaction();
                var parts = line.Split(" => ", StringSplitOptions.RemoveEmptyEntries);
                var inParts = parts[0].Split(", ", StringSplitOptions.RemoveEmptyEntries);
                var outPart = parts[1];

                var outPartParts = outPart.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                reaction.Product = new Reagent(outPartParts[1], long.Parse(outPartParts[0]));

                foreach (var item in inParts)
                {
                    var itemParts = item.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    reaction.Reagents.Add(new Reagent(itemParts[1], long.Parse(itemParts[0])));
                }

                this.Reactions.Add(reaction);
            }
        }

        public long Refine(string chemicalName, long quantity)
        {
            Inventory.Clear();
            OreUsed = 0;

            var refineQueue = new LinkedList<Reagent>();

            refineQueue.AddFirst(new Reagent(chemicalName, quantity));

            while (refineQueue.Count > 0)
            {
                var item = refineQueue.First();
                refineQueue.RemoveFirst();

                if (item.Name == "ORE")
                {
                    OreUsed += item.Quantity;
                    this.AddToInventory("ORE", item.Quantity);
                }
                else
                {
                    // can we make it, do we have stuff in stock?
                    var required = RequiredToMake(item.Name, item.Quantity);
                    required.ForEach(x => WriteLine($"We need to make {x.Quantity} {x.Name}"));

                    if (!required.Any())
                    {
                        // yes we can try and make it
                        React(item.Name, item.Quantity);
                    }
                    else
                    {
                        // we need to add something else to the queue and this again   
                        refineQueue.AddFirst(item);
                        foreach (var reagent in required)
                        {
                            refineQueue.AddFirst(new Reagent(reagent.Name, reagent.Quantity));
                        }
                    }
                }
            }

            return OreUsed;
        }

        private void React(string chemicalName, long requiredQuantity)
        {
            var reaction = this.Reactions.Single(r => r.Product.Name == chemicalName);

            var numberOfReactions = (long)Math.Ceiling((decimal)requiredQuantity / (decimal)reaction.Product.Quantity);

            Write("Consume ");

            foreach (var reagent in reaction.Reagents)
            {
                RemoveFromInventory(reagent.Name, reagent.Quantity * numberOfReactions);
                Write($"{reagent.Quantity * numberOfReactions} {reagent.Name}, ");
            }

            AddToInventory(reaction.Product.Name, reaction.Product.Quantity * numberOfReactions);
            Write($"to produce {reaction.Product.Quantity * numberOfReactions} {reaction.Product.Name}\n");
        }

        private void AddToInventory(string name, long quantity)
        {
            if (Inventory.Any(i => i.Name == name))
            {
                Inventory.Single(i => i.Name == name).Quantity += quantity;
            }
            else
            {
                Inventory.Add(new Reagent(name, quantity));
            }
        }

        private void RemoveFromInventory(string name, long quantity)
        {
            AddToInventory(name, 0 - quantity);
        }

        private bool IsInStock(string chemicalName, long quantity)
        {
            return Inventory.Any(i => i.Name == chemicalName && i.Quantity > quantity);
        }

        private List<Reagent> RequiredToMake(string chemicalName, long requiredQuantity)
        {
            var reaction = this.Reactions.Single(r => r.Product.Name == chemicalName);

            // how many times will we need to do this reaction?
            var noofReactions = (long)Math.Ceiling((decimal)requiredQuantity / (decimal)reaction.Product.Quantity);

            var requiredInventory = new List<Reagent>();

            foreach (var reagent in reaction.Reagents)
            {
                var amountOfReagentRequired = noofReactions * reagent.Quantity;
                var currentStockCount = Inventory.FirstOrDefault(i => i.Name == reagent.Name)?.Quantity ?? 0;
                var mustRefineCount = currentStockCount < amountOfReagentRequired ? amountOfReagentRequired - currentStockCount : 0;
                if (mustRefineCount > 0)
                {
                    requiredInventory.Add(new Reagent(reagent.Name, mustRefineCount));
                }
            }

            return requiredInventory;
        }

        private void WriteLine(string text)
        {
            //if (Output)
            //{
            //    WriteLine(text);
            //}
        }

        private void Write(string text)
        {
            //if (Output)
            //{
            //    Write(text);
            //}
        }
    }
}
