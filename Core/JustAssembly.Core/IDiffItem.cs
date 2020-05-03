using System;
using JustAssembly.Core.DiffItems;
using Mono.Cecil;

namespace JustAssembly.Core
{
    public interface IDiffItem
    {
        DiffType DiffType { get; }

        string ToXml();
        
        string HumanReadable { get; }

        bool IsBreakingChange { get; }

        /// <summary> Visit this and nested differences </summary>
        /// <param name="visit">An action taking the diff item and the current depth</param>
        /// <param name="depth">Initial depth to visit at</param>
        void Visit(Action<IDiffItem, int> visit, int depth = 0);
    }
}
