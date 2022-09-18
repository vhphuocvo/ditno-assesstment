using DitnoCalculateBusinessDay.Models;
using Microsoft.Extensions.Logging;

namespace DitnoCalculateBusinessDay.Services
{
    public class BusinessDayService : IBusinessDayService
    {
        public BusinessDayService(ILogger<BusinessDayService> logger)
        {
        }

        /// <summary>
        /// Calculate business days between two dates, exclude the from date and to date.
        /// </summary>
        /// <param name="dateRange">Range of date to calculate, FROM -> TO</param>
        /// <returns>Calculated number of business days</returns>
        public int CalculateBusinessDays(DateRange dateRange)
        {
            var fromDate = dateRange.FromDate;
            var toDate = dateRange.ToDate;
            var excludedDates = new List<DateTime>
            {
                fromDate,
                toDate
            };

            Func<int, bool> isBusinessDay = days =>
            {
                var currentDate = fromDate.AddDays(days);
                var isNotBusinessDay = currentDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday ||
                                       excludedDates.Exists(x => x.Date == currentDate.Date);
                return !isNotBusinessDay;
            };
            return Enumerable.Range(0, (toDate - fromDate).Days).Count(isBusinessDay);
        }
    }
}
