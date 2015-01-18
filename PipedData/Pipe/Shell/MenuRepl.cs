using System;

namespace Pipe.Shell {
	class MenuRepl : IRepl {
		Menu menu = new Menu();
		string[] message = new string[1];

		public void Read() {
			string userInput = Console.ReadLine();
			this.Evaluate(userInput);
		}

		public bool Evaluate(string command) {
			MenuOptions selection = menu.MenuInteraction(command);
			switch (selection) {
				case MenuOptions.Menu:
					menu.PresentMenu();
					this.Loop();
					return true;

				case MenuOptions.Start:
					var comm = new CommandRepl();
					comm.Loop();
					return true;

				case MenuOptions.FileConverter:
					var fConvert = new FileConverter();
					var evalHandeler = new MenuEvalActionHandler();
					string path = evalHandeler.PathPrompt();
					fConvert.ConvertToPsv(path);
					Console.WriteLine("Conversion Successful");
					this.Loop();
					return true;

				case MenuOptions.Help:
					//TODO:
					this.Loop();
					break;
				case MenuOptions.Exit:
					message.SetValue("Quitting....", 1);
					Print(message);
					System.Environment.Exit(0);
					break;
				default:
					message.SetValue("No Action Performed", 1);
					Print(message);
					this.Loop();
					break;
			}
			return false;
		}

		public void Print(string[] args = null) {
			int i = 0;
			while (args.Length > 0) {
				Console.WriteLine("------------------------------------------------------");
				Console.WriteLine("{0}\n", args[i]);
				i++;
			}
		}
		public void Loop() {
			this.Read();
		}
	}
}
