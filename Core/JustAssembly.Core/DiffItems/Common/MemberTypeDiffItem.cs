using System;
using Mono.Cecil;
using JustAssembly.Core.Extensions;

namespace JustAssembly.Core.DiffItems.Common
{
    public class MemberTypeDiffItem : BaseDiffItem
    {
        private readonly IMemberDefinition oldMember;
        private readonly IMemberDefinition newMember;

        public MemberTypeDiffItem(IMemberDefinition oldMember, IMemberDefinition newMember)
            :base(DiffType.Modified)
        {
            this.oldMember = oldMember;
            this.newMember = newMember;
        }

        public override string GetXmlInfoString()
        {
            string name;

            string oldType;
            this.oldMember.GetMemberTypeAndName(out oldType, out name);

            string newType;
            this.newMember.GetMemberTypeAndName(out newType, out name);

            return string.Format("Member type changed from {0} to {1}.", oldType, newType);
        }
        
        public override string HumanReadable => GetXmlInfoString();

        public override void Visit(Action<IDiffItem, int> visit, int depth = 0) => visit(this, depth);

        public override bool IsBreakingChange => true;
    }
}
