using CodeGenerator.Enums;
using System;
using System.Collections.Generic;

namespace CodeGenerator
{
    public class Method : BaseElement
    {
        public Method()
        {
        }

        public Method(string dataType, string name) : base(dataType, name)
        {
        }

        public Method(AccessModifier accessModifier, KeyWord singleKeyWord, string builtInDataType, string name) : base(builtInDataType, name)
        {
            this.AccessModifier = accessModifier;
            this.KeyWords.Add(singleKeyWord);
        }

        public virtual bool IsVisible { get; set; } = true;

        public List<Parameter> Parameters { get; set; } = new List<Parameter>();


        public string BaseParameters { get; set; }
        public string BaseParametersFormated => BaseParameters != null ? $" : base({BaseParameters})" : "";

        public string Exceptions { get; set; } = null;

        public virtual bool BracesInNewLine { get; set; } = true;

        public List<string> BodyLines { get; set; } = new List<string>();

        public override string Signature => base.Signature + Parameters.ToStringList(Indent + JavaCodeGenerator.IndentSingle);

        public List<string> Annotations { get; set; }

        public override string ToString()
        {
            if (!IsVisible)
                return "";
            string result = JavaCodeGenerator.IndentSingle + base.ToString() + BaseParametersFormated;
            string curentIndent = Util.NewLine + Indent + JavaCodeGenerator.IndentSingle;
            string bracesSuffix = Util.NewLine + Indent;
            result += null != Exceptions ? $"throws {Exceptions}" : string.Empty;
            result += " {";
            result += BodyLines.Count == 0 ? "" : (BracesInNewLine ? curentIndent : curentIndent) + string.Join(curentIndent, BodyLines);
            result += bracesSuffix + "}";
            return result;
        }
    }
}