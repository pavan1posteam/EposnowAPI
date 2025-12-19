using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using EposNow;
using EposNow.Models;

namespace EposNow
{
	class EposnowCsvProducts
	{
		private string BasePath = ConfigurationManager.AppSettings.Get("BaseDirectory");

		private string beercat = ConfigurationManager.AppSettings.Get("beer_cat");

		public EposnowCsvProducts(int StoreId, decimal tax, string BaseUrl, string Token)
		{
			productForCSV(StoreId, tax, BaseUrl, Token);
		}

		public void productForCSV(int storeid, decimal tax, string BaseUrl, string Token)
		{
			clsEposNow clsEposNow2 = new clsEposNow(storeid, tax, BaseUrl, Token);
			List<EposnowProdList.Root> list = clsEposNow2.EposnowSetting(storeid, tax, BaseUrl, Token);
			List<EposnowStockList.Root> inner = clsEposNow2.EposnowStockSetting(storeid, tax, BaseUrl, Token);
			List<ProductsModel> list2 = new List<ProductsModel>();
			List<FullNameProductModel> list3 = new List<FullNameProductModel>();
			#region testing code
			try
			{
				if (storeid == 12778)
				{

					List<ProductsModel> list4 = (from x in
														(
														  from b in list
														     select new
															{
																storeid = storeid,
																upc = ((b.Barcode == null) ? "" : b.Barcode),
																qty = 999,
																sku = ((b.Barcode == null) ? "" : b.Barcode),
																pack = b.Id,
																StoreProductName = b.Name,
																StoreDescription = b.Name,
																price = b.SalePrice,
																sprice = 0,
																start = "",
																end = "",
																tax = tax,
																altupc1 = "",
																altupc2 = "",
																altupc3 = "",
																altupc4 = "",
																altupc5 = ""
															}).Distinct()
	 												 select new ProductsModel
												 {
													 StoreID = x.storeid,
													 upc = x.upc,
													 Qty = Convert.ToInt64(x.qty),
													 sku = x.sku,
													 pack = 1,
													 StoreProductName = x.StoreProductName,
													 StoreDescription = x.StoreDescription,
													 Price = Convert.ToDecimal(x.price),
													 sprice = 0m,
													 Start = x.start,
													 End = x.end,
													 Tax = x.tax,
													 altupc1 = x.altupc1,
													 altupc2 = x.altupc2,
													 altupc3 = x.altupc3,
													 altupc4 = x.altupc4,
													 altupc5 = x.altupc5
												 }).ToList();

					foreach (ProductsModel item in list4)
					{
						try
						{
							ProductsModel productsModel = new ProductsModel();
							FullNameProductModel fullNameProductModel = new FullNameProductModel();
							productsModel.StoreID = storeid;
							string text = "";
							if (item.upc == "")
							{
								continue;
							}
							text = item.upc.ToString();
							decimal.TryParse(text, NumberStyles.Float, null, out var result);
							text = result.ToString();
							if (text == "" || text == "0")
							{
								productsModel.upc = "";
								fullNameProductModel.upc = "";
							}
							else
							{
								productsModel.upc = "#" + text;
								fullNameProductModel.upc = "#" + text;
								productsModel.sku = "#" + text;
								fullNameProductModel.sku = "#" + text;
							}
							productsModel.Qty = item.Qty;
							productsModel.pack = item.pack;
							productsModel.StoreProductName = item.StoreProductName.ToString();
							productsModel.StoreDescription = item.StoreProductName.ToString();
							if (!(item.Price <= 0m))
							{
								productsModel.Price = Convert.ToDecimal(item.Price);
								fullNameProductModel.Price = Convert.ToDecimal(item.Price);
								productsModel.sprice = item.sprice;
								productsModel.Tax = tax;
								productsModel.altupc1 = "";
								productsModel.altupc2 = "";
								productsModel.altupc3 = "";
								productsModel.altupc4 = "";
								productsModel.altupc4 = "";
								productsModel.altupc5 = "";
								fullNameProductModel.pname = item.StoreProductName.ToString();
								fullNameProductModel.pdesc = item.StoreProductName.ToString();
								fullNameProductModel.pack = 1;
								string text2 = item.StoreDescription.Substring(5, 5);
								fullNameProductModel.pcat2 = "";
								fullNameProductModel.country = "";
								fullNameProductModel.region = "";
								if (!string.IsNullOrEmpty(productsModel.upc) && productsModel.Price > 0m && productsModel.Qty > 0 && text2 != " DAN-")
								{
									list2.Add(productsModel);
									list3.Add(fullNameProductModel);
								}
							}
						}

						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
						}
						finally
						{
						}
					}
				}
				#endregion
				#region  old code converted to else block for other store
				else
				{
					List<ProductsModel> list4 = (from x in (from b in list
															join a in inner on b.Id equals a.ProductId
															select new
															{
																storeid = storeid,
																upc = ((b.Barcode == null) ? "" : b.Barcode),
																qty = ((a.ProductStockBatches.ToList().Count > 0) ? a.ProductStockBatches.ToList().FirstOrDefault().CurrentStock : 0),
																sku = ((b.Barcode == null) ? "" : b.Barcode),
																pack = b.Id,
																StoreProductName = b.Name,
																StoreDescription = b.Name,
																price = b.SalePrice,
																sprice = 0,
																start = "",
																end = "",
																tax = tax,
																altupc1 = "",
																altupc2 = "",
																altupc3 = "",
																altupc4 = "",
																altupc5 = ""
															}).Distinct()
												 select new ProductsModel
												 {
													 StoreID = x.storeid,
													 upc = x.upc,
													 Qty = Convert.ToInt64(x.qty),
													 sku = x.sku,
													 pack = 1,
													 StoreProductName = x.StoreProductName,
													 StoreDescription = x.StoreDescription,
													 Price = Convert.ToDecimal(x.price),
													 sprice = 0m,
													 Start = x.start,
													 End = x.end,
													 Tax = x.tax,
													 altupc1 = x.altupc1,
													 altupc2 = x.altupc2,
													 altupc3 = x.altupc3,
													 altupc4 = x.altupc4,
													 altupc5 = x.altupc5
												 }).ToList();



					foreach (ProductsModel item in list4)
					{
						try
						{
							ProductsModel productsModel = new ProductsModel();
							FullNameProductModel fullNameProductModel = new FullNameProductModel();
							productsModel.StoreID = storeid;
							string text = "";
							if (item.upc == "")
							{
								continue;
							}
							text = item.upc.ToString();
							decimal.TryParse(text, NumberStyles.Float, null, out var result);
							text = result.ToString();
							if (text == "" || text == "0")
							{
								productsModel.upc = "";
								fullNameProductModel.upc = "";
							}
							else
							{
								productsModel.upc = "#" + text;
								fullNameProductModel.upc = "#" + text;
								productsModel.sku = "#" + text;
								fullNameProductModel.sku = "#" + text;
							}
							productsModel.Qty = item.Qty;
							productsModel.pack = item.pack;
							productsModel.StoreProductName = item.StoreProductName.ToString();
							productsModel.StoreDescription = item.StoreProductName.ToString();
							if (!(item.Price <= 0m))
							{
								productsModel.Price = Convert.ToDecimal(item.Price);
								fullNameProductModel.Price = Convert.ToDecimal(item.Price);
								productsModel.sprice = item.sprice;
								productsModel.Tax = tax;
								productsModel.altupc1 = "";
								productsModel.altupc2 = "";
								productsModel.altupc3 = "";
								productsModel.altupc4 = "";
								productsModel.altupc4 = "";
								productsModel.altupc5 = "";
								fullNameProductModel.pname = item.StoreProductName.ToString();
								fullNameProductModel.pdesc = item.StoreProductName.ToString();
								fullNameProductModel.pack = 1;
								string text2 = item.StoreDescription.Substring(5, 5);
								fullNameProductModel.pcat2 = "";
								fullNameProductModel.country = "";
								fullNameProductModel.region = "";
								if (!string.IsNullOrEmpty(productsModel.upc) && productsModel.Price > 0m && productsModel.Qty > 0 && text2 != " DAN-")
								{
									list2.Add(productsModel);
									list3.Add(fullNameProductModel);
								}
							}
						}

						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
						}
						finally
						{
						}
					}
				}
				#endregion


