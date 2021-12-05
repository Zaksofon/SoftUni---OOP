﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;

        protected Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            Decorations = new List<IDecoration>();
            Fish = new List<IFish>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }

                name = value;
            }
        }

        public int Capacity { get; private set; }

        public int Comfort => Decorations.Sum(x => x.Comfort);

        public ICollection<IDecoration> Decorations { get; }

        public ICollection<IFish> Fish { get; }

        public void AddFish(IFish fish)
        {
            if (Fish.Count == Capacity)
            {
                throw new ArgumentException(ExceptionMessages.NotEnoughCapacity);
            }

            Fish.Add(fish);
        }

        public bool RemoveFish(IFish fish) => Fish.Remove(fish);

        public void AddDecoration(IDecoration decoration) => Decorations.Add(decoration);

        public void Feed()
        {
            foreach (var fish in Fish)
            {
                fish.Eat();
            }
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name} ({GetType().Name}):");
            sb.AppendLine($"Fish: {(Fish.Any() ? string.Join(", ", Fish.Select(x => x.Name)) : "none")}");
            sb.AppendLine($"Decorations: {Decorations.Count}");
            sb.AppendLine($"Comfort: {Comfort}");
            return sb.ToString().TrimEnd();
        }
    }
}
