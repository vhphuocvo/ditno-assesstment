using System.Globalization;

namespace DitnoCalculateBusinessDay.Tests
{
    public class TestHelper
    {
        private const string DateFormat = "dd/MM/yyyy";

        public static DateTime ConvertToDate(string dateString)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(dateString, DateFormat, provider);
        }
    }
}
