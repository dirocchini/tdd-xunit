using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TDDxUnitCore.CrossCutting.Extensions
{
    public static class StringExtensions
    {
        public static string CleanDocument(this string value)
        {
            return value
                .Replace(".", "")
                .Replace("-", "")
                .Replace(" ", "");
        }
        public static string CleanCNPJ(this string value)
        {
            return value
                .Replace(".", "")
                .Replace("-", "")
                .Replace("\\", "")
                .Replace("/", "")
                .Replace(" ", "");
        }

        public static string ComparableString(this string value)
        {
            return Regex.Replace(value, @"[^\w\d]", "").ToLower();
        }
    }
}
