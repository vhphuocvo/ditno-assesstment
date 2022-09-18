using DitnoCalculateBusinessDay.Models;

namespace DitnoCalculateBusinessDay.Services
{
    public interface IBusinessDayService
    {
        int CalculateBusinessDays(DateRange dateRange);
    }
}
