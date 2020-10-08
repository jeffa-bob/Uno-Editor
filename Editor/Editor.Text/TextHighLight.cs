using System;

namespace Editor.Text
{
    public class TextHighlight
    {
        Color Highlightcolor = new Color();
        long start;
        long end;
        //highlight background = false highlight foreground = true
        bool IsForeground = false;

        public TextHighlight() { }
        public TextHighlight(long start, long end)
        {
            this.start = start;
            this.end = end;
        }
        public TextHighlight(Color color)
        {
            this.Highlightcolor = color;
        }
        public TextHighlight(long start, long end, Color color)
        {
            this.Highlightcolor = color;
            this.start = start;
            this.end = end;
        }
    }
}