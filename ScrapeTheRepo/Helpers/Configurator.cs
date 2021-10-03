using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapeTheRepo.Configurations;
using ScrapeTheRepo.Interfaces;
using ScrapeTheRepo.Structs;

namespace ScrapeTheRepo.Helpers
{
    public class Configurator
    {
        public IRepository Repository { get; set; }
        public List<BinderatorArtifactConfigEntry>? Artifacts { get; private set; }

        public Configurator(IRepository repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task GenerateConfig(List<Artifact> artifacts)
        {
            string json;

            foreach(var artifact in artifacts)
            {
                //var configObj = new BinderatorArtifactConfigEntry(artifact.);
                //json.Append(JsonConvert.SerializeObject(artifact));
            }
        }
    }
}
