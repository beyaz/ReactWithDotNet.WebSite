namespace ReactWithDotNet.VisualDesigner.Views;

sealed class ApplicationPreview : Component
{
    public Task Refresh()
    {
        return Task.CompletedTask;
    }

    protected override Element componentDidCatch(Exception exceptionOccurredInRender)
    {
        return new div(Background(Gray100))
        {
            exceptionOccurredInRender.ToString()
        };
    }

    protected override Task constructor()
    {
        Client.ListenEvent("RefreshComponentPreview", Refresh);

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        var userName = Environment.UserName; // future: get userName from cookie or url

        var appState = GetUserLastState(userName);

        if (appState is null)
        {
            return new div(Size(200), Background(Gray100))
            {
                "Has no state"
            };
        }
        
        var rootElement = appState.ComponentRootElement;
        if (rootElement is null)
        {
            return null;
        }

        VisualElementModel highlightedElement = null;
        {
            var selection = appState.Selection;
            if (selection.VisualElementTreeItemPathHover.HasValue())
            {
                highlightedElement = FindTreeNodeByTreePath(rootElement, selection.VisualElementTreeItemPathHover);
            }
            else if (selection.VisualElementTreeItemPath.HasValue())
            {
                highlightedElement = FindTreeNodeByTreePath(rootElement, selection.VisualElementTreeItemPath);
            }
        }
        

        return renderElement(rootElement);

        Element renderElement(VisualElementModel model)
        {
            HtmlElement element = new div();

            if (model.Tag == "i")
            {
                element = new i();
            }

            element.style.Add(UserSelect(none));
            
            if (model.Text.HasValue())
            {
                element.text = model.Text;
            }
            
            foreach (var property in model.Properties)
            {
                var (success, name, value) = TryParsePropertyValue(property);
                if (success)
                {
                    if (name == "class")
                    {
                        element.AddClass(value);
                    }
                }
            }

            if (highlightedElement == model)
            {
                element.Add(Outline($"1px {dashed} {Blue300}"));
            }

            foreach (var styleGroup in model.StyleGroups ?? [])
            {
                foreach (var styleAttribute in styleGroup.Items ?? [])
                {
                    // try process from plugin
                    {
                        var style = TryProcessStyleAttributeByProjectConfig(styleAttribute);
                        if (style is not null)
                        {
                            element.Add(style);
                            continue;
                        }
                    }
                    
                    switch (styleAttribute)
                    {
                        case "w-full":
                        {
                            element.Add(Width("100%"));
                            continue;
                        }
                    }
                    
                    var (success, name, value) = TryParsePropertyValue(styleAttribute);
                    if (!success)
                    {
                        continue;
                    }
                    
                    var isValueDouble = double.TryParse(value, out var valueAsDouble);

                    switch (name)
                    {
                        case "display":
                        {
                            element.Add(Display(value));
                            continue;
                        }

                        case "background":
                        case "bg":
                        {
                            element.Add(Background(value));
                            continue;
                        }
                        
                        case "font-size":
                        {
                            if (isValueDouble)
                            {
                                element.Add(FontSize(valueAsDouble));
                                continue;
                            }

                            element.Add(FontSize(value));
                            continue;
                        }
                        
                        case "w":
                        case "width":
                        {
                            if (isValueDouble)
                            {
                                element.Add(Width(valueAsDouble));
                                continue;
                            }

                            element.Add(Width(value));
                            continue;
                        }

                        case "h":
                        case "height":
                        {
                            if (isValueDouble)
                            {
                                element.Add(Height(valueAsDouble));
                                continue;
                            }

                            element.Add(Height(value));
                            continue;
                        }

                        case "border-radius":
                        {
                            if (isValueDouble)
                            {
                                element.Add(BorderRadius(valueAsDouble));
                                continue;
                            }

                            element.Add(BorderRadius(value));
                            continue;
                        }

                        case "gap":
                        {
                            if (isValueDouble)
                            {
                                element.Add(Gap(valueAsDouble));
                                continue;
                            }

                            element.Add(Gap(value));
                            continue;
                        }

                        case "padding":
                        {
                            if (isValueDouble)
                            {
                                element.Add(Padding(valueAsDouble));
                                continue;
                            }

                            element.Add(Padding(value));
                            continue;
                        }
                        
                        case "size":
                        {
                            if (isValueDouble)
                            {
                                element.Add(Size(valueAsDouble));
                                continue;
                            }

                            element.Add(Size(value));
                            continue;
                        }
                        
                        case "color":
                        {
                            element.Add(Color(value));
                            continue;
                        }
                        
                        case "pl":
                        case "padding-left":
                        {
                            if (isValueDouble)
                            {
                                element.Add(PaddingLeft(valueAsDouble));
                                continue;
                            }

                            element.Add(PaddingLeft(value));
                            continue;
                        }
                        
                        case "pr":
                        case "padding-right":
                        {
                            if (isValueDouble)
                            {
                                element.Add(PaddingRight(valueAsDouble));
                                continue;
                            }

                            element.Add(PaddingRight(value));
                            continue;
                        }
                        
                        case "pt":
                        case "padding-top":
                        {
                            if (isValueDouble)
                            {
                                element.Add(PaddingTop(valueAsDouble));
                                continue;
                            }

                            element.Add(PaddingTop(value));
                            continue;
                        }
                        
                        case "pb":
                        case "padding-bottom":
                        {
                            if (isValueDouble)
                            {
                                element.Add(PaddingBottom(valueAsDouble));
                                continue;
                            }

                            element.Add(PaddingBottom(value));
                            continue;
                        }
                        
                        case "flex-direction":
                        {
                            element.Add(FlexDirection(value));
                            continue;
                        }
                    }
                }
            }

            if (model.HasNoChild())
            {
                return element;
            }

            element.children.AddRange(model.Children.Select(renderElement));

            return element;
        }
    }
}