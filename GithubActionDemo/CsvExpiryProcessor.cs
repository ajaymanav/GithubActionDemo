using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;

public class CsvExpiryProcessor
{
	private const string CsvPath = "data.csv";
	private const string OutputCsvPath = "data_updated.csv";

	public void Process()
	{
		var updatedLines = new List<string>();
		var lines = File.ReadAllLines(CsvPath);

		updatedLines.Add(lines[0]); // add header

		for (int i = 1; i < lines.Length; i++)
		{
			var line = lines[i];
			var parts = line.Split(',');

			if (parts.Length != 2)
				continue;

			var name = parts[0].Trim();
			if (!DateTime.TryParse(parts[1], out DateTime expiryDate))
				continue;

			if ((expiryDate - DateTime.Today).TotalDays <= 30)
			{
				expiryDate = expiryDate.AddYears(1);
				Console.WriteLine($"Extended expiry for {name} to {expiryDate:yyyy-MM-dd}");
			}

			updatedLines.Add($"{name},{expiryDate:yyyy-MM-dd}");
		}

		File.WriteAllLines(OutputCsvPath, updatedLines);
		Console.WriteLine("Updated CSV written to data_updated.csv");
	}
}
