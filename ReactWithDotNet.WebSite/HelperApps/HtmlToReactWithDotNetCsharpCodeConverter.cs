using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;
using HtmlAgilityPack;
using static ReactWithDotNet.WebSite.HelperApps.ToModifierTransformer;

namespace ReactWithDotNet.WebSite.HelperApps;

static class HtmlToReactWithDotNetCsharpCodeConverter
{
    static readonly IReadOnlyDictionary<string, string> AttributeRealNameMap = new Dictionary<string, string>
    {
        { "class", "className" },
        { "for", "htmlFor" },
        { "rowspan", "rowSpan" },
        { "colspan", "colSpan" },
        { "cellspacing", "cellSpacing" },
        { "cellpadding", "cellPadding" },
        { "tabindex", "tabIndex" },
        { "preserveaspectratio", "preserveAspectRatio" }
    };

    static readonly List<string> ignoredTags = ["rect", "path", "circle", "line"];

    public static string HtmlToCSharp(string htmlRootNode)
    {
        if (string.IsNullOrWhiteSpace(htmlRootNode))
        {
            return null;
        }

        htmlRootNode = AgilityPackageOverride.Encode(htmlRootNode);

        var document = new HtmlDocument();

        document.LoadHtml(htmlRootNode.Trim());

        return ToCSharpCode(ToCSharpCode(document.DocumentNode.FirstChild));
    }

    static string ConvertToCSharpString(string value)
    {
        if (value == null)
        {
            return null;
        }

        if (value.Contains('\u2028'))
        {
            value = value.Replace('\u2028', '\n');
        }

        if (value.Contains('\n'))
        {
            value = value.Replace("\"", "\"\"");
        }
        else
        {
            value = value.Replace("\"", "\\\"");
        }

        if (value.Contains("&nbsp;"))
        {
            return string.Join(", nbsp, ", value.Split(["&nbsp;"], StringSplitOptions.None).Select(ConvertToCSharpString));
        }

        value = '"' + value + '"';

        if (value.Contains('\n'))
        {
            value = '@' + value;
        }

        return value;
    }

    static bool EndsWithPixel(this string value)
    {
        return value?.EndsWith("px", StringComparison.OrdinalIgnoreCase) == true;
    }

    static string GetName(this HtmlAttribute htmlAttribute)
    {
        var name = htmlAttribute.Name;
        if (name.StartsWith(":"))
        {
            name = name[1..];
        }

        if (htmlAttribute.OriginalName != name)
        {
            if (name.All(char.IsLower) && htmlAttribute.OriginalName.Any(char.IsUpper))
            {
                name = htmlAttribute.OriginalName;
            }
        }

        if (AttributeRealNameMap.ContainsKey(name))
        {
            return AttributeRealNameMap[name];
        }

        if (name.Contains(":"))
        {
            var parts = name.Split(":");
            if (parts.Length == 2)
            {
                return parts[0] + char.ToUpper(parts[1][0]) + parts[1][1..];
            }
        }

        return name;
    }

    static string getStringParameter(string prm)
    {
        return TryGetGlobalDeclaredStringConstValue(prm) ?? '"' + prm + '"';
    }

    static string GetTagName(this HtmlAttribute htmlAttribute)
    {
        return htmlAttribute.OwnerNode.Name;
    }

