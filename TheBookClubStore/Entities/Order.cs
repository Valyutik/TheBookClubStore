using System;
using System.Collections.Generic;

namespace TheBookClubStore.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public int DeliveryPointId { get; set; }

    public DateTime Date { get; set; }

    public virtual DeliveryPoint DeliveryPoint { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderStatus Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
