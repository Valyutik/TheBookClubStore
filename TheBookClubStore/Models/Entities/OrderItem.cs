namespace TheBookClubStore.Models.Entities;

public sealed class OrderItem
{
    public int Id { get; init; }

    public int OrderId { get; init; }

    public int ProductId { get; init; }

    public int Quantity { get; init; }

    public Order Order { get; init; } = null!;

    public Product Product { get; init; } = null!;
}
