namespace BusStationCRM.BLL.Extensions
{
    public static class StringExtensions
    {
        public static string NormalizeSearchString(this string search)
        {
            return search.Replace(",", "").Replace(".", "").Replace("?", "").Trim();
        }
    }
}
