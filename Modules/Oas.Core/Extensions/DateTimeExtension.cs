using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using TDevs.Core.Constants;


namespace TDevs.Core
{
    public static class DateTimeExtension
    {
        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        private static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);

        [DebuggerStepThrough]
        public static bool IsValid(this DateTime target)
        {
            return (target >= MinDate) && (target <= MaxDate);
        }

        public static string GetDateTime(DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString(GlobalConstants.DateTimeFormat) : string.Empty;
        }

        public static string AnyToDateString(object inData)
        {
            try
            {
                DateTime date = Convert.ToDateTime(inData);
                return date.ToShortDateString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
