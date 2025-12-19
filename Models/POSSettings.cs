using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EposNow.Models;
using Newtonsoft.Json;

public class POSSettings
{
	public List<POSSetting> PosDetails { get; set; }

	public void IntializeStoreSettings()
	{
		DataSet dataSet = new DataSet();
		List<POSSetting> list = new List<POSSetting>();
		try
		{
			List<SqlParameter> sparams = new List<SqlParameter>();
			sparams.Add(new SqlParameter("@PosId", 46));

			string connectionString = ConfigurationManager.AppSettings.Get("LiquorAppsConnectionString");
			using (SqlConnection connection = new SqlConnection(connectionString))

			{
				using SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = connection;

				sqlCommand.CommandText = "usp_ts_GetStorePosSetting";
				sqlCommand.Parameters.Add(sparams[0]);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				sqlDataAdapter.Fill(dataSet);
			}
			if (dataSet != null || dataSet.Tables.Count > 0)
			{
				foreach (DataRow row in dataSet.Tables[0].Rows)
				{
					POSSetting pOSSetting = new POSSetting();
					pOSSetting.Setting = row["Settings"].ToString();
					StoreSetting storeSetting = new StoreSetting();
					storeSetting.StoreId = Convert.ToInt32((row["StoreId"] == DBNull.Value) ? ((object)0) : row["StoreId"]);
					storeSetting.POSSettings = JsonConvert.DeserializeObject<Setting>(pOSSetting.Setting);
					pOSSetting.PosName = row["PosName"].ToString();
					pOSSetting.PosId = Convert.ToInt32(row["PosId"]);
					pOSSetting.StoreSettings = storeSetting;
					if (pOSSetting.StoreSettings.POSSettings != null)
					{
						pOSSetting.StoreSettings.POSSettings.categoriess = storeSetting.POSSettings.categoriess;
						pOSSetting.StoreSettings.POSSettings.Upc = storeSetting.POSSettings.Upc;
					}
					list.Add(pOSSetting);
				}
			}
			PosDetails = list;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			Console.Read();
		}
	}
}
