
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bakery.Core.Contracts;
using Bakery.Models.BakedFoods;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;

namespace Bakery.Core
{
    public class Controller : IController
    {
        private readonly IList<IBakedFood> bakedFoods = new List<IBakedFood>();
        private readonly IList<IDrink> drinks = new List<IDrink>();
        private readonly IList<ITable> tables = new List<ITable>();
        private decimal totalIncome;

        IBakedFood food = null;
        IDrink drink = null;
        ITable table = null;

        public string AddFood(string type, string name, decimal price)
        {
            switch (type)
            {
                case nameof(Bread):
                    food = new Bread(name, price); break;

                case nameof(Cake):
                    food = new Cake(name, price); break;
            }

            bakedFoods.Add(food);
            return string.Format(OutputMessages.FoodAdded, name, type);
        }

        public string AddDrink(string type, string name, int portion, string brand)
        {
            switch (type)
            {
                case nameof(Tea):
                    drink = new Tea(name, portion, brand); break;

                case nameof(Water):
                    drink = new Water(name, portion, brand); break;
            }

            drinks.Add(drink);
            return string.Format(OutputMessages.DrinkAdded, name, brand);
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            switch (type)
            {
                case nameof(InsideTable):
                    table = new InsideTable(tableNumber, capacity); break;

                case nameof(OutsideTable):
                    table = new OutsideTable(tableNumber, capacity); break;
            }

            tables.Add(table);
            return string.Format(OutputMessages.TableAdded, tableNumber);
        }

        public string ReserveTable(int numberOfPeople)
        {
            var currentTableToReserve = tables.FirstOrDefault(t => t.IsReserved == false && t.Capacity >= numberOfPeople);

            if (currentTableToReserve == null)
            {
                return string.Format(OutputMessages.ReservationNotPossible, numberOfPeople);
            }

            currentTableToReserve.Reserve(numberOfPeople);
            return string.Format(OutputMessages.TableReserved, currentTableToReserve.TableNumber, numberOfPeople);
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            if (table == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }

            var food = bakedFoods.FirstOrDefault(f => f.Name == foodName);
            if (food == null)
            {
                return string.Format(OutputMessages.NonExistentFood, foodName);
            }

            table.OrderFood(food);
            return string.Format(OutputMessages.FoodOrderSuccessful, tableNumber, foodName);
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            if (table == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber); 
            }

            var drink = drinks.FirstOrDefault(d => d.Name == drinkName && d.Brand == drinkBrand);
            if (drink == null)
            {
                return string.Format(OutputMessages.NonExistentDrink, drinkName, drinkBrand);
            }

            table.OrderDrink(drink);
            return string.Format($"Table {tableNumber} ordered {drinkName} {drinkBrand}");
        }

        public string LeaveTable(int tableNumber)
        {
            var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            var bill = table.GetBill();
            totalIncome += bill;
            table.Clear();

            return $"Table: {tableNumber}" + Environment.NewLine + $"Bill: {bill :F2}";
        }

        public string GetFreeTablesInfo()
        {
            var freeTables = tables.Where(t => t.IsReserved == false);
            var result = "";

            foreach (var table in freeTables)
            {
                result = table.GetFreeTableInfo().TrimEnd();
            }

            return result;
        }

        public string GetTotalIncome()
        {
            return $"Total income: {totalIncome :F2}lv";
        }
    }
}
