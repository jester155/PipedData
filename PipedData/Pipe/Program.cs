using Pipe.Query;
using System;

namespace Pipe {
	class Program {
		static void Main(string[] args) {
			string message = string.Empty;

			//var q = "select first,last where id is 5";
			var q = "update first where id is 3 to alex";

			var qf = new QueryFactory(q);
			var df = new DatabaseFactory("Test.psv");

			var query = qf.MakeQuery();

			var d = df.MakeDatabase();
			var qi = new QueryInterpreter(d , query);


			qi.PerformQueryAction(out message);
			Console.WriteLine(message);
		}
	}
}
