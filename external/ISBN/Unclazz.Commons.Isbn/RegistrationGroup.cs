using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Unclazz.Commons.Isbn
{
    sealed class RegistrationGroup
    {
        internal RegistrationGroup(string prefix, string agency, IEnumerable<RegistrationGroupRule> rules)
        {
            CheckUtility.MustNotBeEmpty(prefix, nameof(prefix));
            CheckUtility.MustNotBeEmpty(agency, nameof(agency));
            CheckUtility.MustNotBeEmpty(rules, nameof(rules));

            Prefix = prefix;
            PrefixWithoutHyphen = prefix.Replace("-", string.Empty);
            Agency = agency;
            Rules = rules;
        }

        internal string Prefix { get; }
        internal string PrefixWithoutHyphen { get; }
        internal string Agency { get; }
        internal IEnumerable<RegistrationGroupRule> Rules { get; }
    }
}
