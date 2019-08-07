using CodeGenerator.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    public class Field : BaseElement
    {
        public Field()
        {
        }

        public Field(string builtInDataType, string name) : base(builtInDataType, name)
        {
        }

        public bool IsAutowired { get; set; }

        public List<string> Annotations { get; set; } = new List<string>();

        public override AccessModifier AccessModifier { get; set; } = AccessModifier.Private;

        public virtual string DefaultValue { get; set; }
        protected string DefaultValueFormated => DefaultValue != null ? " = " + DefaultValue : "";

        public override bool HasAttributes => false;

        protected virtual string Ending { get; } = ";";

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (IsAutowired)
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine(Indent + "@Autowired");
            }

            if (Annotations.Any())
            {
                foreach (var annotation in Annotations)
                {
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine(annotation);
                }
            }

            stringBuilder.AppendLine(Indent + base.ToString() + DefaultValueFormated + Ending);
            return stringBuilder.ToString();
        }
    }
}