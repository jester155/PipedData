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

		private bool PerformQueryAction(out string message) {
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

		private List<List<string>> FilterIs() {
			var entries = this.Database.Entries;
			var results = new List<List<string>>();

			foreach(var i in GetIndexesToWorkOn()) {
				foreach(var line in entries) {
					var temp = new List<string>();
					foreach(var entry in line) {
						if(entry.IndexOf(entry) == i && entry == this.Query.Filter.FilterValue) {
							temp.Add(entry);
						}
					}
					results.Add(temp);
				}
			}

			return results;
		}

		private List<List<string>> FilterPartof() {
			var entries = this.Database.Entries;
			var results = new List<List<string>>();

			foreach(var i in GetIndexesToWorkOn()) {
				foreach(var line in entries) {
					var temp = new List<string>();
					foreach(var entry in line) {
						if(entry.IndexOf(entry) == i && entry.Contains(this.Query.Filter.FilterValue)) {
							temp.Add(entry);
						}
					}
					results.Add(temp);
				}
			}

			return results;
		}

		private int IndexToRemove() {
			return GetIndexesToWorkOn(this.Query.Filter.FilterColumn);
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
					foreach(var entry in line) {
						if(line.Count - 1 != line.IndexOf(entry)) {
							this.MessageBuilder.AppendLine(i++ + ": " + entry + "|");
						}
						else this.MessageBuilder.AppendLine(i++ + ": " + entry);
					}
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
					filteredEntries = null;
					break;
				default:
					break;
			}

			if(filteredEntries != null) {
				var i = 0;
				foreach(var line in filteredEntries) {
					foreach(var entry in line) {
						if(FilterIs().IndexOf(line) != FilterIs().Count - 1) {
							MessageBuilder.AppendLine(i++ + ": " + entry + "|");
						}
						else MessageBuilder.AppendLine(i++ + ": " + entry);
					}
				}
			}
		}

		private int[] GetIndexesToWorkOn() {
			return this.Database.Headers
					.Where(entry => this.Query.QueryParameters.Any(a => a == entry))
					.Select(r => Array.IndexOf(this.Database.Headers , r))
					.ToArray();
		}

		private int GetIndexesToWorkOn(string fileter) {
			return this.Database.Headers
					.Where(entry => entry == fileter)
					.Select(r => Array.IndexOf(this.Database.Headers , r))
					.Single();
		}
	}
}
