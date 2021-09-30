using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ScrapeTheRepo.Structs
{
    [XmlRoot]
    public struct MasterIndex
    {
        [XmlArray("metadata")]
        public List<GroupId> GroupIds;
    }
}
