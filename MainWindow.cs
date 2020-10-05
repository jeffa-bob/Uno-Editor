using System;
using System.IO;
using Cairo;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Editor
{
    class MainWindow : Window
    {
        [UI] private Button _openfilebutton = null;
        [UI] private Button _openfolderbutton = null;
        [UI] private Grid _maingrid = null;
        [UI] private Notebook _maineditorbook = null;
        private FolderExplorer _folderexplore = new FolderExplorer();



        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);


            DeleteEvent += Window_DeleteEvent;
            _openfilebutton.Clicked += Openfile_Clicked;
            _openfolderbutton.Clicked += Openfolder_Clicked;
            _folderexplore.openfile += OpenFolderOpenFile;
            _maingrid.AttachNextTo(_folderexplore, _openfilebutton, PositionType.Bottom, 1, 5);
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
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
            FileTextEditor editor = new FileTextEditor();
            editor.Setfile(fc.Filename);
            _maineditorbook.AppendPage(editor,editor.tabutton);
            _maineditorbook.ShowAll();
            }
            //Don't forget to call Destroy() or the FileChooserDialog window won't get closed.
            fc.Dispose();
        }

        private void OpenFolderOpenFile(object sender, EventArgs a){
            FileInfo info = sender as FileInfo;
            FileTextEditor editor = new FileTextEditor();
            editor.Setfile(info);
            _maineditorbook.AppendPage(editor,editor.tabutton);
            _maineditorbook.ShowAll();

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
