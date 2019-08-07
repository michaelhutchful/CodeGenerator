﻿using CodeGenerator.Enums;
using System;
using System.Collections.Generic;

namespace CodeGenerator
{
    public class EnumModel : BaseElement
    {
        public EnumModel(string name = null)
        {
            base.CustomDataType = Util.Enum;
            base.Name = name;
        }

        public override int IndentSize { get; set; } = (int)IndentType.Single * JavaCodeGenerator.DefaultTabSize;

        public new BuiltInDataType BuiltInDataType { get; }

        public new string CustomDataType => base.CustomDataType;

        public new string Name => base.Name;

        public List<EnumValue> EnumValues { get; set; } = new List<EnumValue>();

        public override string ToString()
        {
            string result = base.ToString();
            result += Util.NewLine + Indent + "{";

            result += EnumValues.Count > 0 ? Util.NewLine : "";
            result += string.Join("," + Util.NewLine, EnumValues);
            result += Util.NewLine + Indent + "}";
            return result;
        }
    }

    public class EnumValue
    {
        public EnumValue(string name = null, int? value = null)
        {
            this.Name = name;
            this.Value = value;
        }

        public virtual int IndentSize { get; set; } = (int)IndentType.Double * JavaCodeGenerator.DefaultTabSize;
        public string Indent => new string(' ', IndentSize);

        public string Name { get; set; }

        public int? Value { get; set; }
        public string ValuFormated => Value != null ? " = " + Value : "";

        public override string ToString()
        {
            string result = Indent + Name + ValuFormated;
            return result;
        }
    }
}