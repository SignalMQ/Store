using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Services.Interfaces;

namespace Store.Controllers
{
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [Route("/api/CreateCart")]
        public async Task<IResult> CreateAsync(Cart cart) => await cartService.CreateCart(cart);
    }
}
