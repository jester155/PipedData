using Pipe.Query;
using System;

namespace Pipe {
	class Program {
		static void Main(string[] args) {
			var q = "delete where nums is 123";
			var qf = new QueryFactory(q);
			var df = new DatabaseFactory("Test.psv");
			var d = df.MakeDatabase();
			var query = qf.MakeQuery();
			var qi = new QueryInterpreter(d , query);
			string message = string.Empty;
			qi.PerformQueryAction(out message);
			Console.WriteLine(message);
		}
	}
}
