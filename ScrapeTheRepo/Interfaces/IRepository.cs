using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PuppeteerSharp;

using ScrapeTheRepo.Enums;

namespace ScrapeTheRepo.Interfaces
{
    public interface IRepository
    {
        RepositoryType RepositoryType { get; }
        string RepositoryName { get; }
        string BaseUrl { get; }
        string MasterIndexPath { get; }
        string GroupIndexPath(string groupId);
        string ArtifactPath(string groupId, string version);
    }
}
