using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Models.Tables
{
    public class OutsideTable : Table
    {
        private const decimal PricePerPerson = 3.50m;
        public OutsideTable(int tableNumber, int capacity, decimal pricePerPerson) : base(tableNumber, capacity, PricePerPerson)
        {
        }
    }
}