				if (beercat.Contains(storeid.ToString()))
				{
					List<ProductsModel> collection = (from b in list
													  where b.SalePrice > 0.0
													  where b.CategoryId == 565448
													  select new ProductsModel
													  {
														  StoreID = storeid,
														  upc = ((b.Barcode == null) ? "" : ("#" + b.Barcode)),
														  Qty = 999L,
														  sku = ((b.Barcode == null) ? "" : ("#" + b.Barcode)),
														  pack = 1,
														  StoreProductName = b.Name,
														  StoreDescription = b.Name,
														  Price = (decimal)b.SalePrice,
														  sprice = 0m,
														  Start = "",
														  End = "",
														  Tax = tax,
														  altupc1 = "",
														  altupc2 = "",
														  altupc3 = "",
														  altupc4 = "",
														  altupc5 = ""
													  }).ToList();
					list2.AddRange(collection);
					list2 = (from x in list2.AsEnumerable()
							 group x by x.upc into y
							 select y.First()).ToList();
				}

			}
			catch (Exception ex2)
			{
				Console.WriteLine(ex2.Message);
			}
			finally
			{
			}

						GenerateCSV.GenerateCSVFile(list2, "PRODUCT", storeid, BasePath);
						GenerateCSV.GenerateCSVFile(list3, "FULLNAME", storeid, BasePath);
						Console.WriteLine();
						Console.WriteLine("Product FIle Generated For EposNow " + storeid);
						Console.WriteLine("Fullname FIle Generated For EposNow " + storeid);
	  }
   }
}
