using AutoMapper;
using Azure.Core;
using DemoBackend.Dtos;
using DemoBackend.Interfaces;
using DemoBackend.Models;
using DemoBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoBackend.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private ICartInterface _cartRepository;
        private IItemInterface _itemRepository;
        private IUserInterface _userRepository;
        private IMapper _mapper;

        public CartController(ICartInterface cartRepository,IItemInterface itemRepository,IUserInterface userRepository,IMapper mapper) { 
            _cartRepository = cartRepository;
            _itemRepository = itemRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("getAllCartItems/{userEmail}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllCartItems(string userEmail)
        {
            var cartItems = _mapper.Map<List<Cart>>(_cartRepository.GetAllCartItems(userEmail));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cartItems);
        }

        [HttpPost("addCartItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult AddCartItem(CartDto cartItem)
        {
            if(cartItem == null)
                return BadRequest(ModelState);

            if(!_userRepository.UserExist(cartItem.UserEmail))
                return BadRequest("Invalide User Email");

            if (_cartRepository.CartItemExist(cartItem.UserEmail,cartItem.ItemId))
                return BadRequest("Cart Item Already Added");

            if(!_itemRepository.ItemExist(cartItem.ItemId))
                return BadRequest("Item Doesn't Exist");

            Cart cart = new Cart();
            cart.CartId = _cartRepository.LastItem() + 1;
            cart.UserEmail = cartItem.UserEmail.Trim();
            cart.ItemId = cartItem.ItemId;
            cart.Quantity = cartItem.Quantity;

            bool cartItemAdded = _cartRepository.AddCartItem(cart);

            if (!cartItemAdded)
                return BadRequest("Something went wrong");

            return Ok("Succesfully Added");
        }
        [HttpPost("updateCartItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCartItem(Cart cartItem)
        {
            if (cartItem == null)
                return BadRequest(ModelState);

            if (!_cartRepository.CartItemExist(cartItem.CartId))
                return BadRequest("Cart Doesn't Exsit");

            bool cartItemUpdated = _cartRepository.UpdateCartItem(cartItem);

            if (!cartItemUpdated)
                return BadRequest("Something went wrong");

            return Ok("Succesfully Updated");
        }

        [HttpDelete("deleteCartItem/{cartId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult DeleteCartItem(int cartId)
        {
            if (!_cartRepository.CartItemExist(cartId))
                return BadRequest("Cart Item Doesn't Exist");

            var cartToDelete = _cartRepository.GetCartItem(cartId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_cartRepository.DeleteCartItem(cartToDelete))
                return BadRequest("Something went wrong");

            return NoContent();
        }

        [HttpDelete("deleteAllCartItem/{userEmail}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult DeleteAllCartItem(string userEmail)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_cartRepository.DeleteAllCartItems(userEmail))
                return BadRequest("Something went wrong");

            return Ok("All Cart Items Deleted Succesfully");
        }
    }
}
