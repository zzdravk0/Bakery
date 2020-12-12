using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Models.Tables
{
    public class InsideTable : Table
    {
        private const decimal PricePerPerson = 2.50m;
        public InsideTable(int tableNumber, int capacity, decimal pricePerPerson) : base(tableNumber, capacity, PricePerPerson)
        {
        }
    }
}
