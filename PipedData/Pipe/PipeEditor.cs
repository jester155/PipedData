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

		public void OverwriteDataDile(string dataFile , string[] headers , List<List<string>> entries) {
			using(var sw = new StreamWriter(dataFile)) {
				sw.Write(headers.Aggregate(PipeFormat));
				entries.ForEach(line => sw.Write(Environment.NewLine + line.Aggregate(PipeFormat)));
			}
		}

		public string UpdateEntry(
			string file , int lineIndex ,
			int headerIndex , string newValue ,
			List<List<string>> entries ,
			out List<List<string>> modifiedEntries) {

			modifiedEntries = entries;
			modifiedEntries[lineIndex][headerIndex] = newValue;

			return "The entry has been modified!";
		}
	}
}
