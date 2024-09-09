using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Services.Interfaces;

namespace Store.Controllers
{
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;

        [Route("/api/getCart/{Id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Cart GetCart(int Id)
        {
            return new Cart() 
            {
                Id = Id
            };
        }

        [Route("/api/createCart")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCart(Cart cart)
        {
            try 
            {
                await _cartService.CreateCart(cart);
                return Ok(cart);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("/api/updateCart")]
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public int UpdateCart(Cart cart)
        {
            return cart.Id;
        }

        [Route("/api/deleteCart/{Id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public int DeleteCart(int Id) 
        {
            return Id;
        }
    }
}
