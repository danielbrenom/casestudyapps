namespace Foundation.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return str is null || str == string.Empty;
        }
    }
}