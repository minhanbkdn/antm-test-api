using JOIEnergy.Enums;

namespace JOIEnergy.Services
{
    public interface ICostCalculatorService
    {
        decimal CalculateCostOfLastWeek(string smartMeterId, Supplier supplier);
    }
}