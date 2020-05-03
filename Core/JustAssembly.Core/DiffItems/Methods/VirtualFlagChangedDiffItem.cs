using System;

namespace JustAssembly.Core.DiffItems.Methods
{
    public class VirtualFlagChangedDiffItem : BaseDiffItem
    {
        private readonly bool isNewMethodVirtual;

        public VirtualFlagChangedDiffItem(bool isNewMethodVirtual)
            :base(DiffType.Modified)
        {
            this.isNewMethodVirtual = isNewMethodVirtual;
        }

        public override string GetXmlInfoString() =>
            $"Method changed to {(this.isNewMethodVirtual ? string.Empty : "non-")}virtual.";

        public override bool IsBreakingChange => !this.isNewMethodVirtual;
        
        public override string HumanReadable => GetXmlInfoString();

        public override void Visit(Action<IDiffItem, int> visit, int depth = 0) => visit(this, depth);

    }
}
