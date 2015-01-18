
namespace Pipe.Query {
	public enum QueryOptions {
		Create ,	//. Creates a new database file.
		Select ,	//. Selects a header to read information from
		Insert ,	//. Inserts a new record "row" into the database
		Update ,	//. Updates a current record "row" in the database
		Delete ,	//. Deletes an existing row in the database.
		Use ,		//. Selects the database to use.
		Set ,		//. Sets the database directory to use.
		Import ,	//. Imports an existing data file into memory.
		Invalid		//. Returns if a query option could not be found.
	}
}
