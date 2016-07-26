using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class DiffRootViewModel : MsiTreeViewItemViewModel
    {
        public DiffRootViewModel(WixPdbInfo leftWixPdbInfo, WixPdbInfo rightWixPdbInfo)
            : base(wixPdbInfo: null, parent: null, lazyLoadChildren: true)
        {
            LeftWixPdbInfo = leftWixPdbInfo;
            RightWixPdbInfo = rightWixPdbInfo;
        }

        public WixPdbInfo LeftWixPdbInfo { get; protected set; }
        public WixPdbInfo RightWixPdbInfo { get; protected set; }

        protected override void LoadChildren()
        {
            Table[] leftTables = new Table[LeftWixPdbInfo.Tables.Count];
            LeftWixPdbInfo.Tables.CopyTo(leftTables, 0);
            IEnumerator<Table> enumLeft = leftTables.OrderBy(t => t.Name).GetEnumerator();

            Table[] rightTables = new Table[RightWixPdbInfo.Tables.Count];
            RightWixPdbInfo.Tables.CopyTo(rightTables, 0);
            IEnumerator<Table> enumRight = rightTables.OrderBy(t => t.Name).GetEnumerator();

            bool nextLeft = enumLeft.MoveNext();
            bool nextRight = enumRight.MoveNext();

            while (nextLeft || nextRight)
            {
                if (nextLeft && nextRight)
                {
                    // we have both items, compare them
                    int compare = string.Compare(enumLeft.Current.Name, enumRight.Current.Name, StringComparison.Ordinal);
                    if (compare < 0)
                    {
                        // left is smaller, only add it
                        AddChildTable(enumLeft.Current, null);
                        nextLeft = enumLeft.MoveNext();
                    }
                    else if (compare == 0)
                    {
                        // they have the same name, use them both
                        AddChildTable(enumLeft.Current, enumRight.Current);
                        nextLeft = enumLeft.MoveNext();
                        nextRight = enumRight.MoveNext();
                    }
                    else
                    {
                        // right is smaller, only add it
                        AddChildTable(null, enumRight.Current);
                        nextRight = enumRight.MoveNext();
                    }
                }
                else if (nextLeft)
                {
                    // we only have left items remaining, use them
                    AddChildTable(enumLeft.Current, null);
                    nextLeft = enumLeft.MoveNext();
                }
                else
                {
                    // we only have right items remaining, use them
                    AddChildTable(null, enumRight.Current);
                    nextRight = enumRight.MoveNext();
                }
            }
        }

        private void AddChildTable(Table left, Table right)
        {
            DiffTableViewModel diffTableViewMode = new DiffTableViewModel(LeftWixPdbInfo, left, RightWixPdbInfo, right, this);
            Children.Add(diffTableViewMode);
        }
    }
}
