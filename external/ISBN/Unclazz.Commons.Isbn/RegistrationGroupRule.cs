using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unclazz.Commons.Isbn
{
    sealed class RegistrationGroupRule
    {
        internal RegistrationGroupRule(string range, int length)
        {
            CheckUtility.MustNotBeEmpty(range, nameof(range));
            CheckUtility.MustBeGreaterThanOrEqual0(length, nameof(length));

            var rs = range.Split('-');

            RangeStart = rs[0].Substring(0, length);
            RangeEnd = rs[1].Substring(0, length);
            Length = length;
        }

        internal string RangeStart { get; }
        internal string RangeEnd { get; }
        internal int Length { get; }
    }
}
