using System;
using System.Diagnostics;

namespace TDevs.Core
{
    public static class SystemTime
    {
        private static Func<DateTime> now = () => DateTime.UtcNow;

        public static DateTime Now
        {
            [DebuggerStepThrough]
            get
            {
                return now();
            }

            [DebuggerStepThrough]
            set
            {
                now = () => value;
            }
        }
    }
}