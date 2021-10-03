using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapeTheRepo.Enums;
using ScrapeTheRepo.Interfaces;

namespace ScrapeTheRepo.Repositories
{
    public class GoogleMavenRepository : IRepository
    {
        public RepositoryType RepositoryType => RepositoryType.MAVEN;

        public string RepositoryName => "Google Maven Repository";

        public string BaseUrl => "https://dl.google.com/android/maven2/";

        public string MasterIndexPath => "master-index.xml";

        public string GroupIndexPath(string groupId) => $"{groupId.Replace('.', '/')}/group-index.xml";

        public string ArtifactPath(string groupId, string version) => $"{BaseUrl}{groupId}/{version}";
    }
}
