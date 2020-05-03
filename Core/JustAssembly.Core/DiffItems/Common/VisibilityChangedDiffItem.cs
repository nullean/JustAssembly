using System;
using System.Collections.Generic;
using System.Linq;

namespace JustAssembly.Core.DiffItems.Common
{
    public class VisibilityChangedDiffItem : BaseDiffItem
    {
        private readonly bool reduced;

        public VisibilityChangedDiffItem(bool reduced)
            :base(DiffType.Modified)
        {
            this.reduced = reduced;
        }

        public override string GetXmlInfoString() => $"Member is {(this.reduced ? "less" : "more")} visible.";

        public override string HumanReadable => GetXmlInfoString();

        public override void Visit(Action<IDiffItem, int> visit, int depth = 0) => visit(this, depth);

        public override bool IsBreakingChange => this.reduced;
    }
}
