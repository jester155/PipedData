using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pipe {
	class DatabaseFactory {

		public string Datafile { get; set; }
		private StreamReader DatabaseReader { get; set; }
		const char DELIN = '|';

		public DatabaseFactory(string dataFile) {
			this.Datafile = dataFile;
			this.DatabaseReader = new StreamReader(this.Datafile);
		}

		public Database MakeDatabase() {
			var database = new Database() {
				DataFile = this.Datafile ,
				Headers = GetDatabaseHeaders() ,
				Entries = GetDatbaseEntries()
			};

			this.DatabaseReader.Close();

			return database;
		}

		public string[] GetDatabaseHeaders() {
			return SplitOnPipe(this.DatabaseReader.ReadLine());
		}

		public List<List<string>> GetDatbaseEntries() {
			return this.DatabaseReader.ReadToEnd()
				.Split(new string[] { Environment.NewLine } , StringSplitOptions.None)
				.Select(line => SplitOnPipe(line).ToList())
				.ToList();
		}

		private string[] SplitOnPipe(string line) {
			return line.Split(DELIN);
		}
	}
}
