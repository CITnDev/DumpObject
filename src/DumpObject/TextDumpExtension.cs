using System;
using System.Linq;

namespace DumpObject
{
    public static class TextDumpExtension
    {
        public static string ToText(this DumpLevel dumpLevel, string nullRepresentation = "<null>", string indentString = "\t")
        {
            var dumpText = string.Empty;

            string indent = string.Empty;
            for (var i = 0; i < dumpLevel.Level + 1; i++)
                indent += indentString;

            if (dumpLevel is StopDumpLevel)
            {
                dumpText += "...";
            }
            else if (dumpLevel.Any())
            {
                dumpText += dumpLevel.Type.ToString();
                foreach (var child in dumpLevel)
                {
                    dumpText += Environment.NewLine + indent + " - " + child.Header + " : ";
                    dumpText += child.ToText(nullRepresentation, indentString);
                }
            }
            else
            {
                if (dumpLevel.Value == null)
                    dumpText += nullRepresentation;
                else
                    dumpText += dumpLevel.Value.ToString();
            }

            return dumpText;
        }
    }
}
