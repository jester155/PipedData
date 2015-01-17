using System;

namespace Pipe.Shell {
	class Menu {
		bool IsNewSession = true;

		public void PresentInitialMenu() {
			if (IsNewSession) {
				Console.WriteLine("{0}", "Author: We Know Work\tDate: 01.16.2015\nDescription: Basic CRUD database manipulator \n");
			}

			Console.WriteLine("{1}\n{2}\n{3}\n{4}\n{5}\n",
				"1 : Menu",
				"2 : Start",
				"3 : FileConverter",
				"4 : Help",
				"5 : Exit"
				);
			IsNewSession = false;
		}

		public void MenuInteraction(MenuOptions option) {
			int userSelection = Console.Read();
			switch (userSelection) {
				case 1:
					break;
				case 2:
					break;
				case 3:
					break;
				case 4:
					break;
				case 5:
					break;
				default:
					Console.WriteLine("Invalid Input. Try Again.");
					break;
			}
		}
	}
}
