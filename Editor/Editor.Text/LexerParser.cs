using System;
using Sprache;

namespace Editor.Text
{
    class TextParser
    {
        string text;
        Input input;
        
        public TextParser() { }
        public TextParser(string text)
        {
            this.text = text;
            input = new Input(text);
        }
    }
}