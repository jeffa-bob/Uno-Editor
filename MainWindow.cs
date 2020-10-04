using System;
using System.IO;
using Cairo;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Editor
{
    class MainWindow : Window
    {
        [UI] private Label _label1 = null;
        [UI] private Button _button1 = null;
        [UI] private Button _openfilebutton = null;
        [UI] private Button _openfolderbutton = null;
        [UI] private Grid _maingrid = null;
        private FolderExplorer _folderexplore = new FolderExplorer();

        private int _counter;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);


            DeleteEvent += Window_DeleteEvent;
            _button1.Clicked += Button1_Clicked;
            _openfilebutton.Clicked += Openfile_Clicked;
            _openfolderbutton.Clicked += Openfolder_Clicked;
            _maingrid.Add(_folderexplore);
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void Button1_Clicked(object sender, EventArgs a)
        {
            _counter++;
            _label1.Text = "Hello World! This button has been clicked " + _counter + " time(s).";
            Gdk.RGBA color = new Gdk.RGBA();
            color.Parse("#0ff54c");
            _label1.OverrideColor(StateFlags.Normal, color);
        }

        private void Openfile_Clicked(object sender, EventArgs a)
        {
            Gtk.FileChooserDialog fc =
            new Gtk.FileChooserDialog("Choose the file to open",
                                        this,
                                        FileChooserAction.Open,
                                        "Cancel", ResponseType.Cancel,
                                        "Open", ResponseType.Accept);

            if (fc.Run() == (int)ResponseType.Accept)
            {
                System.IO.FileStream file = System.IO.File.OpenRead(fc.Filename);
                file.Close();
            }
            //Don't forget to call Destroy() or the FileChooserDialog window won't get closed.
            fc.Dispose();
        }

        private void Openfolder_Clicked(object sender, EventArgs a)
        {
            Gtk.FileChooserDialog fc =
            new Gtk.FileChooserDialog("Choose the Directory to open",
                                        this,
                                        FileChooserAction.SelectFolder,
                                        "Cancel", ResponseType.Cancel,
                                        "Open", ResponseType.Accept);

            if (fc.Run() == (int)ResponseType.Accept)
            {
                //System.Console.WriteLine(fc.Filename);
                _folderexplore.SetDirectory(fc.Filename);
            }
            //Don't forget to call Destroy() or the FileChooserDialog window won't get closed.
            fc.Dispose();
        }
    }
}
