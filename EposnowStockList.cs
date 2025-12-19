using System;
using System.Collections.Generic;
using EposNow;

public class EposnowStockList
{
	public class ProductStockBatch
	{
		public int Id { get; set; }

		public int ProductStockId { get; set; }

		public DateTime CreatedDate { get; set; }

		public int CurrentStock { get; set; }

		public int CurrentVolume { get; set; }

		public double CostPrice { get; set; }

		public int? SupplierId { get; set; }

		public object CostPriceMeasurementSchemeItemId { get; set; }

		public object CostPriceMeasurementUnitVolume { get; set; }

		public object CostPriceUnitFactor { get; set; }

		public object CostPriceUnit { get; set; }

		public object StockMeasurementSchemeItemId { get; set; }

		public object StockUnit { get; set; }

		public object StockFactor { get; set; }

		public object MeasurementDetails { get; set; }
	}

	public class Root
	{
		public int Id { get; set; }

		public int ProductId { get; set; }

		public int LocationId { get; set; }

		public int MinStock { get; set; }

		public int MaxStock { get; set; }

		public object MinimumOrderAmount { get; set; }

		public object MultipleOrderAmount { get; set; }

		public List<ProductStockBatch> ProductStockBatches { get; set; }
	}
}
