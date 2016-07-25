using Microsoft.Tools.WindowsInstallerXml;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class FeatureComponentsInfo
    {
        public string Feature_ { get; set; }
        public string Component_ { get; set; }

        static public FeatureComponentsInfo FromRow(Row row)
        {
            FeatureComponentsInfo featureComponent = new FeatureComponentsInfo();

            TableDefinition definition = row.TableDefinition;

            featureComponent.Feature_ = (string)row[definition.Columns.FindFieldIndex("Feature_")];
            featureComponent.Component_ = (string)row[definition.Columns.FindFieldIndex("Component_")];

            return featureComponent;
        }
    }
}
