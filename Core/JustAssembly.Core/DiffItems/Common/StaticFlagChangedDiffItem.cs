using System;
using System.Linq;

namespace JustAssembly.Core.DiffItems.Common
{
    public class StaticFlagChangedDiffItem : BaseDiffItem
    {
        private readonly bool isNewMemberStatic;

        public StaticFlagChangedDiffItem(bool isNewMemberStatic)
            :base(DiffType.Modified)
        {
            this.isNewMemberStatic = isNewMemberStatic;
        }

        public override string GetXmlInfoString() =>
            $"Member changed to {(isNewMemberStatic ? "static" : "instance")}.";

        public override string HumanReadable => GetXmlInfoString();

        public override void Visit(Action<IDiffItem, int> visit, int depth = 0) => visit(this, depth);

        public override bool IsBreakingChange => true;
    }
}
