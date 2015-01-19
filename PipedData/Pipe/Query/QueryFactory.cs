using System;
using System.Collections.Generic;
using System.Linq;

namespace Pipe.Query {
	//.						QueryOption   Filter   FilterType 
	//. Query string example: Select *    Where first_name is mark


	public class QueryFactory {

		public string RawQuery { get; set; }
		public string[] QueryArray { get; set; }

		public QueryFactory(string fullQueryString) {
			this.RawQuery = fullQueryString.ToLower();
			this.QueryArray = GetQueryArray();
		}

		public Query MakeQuery() {
			var query = new Query() {
				QueryOption = GetQueryOption() ,
				HasFilter = HasFilter() ,
				QueryParameters = GetQueryParams() ,
				IsUpdateQuery = GetIsUpdateQuery() ,
				UpdateParameters = GetUpdateParams()
			};

			if(query.HasFilter) {
				query.Filter = CreateFilter();
			}
			else query.Filter = new Filter() { FilterColumn = null , FilterValue = null , FilterOption = FileterOptions.None };

			return query;
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
			var length = GetIsUpdateQuery() ?
				Array.FindIndex(this.QueryArray , q => q == "to") - index : this.QueryArray.Length - index;

			for(int i = index ; i < length + index ; i++) filterData.Add(this.QueryArray[i]);

			filter.FilterColumn = filterData[1];
			filter.FilterValue = filterData[3];
			filter.FilterOption = GetFilterOptions(filterData);

			return filter;
		}

		private string[] GetQueryParams() {
			return this.QueryArray[1].Split(',');
		}

		private bool GetIsUpdateQuery() {
			return this.QueryArray.Any(q => q == "to");
		}
		private string[] GetUpdateParams() {
			return GetIsUpdateQuery() ?
				this.QueryArray[this.QueryArray.Length - 1].Split(',') : null;
		}
	}
}
