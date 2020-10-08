using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;

namespace Editor.Text
{
    public class Document
    {
        string text { get; set; }

        //file extension
        DocTypes filetype { get; set; }

        List<TextHighlight> colors = new List<TextHighlight>();

        public Document() { }
        public Document(string text)
        {
            this.text = text;
        }

        public void ColorizeText()
        {

        }


        public void KeyDownEvent(object sender, EventArgs e)
        {
            text = (sender as string);
        }
    }
}
