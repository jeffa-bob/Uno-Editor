using System;
using Gtk;
using System.IO;

class FileTextEditor : ScrolledWindow
{
    FileInfo openfile;

    public static event EventHandler closefile;

    public Box tabutton = new Box(Orientation.Horizontal, 2);


    TextView view = new TextView();

    public FileTextEditor(): base(){
        view.ShowAll();
        view.AcceptsTab = true;
        view.Monospace = true;
        base.Add(view);
    }

    private void CloseEditor(object sender, EventArgs e){
        closefile?.Invoke(this, e);
    }

    public void Setfile(string filepath)
    {
        Setfile(new FileInfo(filepath));
    }
    public void Setfile(FileInfo file)
    {
        openfile = file;
        try
        {
            using (StreamReader reader = new StreamReader(new FileStream(openfile.FullName, FileMode.Open)))
            {
                string filetext = reader.ReadToEnd();
                view.Buffer = new TextBuffer(null);
                view.Buffer.Text = filetext;

                Label filelabel = new Label(openfile.Name);
                Button filebutton = new Button("window-close", IconSize.Menu);
                filebutton.Clicked += CloseEditor;
                tabutton.Add(filelabel);
                tabutton.Add(filebutton);
                tabutton.ShowAll();
            }
            base.ShowAll();
        }
        catch (Exception) { }
    }

    public void SaveFile()
    {
        using (FileStream file = new FileStream(openfile.FullName, FileMode.OpenOrCreate))
        {
            file.SetLength(0);
            byte[] text = System.Text.Encoding.UTF8.GetBytes(view.Buffer.Text);
            Console.WriteLine(view.Buffer.Text);
            file.Write(text, 0, text.Length);
        }
    }
}
