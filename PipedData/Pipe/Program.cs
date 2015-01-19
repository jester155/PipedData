using Pipe.Query;
using System;

namespace Pipe {
	class Program {
		static void Main(string[] args) {
			string message = string.Empty;

			var q = "select *";
			var q1 = "insert 6,britt,mathis,753";

			var qf = new QueryFactory(q);
			var df = new DatabaseFactory("Test.psv");

			var query = qf.MakeQuery();

			var d = df.MakeDatabase();
			var qi = new QueryInterpreter(d , query);


			qi.PerformQueryAction(out message);
			Console.WriteLine(message);

			query = qf.MakeQuery(q1);
			qi = new QueryInterpreter(d , query);
			qi.PerformQueryAction(out message);
			Console.WriteLine(message);

			query = qf.MakeQuery(q);
			qi = new QueryInterpreter(d , query);
			qi.PerformQueryAction(out message);
			Console.WriteLine(message);
		}
	}
}
