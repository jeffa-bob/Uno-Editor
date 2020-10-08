using System.Collections.Generic;

namespace Editor.Text

{
    public class TextColorTheme : Dictionary<string, Color>
    {
        public TextColorTheme() : base() { }

        public static TextColorTheme Default = new TextColorTheme{
         { "modifier", new Color(0, 0, 200) },
         { "variable", new Color(0, 200, 0) }
        };
    }

}