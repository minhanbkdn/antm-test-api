using JOIEnergy.Enums;
using JOIEnergy.Services;
using Microsoft.AspNetCore.Mvc;

namespace JOIEnergy.Controllers
{
    [Route("energy-cost")]
    public class CostCalculatorController : Controller
    {
        private readonly ICostCalculatorService _costCalculatorService;
        private readonly IAccountService _accountService;

        public CostCalculatorController(ICostCalculatorService costCalculatorService, IAccountService accountService)
        {
            _accountService = accountService;
            _costCalculatorService = costCalculatorService;
        }

        [HttpGet("last-week/{smartMeterId}")]
        public IActionResult GetCostLastWeekByMeterId(string smartMeterId)
        {
            var supplier = _accountService.GetPricePlanIdForSmartMeterId(smartMeterId);
            if (supplier == Supplier.NullSupplier)
            {
                return NotFound(string.Format("Smart Meter ID ({0}) not found", smartMeterId));
            }
            return Ok(_costCalculatorService.CalculateCostOfLastWeek(smartMeterId, supplier));
        }
    }
}