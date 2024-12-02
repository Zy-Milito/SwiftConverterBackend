namespace Common.DTO
{
    public class HistoryForView
    {
        public int Id { get; set; }
        public required DateTime Date { get; set; }
        public int UserId { get; set; }
        public required double Amount { get; set; }
        public required string FromCurrency { get; set; }
        public required string ToCurrency { get; set; }
    }
}
