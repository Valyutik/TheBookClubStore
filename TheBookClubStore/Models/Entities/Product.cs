using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TheBookClubStore.Models.Entities;

public sealed class Product : INotifyPropertyChanged
{
    public int Id { get; init; }

    public string Name { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal? DiscountAmount { get; set; }

    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged();
        }
    }

    public string? Description { get; set; }

    public string? Photo { get; init; }

    // ReSharper disable once CollectionNeverUpdated.Global
    public ICollection<OrderItem> OrderItems { get; init; } = new List<OrderItem>();

    private int _quantity;

    public Product GetCopy()
    {
        return new Product
        {
            Id = Id,
            Name = Name,
            Manufacturer = Manufacturer,
            Price = Price,
            DiscountAmount = DiscountAmount,
            Quantity = Quantity,
            Description = Description,
            Photo = Photo
        };
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
