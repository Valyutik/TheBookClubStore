using System;
using System.Collections.Generic;

namespace TheBookClubStore.Models.Entities;

public sealed class Order
{
    public int Id { get; init; }

    public int UserId { get; init; }

    public int StatusId { get; init; }

    public int DeliveryPointId { get; init; }

    public DateTime Date { get; init; }

    public DeliveryPoint DeliveryPoint { get; init; }

    // ReSharper disable once CollectionNeverUpdated.Global
    public ICollection<OrderItem> OrderItems { get; init; } = new List<OrderItem>();

    public OrderStatus Status { get; init; }

    public User User { get; init; }
}
