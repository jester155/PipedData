using System.IO;
using System.Linq;

namespace Pipe {
	public class PipeEditor {

		public void AppendEntry(string file , string entry) {
			File.AppendAllText(file , entry);
		}
		public string DeleteEntry(string file , int index) {
			var fileContents = File.ReadAllLines(file).ToList();
			var removedEntry = fileContents[index + 1];
			fileContents.RemoveAt(index + 1);
			using(var sw = new StreamWriter(file , true)) {
				sw.Write(fileContents);
			}

			return removedEntry;
		}
	}
}
