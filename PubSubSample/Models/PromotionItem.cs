namespace PubSubSample.Models
{
	public class PromotionItem
	{
		public string? Description { get; set; }
		public decimal SellingPrice { get; set; }
		public decimal DiscountPercentage { get; set; }
		public decimal ActualSellingPrice { get; set; }
	}
}
