using System.IO;
using System.Linq;

namespace Pipe {
	public class PipeEditor {

		public void AppendEntry(string file , string entry) {
			File.AppendAllText(file , entry);
		}
		public void DeleteEntry(string file , int index) {
			var fileContents = File.ReadAllLines(file).ToList();
			fileContents.RemoveAt(index + 1);
			using(var sw = new StreamWriter(file , true)) {
				sw.Write(fileContents);
			}
		}
	}
}
