using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.JavaGenerator
{
    public class JavaSwitch : Method
    {
        public Parameter Parameter
        {
            get
            {
                if (!Parameters.Any())
                {
                    Parameters.Add(new Parameter());
                }
                return base.Parameters[0];
            }
            set
            {
                if (!Parameters.Any())
                {
                    Parameters.Add(new Parameter());
                }
                base.Parameters[0] = value;
            }
        }

        public List<Case> Cases { get; set; } = new List<Case>();

        public string SwitchOn { get; set; }

        public List<string> GetLines()
        {
            var bodyLines = new List<string>();
            bodyLines.Add($"switch ({Parameter.Name}){{");
            bodyLines.AddRange(CreateCases());
            bodyLines.Add($"{JavaCodeGenerator.IndentSingle}default:");
            bodyLines.Add($"{JavaCodeGenerator.IndentSingle}{JavaCodeGenerator.IndentSingle}break;");
            bodyLines.Add("}");
            return bodyLines;
        }

        private List<string> CreateCases()
        {
            var bodyLines = new List<string>();
            string curentIndent = Indent + JavaCodeGenerator.IndentSingle;
            if (Cases.Any())
            {
                foreach (var currentCase in Cases)
                {
                    bodyLines.Add($"{JavaCodeGenerator.IndentSingle}case \"{currentCase.Condition}\":");
                    bodyLines.Add($"{curentIndent}{currentCase.Body}");
                    bodyLines.Add($"{curentIndent}break;");
                }
            }
            return bodyLines;
        }

        public class Case
        {
            public string Body { get; set; }
            public string Condition { get; set; }
        }
    }
}