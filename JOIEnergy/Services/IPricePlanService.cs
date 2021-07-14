using System.Collections.Generic;
using JOIEnergy.Domain;
using JOIEnergy.Enums;

namespace JOIEnergy.Services
{
    public interface IPricePlanService
    {
        Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId);

        PricePlan GetPricePlanBySupplier(Supplier supplier);
    }
}