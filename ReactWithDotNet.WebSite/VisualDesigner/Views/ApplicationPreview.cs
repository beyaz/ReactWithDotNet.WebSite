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
        
        if (appState.HoveredVisualElementTreeItemPath.HasValue())
        {
            rootElement = FindTreeNodeByTreePath(rootElement, appState.HoveredVisualElementTreeItemPath);
        }

        return renderElement(rootElement);

        Element renderElement(VisualElementModel model)
        {
            var element = new div();

            if (model.Text.HasValue())
            {
                element.text = model.Text;
            }

            foreach (var styleGroup in model.StyleGroups ?? [])
            {
                foreach (var styleAttribute in styleGroup.Items_old ?? [])
                {
                    var value = styleAttribute.Value;

                    var isValueDouble = double.TryParse(value, out var valueAsDouble);

                    switch (styleAttribute.Name)
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