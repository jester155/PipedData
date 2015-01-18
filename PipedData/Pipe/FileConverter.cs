using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipe {
	class FileConverter {
		public static Dictionary<string, char> Mime = new Dictionary<string, char>() {
			{".csv", ','}, {".psv",'|'}, {".tab",'\t'}
		};
	}
}
