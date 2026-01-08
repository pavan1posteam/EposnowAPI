using System.Collections.Generic;
using EposNow;

public class EposnowProdList
{
	public class Supplier
	{
		public Root Roots { get; set; }

		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string AddressLine1 { get; set; }

		public object AddressLine2 { get; set; }

		public string Town { get; set; }

		public string County { get; set; }

		public string PostCode { get; set; }

		public string ContactNumber { get; set; }

		public object ContactNumber2 { get; set; }

		public object EmailAddress { get; set; }

		public object Type { get; set; }

		public object ReferenceCode { get; set; }
	}

	public class TaxRate
	{
		public int TaxGroupId { get; set; }

		public int TaxRateId { get; set; }

		public int LocationId { get; set; }

		public int Priority { get; set; }

		public double Percentage { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string TaxCode { get; set; }
	}

	public class SalePriceTaxGroup
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public List<TaxRate> TaxRates { get; set; }
	}

	public class EatOutPriceTaxGroup
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public List<TaxRate> TaxRates { get; set; }
	}

	public class CostPriceTaxGroup
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public List<TaxRate> TaxRates { get; set; }
	}

	public class Root
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public double CostPrice { get; set; }

		public bool IsCostPriceIncTax { get; set; }

		public double SalePrice { get; set; }

		public bool IsSalePriceIncTax { get; set; }

		public double EatOutPrice { get; set; }

		public bool IsEatOutPriceIncTax { get; set; }

		public int CategoryId { get; set; }

		public string Barcode { get; set; }

		public int SalePriceTaxGroupId { get; set; }

		public int EatOutPriceTaxGroupId { get; set; }

		public int CostPriceTaxGroupId { get; set; }

		public object BrandId { get; set; }

		public int SupplierId { get; set; }

		public object PopupNoteId { get; set; }

		public object UnitOfSale { get; set; }

		public object VolumeOfSale { get; set; }

		public object VariantGroupId { get; set; }

		public object MultipleChoiceNoteId { get; set; }

		public object Size { get; set; }

		public object Sku { get; set; }

		public bool SellOnWeb { get; set; }

		public bool SellOnTill { get; set; }

		public string OrderCode { get; set; }

		public object SortPosition { get; set; }

		public object RrPrice { get; set; }

		public int ProductType { get; set; }

		public object TareWeight { get; set; }

		public object ArticleCode { get; set; }

		public bool IsTaxExemptable { get; set; }

		public object ReferenceCode { get; set; }

		public bool IsVariablePrice { get; set; }

		public bool IsArchived { get; set; }

		public object ColourId { get; set; }

		public object MeasurementDetails { get; set; }

		public Supplier Supplier { get; set; }

		public SalePriceTaxGroup SalePriceTaxGroup { get; set; }

		public EatOutPriceTaxGroup EatOutPriceTaxGroup { get; set; }

		public CostPriceTaxGroup CostPriceTaxGroup { get; set; }

		public List<object> ProductTags { get; set; }

		public List<object> ProductUdfs { get; set; }

		public List<object> ProductLocationAreaPrices { get; set; }

		public List<object> ProductImages { get; set; }

		public bool IsMultipleChoiceProductOptional { get; set; }
		public int ContainerFeeId { get; set; } = 0;

    }
}

