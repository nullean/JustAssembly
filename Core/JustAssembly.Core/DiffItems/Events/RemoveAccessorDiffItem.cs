using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace JustAssembly.Core.DiffItems.Events
{
    public class RemoveAccessorDiffItem : BaseMemberDiffItem<MethodDefinition>
    {
        public RemoveAccessorDiffItem(EventDefinition oldEvent, EventDefinition newEvent, IEnumerable<IDiffItem> declarationDiffs)
            : base(oldEvent.RemoveMethod, newEvent.RemoveMethod, declarationDiffs, null)
        {
        }

        public override MetadataType MetadataType => Core.MetadataType.Method;

        protected override string GetElementShortName(MethodDefinition element)
        {
            return "remove";
        }
    }
}
