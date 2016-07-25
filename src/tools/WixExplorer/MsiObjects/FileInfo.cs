using System;
using Microsoft.Tools.WindowsInstallerXml;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    // values from http://msdn.microsoft.com/en-us/library/aa368596(v=vs.85).aspx
    [Flags]
    public enum FileAttributes
    {
        msidbFileAttributesReadOnly = 0x000001,
        msidbFileAttributesHidden = 0x000002,
        msidbFileAttributesSystem = 0x000004,
        msidbFileAttributesVital = 0x000200,
        msidbFileAttributesChecksum = 0x000400,
        msidbFileAttributesPatchAdded = 0x001000,
        msidbFileAttributesNoncompressed = 0x002000,
        msidbFileAttributesCompressed = 0x004000,
    }

    public class FileInfo
    {
        public string File { get; set; }
        public string Component_ { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string Version { get; set; }
        public string Language { get; set; }
        public FileAttributes Attributes { get; set; }
        public int Sequence { get; set; }

        static public FileInfo FromRow(Row row)
        {
            FileInfo file = new FileInfo();

            TableDefinition definition = row.TableDefinition;

            file.File = (string)row[definition.Columns.FindFieldIndex("File")];
            file.Component_ = (string)row[definition.Columns.FindFieldIndex("Component_")];
            file.FileName = (string)row[definition.Columns.FindFieldIndex("FileName")];
            file.FileSize = (int)row[definition.Columns.FindFieldIndex("FileSize")];
            file.Version = (string)row[definition.Columns.FindFieldIndex("Version")];
            file.Language = (string)row[definition.Columns.FindFieldIndex("Language")];
            file.Attributes = (FileAttributes)row[definition.Columns.FindFieldIndex("Attributes")];
            file.Sequence = (int)row[definition.Columns.FindFieldIndex("Sequence")];

            return file;
        }
    }
}
