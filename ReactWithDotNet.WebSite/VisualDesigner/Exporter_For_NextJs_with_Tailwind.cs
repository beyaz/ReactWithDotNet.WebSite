using System.IO;
using System.Text;

namespace ReactWithDotNet.VisualDesigner.Models;

static class Exporter_For_NextJs_with_Tailwind
{
    public static void Export(ApplicationState state)
    {
        var code = GenerateHtml(state.ComponentRootElement);

        File.WriteAllText($"C:\\github\\hopgogo\\web\\enduser-ui\\src\\components\\{state.ComponentName}.tsx", code);
    }

    public static string GenerateHtml(VisualElementModel element, int indentLevel = 0)
    {
        var indent = new string(' ', indentLevel * 2);

        var sb = new StringBuilder();

        List<string> classNames = [];

        // Open tag
        sb.Append($"{indent}<{element.Tag}");

        // Add properties
        foreach (var property in element.Properties)
        {
            var (success, name, value) = TryParsePropertyValue(property);
            if (success)
            {
                sb.Append($" {name}=\"{value}\"");
            }
        }

        foreach (var styleGroup in element.StyleGroups)
        {
            foreach (var styleItem in styleGroup.Items)
            {
                var (success, name, value) = TryParsePropertyValue(styleItem);
                if (success)
                {
                    if (name == "pt")
                    {
                        classNames.Add($"pt-[{value}px]");
                    }
                }
            }
        }

        if (classNames.Any())
        {
            sb.Append($" className=\"{string.Join(" ", classNames)}\"");
        }

        sb.AppendLine(">");

        // Add text content
        if (!string.IsNullOrWhiteSpace(element.Text))
        {
            sb.AppendLine($"{indent}  {element.Text}");
        }

        // Add children
        foreach (var child in element.Children)
        {
            sb.Append(GenerateHtml(child, indentLevel + 1));
        }

        // Close tag
        sb.AppendLine($"{indent}</{element.Tag}>");

        return sb.ToString();
    }
}