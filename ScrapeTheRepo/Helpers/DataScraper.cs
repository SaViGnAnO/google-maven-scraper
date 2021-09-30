using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PuppeteerSharp;
using System.Xml;
using ScrapeTheRepo.Interfaces;
using System.Net.Http;
using System.Xml.Serialization;
using ScrapeTheRepo.Structs;

namespace ScrapeTheRepo.Helpers
{
    public class DataScraper
    {
        private Browser _browser;
        private Page _page;
        private IRepository _repository;
        private HttpClient _client;

        public DataScraper(IRepository repository, bool isHeadless)
        {
            var browseFetcher = new BrowserFetcher(new BrowserFetcherOptions());
            var revisionInfo = browseFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision).GetAwaiter().GetResult();

            _browser = Puppeteer.LaunchAsync(new LaunchOptions { Headless = isHeadless, ExecutablePath = revisionInfo.ExecutablePath }).GetAwaiter().GetResult();
            _page = _browser.NewPageAsync().GetAwaiter().GetResult();
            _repository = repository;
            _client = new HttpClient();
        }

        public async Task<MasterIndex> GetMasterIndex()
        {
            MasterIndex masterIndex;

            try
            {
                var xmlResponse = await _client.GetStreamAsync($"{_repository.BaseUrl}{_repository.MasterIndexPath}");
                var serializer = new XmlSerializer(typeof(MasterIndex));

                masterIndex = (MasterIndex)serializer.Deserialize(xmlResponse)!;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return masterIndex;
        }

        public async Task<GroupIndex> GetGroupIndex(GroupId groupId)
        {
            var xmlResponse = await _client.GetStreamAsync($"{_repository.BaseUrl}{_repository.GroupIndexPath(groupId.Name)}");
            var serializer = new XmlSerializer(typeof(GroupIndex));

            var groupIndex = (GroupIndex)serializer.Deserialize(xmlResponse)!;

            return groupIndex!;
        }

        #region IDisposable

        /// <inheritdoc />
        public async Task Close() => await _browser.CloseAsync();

        #endregion
    }
}
