using System;
using Mono.Unix;

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tomboy.TomboyTask
{
	public class TomboyTaskNoteAddin : NoteAddin
	{

		Gtk.MenuItem item;
		string[] categories;
		
		public override void Initialize (){
		}

		public override void Shutdown (){
		}
		
		public override void OnNoteOpened ()
		{
			if (this.Note.Title.StartsWith("GTD")) {
				item = new Gtk.MenuItem("Update Tasks");
				item.Activated += OnMenuItemActivated;
				item.AddAccelerator ("activate", Window.AccelGroup,
					(uint) Gdk.Key.d, Gdk.ModifierType.ControlMask,
					Gtk.AccelFlags.Visible);
				item.Show ();
				AddPluginMenuItem (item);
			}
		}

		void OnMenuItemActivated (object sender, EventArgs args)
		{
			if (this.Note.Title.Contains(".")) {
				string cats = this.Note.Title.Substring(4,this.Note.Title.Length-4);
				if (cats.Contains(".")) {
					categories = cats.Split('.');
				} else {
					categories = new string[1] {cats};
				}
				
			} else {
				categories = new string[0];
			}
			this.updateContent();
		}
		
		private void updateContent() {
			string content = string.Format("<note-content version=\"0.1\">{0}\n\n\n",this.Note.Title);
			SortedDictionary<string, string> notebook_map = new SortedDictionary<string, string>();
//			SortedDictionary<DateTime, string> timed_events = new SortedDictionary<DateTime, string>();
			foreach (Note note in this.Manager.Notes) {
				if (note.Title.StartsWith("GTD")) continue;
				string[] lines = note.XmlContent.Split('\n');
				string task = "";
				foreach (string line in lines) {
					if (line.Contains("<strikethrough>")) continue;
					int idx = line.IndexOf("@Task");
					if (idx < 0) continue;
					Regex rgx = new Regex("<[^>]*>");	
					task = rgx.Replace(line,"");
					idx = task.IndexOf("@Task");
					//Console.WriteLine(task);
					bool has_cat = false;
//					DateTime dateTime = null;
					string cats = task.Substring(idx,task.Length-idx);
					foreach(string cat in categories) {
//						if (DateTime.TryParse(input, out dateTime)) {
//							
//						}
						if (cats.Contains(cat)) has_cat = true;
					}
					if (!has_cat) continue;
					task = task.Substring(0,idx-1);
					break;
				}
				if (task.Length < 1) continue;
				
				Notebooks.Notebook book = Notebooks.NotebookManager.GetNotebookFromNote(note);
				string book_name = "No notebook";
				if (book != null) {
					book_name = Notebooks.NotebookManager.GetNotebookFromNote(note).Name;
				}
				if (!notebook_map.ContainsKey(book_name)) {
					notebook_map[book_name] = "";
				}
				task = string.Format("{0} ({1})",task,note.Title);
				notebook_map[book_name] += string.Format("<list-item dir=\"ltr\">{0}\n</list-item>", task);
			}
			foreach (string book_name in notebook_map.Keys) {
				content += string.Format("<size:large>{0}</size:large>\n<list>{1}</list>\n\n", book_name, notebook_map[book_name]);
			}
			content += "</note-content>";
			//Console.WriteLine(content);
			this.Note.XmlContent = content;
		}
	}
}
