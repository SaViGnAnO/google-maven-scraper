using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapeTheRepo.Configurations;
using ScrapeTheRepo.Interfaces;

namespace ScrapeTheRepo.Helpers
{
    public class Configurator
    {
        public IRepository Repository { get; set; }
        public BinderatorArtifactConfigEntry[]? Artifacts { get; private set; }

        public Configurator(IRepository repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
