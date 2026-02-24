namespace FinTrackAPI.DTOs
{
    public class TransactionRequestDTO
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty; // "ingreso" o "gasto"
        public int CategoryId { get; set; }
    }
}