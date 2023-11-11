using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TheBookClubStore.Models;
using TheBookClubStore.Models.Entities;

namespace TheBookClubStore.Views;

public partial class OrderPlacementWindow
{
    private readonly User _currentUser;
    private readonly TheBookClubStoreDbContext _context;
    private ObservableCollection<Product> _products;
    
    public OrderPlacementWindow(User user, TheBookClubStoreDbContext context, ObservableCollection<Product> products)
    {
        InitializeComponent();
        _currentUser = user;
        _context = context;
        _products = products;

        if (_currentUser != null)
        {
            EmailTextBox.Text = _currentUser.Email;
            NameTextBox.Text = _currentUser.Name;
            PhoneNumberTextBox.Text = _currentUser.PhoneNumber;
        }

        TypeComboBox.ItemsSource = context.DeliveryPoints.ToList();
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        var order = new Order
        {
            Date = DateTime.Now,
            DeliveryPointId = TypeComboBox.SelectedIndex,
            StatusId = _context.OrderStatuses.First(status => status.Name == "В обработке").Id,
            UserId = _currentUser.Id

        };
        _context.Orders.Add(order);
        _context.SaveChanges();
        
        foreach (var product in _products)
        {
            _context.OrderItems.Add(new OrderItem
            {
                OrderId = order.Id,
                ProductId = product.Id,
                Quantity = product.Quantity
            });
        }
        _context.SaveChanges();
        _products.Clear();
        Close();
    }

    private void PrevButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}