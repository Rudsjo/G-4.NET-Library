/// <summary>
/// Converts a collection into a csv file
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="Items"></param>
/// <returns></returns>
public List<string> ToCSV<T>(List<T> Items) where T : class
{
	// Result list
	List<string> Result = new List<string>();

	// Create the headers
	PropertyInfo[] Info = typeof(T).GetProperties();

	// Get the headers
	string Header = String.Empty;
	Info.ToList().ForEach(i =>
	{
		// Append the property name
		Header += ((Info.ToList().IndexOf(i) < Info.Length - 1) ? i.Name + "," : i.Name);
	});
	// Add the header
	Result.Add(Header);

	// Loop through each item in the collection
	foreach(var Item in Items)
	{
		// Create the row
		string Row = String.Empty;

		// Get info from the item
		PropertyInfo[] inf = Item.GetType().GetProperties();
		// Loop through all properties
		for (int i = 0; i < inf.Length; i++)
			// Add the data to the row
			Row += (i < inf.Length - 1) ? $"\"{inf[i].GetValue(Item)}\"," : $"\"{inf[i].GetValue(Item)}\"";

		// Add the row to the CSV
		Result.Add(Row);
	}

	// Return the result
	return Result;
}