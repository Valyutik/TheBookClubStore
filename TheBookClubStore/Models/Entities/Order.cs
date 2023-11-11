using System;
using System.Collections.Generic;

namespace TheBookClubStore.Models.Entities;

public class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public int DeliveryPointId { get; set; }

    public DateTime Date { get; set; }

    public virtual DeliveryPoint DeliveryPoint { get; set; }

    // ReSharper disable once CollectionNeverUpdated.Global
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderStatus Status { get; set; }

    public virtual User User { get; set; }
}
