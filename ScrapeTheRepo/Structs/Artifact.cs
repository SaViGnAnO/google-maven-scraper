using ScrapeTheRepo.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeTheRepo.Structs
{
    public struct Artifact
    {
        public string GroupName { get; set; }
        public string ArtifactName { get; set; }
        public List<string> Versions { get; set; }
    }
}
