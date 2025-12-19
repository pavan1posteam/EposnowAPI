using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using EposNow;
using EposNow.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

public class clsEposNow
{
	private string StoreId;

	private int page = 1;

	private string AccessToken = "";

	public clsEposNow(int StoreId, decimal tax, string BaseUrl, string RefreshToken)
	{
		try
		{
			Console.WriteLine("Generating EposNow " + StoreId + " Product File....");
			Console.WriteLine("Generating EposNow " + StoreId + " Fullname File....");
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message + " EposNow " + StoreId);
		}
	}

	public List<EposnowProdList.Root> EposnowSetting(int StoreId, decimal tax, string BaseUrl, string Token)
	{
		List<EposnowProdList.Root> list = new List<EposnowProdList.Root>();
		for (int i = 1; i <= 25; i++)
		{
			List<EposnowProdList.Root> list2 = EposNowProduct(i, StoreId, tax, BaseUrl, Token);
			if (list2.Count != 0)
			{
				list.AddRange(list2);
				continue;
			}
			break;
		}
		return list;
	}

	public List<EposnowStockList.Root> EposnowStockSetting(int StoreId, decimal tax, string BaseUrl, string Token)
	{
		List<EposnowStockList.Root> list = new List<EposnowStockList.Root>();
		for (int i = 1; i <= 25; i++)
		{
			List<EposnowStockList.Root> list2 = EposNowStock(i, StoreId, tax, BaseUrl, Token);
			if (list2.Count != 0)
			{
				list.AddRange(list2);
				continue;
			}
			break;
		}
		return list;
	}

	public List<EposnowProdList.Root> EposNowProduct(int PageNo, int StoreId, decimal tax, string BaseUrl, string Token)
	{
		List<EposnowProdList.Root> result = new List<EposnowProdList.Root>();
		string text = null;
		EposnowProdList.Root root = new EposnowProdList.Root();
		RestClient restClient = new RestClient(BaseUrl + "Product/?page=" + PageNo + "&limit=200");
		RestRequest restRequest = new RestRequest(Method.GET);
		restRequest.AddHeader("Authorization", Token);
		restRequest.AddHeader("Content-Type", "application/json");
		ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		IRestResponse restResponse = restClient.Execute(restRequest);
		List<Parameter> list = restResponse.Headers.ToList();
		if (restResponse.StatusCode == HttpStatusCode.OK)
		{
			try
			{
				text = restResponse.Content;
				List<EposnowProdList.Root> source = JsonConvert.DeserializeObject<List<EposnowProdList.Root>>(text, new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				});
				result = source.ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		return result;
	}

	public List<EposnowStockList.Root> EposNowStock(int PageNo, int StoreId, decimal tax, string BaseUrl, string Token)
	{
		List<JArray> list = new List<JArray>();
		List<EposnowStockList.Root> result = new List<EposnowStockList.Root>();
		string text = null;
		Root root = new Root();
		RestClient restClient = new RestClient(BaseUrl + "ProductStock?page=" + PageNo + "&limit=200");
		restClient.Timeout = -1;
		RestRequest restRequest = new RestRequest(Method.GET);
		restRequest.AddHeader("Authorization", Token);
		restRequest.AddHeader("Content-Type", "application/json");
		ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		IRestResponse restResponse = restClient.Execute(restRequest);
		List<Parameter> list2 = restResponse.Headers.ToList();
		if (restResponse.StatusCode == HttpStatusCode.OK)
		{
			try
			{
				text = restResponse.Content;
				List<EposnowStockList.Root> source = JsonConvert.DeserializeObject<List<EposnowStockList.Root>>(text, new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				});
				result = source.ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		return result;
	}
}
