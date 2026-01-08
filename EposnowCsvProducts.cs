using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using EposNow;
using EposNow.Models;

namespace EposNow
{
    class EposnowCsvProducts
    {
        private string BasePath = ConfigurationManager.AppSettings.Get("BaseDirectory");

        private string beercat = ConfigurationManager.AppSettings.Get("beer_cat");

        string Staticqty = ConfigurationManager.AppSettings.Get("Staticqty");
        string beerdeposit = ConfigurationManager.AppSettings.Get("Beerdeposit");

        public EposnowCsvProducts(int StoreId, decimal tax, string BaseUrl, string Token)
        {
            productForCSV(StoreId, tax, BaseUrl, Token);
        }

        public void productForCSV(int storeid, decimal tax, string BaseUrl, string Token)
        {
            try
            {


                clsEposNow clsEposNow2 = new clsEposNow(storeid, tax, BaseUrl, Token);
                List<EposnowProdList.Root> productList = clsEposNow2.EposnowSetting(storeid, tax, BaseUrl, Token);
                List<EposnowStockList.Root> stocklist = clsEposNow2.EposnowStockSetting(storeid, tax, BaseUrl, Token);
                //for categories 
                List<CatList> catlist = clsEposNow2.EposnowCatsSetting(storeid, tax, BaseUrl, Token);

                List<ProductsModel> list2 = new List<ProductsModel>();
                List<FullNameProductModel> list3 = new List<FullNameProductModel>();
                List<ProductsModel> list4 = new List<ProductsModel>();


                #region old object code 
                /* list4 = (from x in
                                            (
                                                        from b in productList
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
                                                //overiding the pack=b.id with pack = 1  
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
                                               }).ToList(); */
                #endregion

                foreach (EposnowProdList.Root prd in productList)
                {
                    try
                    {

                       // ProductsModel productsModel = new ProductsModel();
                        ProductsModel pdm = new ProductsModel();
                      //  FullNameProductModel fullNameProductModel = new FullNameProductModel();
                        FullNameProductModel fnm = new FullNameProductModel();
                        pdm.StoreID = storeid;
                        string text = "";
                        /* if (prd.Barcode == "")
                         {
                             continue;
                         }*/
                        if (string.IsNullOrWhiteSpace(prd.Barcode))
                        {
                            continue;
                        }

                        text = prd.Barcode.ToString();
                        decimal.TryParse(text, NumberStyles.Float, null, out var result);
                        text = result.ToString();
                        if (text == "" || text == "0")
                        {
                            pdm.upc = "";
                            fnm.upc = "";
                        }
                        else
                        {
                            pdm.upc = "#" + text;
                            fnm.upc = "#" + text;
                            pdm.sku = "#" + text;
                            fnm.sku = "#" + text;
                        }
                        var qty = stocklist.FirstOrDefault(s => s.ProductId == prd.Id)?.ProductStockBatches?.FirstOrDefault()?.CurrentStock??0  ;
                        /* long qty = 0;
                         foreach (var stock in stocklist)
                         {
                             if (stock.ProductId == prd.Id)
                             {
                                 if (stock.ProductStockBatches != null &&
                                     stock.ProductStockBatches.Count > 0)
                                 {
                                     qty = stock.ProductStockBatches[0].CurrentStock;
                                 }
                                 break;
                             }
                         }*/
                        pdm.Qty = qty;
                        if (Staticqty.Contains(storeid.ToString()))
                        {
                            pdm.Qty = 999;   //999;
                        }
                        pdm.pack = getpack(prd.Name) ;
                        pdm.uom = GetVolume(prd.Name) ;
                        pdm.StoreProductName = prd.Name.ToString();
                        pdm.StoreDescription = prd.Name.ToString();
                        if (!(prd.SalePrice <= 0))
                        {
                            pdm.Price = Convert.ToDecimal(prd.SalePrice);
                            fnm.Price = pdm.Price ;
                            pdm.sprice = 0;   //prd.sprice;
                            pdm.Tax = tax;

                            pdm.altupc1 = "";
                            pdm.altupc2 = "";
                            pdm.altupc3 = "";
                            pdm.altupc4 = "";
                            pdm.altupc4 = "";
                            pdm.altupc5 = "";
                            fnm.pname = pdm.StoreProductName.ToString();
                            fnm.pdesc = pdm.StoreDescription.ToString();
                            fnm.pack = pdm.pack;
                            fnm.uom = pdm.uom;
                            // string text2 = prd.StoreDescription.Substring(5, 5); 
                            var cat = catlist.FirstOrDefault(c => c.Id == prd.CategoryId)?.Name??"" ;
                            var cat1 =catlist.FirstOrDefault(c=>c.Id==prd.CategoryId)?.Children?.FirstOrDefault()?.Name??""  ;
                            /*string cat = "";
                            string cat1= "";
                            foreach (CatList cats in catlist)
                            {
                             if (cats.Id == prd.CategoryId)
                             {
                               cat = cats.Name;
                                 if (cats.Children != null &&
                                     cats.Children.Count > 0)
                                 {
                                        cat1 = cats.Children[0].Name;
                                 }
                               break;
                             }

                            }*/

                            fnm.pcat = cat ;
                            fnm.pcat1 = cat1;
                            fnm.pcat2 = "";
                            fnm.country = "";
                            fnm.region = "";
                            if (!string.IsNullOrEmpty(pdm.upc) && pdm.Price > 0m && pdm.Qty > 0)
                            {
                                list2.Add(pdm);
                                list3.Add(fnm);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                #region  old code converted to else block for other store
                /*   else
                    {
                        list4 = (from x in (from b in productList
                                            join a in stocklist on b.Id equals a.ProductId
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
                                    fullNameProductModel.pcat = "";
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
                    */
                #endregion

                if (beercat.Contains(storeid.ToString()))
                {
                    List<ProductsModel> collection = (from b in productList
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

            GenerateCSV.GenerateCSVFile(list2, "PRODUCT", storeid, BasePath);
            GenerateCSV.GenerateCSVFile(list3, "FULLNAME", storeid, BasePath);
            Console.WriteLine();
            Console.WriteLine("Product FIle Generated For EposNow " + storeid);
            Console.WriteLine("Fullname FIle Generated For EposNow " + storeid);
            }
            catch (Exception ex2)
            {
                Console.WriteLine(ex2.Message);
            }
            finally
            {
            }
        }

        public string GetVolume(string prodName)
        {
            if (string.IsNullOrWhiteSpace(prodName))
                return "";
            prodName = prodName.ToUpper();
            var m = Regex.Match(prodName, @"\b(?<qty>\d+(?:\.\d+)?)\s*(?<unit>ML|LTR|L|OZ)\b");
            if (!m.Success)
                return "";
            var qty = m.Groups["qty"].Value;
            var unit = m.Groups["unit"].Value;

            // for handling litre possiblities still under developing
            if (unit == "LTR" || unit == "L")
                unit = "L";

            return qty + unit;
        }


        public int getpack(string prodName)
        {
            prodName = prodName.ToUpper();
            var regexMatch = Regex.Match(prodName, @"(?<Result>\d+)\s*PK");
            var prodPack = regexMatch.Groups["Result"].Value;
            if (prodPack.Length > 0)
            {
                int outVal = 0;
                int.TryParse(prodPack.Replace("$", ""), out outVal);
                return outVal;
            }
            return 1;
        }


    } 
}
