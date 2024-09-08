using Store.Models;
using Store.Service.Interface;

namespace Store.Service;

public class CartService : ICartService
{
    public Task<bool> CreateCart(Cart cart, out string errorMessage) 
    {
        if (!(cart.Number == 0))
            if (cart.Goods.Any()) 
                if (cart.Payments.Any()) 
                {
                    errorMessage = string.Empty;
                    return Task.FromResult(true);
                }
                else 
                {
                    errorMessage = "Payments must be contains elements!";
                    return Task.FromResult(false);
                }
            else 
            {
                errorMessage = "Goods must be contains elements!";
                return Task.FromResult(false);
            }
        else 
        {
            errorMessage = "Number must be greater than 0!";
            return Task.FromResult(false);
        }
    }
}
