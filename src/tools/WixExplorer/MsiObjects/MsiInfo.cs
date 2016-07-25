using System.Collections.Generic;
using System.Linq;
using Microsoft.Tools.WindowsInstallerXml;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class MsiInfo
    {
        public MsiInfo(string filePath)
        {
            FilePath = filePath;
        }

        public string Title { get; set; }
        public string Subject { get; set; }
        public string Comments { get; set; }
        public string Keywords { get; set; }
        public string Platform { get; set; }
        public string PackageCode { get; set; }
        public string Schemna { get; set; }
        public string Languages { get; set; }

        public string FilePath { get; set; }
        public Pdb Pdb { get; set; }

        public Dictionary<string, FeatureInfo> Features { get; set; }
        public Dictionary<string, ComponentInfo> Components { get; set; }
        public List<FeatureComponentsInfo> FeatureComponents { get; set; }
        public Dictionary<string, DirectoryInfo> Directories { get; set; }
        public Dictionary<string, FileInfo> Files { get; set; }
        public Dictionary<string, RegistryInfo> RegistryEntries { get; set; }

        public TableCollection Tables
        {
            get
            {
                return Pdb.Output.Tables;
            }
        }

        public void Open()
        {
            Pdb = Pdb.Load(FilePath, false, false);

            Output output = Pdb.Output;

            Features = new Dictionary<string, FeatureInfo>();
            foreach (Row row in output.Tables["Feature"].Rows)
            {
                FeatureInfo feature = FeatureInfo.FromRow(row);
                Features[feature.Feature] = feature;
            }

            Components = new Dictionary<string, ComponentInfo>();
            foreach (Row row in output.Tables["Component"].Rows)
            {
                ComponentInfo component = ComponentInfo.FromRow(row);
                Components[component.Component] = component;
            }

            FeatureComponents = new List<FeatureComponentsInfo>();
            foreach (Row row in output.Tables["FeatureComponents"].Rows)
            {
                FeatureComponentsInfo featureComponent = FeatureComponentsInfo.FromRow(row);
                FeatureComponents.Add(featureComponent);
            }

            Directories = new Dictionary<string, DirectoryInfo>();
            foreach (Row row in output.Tables["Directory"].Rows)
            {
                DirectoryInfo directory = DirectoryInfo.FromRow(row);
                Directories[directory.Directory] = directory;
            }
            foreach (string directory in Directories.Keys.OrderBy(key => key))
            {
                DirectoryInfo directoryInfo = Directories[directory];
                directoryInfo.ComputeMetadata(this);
            }

            Files = new Dictionary<string, FileInfo>();
            foreach (Row row in output.Tables["File"].Rows)
            {
                FileInfo file = FileInfo.FromRow(row);
                Files[file.File] = file;
            }

            RegistryEntries = new Dictionary<string, RegistryInfo>();
            foreach (Row row in output.Tables["Registry"].Rows)
            {
                RegistryInfo registry = RegistryInfo.FromRow(row);
                RegistryEntries[registry.Registry] = registry;
            }
        }
    }
}
