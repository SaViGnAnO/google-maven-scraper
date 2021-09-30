using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ScrapeTheRepo.Structs
{
    public struct Group
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }

    public struct GroupId
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }

    public struct GroupIndex {

    }
}
