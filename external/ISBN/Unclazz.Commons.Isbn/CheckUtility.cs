using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unclazz.Commons.Isbn
{
    static class CheckUtility
    {
        internal static void MustNotBeNull(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        internal static void MustNotBeEmpty<T> (IEnumerable<T> value, string name)
        {
            MustNotBeNull(value, name);
            if (value.Count() == 0)
            {
                throw new ArgumentException(string.Format("Value of {0} must not be empty.", name));
            }
        }

        internal static void MustNotBeEmpty(string value, string name)
        {
            MustNotBeEmpty<char>(value, name);
        }

        internal static void MustBeBetween(int value, int min, int max, string name)
        {
            if (value < min || max < value)
            {
                throw new ArgumentOutOfRangeException(string.Format("Value of {0} must be between {1} and {2}.", name, min, max));
            }
        }

        internal static void MustBeGreaterThanX(int value, int x, string name)
        {
            if (value <= x)
            {
                throw new ArgumentOutOfRangeException(string.Format("Value of {0} must be greater than {1}", name, x));
            }
        }

        internal static void MustBeGreaterThan0(int value, string name)
        {
            MustBeGreaterThanX(value, 0, name);
        }

        internal static void MustBeGreaterThanOrEqual0(int value, string name)
        {
            MustBeGreaterThanX(value, -1, name);
        }
    }
}
