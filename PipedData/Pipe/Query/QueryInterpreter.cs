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
					var row = GetRowToRemove();
					message = PipeEditor.DeleteEntry(this.Database.DataFile , row + 1);
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

		private int GetRowToRemove() {
			var line = new List<string>();
			switch(this.Query.Filter.FilterOption) {
				case FileterOptions.Is:
					line = FilterIs().Select(l => l).Single();
					break;
				case FileterOptions.PartOf:
					line = FilterPartof().Select(l => l).Single();
					break;
				case FileterOptions.None:
				default:
					return -1;
			}

			return this.Database.Entries.IndexOf(line);
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

			if(this.Query.QueryParameters.Length > 0 &&
				this.Query.QueryParameters[0] != "*") {
				filtered = FilterColumns(filtered);
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
				if(line.Count > 1 && line[col].Contains(this.Query.Filter.FilterValue)) {
					filtered.Add(line);
				}
			}

			if(this.Query.QueryParameters.Length > 0 &&
				this.Query.QueryParameters[0] != "*") {
				filtered = FilterColumns(filtered);
			}

			return filtered;
		}

		private List<List<string>> FilterColumns(List<List<string>> filter) {
			var temp = new List<List<string>>();
			var filterCols = this.Database.Headers.Where(h =>
				this.Query.QueryParameters
				.Any(a => a == h))
				.ToList();

			foreach(var line in filter) {
				var tempLine = new List<string>();
				foreach(var col in filterCols) {
					tempLine.Add(line[Array.FindIndex(this.Database.Headers , r => r == col)]);
				}
				temp.Add(tempLine);

			}

			return temp;
		}

		private string PerformInsert() {
			var newEntries = this.Query.QueryParameters.ToList();
			this.Database.Entries.Add(newEntries);
			var lineEntry = "\r\n";

			foreach(var entry in newEntries) {
				lineEntry += (newEntries.IndexOf(entry) != newEntries.Count - 1) ? entry + "|" : entry;
			}

			this.PipeEditor.AppendEntry(this.Database.DataFile , lineEntry);

			return "Inserted row into database";
		}

		private string PerformSelect() {
			var result = string.Empty;
			if(this.Query.QueryParameters.Contains(WILD_CARD) ||
				this.Query.HasFilter) {
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

			var tableHeader = FetchHeader();

			foreach(var header in tableHeader) {
				if(tableHeader.IndexOf(header) != tableHeader.Count - 1) {
					MessageBuilder.Append(header + " | ");
				}
				else MessageBuilder.Append(header);
			}

			MessageBuilder.AppendLine();

			foreach(var line in filteredEntries) {
				foreach(var entry in line) {
					if(line.IndexOf(entry) != line.Count - 1) {
						MessageBuilder.Append(entry + " | ");
					}
					else MessageBuilder.Append(entry);
				}

				MessageBuilder.AppendLine();
			}
		}

		private List<string> FetchHeader() {

			return this.Query.QueryParameters.Contains(WILD_CARD) ?
				this.Database.Headers.ToList() :
				this.Database.Headers.Where(h => this.Query.QueryParameters.Any(p => p == h)).ToList();
				
		}
	}
}
