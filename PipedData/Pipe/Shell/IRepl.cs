
namespace Pipe.Shell {
	public interface IRepl {

		void Read();
		bool Evaluate(string command);
		void Print(string[] args = null);
		void Loop();
	}
}
