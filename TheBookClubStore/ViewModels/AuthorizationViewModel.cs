using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TheBookClubStore.Models;
using TheBookClubStore.Commands;
using Microsoft.EntityFrameworkCore;

namespace TheBookClubStore.ViewModels;

public class AuthorizationViewModel
{
    private readonly TheBookClubStoreDbContext _theBookClubStoreDbContext;
    
    public string? Email { get;  set; }
    public string? Password { get;  set; }
    public ICommand AuthorizeCommand { get; }
    
    public AuthorizationViewModel()
    {
        _theBookClubStoreDbContext = new TheBookClubStoreDbContext();
        _theBookClubStoreDbContext.Users.Load();

        AuthorizeCommand = new RelayCommand(Authorize, _ => Password != null &&
                                                            Email is { Length: > 0 } &&
                                                            Password.Length > 0);
    }

    private void Authorize(object obj)
    {
        try
        {
            var users = _theBookClubStoreDbContext.Users.Where(u =>
                u.Email == Email && u.Password == Password);
            if (users.Any())
            {
                if (users.Any(u => u.Role.Name == "Пользователь"))
                {
                    var unused = new MainViewModel(users.ToList()[0]);
                }
                else if (users.Any(u => u.Role.Name == "Менеджер"))
                {
                }
                else if (users.Any(u => u.Role.Name == "Администратор"))
                {
                }
                if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль");
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
            throw;
        }
    }
}