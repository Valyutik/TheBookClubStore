namespace TheBookClubStore.Models.Entities;

public class OrderItem
{
    public int Id { get; init; }

    public int OrderId { get; init; }

    public int ProductId { get; init; }

    public int Quantity { get; init; }

    public virtual Order Order { get; init; } = null!;

    public virtual Product Product { get; init; } = null!;
}
