using CodeGenerator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGenerator
{
    public abstract class BaseElement
    {
        public BaseElement()
        {
        }

        public BaseElement(string builtInDataType, string name)
        {
            this.BuiltInDataType = builtInDataType;
            CustomDataType = builtInDataType;
            this.Name = name;
        }

        public virtual int IndentSize { get; set; } = (int)JavaCodeGenerator.DefaultTabSize;
        public string Indent => new string(' ', IndentSize);

        public virtual string Comment { get; set; }

        public virtual bool HasAttributes => true;
        public virtual List<AttributeModel> Attributes { get; set; } = new List<AttributeModel>();

        public virtual AccessModifier AccessModifier { get; set; } = AccessModifier.Public;

        protected string AccessFormated =>
            AccessModifier == AccessModifier.Package_Private ? string.Empty : AccessModifier.ToTextLower() + " ";

        public List<KeyWord> KeyWords { get; set; } = new List<KeyWord>();
        public KeyWord SingleKeyWord { set { KeyWords.Add(value); } }
        protected string KeyWordsFormated => KeyWords?.Count > 0 ? string.Join(" ", KeyWords.Select(a => a.ToTextLower())) + " " : "";

        public virtual string BuiltInDataType { get; set; }
        public virtual string CustomDataType { get; set; }
        protected string DataTypeFormated => CustomDataType ?? BuiltInDataType;

        public virtual string Name { get; set; }

        public virtual string Signature => $"{AccessFormated}{KeyWordsFormated}{DataTypeFormated} {Name}";
        public virtual string CamelCaseSignature => $"{AccessFormated}{KeyWordsFormated}{DataTypeFormated} {Name.ToCamelCase()}";

        public void AddAttribute(AttributeModel attributeModel) => Attributes?.Add(attributeModel);

        public override string ToString()
        {
            string result = Comment != null ? (Util.NewLine + Indent + "// " + Comment) : "";
            result += HasAttributes ? Attributes.ToStringList(Indent) : "";
            result += Signature;
            return result;
        }

        public string ToStringCamelCase()
        {
            string result = Comment != null ? (Util.NewLine + Indent + "// " + Comment) : "";
            result += HasAttributes ? Attributes.ToStringList(Indent) : "";
            result += CamelCaseSignature;
            return result;
        }
    }
}