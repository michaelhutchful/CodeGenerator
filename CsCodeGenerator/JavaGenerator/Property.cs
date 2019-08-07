using CodeGenerator.Enums;
using System.Text;

namespace CodeGenerator
{
    public class Property : Field
    {
        public Property()
        {
        }

        public Property(string builtInDataType, string name) : base(builtInDataType, name)
        {
        }

        public override bool HasAttributes => true;

        public override AccessModifier AccessModifier { get; set; } = AccessModifier.Public;

        public bool IsGetOnly { get; set; } = false;

        protected override string Ending => DefaultValue != null ? ";" : "";

        public string GetterBody
        {
            get
            {
                string result = string.Empty;
                result += $" {{{Util.NewLine} ";
                result += $"{Indent}return this.{Name}; {Util.NewLine}{Indent}}}";
                return result;
            }
        }

        public string SetterBody
        {
            get
            {
                string result = string.Empty;
                result += $" {{{Util.NewLine} ";
                result += $"{Indent} this.{Name} = {Name}; {Util.NewLine}{Indent}}}";
                return result;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            stringBuilder.Append($"{Indent}{AccessModifier.Public.ToString().ToLowerInvariant()} " +
                $"{BuiltInDataType} get{Name.ToTitleCase()}(){GetterBody}");

            if (!IsGetOnly)
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
                stringBuilder.Append($"{Indent}{AccessModifier.Public.ToString().ToLowerInvariant()} " +
                    $"{Enums.BuiltInDataType.Void.ToString().ToLowerInvariant()} " +
                    $"set{Name.ToTitleCase()}({BuiltInDataType} {Name}){SetterBody}");
            }

            return stringBuilder.ToString();
        }
    }
}