using System;
using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Domain;
using JOIEnergy.Enums;

namespace JOIEnergy.Services
{
    public class CostCalculatorService : ICostCalculatorService
    {
        private readonly IPricePlanService _pricePlanService;
        private readonly IMeterReadingService _meterReadingService;
        public CostCalculatorService(IPricePlanService pricePlanService, IMeterReadingService meterReadingService)
        {
            _pricePlanService = pricePlanService;
            _meterReadingService = meterReadingService;
        }
        public decimal CalculateCostOfLastWeek(string smartMeterId, Supplier supplier)
        {
            var pricePlan = _pricePlanService.GetPricePlanBySupplier(supplier);
            var today = DateTime.Now;
            var thisSunday = today.AddDays(-(int)today.DayOfWeek).Date;
            var lastSunday = thisSunday.AddDays(-7).Date;
            var lastWeekReadings = _meterReadingService.GetReadingsInPeriod(smartMeterId, lastSunday, thisSunday);
            if (!lastWeekReadings.Any() || pricePlan == null)
            {
                return 0;
            }
            return CalculateCost(pricePlan, lastWeekReadings);
        }

        private decimal CalculateCost(PricePlan pricePlan, List<ElectricityReading> electricityReadings)
        {
            decimal totalCost = 0;
            foreach (var reading in electricityReadings)
            {
                var unitRate = pricePlan.GetPrice(reading.Time);
                totalCost += reading.Reading * unitRate;
            }
            return totalCost;
        }
    }
}