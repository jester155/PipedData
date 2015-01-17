using System;
using System.Collections.Generic;
using System.IO;

namespace Pipe {
	class FileConverter {
		public string Path { get; set; }

		public static Dictionary<string, char> AcceptedMimes = new Dictionary<string, char>() {
			{".csv", ','}, {".tab",'\t'}
		};

		public void ConvertToPsv(string path) {
			this.Path = path;
			var psvPath = new FileInfo(this.Path).Directory +
				"\\" +
				System.IO.Path.GetFileNameWithoutExtension(this.Path) + ".psv";

			var writer = File.Exists(psvPath) ? new StreamWriter(psvPath, true) : new StreamWriter(psvPath);

			string extention = new FileInfo(path).Extension;

			using (writer)
			using (var reader = new StreamReader(Path)) {
				bool foundSupportedExtension = false;
				foreach (var key in AcceptedMimes.Keys) {

					var line = string.Empty;
					if (key == extention) {
						foundSupportedExtension = true;
						while ((line = reader.ReadLine()) != null) {
							line = line.Replace(AcceptedMimes[key], '|');
							writer.WriteLine(line);
						}
						break;
					}
				}

				if (!foundSupportedExtension) {
					Console.WriteLine("Unsupported File Type\nSupported types are: .csv and .tab");
				}
			}
		}
	}
}
