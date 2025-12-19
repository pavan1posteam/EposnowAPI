using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

public class GenerateCSV
{
	public static string GenerateCSVFile(DataTable dt, string Name, int StoreId)
	{
		StringBuilder stringBuilder = new StringBuilder();
		try
		{
			int num = 1;
			int count = dt.Columns.Count;
			foreach (DataColumn column in dt.Columns)
			{
				stringBuilder.Append(column.ColumnName);
				if (num != count)
				{
					stringBuilder.Append(",");
				}
				num++;
			}
			stringBuilder.AppendLine();
			string empty = string.Empty;
			foreach (DataRow row in dt.Rows)
			{
				for (int i = 0; i < count; i++)
				{
					empty = row[i].ToString();
					if (empty.Contains(",") || empty.Contains("\""))
					{
						empty = "\"" + empty.Replace("\"", "\"\"") + "\"";
					}
					stringBuilder.Append(empty);
					if (i != count - 1)
					{
						stringBuilder.Append(",");
					}
				}
				stringBuilder.AppendLine();
			}
			if (!Directory.Exists("Upload\\"))
			{
				Directory.CreateDirectory("Upload\\");
			}
			string text = Name + StoreId + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
			File.WriteAllText("Upload\\" + text, stringBuilder.ToString());
			return text;
		}
		catch (Exception)
		{
		}
		return "";
	}

	public static string GenerateCSVFile<T>(IList<T> list, string Name, int StoreId, string BaseUrl)
	{
		if (list == null || list.Count == 0)
		{
			return "";
		}
		if (!Directory.Exists(BaseUrl + "\\" + StoreId + "\\Upload\\"))
		{
			Directory.CreateDirectory(BaseUrl + "\\" + StoreId + "\\Upload\\");
		}
		string text = Name + StoreId + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
		string path = BaseUrl + "\\" + StoreId + "\\Upload\\" + text;
		Type type = list[0].GetType();
		string newLine = Environment.NewLine;
		using StreamWriter streamWriter = new StreamWriter(path);
		object obj = Activator.CreateInstance(type);
		PropertyInfo[] properties = obj.GetType().GetProperties();
		PropertyInfo[] array = properties;
		foreach (PropertyInfo propertyInfo in array)
		{
			streamWriter.Write(propertyInfo.Name + ",");
		}
		streamWriter.Write(newLine);
		foreach (T item in list)
		{
			PropertyInfo[] array2 = properties;
			foreach (PropertyInfo propertyInfo2 in array2)
			{
				string value = Convert.ToString(item.GetType().GetProperty(propertyInfo2.Name).GetValue(item, null)).Replace(',', ' ') + ",";
				streamWriter.Write(value);
			}
			streamWriter.Write(newLine);
		}
		return text;
	}

	public static void ToCSV(DataTable dtDataTable, string strFilePath)
	{
		StreamWriter streamWriter = new StreamWriter(strFilePath, append: false);
		for (int i = 0; i < dtDataTable.Columns.Count; i++)
		{
			streamWriter.Write(dtDataTable.Columns[i]);
			if (i < dtDataTable.Columns.Count - 1)
			{
				streamWriter.Write(",");
			}
		}
		streamWriter.Write(streamWriter.NewLine);
		foreach (DataRow row in dtDataTable.Rows)
		{
			for (int j = 0; j < dtDataTable.Columns.Count; j++)
			{
				if (!Convert.IsDBNull(row[j]))
				{
					string text = row[j].ToString();
					if (text.Contains(','))
					{
						text = $"\"{text}\"";
						streamWriter.Write(text);
					}
					else
					{
						streamWriter.Write(row[j].ToString());
					}
				}
				if (j < dtDataTable.Columns.Count - 1)
				{
					streamWriter.Write(",");
				}
			}
			streamWriter.Write(streamWriter.NewLine);
		}
		streamWriter.Close();
	}
}