    static bool HasValue(this string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    static IReadOnlyList<HtmlAttribute> RemoveAll(this HtmlAttributeCollection htmlAttributeCollection, Func<HtmlAttribute, bool> match)
    {
        var items = htmlAttributeCollection.Where(match).ToList();

        foreach (var htmlAttribute in items)
        {
            htmlAttributeCollection.Remove(htmlAttribute);
        }

        return items;
    }

    static void RemoveAll(this HtmlNodeCollection htmlNodeCollection, Func<HtmlNode, bool> match)
    {
        var nodes = htmlNodeCollection.Where(match).ToList();

        foreach (var node in nodes)
        {
            htmlNodeCollection.Remove(node);
        }
    }

    /// <summary>
    ///     Removes from end.
    /// </summary>
    static string RemoveFromEnd(this string data, string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        if (data.EndsWith(value, comparison))
        {
            return data.Substring(0, data.Length - value.Length);
        }

        return data;
    }

    /// <summary>
    ///     Removes value from start of str
    /// </summary>
    static string RemoveFromStart(this string data, string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        if (data == null)
        {
            return null;
        }

        if (data.StartsWith(value, comparison))
        {
            return data.Substring(value.Length, data.Length - value.Length);
        }

        return data;
    }

    static string RemovePixelFromEnd(this string value)
    {
        return value?.RemoveFromEnd("px");
    }

    static string ToCSharpCode(IEnumerable<string> lines)
    {
        var sb = new StringBuilder();

        var padding = 0;

        foreach (var line in lines)
        {
            var paddingAsString = "".PadRight(padding * 4, ' ');
            if (line == "{")
            {
                sb.AppendLine(paddingAsString + line);
                padding++;
                continue;
            }

            if (line == "}" || line == "},")
            {
                if (padding == 0)
                {
                    throw new InvalidOperationException("Padding is already zero.");
                }

                padding--;
                paddingAsString = "".PadRight(padding * 4, ' ');
            }

            sb.AppendLine(paddingAsString + line);
        }

        return sb.ToString().Trim();
    }

    static List<string> ToCSharpCode(HtmlNode htmlNode)
    {
        if (htmlNode.HasAttributes)
        {
            htmlNode.Attributes.Remove("onclick");
        }

        return ToCSharpCode(new Data
        {
            htmlNode = htmlNode
        });
    }

    static List<string> ToCSharpCode(Data data)
    {
        data = initHtmlNodeName(data);

        if (data.htmlNodeName == "#text")
        {
            if (string.IsNullOrWhiteSpace(data.htmlNode.InnerText))
            {
                return [];
            }

            if (data.htmlNode.InnerText == "&nbsp;")
            {
                return ["nbsp"];
            }

            return [ConvertToCSharpString(data.htmlNode.InnerText)];
        }

        if (data.htmlNodeName == "br")
        {
            return ["br"];
        }

        data = data with { modifiers = [] };

        data = grabStyleAttribute(data);
        data = arrangeSvgSizeAttribute(data);
        data = moveStylableAttributesToStyleForSvgAndPath(data);
        data = tryArrangeInnerNodeText(data);
        data = arrangeFlex(data);
        data = arrangeShortwayStyle(data);
        data = removeComments(data);
        data = convertAllAttributesToModifiers(data);
        data = moveStyleToModifiers(data);
        data = moveClassNameModifierToFirst(data);

        if (data.htmlNode.ChildNodes.Count == 0)
        {
            return leafElementToString(data);
        }

        if (data.htmlNode.ChildNodes.Count == 1 && data.htmlNode.ChildNodes[0].Name == "#text")
        {
            if (data.htmlNode.Attributes.Count == 0 && data.style is null)
            {
                return [$"({data.htmlNodeName})" + ConvertToCSharpString(data.htmlNode.ChildNodes[0].InnerText)];
            }

            return
            [
                $"new {data.htmlNodeName}({JoinModifiers(data.modifiers)})",
                "{",
                ConvertToCSharpString(data.htmlNode.ChildNodes[0].InnerText),
                "}"
            ];
        }

        return exportMultiLine(data);

        static Data moveClassNameModifierToFirst(Data data)
        {
            var classNameModifierCode = data.modifiers.FirstOrDefault(x => x.Success && x.PartName == "ClassName");
            if (classNameModifierCode is not null)
            {
                data.modifiers.Remove(classNameModifierCode);

                data.modifiers.Insert(0, classNameModifierCode.PartParameterWithoutParanthesis);
            }

            return data;
        }

        static List<string> leafElementToString(Data data)
        {
            Debug.Assert(data.htmlNode.ChildNodes.Count == 0);

            var sb = new StringBuilder();
            sb.Append($"new {data.htmlNodeName}");

            var textModifierCode = data.modifiers.FirstOrDefault(x => x.Success && x.PartName == "Text");
            if (textModifierCode is not null)
            {
                data.modifiers.Remove(textModifierCode);
            }

            var isConstructorWritten = false;

            if (data.modifiers.Count > 0)
            {
                isConstructorWritten = true;

                sb.Append("(");
                sb.Append(JoinModifiers(data.modifiers));
                sb.Append(")");
            }

            var lines = new List<string> { sb.ToString() };

            List<string> objectInitializations = [];

            if (textModifierCode is not null)
            {
                objectInitializations.Add(textModifierCode.PartParameterWithoutParanthesis.RemoveFromStart("\"").RemoveFromEnd("\""));
            }

            if (data.htmlNode.Attributes.Any())
            {
                foreach (var list in data.htmlNode.Attributes.Select(attributeToString))
                {
                    objectInitializations.AddRange(list);
                }
            }

            if (objectInitializations.Count > 0)
            {
                lines.Add("{");

                lines.AddRange(objectInitializations.Select(line => line + ","));

                lines[^1] = lines[^1].RemoveFromEnd(",");

                lines.Add("}");
            }

            if (isConstructorWritten is false && objectInitializations.Count is 0)
            {
                lines[^1] += "()";
            }

            return lines;

            List<string> attributeToString(HtmlAttribute attribute)
            {
                if (attribute.Name == "style" && data.style is not null)
                {
                    if (canStyleExportInOneLine(data.style))
                    {
                        return [string.Join(", ", data.style.ToDictionary().Select(p => TryConvertToModifier_From_Mixin_Extension(p.Key, p.Value)).Where(x => x.success).Select(x => x.modifierCode))];
                    }

                    var returnList = new List<string>
                    {
                        "style =",
                        "{"
                    };

                    returnList.AddRange(data.style.ToDictionary().Select(toLine));

                    returnList[^1] = returnList[^1].RemoveFromEnd(",");

                    returnList.Add("}");

                    return returnList;

                    static string toLine(KeyValuePair<string, string> kv)
                    {
                        return kv.Key + " = " + getStringParameter(kv.Value) + ",";
                    }
                }

                var propertyInfo = TryFindProperty(attribute.GetTagName(), attribute.GetName());
                if (propertyInfo is not null)
                {
                    if (propertyInfo.PropertyType.IsGenericType)
                    {
                        if (propertyInfo.PropertyType.GetGenericTypeDefinition().Name.StartsWith("UnionProp`"))
                        {
                            var genericArguments = propertyInfo.PropertyType.GetGenericArguments();

                            if (genericArguments.Contains(typeof(double)) ||
                                genericArguments.Contains(typeof(double)) ||
                                genericArguments.Contains(typeof(double?)))
                            {
                                if (double.TryParse(attribute.Value.Replace(".", ""), out _))
                                {
                                    return [$"{propertyInfo.Name} = {attribute.Value}"];
                                }
                            }
                        }
                    }

                    return [$"{propertyInfo.Name} = {getStringParameter(attribute.Value)}"];
                }

                if (canBeExportInOneLine(data))
                {
                    return [$"/* {attribute.GetName()} = \"{attribute.Value}\"*/"];
                }

                return [$"// {attribute.GetName()} = \"{attribute.Value}\""];
            }
        }

        static Data grabStyleAttribute(Data data)
        {
            var styleAttribute = data.htmlNode.Attributes["style"];
            if (styleAttribute != null)
            {
                if (!string.IsNullOrWhiteSpace(styleAttribute.Value))
                {
                    data = data with { style = Style.ParseCss(styleAttribute.Value) };

                    if (data.style.backgroundImage.HasValue())
                    {
                        data.style.backgroundImage = AgilityPackageOverride.DecodeValue(data.style.backgroundImage);
                    }
                }

                data.htmlNode.Attributes.Remove("style");
            }

            return data;
        }

        static List<string> exportMultiLine(Data data)
        {
            var partConstructor = "";
            if (data.modifiers.Count > 0)
            {
                partConstructor = $"({JoinModifiers(data.modifiers)})";
            }

            var lines = new List<string>
            {
                $"new {data.htmlNodeName}{partConstructor}",
                "{"
            };

            foreach (var items in data.htmlNode.ChildNodes.Select(ToCSharpCode))
            {
                if (items.Count > 0)
                {
                    items[^1] += ",";
                }

                lines.AddRange(items);
            }

            if (lines[^1].EndsWith(",", StringComparison.OrdinalIgnoreCase))
            {
                lines[^1] = lines[^1].Remove(lines[^1].Length - 1);
            }

            lines.Add("}");

            return lines;
        }

        static Data convertAllAttributesToModifiers(Data data)
        {
            if (!ignoredTags.Contains(data.htmlNode.Name))
            {
                var attributes = new List<HtmlAttribute>(data.htmlNode.Attributes);

                foreach (var htmlAttribute in attributes)
                {
                    var (success, modifierCode) = TryConvertToModifier(htmlAttribute);
                    if (success)
                    {
                        data.modifiers.Add(modifierCode);

                        data.htmlNode.Attributes.Remove(htmlAttribute);
                    }
                }
            }

            return data;
        }

        static bool canBeExportInOneLine(Data data)
        {
            if (data.htmlNode.Attributes.Contains("text"))
            {
                return false;
            }

            if (data.style is not null)
            {
                if (canStyleExportInOneLine(data.style) is false)
                {
                    return false;
                }
            }

            if (data.htmlNode.ChildNodes.Count == 0 && data.modifiers.Count > 0)
            {
                return false;
            }

            if (data.htmlNode.Attributes.Count > 0)
            {
                return false;
            }

            return true;
        }

        static Data moveStyleToModifiers(Data data)
        {
            if (data.style is not null)
            {
                data.modifiers.AddRange(data.style.ToDictionary().Select(p => TryConvertToModifier_From_Mixin_Extension(p.Key, p.Value)).Select(ModifierCode.From));

                data = data with { style = null };
            }

            return data;
        }

        static Data removeComments(Data data)
        {
            data.htmlNode.ChildNodes.RemoveAll(childNode => childNode.Name == "#comment");

            return data;
        }

        static Data arrangeShortwayStyle(Data data)
        {
            if (data.style is null)
            {
                return data;
            }

            // border
            foreach (var prefix in new[] { "borderTop", "borderRight", "borderLeft", "borderBottom" })
            {
                var xStyle = data.style[$"{prefix}Style"];
                var xWidth = data.style[$"{prefix}Width"];
                var xColor = data.style[$"{prefix}Color"];

                if (data.style[prefix] is null)
                {
                    if (string.IsNullOrWhiteSpace(xStyle) is false &&
                        string.IsNullOrWhiteSpace(xWidth) is false &&
                        string.IsNullOrWhiteSpace(xColor) is false)
                    {
                        data.style[prefix] = $"{xWidth} {xStyle} {xColor}";

                        data.style[$"{prefix}Style"] = data.style[$"{prefix}Width"] = data.style[$"{prefix}Color"] = null;
                    }

                    if (string.IsNullOrWhiteSpace(xStyle) is false &&
                        string.IsNullOrWhiteSpace(xWidth) is false &&
                        string.IsNullOrWhiteSpace(xColor))
                    {
                        data.style[prefix] = $"{xWidth} {xStyle}";

                        data.style[$"{prefix}Style"] = data.style[$"{prefix}Width"] = data.style[$"{prefix}Color"] = null;
                    }
                }
            }

            // p a d d i n g
            if (data.style.paddingTop.HasValue() &&
                data.style.paddingRight.HasValue() &&
                data.style.paddingBottom.HasValue() &&
                data.style.paddingLeft.HasValue())
            {
                data.style.padding = $"{data.style.paddingTop} {data.style.paddingRight} {data.style.paddingBottom} {data.style.paddingLeft}";

                data.style.paddingTop = data.style.paddingRight = data.style.paddingBottom = data.style.paddingLeft = null;
            }

            // m a r g i n
            if (data.style.marginTop.HasValue() &&
                data.style.marginRight.HasValue() &&
                data.style.marginBottom.HasValue() &&
                data.style.marginLeft.HasValue())
            {
                data.style.margin = $"{data.style.marginTop} {data.style.marginRight} {data.style.marginBottom} {data.style.marginLeft}";

                data.style.marginTop = data.style.marginRight = data.style.marginBottom = data.style.marginLeft = null;
            }

            // padding: TopBottom
            if (data.style.paddingTop.EndsWithPixel() &&
                data.style.paddingBottom.EndsWithPixel() &&
                data.style.paddingTop == data.style.paddingBottom)
            {
                data.style.padding = MarkAsAlreadyCalculatedModifier($"PaddingTopBottom({data.style.paddingTop.RemovePixelFromEnd()})");

                data.style.paddingTop = data.style.paddingBottom = null;
            }

            // padding: LeftRight
            if (data.style.paddingLeft.EndsWithPixel() &&
                data.style.paddingRight.EndsWithPixel() &&
                data.style.paddingLeft == data.style.paddingRight)
            {
                data.style.padding = MarkAsAlreadyCalculatedModifier($"PaddingLeftRight({data.style.paddingLeft.RemovePixelFromEnd()})");

                data.style.paddingLeft = data.style.paddingRight = null;
            }

            // margin: TopBottom
            if (data.style.marginTop.EndsWithPixel() &&
                data.style.marginBottom.EndsWithPixel() &&
                data.style.marginTop == data.style.marginBottom)
            {
                data.style.margin = MarkAsAlreadyCalculatedModifier($"MarginTopBottom({data.style.marginTop.RemovePixelFromEnd()})");

                data.style.marginTop = data.style.marginBottom = null;
            }

            // margin: LeftRight
            if (data.style.marginLeft.EndsWithPixel() &&
                data.style.marginRight.EndsWithPixel() &&
                data.style.marginLeft == data.style.marginRight)
            {
                data.style.margin = MarkAsAlreadyCalculatedModifier($"MarginLeftRight({data.style.marginLeft.RemovePixelFromEnd()})");

                data.style.marginLeft = data.style.marginRight = null;
            }

            // padding: SizeFull
            if (data.style.width == "100%" && data.style.height == "100%")
            {
                data.style.width = MarkAsAlreadyCalculatedModifier("SizeFull");

                data.style.height = null;
            }

            // margin: WidthHeight
            if (data.style.width.EndsWithPixel() &&
                data.style.height.EndsWithPixel() &&
                data.style.width == data.style.height)
            {
                data.style.width = MarkAsAlreadyCalculatedModifier($"Size({data.style.width.RemovePixelFromEnd()})");

                data.style.height = null;
            }

            // Border
            if (data.style.border.HasValue() && data.style.borderRadius.HasValue())
            {
                var valueParts = data.style.border.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (valueParts.Length == 3)
                {
                    if (valueParts[0].EndsWithPixel() && data.style.borderRadius.EndsWithPixel())
                    {
                        data.style.border = MarkAsAlreadyCalculatedModifier($"Border({valueParts[0].RemovePixelFromEnd()}, {getStringParameter(valueParts[1])}, {getStringParameter(valueParts[2])}, {data.style.borderRadius.RemovePixelFromEnd()})");

                        data.style.borderRadius = null;
                    }
                }
            }

            // Font
            if (data.style.font is null &&
                data.style.fontWeight.HasValue() &&
                data.style.fontSize.HasValue() &&
                data.style.fontFamily.HasValue())
            {
                var fontWeight = data.style.fontWeight;
                if (!(fontWeight is "100" ||
                      fontWeight is "200" ||
                      fontWeight is "300" ||
                      fontWeight is "400" ||
                      fontWeight is "500" ||
                      fontWeight is "600" ||
                      fontWeight is "700" ||
                      fontWeight is "800" ||
                      fontWeight is "900"))
                {
                    fontWeight = '"' + fontWeight + '"';
                }

                var fontSize = data.style.fontSize.RemovePixelFromEnd();

                var lineHeight = string.Empty;

                if (data.style.lineHeight.HasValue())
                {
                    lineHeight = ", " + data.style.lineHeight.RemovePixelFromEnd();

                    data.style.lineHeight = null;
                }

                var fontFamily = data.style.fontFamily.Replace('"', '\'');

                var color = string.Empty;

                if (data.style.color.HasValue())
                {
                    color = ", " + getStringParameter(data.style.color);

                    data.style.color = null;
                }

                data.style.font = MarkAsAlreadyCalculatedModifier($"Font({fontWeight}, {fontSize}{lineHeight}, \"{fontFamily}\"{color})");

                data.style.fontFamily = null;
                data.style.fontSize   = null;
                data.style.fontWeight = null;
            }

            return data;
        }

        static Data arrangeFlex(Data data)
        {
            if (data.htmlNodeName != "div")
            {
                return data;
            }

            var style = data.style;

            if (style is null)
            {
                return data;
            }

            // c o l u m n s
            if (style.display == "inline-flex" &&
                style.flexDirection == "column" &&
                style.justifyContent == "center" &&
                style.alignItems == "center")
            {
                data = data with { htmlNodeName = "InlineFlexColumnCentered" };

                style.display = style.flexDirection = style.justifyContent = style.alignItems = null;
            }

            if (style.display == "inline-flex" &&
                style.flexDirection == "column")
            {
                data = data with { htmlNodeName = "InlineFlexColumn" };

                style.display = style.flexDirection = null;
            }

            if (style.display == "flex" &&
                style.flexDirection == "column" &&
                style.justifyContent == "center" &&
                style.alignItems == "center")
            {
                data = data with { htmlNodeName = "FlexColumnCentered" };

                style.display = style.flexDirection = style.justifyContent = style.alignItems = null;
            }

            if (style.display == "flex" && style.flexDirection == "column")
            {
                data = data with { htmlNodeName = "FlexColumn" };

                style.display = style.flexDirection = null;
            }

            // r o w
            if (style.display == "inline-flex" &&
                (style.flexDirection is null || style.flexDirection == "row") &&
                style.justifyContent == "center" &&
                style.alignItems == "center")
            {
                data = data with { htmlNodeName = "InlineFlexRowCentered" };

                style.display = style.flexDirection = style.justifyContent = style.alignItems = null;
            }

            if (style.display == "inline-flex" &&
                (style.flexDirection is null || style.flexDirection == "row"))
            {
                data = data with { htmlNodeName = "InlineFlexRow" };

                style.display = style.flexDirection = null;
            }

            if (style.display == "flex" &&
                (style.flexDirection is null || style.flexDirection == "row") &&
                style.justifyContent == "center" &&
                style.alignItems == "center")
            {
                data = data with { htmlNodeName = "FlexRowCentered" };

                style.display = style.flexDirection = style.justifyContent = style.alignItems = null;
            }

            if (style.display == "flex" && (style.flexDirection is null || style.flexDirection == "row"))
            {
                data = data with { htmlNodeName = "FlexRow" };

                style.display = style.flexDirection = null;
            }

            return data;
        }

        static Data tryArrangeInnerNodeText(Data data)
        {
            if (data.htmlNode.ChildNodes.Count == 1 && data.htmlNode.ChildNodes[0].Name == "#text")
            {
                var text = data.htmlNode.ChildNodes[0].InnerText.Trim();

                if (string.IsNullOrWhiteSpace(text) is false)
                {
                    data.modifiers.Insert(0, $"Text(\"{ConvertToCSharpString(text)}\")");
                }

                data.htmlNode.ChildNodes.RemoveAt(0);
            }

            return data;
        }

        static Data moveStylableAttributesToStyleForSvgAndPath(Data data)
        {
            var htmlNode = data.htmlNode;

            if (htmlNode.Name == "svg" || htmlNode.Name == "path")
            {
                foreach (var htmlAttribute in htmlNode.Attributes.RemoveAll(x => isStyleAttribute(htmlNode, x)))
                {
                    data = data with { style = data.style ?? new() };

                    data.style[htmlAttribute.Name] = htmlAttribute.Value;
                }
            }

            return data;

            static bool isStyleAttribute(HtmlNode htmlNode, HtmlAttribute htmlAttribute)
            {
                if (htmlNode.Name == "svg" && "size".Equals(htmlAttribute.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                if (TryFindProperty(htmlNode.Name, htmlAttribute.Name) is null)
                {
                    if (typeof(Style).GetProperty(htmlAttribute.Name.Replace("-", ""), BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase) is not null)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        static Data arrangeSvgSizeAttribute(Data data)
        {
            if (data.htmlNode.Name == "svg")
            {
                if (data.htmlNode.Attributes.Contains("xmlns") && data.htmlNode.Attributes["xmlns"].Value == "http://www.w3.org/2000/svg")
                {
                    data.htmlNode.Attributes.Remove("xmlns");
                }

                if (data.htmlNode.Attributes.Contains("width") &&
                    data.htmlNode.Attributes.Contains("height") &&
                    data.htmlNode.Attributes["width"].Value == data.htmlNode.Attributes["height"].Value)
                {
                    data.htmlNode.Attributes.Add("size", data.htmlNode.Attributes["width"].Value);

                    data.htmlNode.Attributes.Remove("width");
                    data.htmlNode.Attributes.Remove("height");
                }
            }

            return data;
        }

        static Data initHtmlNodeName(Data data)
        {
            var htmlNodeName = data.htmlNode.OriginalName;
            if (htmlNodeName == "clippath")
            {
                htmlNodeName = "clipPath";
            }

            return data with { htmlNodeName = htmlNodeName };
        }

        static bool canStyleExportInOneLine(Style style)
        {
            return style.ToDictionary().Count <= 3;
        }

        static string JoinModifiers(IReadOnlyList<ModifierCode> modifiers)
        {
            return string.Join(" ", modifiers.Where(ModifierCode.IsFail).Select(x => x.Code)) +
                   string.Join(", ", modifiers.Where(ModifierCode.IsSuccess).Select(x => x.Code));
        }
    }

    static (bool success, string modifierCode) TryConvertToModifier(HtmlAttribute htmlAttribute)
    {
        var name = htmlAttribute.GetName();
        var value = htmlAttribute.Value;
        var tagName = htmlAttribute.OwnerNode.Name;

        return ToModifierTransformer.TryConvertToModifier(tagName, name, value);
    }

    class AgilityPackageOverride
    {
        public static string DecodeValue(string value)
        {
            return HttpUtility.UrlDecode(value);
        }

        public static string Encode(string styleText)
        {
            var index = 0;
            while (true)
            {
                var (hasChange, newIndex, styleTextNewValue) = EncodeUrl(styleText, index);
                if (hasChange is false)
                {
                    return styleText;
                }

                index = newIndex;

                styleText = styleTextNewValue;
            }
        }

        static (bool hasChange, int newIndex, string styleTextNewValue) EncodeUrl(string styleText, int startIndex)
        {
            var beginIndex = styleText.IndexOf("url(", startIndex, StringComparison.OrdinalIgnoreCase);
            if (beginIndex > 0)
            {
                var endIndex = styleText.IndexOf(")", beginIndex, StringComparison.OrdinalIgnoreCase);
                if (endIndex > 0)
                {
                    var value = styleText.Substring(beginIndex, endIndex - beginIndex + 1);

                    var partBegin = styleText.Substring(0, beginIndex);
                    var partEnd = styleText.Substring(endIndex + 1, styleText.Length - endIndex - 1);

                    return (hasChange: true, newIndex: endIndex + 1, styleTextNewValue: partBegin + EncodeValue(value) + partEnd);
                }
            }

            return default;
        }

        static string EncodeValue(string value)
        {
            return UrlEncoder.Default.Encode(value);
        }
    }

    sealed class ModifierCode
    {
        public string Code { get; private init; }

        public string PartName
        {
            get
            {
                var leftParanthesisIndex = Code.IndexOf('(', StringComparison.OrdinalIgnoreCase);
                if (leftParanthesisIndex <= 0)
                {
                    return Code;
                }

                return Code[..leftParanthesisIndex];
            }
        }

        public string PartParameterWithoutParanthesis
        {
            get
            {
                var leftParanthesisIndex = Code.IndexOf('(', StringComparison.OrdinalIgnoreCase);
                if (leftParanthesisIndex > 0)
                {
                    var rightParanthesisIndex = Code.LastIndexOf(')');
                    if (rightParanthesisIndex > 0)
                    {
                        return Code.Substring(leftParanthesisIndex + 1, rightParanthesisIndex - leftParanthesisIndex - 1);
                    }
                }

                return null;
            }
        }

        public bool Success { get; private init; }

        public static ModifierCode From((bool success, string modifierCode) tuple)
        {
            return new() { Code = tuple.modifierCode, Success = tuple.success };
        }

        public static bool IsFail(ModifierCode item)
        {
            return item.Success is false;
        }

        public static bool IsSuccess(ModifierCode item)
        {
            return item.Success;
        }

        public static implicit operator ModifierCode(string code)
        {
            return FromString(code);
        }

        public override string ToString()
        {
            return Success ? Code : "fail";
        }

        static ModifierCode FromString(string code)
        {
            return new() { Code = code, Success = true };
        }
    }

    record Data
    {
        public HtmlNode htmlNode { get; init; }

        public string htmlNodeName { get; init; }

        public List<ModifierCode> modifiers { get; init; }

        public Style style { get; init; }
    }
}