﻿using System;
using System.Collections.Generic;

namespace TheBookClubStore.Entities;

public partial class DeliveryPoint
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
