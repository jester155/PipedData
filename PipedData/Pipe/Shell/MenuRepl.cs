using System;

namespace Pipe.Shell {
	class MenuRepl : IRepl {
		Menu menu = new Menu();

		public void Read(string command) {
			string userInput = Console.ReadLine();
		}

		public bool Evaluate(string command) {
			MenuOptions selection = menu.MenuInteraction(command);
			switch (selection) {
				case MenuOptions.Menu:
					menu.PresentMenu();
					return true;

				case MenuOptions.Start:
					var comm = new CommandRepl();
					comm.Loop();
					return true;

				case MenuOptions.FileConverter:
					var fConvert = new FileConverter();
					var argHandeler = new ArgumentsHandler(new string[] { });
					string path = argHandeler.PathPrompt();
					fConvert.ConvertToPsv(path);
					return true;

				case MenuOptions.Help:
					//TODO:
					break;
				case MenuOptions.Exit:
					System.Environment.Exit(0);
					break;
				default:
					break;
			}
			return false;
		}

		public void Print(string[] args = null) {
			int i = 0;
			while (args.Length > 0) {
				Console.WriteLine("{0}\n", args[i]);
				i++;
			}
		}
		public void Loop() {
			throw new NotImplementedException();
		}
	}
}
