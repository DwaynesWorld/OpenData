namespace OpenData.Core
{
    using System.Globalization;

    internal static class StringExtensions
    {
        internal static string FormatWith(this string value, string arg0)
            => string.Format(CultureInfo.InvariantCulture, value, arg0);
    }
}