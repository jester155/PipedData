
using System.Collections.Generic;
namespace Pipe {
	class Database {
		public string DataFile { get; set; }
		public string[] Headers { get; set; }
		public List<List<string>> Entries { get; set; }
	}
}
