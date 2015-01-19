using System;
using System.IO;
using System.Linq;

namespace Pipe {
	public class PipeEditor {

		public static Func<string , string , string> PipeFormat = (c , n) => string.Format("{0}|{1}" , c , n);

		public string CreateDataFile(string file , string[] header) {
			using(var sw = new StreamWriter(file)) {

				sw.Write(header.Aggregate(PipeFormat));
			}

			return string.Format("Created datafile @ {0}\nheader: {1}" ,
				file , header.Aggregate(PipeFormat));
		}

		public void AppendEntry(string file , string entry) {
			using(var sw = new StreamWriter(file , true)) {
				sw.Write(Environment.NewLine + entry);
			}
		}

		public string DeleteEntry(string file , int index) {
			var fileContents = File.ReadAllLines(file).ToList();
			var removedEntry = fileContents[index];
			fileContents.RemoveAt(index);
			using(var sw = new StreamWriter(file , false)) {
				fileContents.ForEach(item => sw.Write(item));
			}

			return removedEntry;
		}
	}
}
