﻿namespace Common.DTO
{
    public class PlanForView
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required double Price { get; set; }
        public required int? MaxConversions { get; set; }
    }
}
