using System.Collections.Generic;

namespace TheBookClubStore.Models.Entities;

public class DeliveryPoint
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string Address { get; init; } = null!;

    // ReSharper disable once CollectionNeverUpdated.Global
    public virtual ICollection<Order> Orders { get; init; } = new List<Order>();
}
