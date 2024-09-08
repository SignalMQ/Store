using Store.Models;

namespace Store.Service.Interface;

public interface ICartService
{
    public Task<bool> CreateCart(Cart cart, out string errorMessage);
}
