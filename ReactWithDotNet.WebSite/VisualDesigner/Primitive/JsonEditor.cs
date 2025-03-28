using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;
using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.VisualDesigner.Primitive;

static class JsonEditor
{
    public static Element NewJsonEditor(Expression<Func<string>> valueBind)
    {
        return new Editor
        {
            defaultLanguage = "json",
            valueBind       = valueBind,
            options =
            {
                renderLineHighlight = "none",
                fontFamily          = "'IBM Plex Mono Medium', 'Courier New', monospace",
                fontSize            = 11,
                minimap             = new { enabled = false },
                formatOnPaste       = true,
                formatOnType        = true,
                automaticLayout     = true,
                lineNumbers         = false
            }
        };
    }
}