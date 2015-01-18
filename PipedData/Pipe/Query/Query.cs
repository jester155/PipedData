
namespace Pipe.Query {

	public struct Filter {
		public string FilterColumn { get; set; }
		public string FilterValue { get; set; }
		public FileterOptions FilterOption { get; set; }
	}

	public class Query {

		public Query() { }
		public Query(Filter filter) {
			this.Filter = filter;
		}

		public string[] UpdateParameters { get; set; }
		public QueryOptions QueryOption { get; set; }
		public Filter Filter { get; set; }
		public string[] QueryParameters { get; set; }
		public bool HasFilter { get; set; }
		public bool IsUpdateQuery { get; set; }
	}
}
