using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ScrapeTheRepo.Structs
{
    public struct Group {
        public string GroupName {  get; set; }
        public List<Artifact> ArtifactList {  get; set; }
    }
}
