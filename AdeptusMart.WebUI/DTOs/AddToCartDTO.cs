namespace AdeptusMart05.WebUI.DTOs
{
    public class AddToCartDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string SessionId { get; set; }
    }
}
