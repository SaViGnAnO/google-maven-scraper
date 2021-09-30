using System;

namespace ScrapeTheRepo.Configurations
{
    public class BinderatorArtifactConfigEntry
    {
        public BinderatorArtifactConfigEntry(string groupId, string artifactId, string version, string? nugetId,
            string? nugetVersion, bool? isDependencyOnly)
        {
            GroupId = groupId ?? throw new ArgumentNullException(nameof(groupId));
            ArtifactId = artifactId ?? throw new ArgumentNullException(nameof(artifactId));
            Version = version ?? throw new ArgumentNullException(nameof(version));
            NugetId = nugetId;
            NugetVersion = nugetVersion;
            IsDependencyOnly = isDependencyOnly;
        }

        public string GroupId { get; set; }
        public string ArtifactId { get; set; }
        public string Version { get; set; }
        public string? NugetId { get; set; }
        public string? NugetVersion { get; set; }
        public bool? IsDependencyOnly { get; set; }
    }
}
