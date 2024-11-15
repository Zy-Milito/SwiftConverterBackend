﻿namespace Common.DTO
{
    public class CurrencyForView
    {
        public required string Name { get; set; }
        public required string Symbol { get; set; }
        public required string ISOCode { get; set; }
        public required double ExchangeRate { get; set; }
    }
}
