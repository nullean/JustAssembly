using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace JustAssembly.Core.DiffItems.Methods
{
    public class ParameterDiffItem : BaseDiffItem
    {
        private readonly ParameterDefinition oldParameter;
        private readonly ParameterDefinition newParameter;

        public ParameterDiffItem(ParameterDefinition oldParameter, ParameterDefinition newParameter)
            : base(DiffType.Modified)
        {
            this.oldParameter = oldParameter;
            this.newParameter = newParameter;
        }

        public override string GetXmlInfoString() =>
            $"Parameter name changed from {this.oldParameter.Name} to {this.newParameter.Name}.";

        public override string HumanReadable => GetXmlInfoString();

        public override void Visit(Action<IDiffItem, int> visit, int depth = 0) => visit(this, depth);

        public override bool IsBreakingChange => true;
    }
}
