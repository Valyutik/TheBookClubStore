using TheBookClubStore.Views;
using TheBookClubStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TheBookClubStore.Commands;
using TheBookClubStore.Models.Entities;

namespace TheBookClubStore.ViewModels;

public sealed class MainViewModel : INotifyPropertyChanged
{
    public User User { get; }

    private string _title;
    public string Title
    {
        get => _title;
        private set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string ProductNameFilter { get; set; }
    public string ProductManufactureFilter { get; set; }

    public Product CurrentProduct { get; set; }
    public ObservableCollection<Product>? CurrentProducts
    {
        get => _currentProducts;
        private set
        {
            _currentProducts = value;
            OnPropertyChanged();
        }
    }
    public RelayCommand AddProductToCartCommand { get; }
    public RelayCommand RemoveProductToCartCommand { get; }
    public RelayCommand ChangeTableDisplayTypeCommand { get; }
    public RelayCommand PlaceOrderCommand { get; }
    public RelayCommand SearchCommand { get; }
    
    private ObservableCollection<Product>? _currentProducts;
    private ObservableCollection<Product>? Products { get; }
    private ObservableCollection<Product>? ShoppingCart { get; }
    private readonly TheBookClubStoreDbContext _dbContext = new();
    private bool _isShoppingCart;
    
    public MainViewModel(User user)
    {
        User = user;
        Title = "Товары";
        _dbContext.Database.EnsureCreated();
        _dbContext.Products.Load();
        
        Products = _dbContext.Products.Local.ToObservableCollection();
        CurrentProducts = Products;
        ShoppingCart = new ObservableCollection<Product>();
        
        AddProductToCartCommand = new RelayCommand(AddProductToCart);
        RemoveProductToCartCommand = new RelayCommand(RemoveProductToCart);
        ChangeTableDisplayTypeCommand = new RelayCommand(ChangeTableDisplayType);
        PlaceOrderCommand = new RelayCommand(PlaceOrder);
        SearchCommand = new RelayCommand(SetProductFilter);
        
        var window = new MainWindow
        {
            DataContext = this
        };
        window.Show();
    }

    private void SetProductFilter(object obj)
    {
        CurrentProducts = new ObservableCollection<Product>(_dbContext.Products.Where(product =>
            EF.Functions.Like(product.Name, $"%{ProductNameFilter}%") &&
            EF.Functions.Like(product.Manufacturer, $"%{ProductManufactureFilter}%")));
    }

    private void ChangeTableDisplayType(object obj)
    {
        if (_isShoppingCart)
        {
            _isShoppingCart = false;
            Title = "Товары";
            CurrentProducts = Products;
        }
        else
        {
            Title = "Корзина";
            _isShoppingCart = true;
            CurrentProducts = ShoppingCart;
        }
    }

    private void AddProductToCart(object obj)
    {
        if (_isShoppingCart)
        {
            if (Products != null && Products.All(product => product.Id != CurrentProduct.Id)) return;
            {
                var product = Products?.First(product => product.Id == CurrentProduct.Id);
                
                if (CurrentProduct.Quantity >= product?.Quantity + CurrentProduct.Quantity) return;
                if (product != null) product.Quantity--;
                CurrentProduct.Quantity++;
            }
        }
        else
        {
            if (CurrentProduct.Quantity <= 0) return;
            if (ShoppingCart != null && ShoppingCart.Any(product => product.Id == CurrentProduct.Id))
            {
                var products = ShoppingCart.Where(product => product.Id == CurrentProduct.Id);
                products.First().Quantity++;
                CurrentProduct.Quantity--;
            }
            else
            {
                CurrentProduct.Quantity--;
                var currentProduct = CurrentProduct.GetCopy();
                currentProduct.Quantity = 0;
                currentProduct.Quantity++;
                ShoppingCart?.Add(currentProduct);
            }
        }
    }
    
    private void RemoveProductToCart(object obj)
    {
        if (_isShoppingCart)
        {
            if (Products != null && Products.All(product => product.Id != CurrentProduct.Id)) return;
            
            var product = Products?.First(product => product.Id == CurrentProduct.Id);

            if (CurrentProduct.Quantity > 0)
            {
                if (product != null) product.Quantity++;
                CurrentProduct.Quantity--;
            }
        }
        else
        {
            if (ShoppingCart?.Count > 0 && ShoppingCart?.First(product => product.Id == CurrentProduct.Id).Quantity >= 0)
            {
                if (ShoppingCart != null && ShoppingCart.Any(product => product.Id == CurrentProduct.Id))
                {
                    var products = ShoppingCart.Where(product => product.Id == CurrentProduct.Id);
                    products.First().Quantity--;
                    CurrentProduct.Quantity++;
                }
                else
                {
                    CurrentProduct.Quantity++;
                    var currentProduct = CurrentProduct.GetCopy();
                    currentProduct.Quantity = 0;
                    currentProduct.Quantity--;
                    ShoppingCart?.Add(currentProduct);
                }
            }
        }

        if (ShoppingCart is { Count: > 0 } && ShoppingCart.Any(product => product.Id == CurrentProduct.Id && product.Quantity <= 0))
        {
            ShoppingCart?.Remove(ShoppingCart.First(product => product.Id == CurrentProduct.Id && product.Quantity <= 0));
        }
    }

    private void PlaceOrder(object obj)
    {
        if (ShoppingCart != null)
        {
            var window = new OrderPlacementWindow(User, _dbContext, ShoppingCart);
            window.Show();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}