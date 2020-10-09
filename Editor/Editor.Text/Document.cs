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

        private FileInfo path;

        List<TextHighlight> colors = new List<TextHighlight>();

        public Document() { }
        public Document(FileInfo path)
        {
            ReadFile(path);
        }

        public Document(string text)
        {
            this.text = text;
        }

        public void ColorizeText()
        {

        }

        public void Savefile()
        { 
            using (FileStream file = new FileStream(path.FullName, FileMode.OpenOrCreate))
            {
                file.SetLength(0);
                byte[] text = System.Text.Encoding.UTF8.GetBytes(this.text);
                file.Write(text, 0, text.Length);
            }
        }

        public void ReadFile(FileInfo path)
        {
            this.path = path;
            try
            {
                using (StreamReader reader = new StreamReader(new FileStream(path.FullName, FileMode.Open)))
                {
                    this.text = reader.ReadToEnd();
                }
            }
            catch (Exception) { }
        }

        public void KeyDownEvent(object sender, EventArgs e)
        {
            text = (sender as string);
        }
    }
}
