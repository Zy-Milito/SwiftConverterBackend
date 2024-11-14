namespace Common.DTO
{
    public class ResponseForLogin
    {
        public required int Status { get; set; }
        public required string Message { get; set; }
        public required string Token { get; set; }
    }
}
