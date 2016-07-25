using System;
using Microsoft.Tools.WindowsInstallerXml;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    // values from http://msdn.microsoft.com/en-us/library/aa368585(v=vs.85).aspx
    [Flags]
    public enum FeatureAttributes
    {
        msidbFeatureAttributesFavorLocal = 0,
        msidbFeatureAttributesFavorSource = 1,
        msidbFeatureAttributesFollowParent = 2,
        msidbFeatureAttributesFavorAdvertise = 4,
        msidbFeatureAttributesDisallowAdvertise = 8,
        msidbFeatureAttributesUIDisallowAbsent = 16,
        msidbFeatureAttributesNoUnsupportedAdvertise = 32,
    }

    public class FeatureInfo
    {
        public string Feature { get; set; }
        public string Feature_Parent { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Display { get; set; }
        public int Level { get; set; }
        public string Directory_ { get; set; }
        public FeatureAttributes Attributes { get; set; }

        static public FeatureInfo FromRow(Row row)
        {
            FeatureInfo feature = new FeatureInfo();

            TableDefinition definition = row.TableDefinition;

            feature.Feature = (string)row[definition.Columns.FindFieldIndex("Feature")];
            feature.Feature_Parent = (string)row[definition.Columns.FindFieldIndex("Feature_Parent")];
            feature.Title = (string)row[definition.Columns.FindFieldIndex("Title")];
            feature.Description = (string)row[definition.Columns.FindFieldIndex("Description")];
            feature.Display = (int)row[definition.Columns.FindFieldIndex("Display")];
            feature.Level = (int)row[definition.Columns.FindFieldIndex("Level")];
            feature.Directory_ = (string)row[definition.Columns.FindFieldIndex("Directory_")];
            feature.Attributes = (FeatureAttributes)row[definition.Columns.FindFieldIndex("Attributes")];

            return feature;
        }
    }
}
