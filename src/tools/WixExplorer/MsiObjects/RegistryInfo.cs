using Microsoft.Tools.WindowsInstallerXml;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    // values from http://msdn.microsoft.com/en-us/library/aa371168(v=vs.85).aspx
    public enum RegistryRoot
    {
        msidbRegistryRootNone = -1,
        msidbRegistryRootClassesRoot = 0,
        msidbRegistryRootCurrentUser = 1,
        msidbRegistryRootLocalMachine = 2,
        msidbRegistryRootUsers = 3,
    }

    public class RegistryInfo
    {
        public string Registry { get; set; }
        public RegistryRoot Root { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Component_ { get; set; }

        static public RegistryInfo FromRow(Row row)
        {
            RegistryInfo registry = new RegistryInfo();

            TableDefinition definition = row.TableDefinition;

            registry.Registry = (string)row[definition.Columns.FindFieldIndex("Registry")];
            registry.Root = (RegistryRoot)row[definition.Columns.FindFieldIndex("Root")];
            registry.Key = (string)row[definition.Columns.FindFieldIndex("Key")];
            registry.Name = (string)row[definition.Columns.FindFieldIndex("Name")];
            registry.Value = (string)row[definition.Columns.FindFieldIndex("Value")];
            registry.Component_ = (string)row[definition.Columns.FindFieldIndex("Component_")];

            return registry;
        }
    }
}
