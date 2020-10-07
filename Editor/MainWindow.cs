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
    [UI] private Button _SaveButton = null;
    [UI] private Box _mainbox = null;
    [UI] private Box _savebuttonbox = null;
    [UI] private Box _mainpaned = null;
    [UI] private Notebook _maineditorbook = null;
    private FolderExplorer _folderexplore = new FolderExplorer();
    private bool blankopen = true;


    public MainWindow() : this(new Builder("MainWindow.glade"))
    {
      _mainpaned.Add(_maineditorbook);

    }
    public MainWindow(string path) : this(new Builder("MainWindow.glade"))
    {
      Console.WriteLine("The Directory is " + path);

      if (path != null)
      {
        _folderexplore.SetDirectory(path);
      }
      _mainpaned.Add(_maineditorbook);
      _mainpaned.Add(_folderexplore);

    }


    private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
    {

      base.SetDefaultSize(900, 900);
      CssProvider provider = new CssProvider();
      provider.LoadFromPath(@"Styles/gtk-dark.gtk-3.0.Materia.css");

      builder.Autoconnect(this);

      Gtk.StyleContext.AddProviderForScreen(Gdk.Screen.Default, provider, 800);

      DeleteEvent += Window_DeleteEvent;
      _openfilebutton.Clicked += Openfile_Clicked;
      _openfolderbutton.Clicked += Openfolder_Clicked;
      FileTextEditor.closefile += CloseEditor;
      _folderexplore.openfile += OpenFolderOpenFile;

      //Added to keep the Notebook from Breaking
      _maineditorbook.Add(new Label("Open A file"));

    }

    private void Window_DeleteEvent(object sender, DeleteEventArgs a)
    {
      Application.Quit();
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
      FileTextEditor a = _maineditorbook.CurrentPageWidget as FileTextEditor;
      a.SaveFile();
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
        editor.Expand = true;
        editor.Setfile(fc.Filename);
        _maineditorbook.AppendPage(editor, editor.tabutton);
        _maineditorbook.SetTabReorderable(editor, true);

        if (blankopen)
        {
          _maineditorbook.RemovePage(0);
          blankopen = false;
        }
        _maineditorbook.CurrentPage = _maineditorbook.NPages - 1;
        _maineditorbook.ShowAll();
      }
      //Don't forget to call Destroy() or the FileChooserDialog window won't get closed.
      fc.Dispose();
    }

    private void OpenFolderOpenFile(object sender, EventArgs a)
    {
      FileInfo info = sender as FileInfo;
      FileTextEditor editor = new FileTextEditor();
      editor.Expand = true;
      editor.Setfile(info);
      _maineditorbook.AppendPage(editor, editor.tabutton);
      _maineditorbook.SetTabReorderable(editor, true);

      if (blankopen)
      {
        _maineditorbook.RemovePage(0);
        blankopen = false;
      }
      _maineditorbook.CurrentPage = _maineditorbook.NPages - 1;
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
        _mainpaned.Remove(_folderexplore);
        _mainpaned.Add(_folderexplore);
      }
      //Don't forget to call Destroy() or the FileChooserDialog window won't get closed.
      fc.Dispose();
    }

    public void CloseEditor(object sender, EventArgs e)
    {
      if (_maineditorbook.NPages == 1)
      {
        _maineditorbook.Add(new Label("Open A file"));
        blankopen = true;
      }
      _maineditorbook.Remove(sender as FileTextEditor);
    }

  }
}
