using System;

namespace Pipe.Shell {
	class CommandRepl : IRepl {
		public void Read() {
			string userCommand = Console.ReadLine();
			Evaluate(userCommand);
		}

		public bool Evaluate(string command) {
			throw new NotImplementedException();
		}

		public void Print(string[] args) {
			throw new NotImplementedException();
		}
		public void Loop() {
			throw new NotImplementedException();
		}
	}
}
