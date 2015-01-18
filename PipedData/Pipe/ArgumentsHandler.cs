using System;

namespace Pipe {
	class ArgumentsHandler {
		public string[] Arguments { get; set; }
		public ArgumentsHandler(string[] args) {
			this.Arguments = args;
		}
		public string PathPrompt() {
			string path = string.Empty;
			Console.WriteLine("Input the path to the file you want to convert...");
			path = Console.ReadLine();
			return path;
		}

	}
}
