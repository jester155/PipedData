using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pipe.Query {
	public class QueryInterpreter {

		const string WILD_CARD = "*";
		public Query Query { get; private set; }
		public Database Database { get; set; }
		public StringBuilder MessageBuilder { get; set; }
		public PipeEditor PipeEditor { get; set; }

		public QueryInterpreter(Database database , Query query) {
			this.Query = query;
			this.Database = database;
			this.MessageBuilder = new StringBuilder();
			this.PipeEditor = new PipeEditor();
		}

		//. TODO
		public string[] InterpretQuery() {
			return null;
		}

		public bool PerformQueryAction(out string message) {
			this.MessageBuilder = new StringBuilder();
			message = string.Empty;

			switch(this.Query.QueryOption) {
				case QueryOptions.Create:
					return true;
				case QueryOptions.Select:
					message = PerformSelect();
					return true;
				case QueryOptions.Insert:
					message = PerformInsert();
					return true;
				case QueryOptions.Update:
					return true;
				case QueryOptions.Delete:
					message = PipeEditor.DeleteEntry(this.Database.DataFile , 0);
					return true;
				case QueryOptions.Use:
					return true;
				case QueryOptions.Set:
					return true;
				case QueryOptions.Import:
					return true;
				case QueryOptions.Invalid:
					return true;
				default:
					message = "No action was performed.";
					return false;
			}
		}

		private int GetCol() {
			var header = this.Database.Headers
				.Where(h => h == this.Query.Filter.FilterColumn)
				.Single();

			return Array.IndexOf(this.Database.Headers , header);
		}

		private List<List<string>> FilterIs() {
			var filtered = new List<List<string>>();

			int col = GetCol();

			var lines = this.Database.Entries
				.Select(line => line)
				.ToList();

			foreach(var line in lines) {
				if(this.Query.Filter.FilterValue == line[col]) {
					filtered.Add(line);
				}
			}

			return filtered;
		}

		private List<List<string>> FilterPartof() {

			int col = GetCol();

			var lines = this.Database.Entries
				.Select(line => line)
				.ToList();

			var filtered = new List<List<string>>();

			foreach(var line in lines) {
				if(line[col].Contains(this.Query.Filter.FilterValue)) {
					filtered.Add(line);
				}
			}

			return filtered;
		}

		private string PerformInsert() {
			var newEntries = this.Query.QueryParameters.ToList();
			this.Database.Entries.Add(newEntries);
			var lineEntry = Environment.NewLine;

			foreach(var entry in newEntries) {
				lineEntry += (entry.IndexOf(entry) != newEntries.Count - 1) ? entry + "|" : entry;
			}

			this.PipeEditor.AppendEntry(this.Database.DataFile , lineEntry);

			return "Inserted row into database";
		}

		private string PerformSelect() {
			var result = string.Empty;
			if(this.Query.QueryParameters.Contains(WILD_CARD)) {
				var i = 0;
				foreach(var line in this.Database.Entries) {
					this.MessageBuilder.Append(i++ + ": ");
					foreach(var entry in line) {
						if(line.Count - 1 != line.IndexOf(entry)) {
							this.MessageBuilder.Append(entry + "|");
						}
						else this.MessageBuilder.Append(entry);
					}
					this.MessageBuilder.AppendLine();
				}
			}
			else if(this.Query.HasFilter) {
				BuildMessage(this.Query.Filter.FilterOption);
			}

			return this.MessageBuilder.ToString();
		}

		private void BuildMessage(FileterOptions filterOptions) {
			var filteredEntries = new List<List<string>>();
			switch(filterOptions) {
				case FileterOptions.Is:
					filteredEntries = FilterIs();
					break;
				case FileterOptions.PartOf:
					filteredEntries = FilterPartof();
					break;
				case FileterOptions.None:
					filteredEntries = this.Database.Entries;
					break;
				default:
					break;
			}

			var i = 0;
			foreach(var line in filteredEntries) {
				MessageBuilder.Append(i++ + ": ");
				foreach(var entry in line) {
					if(line.IndexOf(entry) != line.Count - 1) {
						MessageBuilder.Append(entry + ": ");
					}
					else MessageBuilder.Append(entry);
				}

				MessageBuilder.AppendLine();
			}
		}
	}
}
