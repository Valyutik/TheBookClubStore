using System.Collections.Generic;

namespace TheBookClubStore.Models.Entities;

public sealed class OrderStatus
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public ICollection<Order> Orders { get; init; } = new List<Order>();
}
