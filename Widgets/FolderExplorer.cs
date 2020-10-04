using Gtk;
using System.IO;
using System.Linq;
using System;
using UI = Gtk.Builder.ObjectAttribute;
using System.Collections.Generic;

class FolderExplorer : ScrolledWindow
{

    private Box box = new Box(Orientation.Vertical, 2);

    private Label Current_Directory = new Label();

    public event EventHandler openfile;

    private DirectoryInfo directory;

    public FolderExplorer() : base()
    {
        base.BorderWidth = 2;
        Gdk.RGBA color = new Gdk.RGBA();
        color.Parse("#0ff54c");
        base.WidthRequest = 100;
        base.HeightRequest = 400;
        //base.OverrideBackgroundColor(StateFlags.Backdrop, color);
    }

    private void clickhandler(object sender, EventArgs e)
    {
        Button clicker = sender as Button;
        openfile.Invoke(new FileInfo(clicker.ActionName), e);
    }

    public void SetDirectory(string path)
    {
        if (!System.IO.Path.EndsInDirectorySeparator(path))
            path += System.IO.Path.DirectorySeparatorChar;
        Console.WriteLine(path);
        directory = new DirectoryInfo(path);
        Current_Directory = new Label(directory.Name);
        box.Add(Current_Directory);
        FileInfo[] tempfiles = directory.GetFiles();

        foreach (FileInfo file in tempfiles)
        {
            Button filebutton = new Button(file.FullName);
            filebutton.Label = file.Name;
            filebutton.ActionName = file.FullName;
            filebutton.Clicked += clickhandler;
            filebutton.Sensitive = true;
            box.Add(filebutton);
        }
        box.ShowAll();
        base.Add(box);
        base.ShowAll();
    }
}