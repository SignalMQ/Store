using Store.Models;

namespace Store.Services.Interfaces;

public interface ICartService
{
    public Task<bool> CreateCart(Cart cart, out string errorMessage);
}
