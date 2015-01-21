using System;
using System.Collections.Generic;
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

		public string UpdateEntry(
			string file , int lineIndex ,
			string[] newValues ,
			List<List<string>> entries ,
			string[] pickedHeaders ,
			string[] headers ,
			out List<List<string>> modifiedEntries) {

			modifiedEntries = entries;
//<<<<<<< HEAD

			for(int i = 0 ; i < pickedHeaders.Length ; i++) {
				modifiedEntries[lineIndex][Array.IndexOf(headers , pickedHeaders[i])] = newValues[i];
			}

			using(var sw = new StreamWriter(file , false)) {
				sw.Write(headers.Aggregate(PipeFormat));
				modifiedEntries.ForEach(line => {
					sw.Write(Environment.NewLine + line.Aggregate(PipeFormat).TrimEnd(new char[] { '\r' , '\n' }));
				});
			}
//=======
//			modifiedEntries[lineIndex][headerIndex] = newValue;
//>>>>>>> origin/QueryHandler

			return "The entry has been modified!";
		}
	}
}
