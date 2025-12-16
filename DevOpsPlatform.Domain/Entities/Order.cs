namespace DevOpsPlatform.Domain.Entities
{
	public class Order
	{
		public Guid Id { get; set; }
		public string OrderNumber { get; set; } = default!;
		public decimal TotalAmount { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
