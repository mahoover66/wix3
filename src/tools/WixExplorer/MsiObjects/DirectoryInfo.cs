using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Tools.WindowsInstallerXml;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class DirectoryInfo
    {
        public string Directory { get; set; }
        public string Directory_Parent { get; set; }
        public string DefaultDir { get; set; }

        public string DirectoryName { get; set; }
        public string FullPath { get; set; }
        public DirectoryInfo Parent { get; set; }

        static public DirectoryInfo FromRow(Row row)
        {
            DirectoryInfo directory = new DirectoryInfo();

            TableDefinition definition = row.TableDefinition;

            directory.Directory = (string)row[definition.Columns.FindFieldIndex("Directory")];
            directory.Directory_Parent = (string)row[definition.Columns.FindFieldIndex("Directory_Parent")];
            directory.DefaultDir = (string)row[definition.Columns.FindFieldIndex("DefaultDir")];

            return directory;
        }

        public void ComputeMetadata(WixPdbInfo WixPdbInfo)
        {
            DirectoryName = s_dirNameRegex.Value.Replace(DefaultDir, "");

            // start FullPath as the current directory path
            FullPath = DirectoryName;

            // now get the parent
            DirectoryInfo parent;
            if (!string.IsNullOrEmpty(Directory_Parent) && WixPdbInfo.Directories.TryGetValue(Directory_Parent, out parent))
            {
                // we have a parent
                Parent = parent;

                // has its metadata been computed?
                if (string.IsNullOrEmpty(Parent.FullPath))
                {
                    Parent.ComputeMetadata(WixPdbInfo);
                }

                // compute the final full path
                if (DirectoryName.Equals("."))
                {
                    FullPath = Parent.FullPath;
                }
                else
                {
                    FullPath = Path.Combine(Parent.FullPath, DirectoryName);
                }
            }
        }

        private static readonly Lazy<Regex> s_dirNameRegex = new Lazy<Regex>(() => new Regex(@"^.*\|", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled));
    }
}
