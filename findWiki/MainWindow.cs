using System;
using Gtk;
using System.Diagnostics;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


	protected void onQuit (object sender, EventArgs e)
	{
		Application.Quit ();
	}

	protected void onHelp (object sender, EventArgs e)
	{
		result.Text = " Type the search in the text box and\n" +
			" hit enter or click on Search button to search.";
	}



	protected void onSearch (object sender, EventArgs e)
	{
	
		String text = entry1.Text;
		Console.WriteLine (run_python (text));
		result.Text = run_python(text);;
	}

     
    protected string run_python(string args)
	{
		/*the code executes as " Filename Arguments " in shell

		Here,
		Filename = Python
		Arguments = complete_address_of_python_file.py arguments 

        i.e. it runs "python filename.py arguments " in the shell

		StandardOutput stores the output of program displayed in the shell */ 

		string directory = System.AppDomain.CurrentDomain.BaseDirectory;//for current directory of app

		Process proc = new Process ();
		proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
		proc.StartInfo.FileName = "python";
		proc.StartInfo.Arguments = string.Format("{0}/wikisearch.py {1}", directory, args);
		proc.StartInfo.UseShellExecute = false;
		proc.StartInfo.RedirectStandardError = true;
		proc.StartInfo.RedirectStandardInput = true;
		proc.StartInfo.RedirectStandardOutput = true;
		proc.Start ();

		string output = (proc.StandardOutput.ReadToEnd());

		return output;
	}

	protected void onEntered (object sender, EventArgs e)
	{
		onSearch(sender, e);
	}
}
