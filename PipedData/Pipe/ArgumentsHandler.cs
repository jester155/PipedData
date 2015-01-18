using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipe {
	class ArgumentsHandler {
		public string[] Arguments { get; set; }
		public ArgumentsHandler(string[] args) {
			this.Arguments = args;
        }
	}
}
