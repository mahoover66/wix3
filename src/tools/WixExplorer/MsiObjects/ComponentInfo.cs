using System;
using Microsoft.Tools.WindowsInstallerXml;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    // values from http://msdn.microsoft.com/en-us/library/aa368007(v=vs.85).aspx
    [Flags]
    public enum ComponentAttributes
    {
        msidbComponentAttributesLocalOnly = 0x0000,
        msidbComponentAttributesSourceOnly = 0x0001,
        msidbComponentAttributesOptional = 0x0002,
        msidbComponentAttributesRegistryKeyPath = 0x0004,
        msidbComponentAttributesSharedDllRefCount = 0x0008,
        msidbComponentAttributesPermanent = 0x0010,
        msidbComponentAttributesODBCDataSource = 0x0020,
        msidbComponentAttributesTransitive = 0x0040,
        msidbComponentAttributesNeverOverwrite = 0x0080,
        msidbComponentAttributes64bit = 0x0100,
        msidbComponentAttributesDisableRegistryReflection = 0x0200,
        msidbComponentAttributesUninstallOnSupersedence = 0x0400,
        msidbComponentAttributesShared = 0x0800,
    }

    public class ComponentInfo
    {
        public string Component { get; set; }
        public string ComponentId { get; set; }
        public string Directory_ { get; set; }
        public ComponentAttributes Attributes { get; set; }
        public string Condition { get; set; }
        public string KeyPath { get; set; }

        public Guid ComponentGuid { get; set; }

        static public ComponentInfo FromRow(Row row)
        {
            ComponentInfo component = new ComponentInfo();

            TableDefinition definition = row.TableDefinition;

            component.Component = (string)row[definition.Columns.FindFieldIndex("Component")];
            component.ComponentId = (string)row[definition.Columns.FindFieldIndex("ComponentId")];
            component.Directory_ = (string)row[definition.Columns.FindFieldIndex("Directory_")];
            component.Attributes = (ComponentAttributes)row[definition.Columns.FindFieldIndex("Attributes")];
            component.Condition = (string)row[definition.Columns.FindFieldIndex("Condition")];
            component.KeyPath = (string)row[definition.Columns.FindFieldIndex("KeyPath")];

            if (!string.IsNullOrEmpty(component.ComponentId) && !component.ComponentId.Equals("*"))
            {
                component.ComponentGuid = new Guid(component.ComponentId);
            }

            return component;
        }
    }
}
