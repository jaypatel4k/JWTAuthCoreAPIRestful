﻿namespace JWTAuthCoreAPIRestful.Models
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string ProductUrl { get; set; }
    }
}
