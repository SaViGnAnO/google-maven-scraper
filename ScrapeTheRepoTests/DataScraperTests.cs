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
using System.Linq;

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

            await _scraper!.Close();
            Assert.True(_masterIndex!.GroupIds?.Count >= 200);
        }

        [Test]
        public async Task GoogleMavenGetGroupIndexReturnsXmlNodeList()
        {
            _masterIndex = await _scraper!.GetMasterIndex();
            var groupIndexList = new List<Group>();

            foreach(var groupId in _masterIndex.GroupIds!)
            {
                var groupIndex = await _scraper!.GetGroupIndex(groupId);
                groupIndexList.Add(groupIndex);
            }

            await _scraper!.Close();
            Assert.True(groupIndexList.Count > 0);
        }

        [Test]
        public async Task GetGroupIndexReturnsOk()
        {
            _masterIndex = await _scraper!.GetMasterIndex();
            var groupIndexList = new List<Group>();

            foreach (var groupId in _masterIndex.GroupIds!)
            {
                var groupIndex = await _scraper!.GetGroupIndex(groupId);
                groupIndexList.Add(groupIndex);
            }

            Console.WriteLine(JsonConvert.SerializeObject(groupIndexList));

            await _scraper!.Close();
            Assert.True(_masterIndex!.GroupIds?.Count >= 200);
        }

        [Test]
        public async Task GoogleMavenGetArtifactsReturnsOk()
        {
            _masterIndex = await _scraper!.GetMasterIndex();
            var groupIndexList = new List<Group>();
            var artifactList = new List<Artifact>();

            foreach (var groupId in _masterIndex.GroupIds!)
            {
                var groupIndex = await _scraper!.GetGroupIndex(groupId);
                groupIndexList.Add(groupIndex);
            }

            foreach(var group in groupIndexList)
            {
                foreach(var a in group.ArtifactList)
                {
                    a.Versions.Reverse();
                    await _scraper!.GetArtifactFiles(group.GroupName, a.ArtifactName, a.Versions[0], $"{AppContext.BaseDirectory}Artifacts\\");
                }
            }

            await _scraper!.Close();
            Assert.True(_masterIndex!.GroupIds?.Count >= 200);
        }
    }
}