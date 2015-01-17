using System;

namespace Pipe.Shell {
	class Menu {
		bool IsNewSession = true;
		private string[] MenuPrint = { "Menu", "Start", "FileConverter", "Help", "Exit" };

		public void PresentInitialMenu() {
			if (IsNewSession) {
				Console.WriteLine("{0}", "Author: We Know Work\tDate: 01.16.2015\nDescription: Basic CRUD database manipulator \n");
			}

			Console.WriteLine("{1}\n{2}\n{3}\n{4}\n{5}\n",
				"1 : Menu",
				"2 : Start",
				"3 : File Converter",
				"4 : Help",
				"5 : Exit"
				);
			IsNewSession = false;
		}

		public void MenuInteraction(MenuOptions option) {
			string userSelection = Console.ReadLine();
		}

		private MenuOptions GetMenuOption(string option) {

			int selection = -1;

			if (!int.TryParse(option, out selection))
				selection = Array.FindIndex(MenuPrint, o => o == option.Replace(" ", string.Empty));
			else selection -= 1;


			return (MenuOptions)selection;
		}
	}
}
