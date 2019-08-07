using CodeGenerator.Enums;
using System;
using System.Collections.Generic;

namespace CodeGenerator
{
    public class Parameter
    {
        public Parameter()
        {
        }

        public Parameter(string value)
        {
            this.Value = value;
        }

        public Parameter(string builtInDataType, string name)
        {
            this.BuiltInDataType = builtInDataType;
            CustomDataType = builtInDataType;
            this.Name = name;
        }

        public Parameter(Field field)
        {
            CustomDataType = field.CustomDataType;
            Name = field.Name;
        }

        public KeyWord? KeyWord { get; set; }

        public string BuiltInDataType { get; set; }

        public bool IsAnnotated { get; set; }

        public string Annotation { get; set; }

        public string CustomDataType { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string KeyWordFormated => KeyWord?.ToTextLower();

        public string DataTypeFormated => CustomDataType == null ? BuiltInDataType : CustomDataType;

        public string NameValueFormated => (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Value)) ? Name + Value : Name + " = " + Value;

        public override string ToString()
        {
            var result = string.Empty;

            if (IsAnnotated)
            {
                result += Annotation + " ";
            }

            result += KeyWordFormated + DataTypeFormated + " " + NameValueFormated;
            return result;
        }
    }

    public static class ParameterExtensions
    {
        public static string ToStringList(this List<Parameter> parameters, string indent)
        {
            string parametersString = string.Join($", {Util.NewLine}{indent}", parameters);
            string result = $"({parametersString})";
            return result;
        }
    }
}