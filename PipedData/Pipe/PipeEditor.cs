﻿using System.IO;
using System.Linq;

namespace Pipe {
	public class PipeEditor {

		public void AppendEntry(string file , string entry) {
			File.AppendAllText(file , entry);
		}
		public string DeleteEntry(string file , int index) {
			var fileContents = File.ReadAllLines(file).ToList();
			var removedEntry = fileContents[index];
			fileContents.RemoveAt(index);
			using(var sw = new StreamWriter(file , false)) {
				foreach(var item in fileContents) {
					sw.WriteLine(item);
				}
			}

			return removedEntry;
		}
	}
}
