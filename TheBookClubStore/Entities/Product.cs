using System;
using System.Collections.Generic;

namespace TheBookClubStore.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal? DiscountAmount { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
