using System.IO;
using System.Text;

namespace ReactWithDotNet.VisualDesigner.Models;

static class Exporter_For_NextJs_with_Tailwind
{
    public static void Export(ApplicationState state)
    {
        const int indentLevel = 1;

        var indent = new string(' ', indentLevel * 4);

        var file = new StringBuilder();

        file.AppendLine("import Link from \"next/link\";");
        file.AppendLine();

        file.AppendLine($"export default function {state.ComponentName}() {{");

        var partRender = GenerateHtml(state.ComponentRootElement, indentLevel);

        file.AppendLine($"{indent}return (");
        file.AppendLine(partRender);
        file.AppendLine($"{indent});");

        file.AppendLine("}");

        File.WriteAllText($"C:\\github\\hopgogo\\web\\enduser-ui\\src\\components\\{state.ComponentName}.tsx", file.ToString());
    }

    public static string GenerateHtml(VisualElementModel element, int indentLevel = 0)
    {
        var indent = new string(' ', indentLevel * 4);

        var sb = new StringBuilder();

        List<string> classNames = [];

        // Open tag
        var tag = element.Tag;

        if (tag == "a")
        {
            tag = "Link";
        }

        sb.Append($"{indent}<{tag}");

        // Add properties
        foreach (var property in element.Properties)
        {
            var (success, name, value) = TryParsePropertyValue(property);
            if (success)
            {
                if (name == "class")
                {
                    classNames.AddRange(value.Split(" ", StringSplitOptions.RemoveEmptyEntries));
                    continue;
                }

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
                    if (name == "W" || name == "w" || name == "width")
                    {
                        if (value == "fit-content")
                        {
                            classNames.Add("w-fit");
                            continue;
                        }

                        classNames.Add($"w-[{value}px]");
                        continue;
                    }

                    if (name == "h" || name == "height")
                    {
                        if (value == "fit-content")
                        {
                            classNames.Add("h-fit");
                            continue;
                        }

                        classNames.Add($"h-[{value}px]");
                        continue;
                    }

                    if (name == "pt")
                    {
                        classNames.Add($"pt-[{value}px]");
                        continue;
                    }

                    if (name == "pb")
                    {
                        classNames.Add($"pb-[{value}px]");
                        continue;
                    }

                    if (name == "pl")
                    {
                        classNames.Add($"pl-[{value}px]");
                        continue;
                    }

                    if (name == "pr")
                    {
                        classNames.Add($"pr-[{value}px]");
                        continue;
                    }

                    if (name == "px")
                    {
                        classNames.Add($"px-[{value}px]");
                        continue;
                    }

                    if (name == "py")
                    {
                        classNames.Add($"py-[{value}px]");
                        continue;
                    }

                    if (name == "display")
                    {
                        classNames.Add($"{value}");
                        continue;
                    }

                    if (name == "color")
                    {
                        if (Project.Colors.TryGetValue(value, out var htmlColor))
                        {
                            value = htmlColor;
                        }

                        classNames.Add($"text-[{value}]");
                        continue;
                    }

                    if (name == "gap")
                    {
                        classNames.Add($"gap-[{value}px]");
                        continue;
                    }

                    if (name == "flex-direction")
                    {
                        if (value == "column")
                        {
                            classNames.Add("flex-col");
                            continue;
                        }
                    }

                    if (name == "align-items")
                    {
                        classNames.Add($"items-{value.RemoveFromStart("align-")}");
                        continue;
                    }

                    if (name == "justify-content")
                    {
                        if (value == "space-between")
                        {
                            classNames.Add("justify-between");
                            continue;
                        }
                    }

                    if (name == "border-radius")
                    {
                        classNames.Add($"rounded-[{value}px]");
                        continue;
                    }

                    if (name == "font-size")
                    {
                        classNames.Add($"[font-size:{value}px]");
                        continue;
                    }

                    if (name == "border")
                    {
                        var parts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 3)
                        {
                            if (Project.Colors.TryGetValue(parts[2], out var htmlColor))
                            {
                                parts[2] = htmlColor;
                            }

                            if (parts[0] == "1px" && parts[1] == "solid")
                            {
                                classNames.Add("border");
                                classNames.Add($"border-[{parts[2]}]");
                                continue;
                            }

                            classNames.Add($"border-[{parts[0]}]");
                            classNames.Add($"border-[{parts[1]}]");
                            classNames.Add($"border-[{parts[2]}]");

                            continue;
                        }
                    }
                }

                classNames.Add(styleItem);
            }
        }

        if (classNames.Any())
        {
            sb.Append($" className=\"{string.Join(" ", classNames)}\"");
        }

        var hasSelfClose = element.Children.Count == 0 && element.Text.HasNoValue();
        if (hasSelfClose)
        {
            sb.AppendLine(" />");
            return sb.ToString();
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
        sb.AppendLine($"{indent}</{tag}>");

        return sb.ToString();
    }
}