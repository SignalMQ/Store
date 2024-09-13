using Store.Models;

namespace Store.Services.Interfaces;

public interface ICartService
{
    public Task<IResult> CreateCart(Cart cart);
}
