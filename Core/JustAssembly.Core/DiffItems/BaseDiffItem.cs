using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using JustAssembly.Core.Extensions;
using Mono.Cecil;

namespace JustAssembly.Core.DiffItems
{
    public abstract class BaseDiffItem : IDiffItem
    {
        private readonly DiffType diffType;

        public DiffType DiffType => this.diffType;

        public BaseDiffItem(DiffType diffType) => this.diffType = diffType;

        public string ToXml()
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented })
                {
                    this.ToXml(xmlWriter);
                }

                return stringWriter.ToString();
            }
        }

        public abstract string GetXmlInfoString();

        public abstract string HumanReadable { get; }

        internal virtual void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("DiffItem");
            writer.WriteAttributeString("DiffType", this.DiffType.ToString());
            writer.WriteString(GetXmlInfoString());
            writer.WriteEndElement();
        }

        public abstract bool IsBreakingChange { get; }
        
        public abstract void Visit(Action<IDiffItem, int> visit, int depth = 0);
    }

    public abstract class BaseDiffItem<T> : BaseDiffItem, IMetadataDiffItem<T> where T : class, IMetadataTokenProvider
    {
        private readonly T oldElement;
        private readonly T newElement;
        private readonly IEnumerable<IDiffItem> declarationDiffs;
        private readonly IEnumerable<IMetadataDiffItem> childrenDiffs;

        public T OldElement => this.oldElement;

        public T NewElement => this.newElement;

        public abstract MetadataType MetadataType { get; }

        public uint OldTokenID => this.oldElement.MetadataToken.ToUInt32();

        public uint NewTokenID => this.newElement.MetadataToken.ToUInt32();

        public IEnumerable<IDiffItem> DeclarationDiffs => this.declarationDiffs;

        public IEnumerable<IMetadataDiffItem> ChildrenDiffs => this.childrenDiffs;

        public BaseDiffItem(T oldElement, T newElement, IEnumerable<IDiffItem> declarationDiffs, IEnumerable<IMetadataDiffItem> childrenDiffs)
            : base(newElement == null ? DiffType.Deleted : (oldElement == null ? DiffType.New : DiffType.Modified))
        {
            this.oldElement = oldElement;
            this.newElement = newElement;
            this.declarationDiffs = declarationDiffs != null ? declarationDiffs.ToList() : Enumerable.Empty<IDiffItem>();
            this.childrenDiffs = childrenDiffs != null ? childrenDiffs.ToList() : Enumerable.Empty<IMetadataDiffItem>();
        }

        protected T GetElement()
        {
            return this.newElement ?? this.oldElement;
        }

        protected abstract string GetElementShortName(T element);

        public override string HumanReadable => GetElementShortName(GetElement());

        public override void Visit(Action<IDiffItem, int> visit, int depth = 0)
        {
            visit(this, depth);
            if (!this.DeclarationDiffs.IsEmpty())
            {
                foreach (var diffItem in this.DeclarationDiffs)
                    visit(diffItem, ++depth);
            }

            foreach (var item in this.ChildrenDiffs)
                visit(item, ++depth);
        }

        internal override void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement(MetadataType.ToString());
            writer.WriteAttributeString("Name", GetElementShortName(this.GetElement()));
            writer.WriteAttributeString("DiffType", DiffType.ToString());

            if (!this.DeclarationDiffs.IsEmpty())
            {
                writer.WriteStartElement("DeclarationDiffs");
                foreach (BaseDiffItem item in this.DeclarationDiffs)
                {
                    item.ToXml(writer);
                }
                writer.WriteEndElement();
            }

            foreach (BaseDiffItem item in this.ChildrenDiffs)
            {
                item.ToXml(writer);
            }

            writer.WriteEndElement();
        }

        public override string GetXmlInfoString()
        {
            throw new NotSupportedException();
        }

        private bool? isBreakingChange;
        public override bool IsBreakingChange
        {
            get
            {
                if (isBreakingChange == null)
                {
                    if(this.DiffType != Core.DiffType.Modified)
                    {
                        this.isBreakingChange = this.DiffType == Core.DiffType.Deleted;
                    }
                    else
                    {
                        this.isBreakingChange = EnumerableExtensions.ConcatAll<IDiffItem>(this.declarationDiffs, this.childrenDiffs).Any(item => item.IsBreakingChange);
                    }
                }
                return isBreakingChange.Value;
            }
        }
    }
}
