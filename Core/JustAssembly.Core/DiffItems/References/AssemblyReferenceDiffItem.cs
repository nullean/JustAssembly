using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace JustAssembly.Core.DiffItems.References
{
    public class AssemblyReferenceDiffItem : BaseDiffItem<AssemblyNameReference>
    {
        public AssemblyReferenceDiffItem(AssemblyNameReference oldReference, AssemblyNameReference newReference, IEnumerable<IDiffItem> declarationDiffs)
            : base(oldReference, newReference, declarationDiffs, null)
        {
        }

        public override MetadataType MetadataType => Core.MetadataType.AssemblyReference;

        protected override string GetElementShortName(AssemblyNameReference element)
        {
            return element.FullName;
        }
    }
}
