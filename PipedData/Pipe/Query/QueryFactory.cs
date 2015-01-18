using System;
using System.Collections.Generic;
using System.Linq;

namespace Pipe.Query {
	//.						QueryOption   Filter   FilterType 
	//. Query string example: Select *    Where first_name is mark


	public class QueryFactory {

		public string RawQuery { get; set; }
		public string[] QueryArray { get; set; }

		public QueryFactory(string fullQuery) {
			this.RawQuery = fullQuery.ToLower();
			this.QueryArray = GetQueryArray();
		}

		public Query MakeQuery() {
			return new Query(CreateFilter()) {
				QueryOption = GetQueryOption() ,
				HasFilter = HasFilter() ,
				QueryParameters = GetQueryParams()
			};
		}

		private QueryOptions GetQueryOption() {

			switch(this.QueryArray[0]) {
				case "select":
					return QueryOptions.Select;
				case "Create":
					return QueryOptions.Create;
				case "insert":
					return QueryOptions.Insert;
				case "update":
					return QueryOptions.Update;
				case "delete":
					return QueryOptions.Delete;
				case "use":
					return QueryOptions.Use;
				case "set":
					return QueryOptions.Set;
				case "import":
					return QueryOptions.Import;
				default:
					return QueryOptions.Invalid;
			}
		}

		private FileterOptions GetFilterOptions(List<string> filterData) {

			string filterParam = filterData.Contains("is") ?
				filterData[filterData.FindIndex(q => q == "is")] : filterData[filterData.FindIndex(q => q == "partof")];

			switch(filterParam) {
				case "is":
					return FileterOptions.Is;
				case "partof":
					return FileterOptions.PartOf;
				default:
					return FileterOptions.None;
			}
		}

		private bool HasFilter() {
			return this.RawQuery.Split(' ').Any(a => a == "where");
		}

		private string[] GetQueryArray() {
			return this.RawQuery.Split(' ');
		}

		private Filter CreateFilter() {
			Filter filter = new Filter();

			var filterData = new List<string>();

			var index = Array.FindIndex(this.QueryArray , idx => idx == "where");
			var length = this.QueryArray.Length - index;

			for(int i = index ; i < length ; i++) filterData.Add(this.QueryArray[i]);

			filter.FilterColumn = filterData[1];
			filter.FilterValue = filterData[3];
			filter.FilterOption = GetFilterOptions(filterData);

			return filter;
		}

		private string[] GetQueryParams() {
			return this.QueryArray[1].Split(',');
		}
	}
}
