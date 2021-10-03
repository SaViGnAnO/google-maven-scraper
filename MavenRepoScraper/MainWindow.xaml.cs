using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ScrapeTheRepo;
using ScrapeTheRepo.Helpers;
using ScrapeTheRepo.Interfaces;
using ScrapeTheRepo.Repositories;
using ScrapeTheRepo.Structs;

namespace MavenRepoScraper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IRepository Repository;
        private DataScraper Scraper;

        public MasterIndex? MasterIndex;
        public List<Group>? GroupIndexList;
        public List<Artifact> Artifacts;

        public MainWindow()
        {
            InitializeComponent();
            Repository = new GoogleMavenRepository();
            Scraper = new DataScraper(Repository, true);

            GetArtifactList().GetAwaiter().GetResult();

            var listItems = new List<ArtifactListItem>();

            foreach(var artifact in Artifacts!)
            {
                listItems.Add(new ArtifactListItem(false, artifact.GroupName, artifact.ArtifactName));
            }

            ArtifactList.ItemsSource = listItems;
        }

        public async Task GetArtifactList()
        {
            MasterIndex = await Scraper!.GetMasterIndex();
            GroupIndexList = new List<Group>();

            foreach (var groupId in MasterIndex?.GroupIds!)
            {
                GroupIndexList.Add(await Scraper.GetGroupIndex(groupId));
            }

            foreach (var group in GroupIndexList)
            {
                foreach (var artifact in group.ArtifactList)
                {
                    Artifacts.Add(artifact);
                }
            }
        }

        public class ArtifactListItem : ListItem
        {
            public bool IsChecked { get; set; }
            public string GroupName { get; set; }
            public string ArtifactName { get; set; }

            public ArtifactListItem(bool isChecked, string groupName, string artifactName)
            {
                IsChecked = isChecked;
                GroupName = groupName;
                ArtifactName = artifactName;
            }
        }
    }
}
