using System;
using System.Linq;
using System.Text;

namespace Pipe.Query {
	public class QueryInterpreter {

		const string WILD_CARD = "*";
		public Query Query { get; private set; }
		public Database Database { get; private set; }
		public StringBuilder MessageBuilder { get; private set; }
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
					break;
				case QueryOptions.Select:
					message = PerformSelect();
					return true;
				case QueryOptions.Insert:
					message = PerformInsert();
					break;
				case QueryOptions.Update:
					break;
				case QueryOptions.Delete:
					break;
				case QueryOptions.Use:
					break;
				case QueryOptions.Set:
					break;
				case QueryOptions.Import:
					break;
				case QueryOptions.Invalid:
					break;
				default:
					message = "No action was performed.";
					return false;
			}
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
				foreach(var line in this.Database.Entries) {
					foreach(var entry in line) {
						if(line.Count - 1 != line.IndexOf(entry)) {
							this.MessageBuilder.AppendLine(entry + "|");
						}
						else this.MessageBuilder.AppendLine(entry);
					}
				}
			}
			else {
				int[] columnNums = GetIndexesToWorkOn();
				foreach(var num in columnNums) {
					foreach(var line in this.Database.Entries) {
						foreach(var entry in line) {

							if(entry.IndexOf(entry) == num) {
								if(entry.IndexOf(entry) != entry.Length - 1) {
									this.MessageBuilder.AppendLine(entry + "|");
								}
								else this.MessageBuilder.AppendLine(entry);
							}
						}
					}
				}
			}

			return this.MessageBuilder.ToString();
		}

		private int[] GetIndexesToWorkOn() {
			return this.Database.Headers
					.Where(entry => this.Query.QueryParameters.Any(a => a == entry))
					.Select(r => Array.IndexOf(this.Database.Headers , r))
					.ToArray();
		}
	}
}
