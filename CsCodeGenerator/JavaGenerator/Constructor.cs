using System.Collections.Generic;
using System.Text;

namespace CodeGenerator
{
    public class Constructor : Method
    {
        public Constructor(string name)
        {
            base.BuiltInDataType = null;
            base.Name = name;
        }

        public override bool IsVisible { get; set; } = true;
        public new string Name => base.Name;
        public bool IsAutowired { get; set; }
        public new string BuiltInDataType => base.BuiltInDataType;
        public new string CustomDataType => base.CustomDataType;

        public override string Signature
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();

                if (IsAutowired)
                {
                    BodyLines.Clear();

                    foreach (var parameter in Parameters)
                    {
                        BodyLines.Add($"this.{parameter.Name} = {parameter.Name};");
                    }

                    stringBuilder.AppendLine($"@Autowired");
                }

                stringBuilder.Append(Indent + AccessFormated + Name.ToTitleCase() + Parameters.ToStringList(Indent + JavaCodeGenerator.IndentSingle));
                return stringBuilder.ToString();
            }
        }
    }
}