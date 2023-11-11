using System.Collections.Generic;

namespace TheBookClubStore.Models.Entities;

public class OrderStatus
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public virtual ICollection<Order> Orders { get; init; } = new List<Order>();
}
