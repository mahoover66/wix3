using System;
using System.Collections;
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
            IEnumerator enumLeft = LeftWixPdbInfo.Tables.GetEnumerator();
            IEnumerator enumRight = RightWixPdbInfo.Tables.GetEnumerator();

            bool nextLeft = enumLeft.MoveNext();
            bool nextRight = enumRight.MoveNext();

            while (nextLeft || nextRight)
            {
                if (nextLeft && nextRight)
                {
                    // we have both items, compare them
                    Table left = (Table)enumLeft.Current;
                    Table right = (Table)enumRight.Current;
                    // this compares the table name then the columns
                    int compare = left.Definition.CompareTo(right.Definition);
                    if (compare < 0)
                    {
                        // left is smaller, only add it
                        AddChildTable(left, null);
                        nextLeft = enumLeft.MoveNext();
                    }
                    else if (compare == 0)
                    {
                        // they have the same name, use them both
                        AddChildTable(left, right);
                        nextLeft = enumLeft.MoveNext();
                        nextRight = enumRight.MoveNext();
                    }
                    else
                    {
                        // right is smaller, only add it
                        AddChildTable(null, right);
                        nextRight = enumRight.MoveNext();
                    }
                }
                else if (nextLeft)
                {
                    // we only have left items remaining, use them
                    Table left = (Table)enumLeft.Current;
                    AddChildTable(left, null);
                    nextLeft = enumLeft.MoveNext();
                }
                else
                {
                    // we only have right items remaining, use them
                    Table right = (Table)enumRight.Current;
                    AddChildTable(null, right);
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
