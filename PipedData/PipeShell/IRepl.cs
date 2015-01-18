
namespace PipeShell {
	public interface IRepl {
		public void Read(string command);

		public bool Evaluate(string command);
		public void Print();
	}
}
