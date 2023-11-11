using System.Collections.Generic;

namespace TheBookClubStore.Models.Entities;

public class User
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string PhoneNumber { get; init; } = null!;

    public string? Photo { get; init; }

    public int RoleId { get; init; }

    // ReSharper disable once CollectionNeverUpdated.Global
    public virtual ICollection<Order> Orders { get; init; } = new List<Order>();

    public virtual Role Role { get; init; } = null!;
}
