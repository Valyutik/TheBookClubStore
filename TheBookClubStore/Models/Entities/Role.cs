using System.Collections.Generic;

namespace TheBookClubStore.Models.Entities;

public sealed class Role
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    // ReSharper disable once CollectionNeverUpdated.Global
    public ICollection<User> Users { get; init; } = new List<User>();
}
