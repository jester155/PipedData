using System;

namespace Pipe.Extensions {
	public static class StringExtensions {
		public static bool EqualsIgnoreCase(this string s , string compare) {
			return string.Equals(s , compare , StringComparison.OrdinalIgnoreCase);
		}
	}
}
