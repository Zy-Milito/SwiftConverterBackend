namespace Common.DTO
{
    public class ConversionForCreation
    {
        public required int FromCurrencyId { get; set; }
        public required int ToCurrencyId { get; set; }
    }
}
