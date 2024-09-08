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
        public async void CreateCart(Cart cart)
        {
            if (await _cartService.CreateCart(cart, out string errorMessage)) 
                Ok();
            else 
                BadRequest(errorMessage);
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
