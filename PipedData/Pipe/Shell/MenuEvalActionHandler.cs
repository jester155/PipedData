using System;

namespace Pipe.Shell {
	class MenuEvalActionHandler {
		public string PathPrompt() {
			string path = string.Empty;
			Console.WriteLine("Input the path to the file you want to convert...");
			path = Console.ReadLine();
			return path;
		}
	}
}
