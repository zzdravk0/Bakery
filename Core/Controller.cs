using Bakery.Core.Contracts;
using Bakery.Models.BakedFoods;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables;
using Bakery.Models.Tables.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Core
{
    public class Controller : IController
    {
        List<IBakedFood> bakedFoods;
        List<IDrink> drinks;
        List<ITable> tables;
        decimal TotalIncomeRestaurant;
        public Controller()
        {
            bakedFoods = new List<IBakedFood>();
            drinks = new List<IDrink>();
            tables = new List<ITable>();

        }
        public string AddDrink(string type, string name, int portion, string brand)
        {
            if (type == "Tea")
            {
                Tea tea = new Tea(name, portion, 2.50m, brand);
                drinks.Add(tea);
                return $"Added {name} ({brand}) to the drink menu";
            }
            if (type == "Water")
            {
                Water tea = new Water(name, portion, 1.50m, brand);
                drinks.Add(tea);
                return $"Added {name} ({brand}) to the drink menu";
            }
            return null;
        }

        public string AddFood(string type, string name, decimal price)
        {
            if (type == "Bread")
            {
                Bread bread = new Bread(name, 200, price);
                bakedFoods.Add(bread);
                return $"Added {name} ({type}) to the menu";
            }
            else if (type == "Cake")
            {
                Cake bread = new Cake(name, 245, price);
                bakedFoods.Add(bread);
                return $"Added {name} ({type}) to the menu";
            }
            return null;
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            if (type == "InsideTable")
            {
                InsideTable table = new InsideTable(tableNumber, capacity, 2.50m);
                tables.Add(table);
                return $"Added table number {tableNumber} in the bakery";
            }
            else if (type == "OutsideTable")
            {
                OutsideTable table = new OutsideTable(tableNumber, capacity, 3.50m);
                tables.Add(table);
                return $"Added table number {tableNumber} in the bakery";
            }
            return null;
        }

        public string GetFreeTablesInfo()
        {
            var freeTables = tables.Where(x => !x.IsReserved);
            StringBuilder strBuilder = new StringBuilder();
            foreach (var table in freeTables)
            {
                strBuilder.AppendLine(table.GetFreeTableInfo());
            }
            return strBuilder.ToString().TrimEnd();
        }

        public string GetTotalIncome()
        {
            var totalIncome = TotalIncomeRestaurant;
            return $"Total income: {totalIncome.ToString("F2")}lv";
        }

        public string LeaveTable(int tableNumber)
        {
            var getTable = tables.Where(x => x.TableNumber <= tableNumber).FirstOrDefault();
            if (getTable != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"Table: {tableNumber}");
                stringBuilder.AppendLine($"Bill: {getTable.GetBill() + getTable.NumberOfPeople * getTable.PricePerPerson}");
                TotalIncomeRestaurant += getTable.GetBill() + getTable.NumberOfPeople * getTable.PricePerPerson;
                return stringBuilder.ToString().TrimEnd();
            }
            else return null;
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            var getTable = tables.Where(x => x.TableNumber == tableNumber).FirstOrDefault();
            if (getTable != null)
            {
                if (!drinks.Any(x => x.Name == drinkName))
                    return $"There is no {drinkName} {drinkBrand} available";
                else
                {
                    var getDrink = drinks.Where(x => x.Name == drinkName).First();
                    getTable.OrderDrink(getDrink);
                    return $"Table {tableNumber} ordered {drinkName} {drinkBrand}";
                }
            }
            else
            {
                return $"Could not find table {tableNumber}";
            }
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            var getTable = tables.Where(x => x.TableNumber == tableNumber).FirstOrDefault();
            if (getTable != null)
            {
                if (!bakedFoods.Any(x => x.Name == foodName))
                    return $"No {foodName} in the menu";
                else
                {
                    var bakedFoodss = bakedFoods.Where(x => x.Name == foodName).First();
                    getTable.OrderFood(bakedFoodss);
                    return $"Table {tableNumber} ordered {foodName}";
                }
            }
            else
            {
                return $"Could not find table {tableNumber}";
            }
        }

        public string ReserveTable(int numberOfPeople)
        {
            var getTable = tables.Where(x => x.Capacity >= numberOfPeople && !x.IsReserved).FirstOrDefault();
            if (getTable != null)
            {
                getTable.Reserve(numberOfPeople);
                return $"Table {getTable.TableNumber} has been reserved for {numberOfPeople} people";

            }
            else
            {
                return $"No available table for {numberOfPeople} people";
            }
        }
    }
}
