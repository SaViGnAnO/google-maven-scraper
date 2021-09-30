using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using NUnit.Framework;

using ScrapeTheRepo.Helpers;
using ScrapeTheRepo.Enums;
using ScrapeTheRepo.Interfaces;
using ScrapeTheRepo.Repositories;
using ScrapeTheRepo.Structs;

namespace ScrapeTheRepoTests
{
    public class DataScraperTests
    {
        private IRepository? _repository;
        private DataScraper? _scraper;

        private MasterIndex? _masterIndex;

        [SetUp]
        public void Setup()
        {
            _repository = new GoogleMavenRepository();
            _scraper = new DataScraper(_repository!, true);
        }

        [Test]
        public async Task GoogleMavenGetMasterIndexReturnsXmlNodeList()
        {
            _masterIndex = await _scraper!.GetMasterIndex();

            await _scraper.Close();
            Assert.IsNotNull(_masterIndex);
        }

        [Test]
        public async Task GoogleMavenGetGroupIndexReturnsXmlNodeList()
        {
            _masterIndex = await _scraper!.GetMasterIndex();

            //var resultList = new List<XmlNodeList>();
            //foreach (XmlNode groupId in _masterIndex!.GroupIds)
            //{
            //    resultList.Add(await _scraper!.GetGroupIndex(_repository!.GroupIndexPath(group.Name)));
            //}

            

            await _scraper!.Close();
            Assert.Pass();
        }
    }
}