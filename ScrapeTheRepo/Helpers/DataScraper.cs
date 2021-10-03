using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;
using PuppeteerSharp;
using System.Xml;
using ScrapeTheRepo.Interfaces;
using System.Net.Http;
using System.Xml.Serialization;
using ScrapeTheRepo.Structs;
using ScrapeTheRepo.Enums;
using ScrapeTheRepo.Extensions;
using Newtonsoft.Json;

namespace ScrapeTheRepo.Helpers
{
    public class DataScraper
    {
        private Browser _browser;
        private Page _page;
        private IRepository _repository;
        private HttpClient _client;
        private Group _groupIndex;
        private BrowserFetcher _fetcher;

        public DataScraper(IRepository repository, bool isHeadless)
        {
            _fetcher = new BrowserFetcher(new BrowserFetcherOptions());
            var revisionInfo = GetBrowserRevision().GetAwaiter().GetResult();

            _browser = Puppeteer.LaunchAsync(new LaunchOptions { Headless = isHeadless, ExecutablePath = revisionInfo.ExecutablePath }).GetAwaiter().GetResult();
            _page = _browser.NewPageAsync().GetAwaiter().GetResult();
            _repository = repository;
            _client = new HttpClient();
        }

        public async Task<RevisionInfo> GetBrowserRevision()
        {
            return await _fetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
        }

        public async Task<MasterIndex> GetMasterIndex()
        {
            List<string>? masterIndexGroups;

            try
            {
                var xmlResponse = await _client.GetStringAsync($"{_repository.BaseUrl}{_repository.MasterIndexPath}");
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlResponse);

                //remove me
                masterIndexGroups = new List<string>();

                var metadataNode = xmlDocument.SelectSingleNode("metadata");

                foreach(XmlNode node in metadataNode!.ChildNodes)
                {
                    masterIndexGroups.Add(node.Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return new MasterIndex { GroupIds = masterIndexGroups! };
        }

        public async Task<Group> GetGroupIndex(string groupId)
        {
            var groupIndexUrl = $"{_repository.BaseUrl}{_repository.GroupIndexPath(groupId)}";
            var xmlResponse = await _client.GetStringAsync(groupIndexUrl);
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlResponse);

            var groupIndex = xmlDocument.SelectSingleNode(groupId);
            List<Artifact> allVersions = new List<Artifact>();

            foreach(XmlNode node in groupIndex!.ChildNodes)
            {
                var versions = node.Attributes!["versions"]!.Value!.Split(',').ToList();
                allVersions.Add(new Artifact
                {
                    GroupName = groupId,
                    ArtifactName = node.Name,
                    Versions = versions
                });
            }
            _groupIndex = new Group { GroupName = groupId, ArtifactList = allVersions };
            return _groupIndex;
        }

        public async Task GetArtifactFiles(string groupId, string artifact, string version, string destinationPath)
        {
            var artifactUrlList = new List<string>();
            var fileExtList = new List<string>()
            {
                ".pom",
                ".module",
                ".aar",
                "-sources.jar",
                "-javadoc.jar"
            };

            foreach(var fileExt in fileExtList)
            {
                try
                {
                    var url = $"{_repository.BaseUrl}{groupId.Replace('.', '/')}/{artifact}/{version}/{artifact}-{version}{fileExt}";
                    Console.WriteLine(url);
                    var fileBytes = await _client.GetByteArrayAsync(url);

                    Directory.CreateDirectory($"{destinationPath}{artifact}\\{version}\\");
                    await File.WriteAllBytesAsync($"{destinationPath}{artifact}\\{version}\\{artifact}-{version}{fileExt}", fileBytes);
                } catch (HttpRequestException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public async Task Close() => await _browser.CloseAsync();
    }
}
