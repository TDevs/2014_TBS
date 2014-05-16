using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace TDevs.Core
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> result, int total)
        {
            Result = new ReadOnlyCollection<T>(new List<T>(result));
            Total = total;
        }

        public PagedResult()
            : this(new List<T>(), 0)
        {
        }

        public ICollection<T> Result { get; private set; }

        public int Total { get; private set; }

        public bool IsEmpty
        {
            [DebuggerStepThrough]
            get
            {
                return Result.Count == 0 || Total == 0;
            }
        }
    }
}
