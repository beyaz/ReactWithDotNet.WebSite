using System.IO;
using System.Text;

namespace ReactWithDotNet.VisualDesigner.Models;

static class Exporter_For_NextJs_with_Tailwind
{
    public static void Export(ApplicationState state)
    {
        var code = GenerateHtml(state.ComponentRootElement);
        
        File.WriteAllText("C:\\github\\hopgogo\\web\\enduser-ui\\src\\components\\a.html", code);
    }
    
    public static string GenerateHtml(VisualElementModel element, int indentLevel = 0)
    {
        var indent = new string(' ', indentLevel * 2);
        var sb = new StringBuilder();
        
        // Open tag
        sb.Append($"{indent}<{element.Tag}");
        
        // Add properties
        if (element.Properties.Any())
        {
            sb.Append(" " + string.Join(" ", element.Properties));
        }
        
        // Add style
        var styles = element.StyleGroups
            .SelectMany(g => g.Items)
            .ToList();
        
        if (styles.Any())
        {
            sb.Append($" style=\"{string.Join(" ", styles)}\"");
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