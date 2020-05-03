﻿using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace JustAssembly.Core.DiffItems.Properties
{
    public class GetAccessorDiffItem : BaseDiffItem<MethodDefinition>
    {
        public GetAccessorDiffItem(PropertyDefinition oldProperty, PropertyDefinition newProperty, IEnumerable<IDiffItem> declarationDiffs)
            : base(oldProperty.GetMethod, newProperty.GetMethod, declarationDiffs, null)
        {
        }

        public override MetadataType MetadataType => Core.MetadataType.Method;

        protected override string GetElementShortName(MethodDefinition element)
        {
            return "get";
        }
    }
}
