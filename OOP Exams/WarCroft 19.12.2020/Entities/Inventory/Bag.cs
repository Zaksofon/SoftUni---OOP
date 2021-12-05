
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WarCroft.Constants;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public abstract class Bag : IBag
    {
        private readonly IList<Item> internalItems;

        protected Bag(int capacity)
        {
            Capacity = capacity;
            this.internalItems = new List<Item>();
            Items = new ReadOnlyCollection<Item>(internalItems);
        }
        public int Capacity { get ; set; }

        public int Load => Items.Sum(i => i.Weight);

        public IReadOnlyCollection<Item> Items { get; }


        public void AddItem(Item item)
        {
            if ((Load + item.Weight) > Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.ExceedMaximumBagCapacity);
            }
            internalItems.Add(item);
        }

        public Item GetItem(string name)
        {
            if (Items.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);
            }

            var resultItem = Items.FirstOrDefault(n => n.GetType().Name == name);

            if (resultItem == null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.ItemNotFoundInBag, name));
            }

            internalItems.Remove(resultItem);
            return resultItem;
        }
    }
}
