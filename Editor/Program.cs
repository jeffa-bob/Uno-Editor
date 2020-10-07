using System;
using Gtk;

namespace Editor
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            var app = new Application("org.Editor.Editor", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);
            MainWindow win = null;

            if (args.Length == 1)
            {
                if (System.IO.Directory.Exists(args[0]))
                    win = new MainWindow(args[0]);
                else
                    win = new MainWindow();
            }
            else
                win = new MainWindow();

            app.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}
