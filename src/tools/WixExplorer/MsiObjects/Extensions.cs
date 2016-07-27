using System;
using System.Collections.Generic;
using Microsoft.Tools.WindowsInstallerXml;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public static class Extensions
    {
        public static int FindFieldIndex(this ColumnDefinitionCollection columns, string name)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                if (columns[i].Name.Equals(name, StringComparison.Ordinal))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int CompareTo(this Row current, Row other)
        {
            int compare = current.TableDefinition.CompareTo(other.TableDefinition);

            ColumnDefinitionCollection columns = current.TableDefinition.Columns;
            for (int i = 0; 0 == compare && i < columns.Count; ++i)
            {
                if (!columns[i].IsPrimaryKey)
                {
                    // since all of the primary keys come first, if this isn't a primary key,
                    // there aren't any more primary keys.  We are treating rows with matching
                    // primary keys as equal, even if other fields have different data
                    break;
                }

                compare = current.Fields[i].CompareTo(other.Fields[i]);
            }

            return compare;
        }

        public static int CompareTo(this Field current, Field other)
        {
            if (null == other)
            {
                // everything is greater than null
                return 1;
            }

            int compare;
            if (current.Column.Type == other.Column.Type)
            {
                // types are the same, so compare the values
                switch (current.Column.Type)
                {
                    case ColumnType.Number:
                        int currentNum = (int)current.Data;
                        int otherNum = (int)other.Data;
                        if (currentNum == otherNum)
                        {
                            compare = 0;
                        }
                        else if (currentNum < otherNum)
                        {
                            compare = -1;
                        }
                        else
                        {
                            compare = 1;
                        }
                        break;

                    case ColumnType.String:
                    case ColumnType.Localized:
                        compare = string.Compare((string)current.Data, (string)other.Data, StringComparison.Ordinal);
                        break;

                    default:
                        // punt on comparing binaries
                        compare = 0;
                        break;
                }
            }
            else if (current.Column.Type < other.Column.Type)
            {
                // different type
                compare = -1;
            }
            else
            {
                // different type
                compare = 1;
            }

            return compare;
        }
    }
}
