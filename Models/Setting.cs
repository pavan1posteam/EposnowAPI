using System.Collections.Generic;
using EposNow.Models;

public class Setting
{
	public string ClientId { get; set; }

	public string ClientSecret { get; set; }

	public int AccountID { get; set; }

	public string RefreshToken { get; set; }

	public string Token { get; set; }

	public string merchantId { get; set; }

	public string Store_id { get; set; }

	public string I_Storeid { get; set; }

	public string Username { get; set; }

	public string Password { get; set; }

	public string Pin { get; set; }

	public int SHOPID { get; set; }

	public string Code { get; set; }

	public string tokenid { get; set; }

	public string instock { get; set; }

	public string category { get; set; }

	public string BaseUrl { get; set; }

	public decimal tax { get; set; }

	public decimal mixtax { get; set; }

	public string PosFileName { get; set; }

	public string PosFileName2 { get; set; }

	public string APIKey { get; set; }

	public int StoreMapId { get; set; }

	public decimal liquortax { get; set; }

	public decimal liquortaxrateperlitre { get; set; }

	public List<categories> categoriess { get; set; }

	public string LocationId { get; set; }

	public bool IsSalePrice { get; set; }

	public bool IsMarkUpPrice { get; set; }

	public int MarkUpValue { get; set; }

	public bool IsApi { get; set; }

	public List<UPC> Upc { get; set; }

	public decimal beertax { get; set; }

	public decimal winetax { get; set; }

	public int client_id { get; set; }
}
