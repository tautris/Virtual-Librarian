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
    static class RegistrationGroups
    {
        static IEnumerable<RegistrationGroup> _cache = null;
        static internal IEnumerable<RegistrationGroup> LoadGroups()
        {
            Func<XElement, RegistrationGroupRule> rule = r =>
            {
                return new RegistrationGroupRule(r.Element("Range").Value,
                    int.Parse(r.Element("Length").Value));
            };
            Func<XElement, RegistrationGroup> group = g =>
            {
                var rs = g.Descendants("Rule").Select(rule).ToList();
                return new RegistrationGroup(g.Element("Prefix").Value, g.Element("Agency").Value, rs);
            };

            var xmlPath = GetRangeMessageXmlPath();
            _cache = XDocument.Load(xmlPath)
                .Descendants("RegistrationGroups")
                .Elements("Group")
                .Select(group).ToList();
            return _cache;
        }
        static internal IEnumerable<RegistrationGroup> GetGroups()
        {
            if (_cache == null)
            {
                return LoadGroups();
            }
            else
            {
                return _cache;
            }
        }
        static string GetRangeMessageXmlPath()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(dir, "RangeMessage.xml");
        }
    }
}
