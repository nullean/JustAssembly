using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace JustAssembly.Core
{
    public interface IMetadataDiffItem : IDiffItem
    {
        ICollection<IDiffItem> DeclarationDiffs { get; }
        ICollection<IMetadataDiffItem> ChildrenDiffs { get; }
        int Count { get; }

        MetadataType MetadataType { get; }

        uint OldTokenID { get; }
        uint NewTokenID { get; }
    }

    public interface IMetadataDiffItem<T> : IMetadataDiffItem where T : class, IMetadataTokenProvider
    {
        T OldElement { get; }
        T NewElement { get; }
    }
}
