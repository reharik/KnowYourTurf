using System;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;

namespace KnowYourTurf.Core.Services
{
    public interface IUnitSizeTimesQuantyCalculator
    {
        decimal CalculateLbsPerUnit(InventoryProduct product);
    }
    public class UnitSizeTimesQuantyCalculator : IUnitSizeTimesQuantyCalculator
    {
        public decimal CalculateLbsPerUnit(InventoryProduct product)
        {
            if (product.UnitType == UnitType.Lbs.ToString())
            {
                return product.SizeOfUnit;
            }
            if (product.UnitType == UnitType.Oz.ToString())
            {
                return Math.Round(Convert.ToDecimal(Convert.ToDecimal(product.SizeOfUnit)/ 16), 2);
            }
            if (product.UnitType == UnitType.Tons.ToString())
            {
                return product.SizeOfUnit * 2000;
            }

            return 0;
        }

    }
}