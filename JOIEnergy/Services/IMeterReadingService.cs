using System;
using System.Collections.Generic;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public interface IMeterReadingService
    {
        List<ElectricityReading> GetReadings(string smartMeterId);

        List<ElectricityReading> GetReadingsInPeriod(string smartMeterId, DateTime startDate, DateTime endDate);

        void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings);
    }
}